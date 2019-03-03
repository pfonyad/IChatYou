namespace IChatYou.DAL.Menu
{
    using IChatYou.Common.Extensions;
    using System.Collections.Generic;

    public class MenuItem
    {
        /// <summary>
        /// string Action - the name of the action - eg. "Index"
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// string Area - the name of the area - eg. "ManageClient", ""
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// string Controller - the name of the controller - eg. "Client"
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// string Text - the text to display for the link - eg. "ManageClient"
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Comma separated list
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// MVC Route values
        /// </summary>
        public object RouteValues { get; set; }

        public List<MenuItem> Items { get; set; } = new List<MenuItem>();

        public bool IsSeparator => Text == Separator;

        public const string AuthenticatedUser = "[Authenticated]";
        public const string AnonymousUser = "[Anonymous]";
        public const string Separator = "---";


        public MenuItem(string text, string roles)
        {
            Text = text;
            Roles = roles;
        }

        public MenuItem(string text, string action, string controller, string roles) :
            this(text, roles)
        {
            Action = action;
            Controller = controller.RemoveController();
        }

        public MenuItem(string text, string action, string controller, string roles, object routeValues) :
            this(text, action, controller, roles)
        {
            RouteValues = routeValues;
        }

        public MenuItem Clone()
        {
            return new MenuItem(Text, Action, Controller, Roles, RouteValues);
        }
    }
}