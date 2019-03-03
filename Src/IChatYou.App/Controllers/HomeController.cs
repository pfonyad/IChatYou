namespace IChatYou.App.Controllers
{
    using DAL.Repositories.Interfaces;
    using IChatYou.BL.IdentityServices;
    using IChatYou.BL.Services.Interfaces;
    using IChatYou.DAL.Menu;
    using Libraries;
    using Microsoft.AspNet.Identity;
    using Serilog;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IMenuService menuService;

        public HomeController(IUserRepository userRepository, ILogger logger, ApplicationUserManager userManager, IMenuService menuService)
        {
            this.userRepository = userRepository;
            this.applicationUserManager = userManager;
            this.menuService = menuService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var userRoles = new List<string>() { MenuItem.AnonymousUser };

            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = applicationUserManager.FindById(userId);

                if (user != null)
                {
                    userRoles.Add(MenuItem.AuthenticatedUser);
                    userRoles.AddRange(applicationUserManager.GetRoles(userId));
                }
            }

            return PartialView("_Menu", menuService.GetAuthorizedMenuItems(MainMenu.GetMenu(), userRoles));
        }
    }
}
