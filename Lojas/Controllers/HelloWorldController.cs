using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lojas.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/

        public IActionResult Index ()
        {
            return View ();
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        // public string Welcome(string name, int numTimes = 1)
        // {
        //     return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        // }
        // public string Welcome (string name, int ID = 1)
        // {
        //     return HtmlEncoder.Default.Encode ($"Hello {name}, ID: {ID}");
        // }
        public IActionResult Welcome (string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View ();
        }
    }
}