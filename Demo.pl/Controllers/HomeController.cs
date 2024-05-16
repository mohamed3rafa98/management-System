using Demo.pl.ViewModel;
using Demo.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.pl.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IScopedService scoped1;
        //private readonly IScopedService scoped2;
        //private readonly ITransientService transient1;
        //private readonly ITransientService transient2;
        //private readonly ISingletonService  singleton1;
        //private readonly ISingletonService singleton2;


        public HomeController(ILogger<HomeController> logger 
                                                       /*IScopedService scoped1 ,IScopedService scoped2 ,
                                                       ITransientService transient1 , ITransientService transient2,
                                                       ISingletonService singleton1 , ISingletonService singleton2*/)
        {
            _logger = logger;
            //this.scoped1 = scoped1;
            //this.scoped2 = scoped2;
            //this.transient1 = transient1;
            //this.transient2 = transient2;
            //this.singleton1 = singleton1;
            //this.singleton2 = singleton2;
        }

        //public string TestLifeTime()
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append($"Scoped01 :: {scoped1.GetGuid()}\n");
        //    builder.Append($"Scoped02 :: {scoped2.GetGuid()}\n\n");
        //    builder.Append($"Transient01 :: {transient1.GetGuid()}\n");
        //    builder.Append($"transient02 :: {transient2.GetGuid()}\n\n");
        //    builder.Append($"singleton01 :: {singleton1.GetGuid()}\n");
        //    builder.Append($"singleton02 :: {singleton2.GetGuid()}");

        //    return builder.ToString();
        //}
        public IActionResult Index()
        {
            return View();
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
