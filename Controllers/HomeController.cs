using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paination.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Paination.Controllers
{
    public class HomeController : Controller
    {
        //https://www.youtube.com/watch?v=O57nsLyZubc source 
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int pg=1)
        {
            using (var con=new SqlConnection(@"Server=DESKTOP-NIOCFBP;Database=NewDatabaseSample;Trusted_Connection=True;"))
            {
                var result =con.Query<ExampleTable>("select id,Fullname from NewTableSample").ToList();
                const int pageSize=10;
                if(pg<1){
                    pg=1;
                }
                int rescCount=result.Count();
                var pagin=new Pagin(rescCount,pg,pageSize);
                int resSkip=(pg-1)*pageSize;
                var data=result.Skip(resSkip).Take(pagin.PageSize).ToList();
                ViewBag.Pagin=pagin;
                return View(data);
            }
        }
        // public List<ExampleTable> GetPaginationResult()
        // {
        //     using (var con=new SqlConnection(@"Server=DESKTOP-NIOCFBP;Database=NewDatabaseSample;Trusted_Connection=True;"))
        //     {
        //         var result =con.Query<ExampleTable>("select * from NewTableSample").ToList();
        //         return result;
        //     }
        // }
        // public int GetCount()
        // {
        //     using (var con=new SqlConnection(@"Server=DESKTOP-NIOCFBP;Database=NewDatabaseSample;Trusted_Connection=True;"))
        //     {
        //         return con.ExecuteScalar<int>("select count(id) from NewTableSample");
        //     }
        // }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
