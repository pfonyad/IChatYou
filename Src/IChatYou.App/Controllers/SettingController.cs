namespace IChatYou.App.Controllers
{
    using AutoMapper;
    using Common.Constants;
    using DataTables.AspNet.Core;
    using DataTables.AspNet.Mvc5;
    using IChatYou.BL.Services.Interfaces;
    using IChatYou.DAL.Entities.Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Web.Mvc;

    [Authorize(Roles = Authentication.Roles.WebAdminSetting)]
    public class SettingController : Controller
    {
        private readonly ISettingService settingService;

        public SettingController(ISettingService settingService)
        {
            this.settingService = settingService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(Setting setting)
        {
            settingService.Update(setting);

            return Json(new { success = true});
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            var data = settingService.GetAllSetting().AsQueryable();
            var filteredData = data.Where(item => item.Name.Contains(request.Search.Value));

            var orderings = request.Columns
                .Where(c => c.Sort != null)
                .OrderBy(c => c.Sort.Order)
                .Select(c => $"{c.Name} {c.Sort.Direction}")
                .DefaultIfEmpty($"{nameof(Setting.Name)} asc")
                .Aggregate((current, next) => current + ", " + next);

            var orderedData = filteredData.OrderBy(orderings);

            var dataPage = orderedData.Skip(request.Start).Take(request.Length);
            var response = DataTablesResponse.Create(request, data.Count(), orderedData.Count(), Mapper.Map<IEnumerable<Setting>>(dataPage));

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
    }
}