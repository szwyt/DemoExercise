using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDb.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContext<Province> _context;
        public HomeController(ILogger<HomeController> logger, DbContext<Province> context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name = "", int pageSize = 10, int pageIndex = 1)
        {
            var predicate = PredicateBuilder.New<Province>(true);
            if (!string.IsNullOrWhiteSpace(name))
                predicate.And(x => x.Name.Contains(name));
            PageModel<Province> pageModel = await _context.Get(pageIndex, pageSize, o => o.AddTime, predicate, false);
            return Json(new { total = pageModel.dataCount, rows = pageModel.data });
        }

        [HttpGet]
        public async Task<IActionResult> Privacy(string id = "")
        {
            Province model = await _context.Get(id);
            return View(model ?? new Province());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Province info)
        {

            if (string.IsNullOrWhiteSpace(info.Id))
                await _context.Create(info);
            else
            {
                Province model = await _context.Get(info.Id);
                if (model == null)
                {
                    return Json(new { flag = false, msg = "操作失败,没有该数据!" });
                }

                model.Name = info.Name;
                model.Age = info.Age;
                await _context.Update(model.Id, model);
            }

            return Json(new { flag = true, msg = "操作成功!" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id = "")
        {
            await _context.Remove(id);
            return Json(new { flag = true, msg = "操作成功!" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
