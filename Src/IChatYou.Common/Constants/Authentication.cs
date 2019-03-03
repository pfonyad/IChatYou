namespace IChatYou.Common.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Authentication
    {
        public const string AdministratorUser = "Admin";

        public class Groups
        {
            public const string Administrator = "Administrator";
        }

        public class Roles
        {
            public const string Admin = "Admin";

            public const string WebAdminUser = "WebAdminFelhasznalo";
            public const string WebAdminLog = "WebAdminLog";
            public const string WebAdminSetting = "WebAdminBeallitas";
        }

        public static Lazy<IReadOnlyCollection<string>> AllRolesList =
            new Lazy<IReadOnlyCollection<string>>(() => typeof(Roles)
                .GetFields()
                .Select(fi => fi.GetRawConstantValue().ToString())
                .ToList());
    }
}
