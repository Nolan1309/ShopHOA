using ShopBanHoa.Areas.Admin.DAO_ADMIN;
using ShopBanHoa.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHoa.Controllers
{
    public class HomeController : Controller
    {
        DataConnection db = new DataConnection();

        public ActionResult Index()
        {

            return View();
        }
        [ChildActionOnly]
        public ActionResult HeaderMenu()
        {
            CategoryModel model = new CategoryModel();
            var model1 = model.Getds();
            return PartialView(model1);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}