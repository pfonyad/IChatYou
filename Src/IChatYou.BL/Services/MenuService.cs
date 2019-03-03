namespace IChatYou.BL.Services
{
    using IChatYou.DAL.Menu;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;

    public class MenuService : IMenuService
    {
        public List<MenuItem> GetAuthorizedMenuItems(List<MenuItem> menu, List<string> userRoles)
        {
            var model = new List<MenuItem>();

            FilterMenuItems(menu, model, userRoles);
            return model;
        }

        private void FilterMenuItems(List<MenuItem> sourceItems, List<MenuItem> filteredValues, List<string> userRoles)
        {
            var menuItems = new List<MenuItem>();
            foreach (var menuItem in sourceItems)
            {
                var roles = (menuItem.Roles ?? "").Split(new[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (!roles.Any(userRoles.Contains))
                {
                    continue;
                }

                var newItem = menuItem.Clone();

                if (menuItem.Items.Any())
                {
                    FilterMenuItems(menuItem.Items, newItem.Items, userRoles);
                }

                menuItems.Add(newItem);
            }

            menuItems = menuItems.ToObservable()
                .DistinctUntilChanged(m => m.Text)
                .ToEnumerable().ToList();

            if (menuItems.Any() && 
                menuItems.First().Text == MenuItem.Separator)
            {
                menuItems.RemoveAt(0);
            }
            if (menuItems.Any() && 
                menuItems.Last().Text == MenuItem.Separator)
            {
                menuItems.RemoveAt(menuItems.Count - 1);
            }

            filteredValues.AddRange(menuItems);
        }

        
    }
}
