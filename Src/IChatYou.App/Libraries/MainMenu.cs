namespace IChatYou.App.Libraries
{
    using Controllers;
    using IChatYou.Common.Constants;
    using IChatYou.DAL.Menu;
    using System.Collections.Generic;

    public static class MainMenu
    {
        public static List<MenuItem> GetMenu()
        {
            return new List<MenuItem>
            {
                new MenuItem("Aministration", MenuItem.AnonymousUser)
                {
                    Items = new List<MenuItem>
                    {
                        new MenuItem(MenuItem.Separator, null, null, Authentication.Roles.Admin),
                        new MenuItem("Logs", nameof(SerilogController.Index), nameof(SerilogController), Authentication.Roles.WebAdminLog),
                        new MenuItem("Settings", nameof(SettingController.Index), nameof(SettingController), Authentication.Roles.WebAdminSetting),
                    }
                },
                new MenuItem("Help", MenuItem.AuthenticatedUser)
                {
                    Items = new List<MenuItem>
                    {
                        new MenuItem("About", nameof(HomeController.About), nameof(HomeController), MenuItem.AnonymousUser),
                        new MenuItem(MenuItem.Separator, null, null, MenuItem.AnonymousUser),
                        new MenuItem("Contact", nameof(HomeController.Contact), nameof(HomeController), MenuItem.AnonymousUser)
                    }
                }
            };
        }
    }
}