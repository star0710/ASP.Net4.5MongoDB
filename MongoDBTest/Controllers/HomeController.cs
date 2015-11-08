using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDBTest.Models;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MongoDBTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// MongoDB測試-ProxyAPICall
        /// </summary>
        /// <returns></returns>
        public ActionResult ProxyAPICall_Add()
        {
            ProxyAPICallModels model = new ProxyAPICallModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult ProxyAPICall_Add(ProxyAPICallModels pac)
        {
            object result = pac.Creat();
            return View();
        }

        public ActionResult ProxyAPICall_ListAll()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ProxyAPICall_Load()
        {
            object result = new object();

            try
            {
                List<ProxyAPICallModels> pacs = new List<ProxyAPICallModels>();
                result = ProxyAPICallModels.GetAll(out pacs);
                if (result.ToString() == "0")
                {
                    result = pacs;
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}
