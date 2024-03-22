using CT_Store.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CT_Store.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [Authorize]
        public ActionResult Index()
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            //if (ShoppingCart.Count == 0)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            ViewBag.Tongsoluong = ShoppingCart.Sum(p => p._quantity);
            ViewBag.TongTien = ShoppingCart.Sum(p => (p._quantity * p._price));

            return View(ShoppingCart);
        }

        public List<CartItem> GetShoppingCartFromSession()
        {
            var listShoppingCart = Session["Cart"] as List<CartItem>;
            if(listShoppingCart == null)
            {
                listShoppingCart= new List<CartItem>();
                Session["Cart"] = listShoppingCart;
            }
            return listShoppingCart;
        }
        [Authorize]
        public RedirectToRouteResult AddToCart(int id)
        {
            StoreModelContext db = new StoreModelContext();
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            CartItem findCartItem = ShoppingCart.FirstOrDefault(m => m._product_id == id);
            if (findCartItem == null)
            {
                Product findProduct = db.Products.First(m => m.ProductID == id);
                CartItem newItem = new CartItem()
                {
                    _product_id = findProduct.ProductID,
                    _product_name = findProduct.ProductName,
                    _quantity = 1,
                    _image_main = findProduct.ImageMain,
                    _price = findProduct.Price
                };
                ShoppingCart.Add(newItem);
            }
            else
            {
                findCartItem._quantity++;
            }
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult CartSummary()
        {
            ViewBag.CartCount = GetShoppingCartFromSession().Count();
            return PartialView("CartSummary");
        }

        //public ActionResult Order()
        //{
        //    string curentUserId = User.Identity.GetUserId();
        //    StoreModelContext context = new StoreModelContext();
        //    using (DbContextTransaction transaction = context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            List<CartItem> listCartItems = GetShoppingCartFromSession();
        //            Order objOrder = new Order()
        //            {

        //                UserID = curentUserId,
        //                OrderDay = DateTime.Now,
        //                TotalPrice = listCartItems.Sum(p => (p._quantity * p._price)),
        //                isPaid = false,
        //                FullName = 
        //            };
        //            objOrder = context.Orders.Add(objOrder);
        //            context.SaveChanges();

        //            foreach (var item in listCartItems)
        //            {
        //                OrderDetail detail = new OrderDetail()
        //                {
        //                    OrderNo = objOrder.OrderNo,
        //                    Id = item.Id,
        //                    Quantity = item.Quantity,
        //                    Price = item.Price,
        //                };
        //                context.OrderDetails.Add(detail);
        //                context.SaveChanges();
        //            }
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            return Content("Gặp lỗi khi đặt hàng " + ex.Message);
        //        }
        //    }
        //    Session["Giohang"] = null;
        //    return RedirectToAction("ConformOrder", "ShoppingCart");
        //}

        [Authorize]
        [HttpPost]
        public ActionResult Order(string name,string phonenumber, string address)
        {
            
            string curentUserId = User.Identity.GetUserId();
            StoreModelContext context = new StoreModelContext();
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<CartItem> listCartItems = GetShoppingCartFromSession();
                    Order objOrder = new Order()
                    {

                        UserID = curentUserId,
                        OrderDay = DateTime.Now,
                        TotalPrice = listCartItems.Sum(p => (p._quantity * p._price)),
                        isPaid = false,
                        FullName = name,
                        Address= address,
                        PhoneNumber = phonenumber
                        };
                    objOrder = context.Orders.Add(objOrder);
                    context.SaveChanges();

                    foreach (var item in listCartItems)
                    {
                        OrderDetail detail = new OrderDetail()
                        {
                            OrderID = objOrder.OrderID,
                            ProductID = item._product_id,
                            Quantity = item._quantity,
                            Price = item._price,
                        };
                        context.OrderDetails.Add(detail);
                        context.SaveChanges();
                    }
                    //listCartItems = null;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //return Content("Gặp lỗi khi đặt hàng " + ex.Message);
                    return Json(new { error = "Gặp lỗi khi đặt hàng " + ex.Message });
                }
            }
            Session["Cart"] = null;
            return Json(new { success = true, message = "Mua hàng thành công!", url = Url.Action("ComformOrder", "Cart") });
        }

        public ActionResult ComformOrder()
        {
            return View();
        }
        public ActionResult DeleteCart(int id)
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            CartItem findCart = ShoppingCart.Find(m => m._product_id == id);
            if (findCart != null)
            {
                ShoppingCart.Remove(findCart);
            }

            return RedirectToAction("Index");
        }
        public ActionResult UpdateCart(int id, int txtQuantity)
        {
            var findItem = GetShoppingCartFromSession().FirstOrDefault(m => m._product_id == id);
            if (findItem != null)
                findItem._quantity = txtQuantity;
            return RedirectToAction("Index", "Cart");
        }
    }
}