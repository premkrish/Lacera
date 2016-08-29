using System.Collections.Generic;
using System.Web.Mvc;
using Lacera.Enums;
using Lacera.Objects.Models;
using Lacera.Parser;

namespace Lacera.Mvc.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Index Action result
        /// </summary>
        /// <returns>The View</returns>
        public ActionResult Index()
        {
            // Get file from App_Data folder
            var path = HttpContext.Server.MapPath("~/App_Data/Lacera.csv"); 

            //Create a new instance for the csv parser class and setup the business rule
            var parser = new CsvParser();
            var businessRule = new List<InvalidRecordRules> { InvalidRecordRules.InvalidDate, InvalidRecordRules.InvalidAmount, InvalidRecordRules.MissingField };

            //Parse the CSV 
            var employee = parser.Parse<Employee>(path, businessRule);

            //Bind the model to the view
            return View(employee);
        }
    }
}