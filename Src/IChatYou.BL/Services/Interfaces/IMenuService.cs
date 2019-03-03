namespace IChatYou.BL.Services.Interfaces
{
    using IChatYou.DAL.Menu;
    using System.Collections.Generic;

    public interface IMenuService
    {
        List<MenuItem> GetAuthorizedMenuItems(List<MenuItem> menu, List<string> userRoles);
    }
}