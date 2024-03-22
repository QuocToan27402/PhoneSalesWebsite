using CT_Store.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CT_Store.Controllers
{
    public class HomeController : Controller
    {
        // GET: Product
        public HomeController()
        {
        }
        public ActionResult Index(int? page)
        {
            var context = new StoreModelContext();
            int pageSize = 8;
            int pageIndex = page.HasValue ? page.Value : 1;
            var listProduct = context.Products.ToList().ToPagedList(pageIndex, pageSize);
            return View(listProduct);
        }

        public ActionResult GetCategory()
        {
            var context = new StoreModelContext();
            var listCategory = context.Categories.ToList();
            return PartialView(listCategory);
        }
        public ActionResult GetCategoryOnMobile()
        {
            var context = new StoreModelContext();
            var listCategoryOnMobile = context.Categories.ToList();
            return PartialView(listCategoryOnMobile);
        }

        public ActionResult GetProductByCategory(int id)
        {
            var context = new StoreModelContext();
            var result = context.Products.Where(p => p.CategoryID == id).ToList().ToPagedList(1, 8);
            return View("Index", result);
        }

        public ActionResult Detail(int id)
        {
            var context = new StoreModelContext();
            var findProduct = context.Products.FirstOrDefault(p => p.ProductID == id);
            if (findProduct == null)
                return HttpNotFound("Không tìm thấy mã sản phẩm này!");
            return View(findProduct);

        }
        public ActionResult Search(string searchString)
        {
            var context = new StoreModelContext();

            var result = context.Products.Where(m => m.ProductName.Contains(searchString)).ToList().ToPagedList(1, 8);
            if (result.Count > 0)
                return View("Index", result);
            return HttpNotFound("Thông tin tìm kiếm chưa có. Xin cảm ơn!");
        }

        public ActionResult SortPriceHighToLow()
        {
            var context = new StoreModelContext();
            var listPriceHighToLow = context.Products.OrderByDescending(p => p.Price).ToList().ToPagedList(1, 8);
            return View("Index", listPriceHighToLow);
        }

        public ActionResult SortPriceLowToHigh()
        {
            var context = new StoreModelContext();
            var listPriceLowToHigh = context.Products.OrderBy(p => p.Price).ToList().ToPagedList(1, 8);
            return View("Index", listPriceLowToHigh);
        }

        public ActionResult HistoryOrder()
        {
            var context = new StoreModelContext();
            string curentUserId = User.Identity.GetUserId();
            var result = context.Orders.Where(p => p.UserID == curentUserId).ToList();
            return View(result);
        }
    }
}