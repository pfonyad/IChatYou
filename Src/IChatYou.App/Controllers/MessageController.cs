namespace IChatYou.App.Controllers
{
    using AutoMapper;
    using DataTables.AspNet.Core;
    using DataTables.AspNet.Mvc5;
    using IChatYou.BL.Services.Interfaces;
    using IChatYou.DAL.Entities;
    using IChatYou.DAL.Entities.User;
    using IChatYou.DAL.Repositories.Interfaces;
    using Microsoft.AspNet.Identity;
    using Models;
    using Newtonsoft.Json;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Web.Mvc;
    using ViewModels;

    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageRepository messageRepository;
        private readonly IUserRepository userRepository;
        private readonly ILimitRepository limitRepository;
        private readonly IMessageService messageService;
        private readonly ILogger logger;

        public MessageController(IMessageRepository messageRepository, IUserRepository userRepository, ILimitRepository limitRepository, ILogger logger, IMessageService messageService)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
            this.limitRepository = limitRepository;
            this.logger = logger;
            this.messageService = messageService;
        }

        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(string targetUserName, string message)
        {
            var result = new AjaxBaseResultModel();

            try
            {
                if (limitRepository.GetLimitByUserId(User.Identity.GetUserId()) < 1)
                {
                    result.IsSuccess = true;
                    result.Result = "You have not enough message to sent!";
                }
                else
                {
                    messageService.Send(User.Identity.GetUserId(), targetUserName, message);
                    limitRepository.DecreaseLimitByUserId(User.Identity.GetUserId());

                    result.IsSuccess = true;
                    result.Result = "Message sent!";
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "SendMessage");
                result.Exception = ex.Message;
            }

            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult Messages()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckState()
        {
            var result = new AjaxBaseResultModel();

            try
            {
                var user = userRepository.Get(User.Identity.GetUserId());

                if (user == null)
                {
                    throw new Exception("UserNotFound");
                }

                result.IsSuccess = true;
                result.Result = user.IsVisible;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "ChangeState");
                result.Exception = ex.Message;
            }

            return Content(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SwitchState()
        {
            var result = new AjaxBaseResultModel();

            try
            {
                var user = userRepository.Get(User.Identity.GetUserId());

                if (user == null)
                {
                    throw new Exception("UserNotFound");
                }

                user.IsVisible = !user.IsVisible;
                userRepository.SaveChanges();

                result.IsSuccess = true;
                result.Result = user.IsVisible;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "SwitchState");
                result.Exception = ex.Message;
            }

            return Content(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTodayLeft()
        {
            var result = new AjaxBaseResultModel();

            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    result.Result = limitRepository.GetLimitByUserId(User.Identity.GetUserId());
                    result.IsSuccess = true;
                }
                else
                {
                    result.Result = "NoUser";
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "GetTodayLeft");
                result.Exception = ex.Message;
            }

            return Content(JsonConvert.SerializeObject(result));
        }

        public ActionResult GetMyMessages(IDataTablesRequest request)
        {
            IDataTablesResponse response = null;

            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("request");
                }
                var d = messageRepository.GetMessagesByUserId(User.Identity.GetUserId());
                var data = Mapper.Map<IEnumerable<MessageViewModel>>(d);

                if (!string.IsNullOrEmpty(request.Search.Value))
                {
                    data = data.Where(itm => itm.Sender.Contains(request.Search.Value) || itm.Sent.ToString().Contains(request.Search.Value));
                }

                var orderings = request.Columns
                    .Where(c => c.Sort != null)
                    .OrderBy(c => c.Sort.Order)
                    .Select(c => $"{c.Name} {c.Sort.Direction}")
                    .DefaultIfEmpty($"{nameof(BaseMessage.Sent)} asc")
                    .Aggregate((current, next) => current + ", " + next);

                var orderedData = data.OrderBy(orderings);

                var dataPage = orderedData.Skip(request.Start).Take(request.Length);

                response = DataTablesResponse.Create(request, data.Count(), orderedData.Count(), dataPage);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "GetMyMessages");
            }

            return new DataTablesJsonResult(response ?? DataTablesResponse.Create(request, "No data available."), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAvailableUsers(IDataTablesRequest request)
        {
            IDataTablesResponse response = null;

            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("request");
                }

                var data = userRepository.GetAll().Where(usr => usr.IsVisible && usr.Id != User.Identity.GetUserId());

                if (!string.IsNullOrEmpty(request.Search.Value))
                {
                    data = data.Where(item => item.FullName.Contains(request.Search.Value) || item.UserName.Contains(request.Search.Value) || item.Email.Contains(request.Search.Value));
                }

                var orderings = request.Columns
                    .Where(c => c.Sort != null)
                    .OrderBy(c => c.Sort.Order)
                    .Select(c => $"{c.Name} {c.Sort.Direction}")
                    .DefaultIfEmpty($"{nameof(ApplicationUser.FullName)} asc")
                    .Aggregate((current, next) => current + ", " + next);

                var orderedData = data.OrderBy(orderings);

                var dataPage = orderedData.Skip(request.Start).Take(request.Length).Select(usr => new { username = usr.UserName, fullname = usr.FullName, email = usr.Email });

                response = DataTablesResponse.Create(request, data.Count(), orderedData.Count(), dataPage);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "GetAvailableUsers");
            }
            
            return new DataTablesJsonResult(response ?? DataTablesResponse.Create(request, "No data available."), JsonRequestBehavior.AllowGet);
        }
    }
}