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
            //pg is the page number the user curently at

            if(pg<1){//dont let user set pg bellow 1
                pg=1;
            }

            using (var con=new SqlConnection(@"Server=DESKTOP-NIOCFBP;Database=NewDatabaseSample;Trusted_Connection=True;"))
            {
                // if we want serch functunality put it in the query via paramiter from url
                var result =con.Query<ExampleTable>("select id,Fullname from NewTableSample").ToList(); // get all data
                
                int rowCount=result.Count();//count how many records do we have using Linq

                var pagin=new Pagin(rowCount,pg); //create object of Pagin and send row Count and page number

                int resSkip=(pg-1) * pagin.PageSize;//set how much we should offset the records
                var data=result.Skip(resSkip).Take(pagin.PageSize).ToList(); // this is offset and fetch
                ViewBag.Pagin=pagin;//we will send this to the view
                return View(data);//return the offset and fetch that we want
            }
        }
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
