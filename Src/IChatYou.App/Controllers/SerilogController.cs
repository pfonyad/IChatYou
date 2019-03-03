namespace IChatYou.App.Controllers
{
    using AutoMapper;
    using Common.Constants;
    using DAL;
    using DataTables.AspNet.Core;
    using DataTables.AspNet.Mvc5;
    using IChatYou.DAL.Entities.Base;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Web.Mvc;

    [Authorize(Roles = Authentication.Roles.WebAdminLog)]
    public class SerilogController : Controller
    {
        private readonly ApplicationDbContext context;

        public SerilogController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            var data = context.Logs;
            var filteredData = data.Where(item =>
                item.Message.Contains(request.Search.Value) ||
                item.Exception.Contains(request.Search.Value));

            var columnFilters = request.Columns
                .Where(c => !string.IsNullOrEmpty(c.Search?.Value))
                .Select(c => c)
                .ToList();

            filteredData = columnFilters.Aggregate(filteredData, FilterTranslate);

            var orderings = request.Columns
                .Where(c => c.Sort != null)
                .OrderBy(c => c.Sort.Order)
                .Select(c => $"{c.Name} {c.Sort.Direction}")
                .DefaultIfEmpty($"{nameof(Log.Id)} desc")
                .Aggregate((current, next) => current + ", " + next);

            var orderedData = filteredData.OrderBy(orderings);

            var dataPage = orderedData.Skip(request.Start).Take(request.Length).ToList();
            var response = DataTablesResponse.Create(request, data.Count(), orderedData.Count(), Mapper.Map<IEnumerable<Log>>(dataPage));

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        private IQueryable<Log> FilterTranslate(IQueryable<Log> current, IColumn columnFilter)
        {
            if (columnFilter.Name == nameof(Log.TimeStamp))
            {
                var split = columnFilter.Search.Value
                    .Split(new[] {" - "}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(t =>
                    {
                        return DateTime.TryParseExact(t, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt)
                            ? dt
                            : DateTime.MinValue;
                    })
                    .Where(dt => dt > DateTime.MinValue)
                    .ToArray();

                var from = split[0];
                if (split.Length == 1)
                {
                    return current.Where(w => w.TimeStamp >= from);
                }

                var to = split[1].AddDays(1).AddSeconds(-1);
                if (split.Length > 1)
                {
                    return current.Where(w => w.TimeStamp >= from && w.TimeStamp <= to);
                }

                return current;
            }

            return current.Where($"{columnFilter.Name} = @0", columnFilter.Search.Value);
        }
    }
}