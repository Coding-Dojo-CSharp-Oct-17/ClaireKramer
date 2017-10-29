using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using commerce.Models;
using System.Linq;

namespace commerce.Controllers
{
    public class HomeController : Controller
    {
        private CommerceContext _context;
        public HomeController(CommerceContext context) {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Product> RecentProducts = _context.Products.ToList();
            List<Order> RecentOrders = _context.Orders.OrderByDescending(p => p.CreatedAt).ToList();
            List<Customer> NewCustomers = _context.Customers.OrderByDescending(c => c.CreatedAt).ToList();
            ViewBag.RecentProducts = RecentProducts;
            ViewBag.RecentOrders = RecentOrders;
            ViewBag.NewCustomers = NewCustomers;
            return View();
        }

        [HttpGet]
        [Route("Customers")]
        public IActionResult Customers() {
            List<Customer> AllCustomers = _context.Customers.ToList();
            ViewBag.AllCustomers = AllCustomers;
            return View();
        }

        [HttpGet]
        [Route("Orders")]
        public IActionResult Orders() {
            List<Order> AllOrders = _context.Orders.ToList();
            List<Customer> AllCustomers = _context.Customers.OrderBy(c => c.CustomerName).ToList();
            List<Product> AllProducts = _context.Products.OrderBy(p => p.ProductName).ToList();
            ViewBag.AllOrders = AllOrders;
            ViewBag.AllCustomers = AllCustomers;
            ViewBag.AllProducts = AllProducts;
            return View();
        }

        [HttpGet]
        [Route("Products")]
        public IActionResult Products() {
            List<Product> AllProducts = _context.Products.ToList();
            ViewBag.AllProducts = AllProducts;
            return View();
        }

        [HttpPost]
        [Route("NewOrder")]
        public IActionResult NewOrder(Order model) {
            Customer CurrentCustomer = _context.Customers.SingleOrDefault(c => c.CustomerId == model.CustomerId);
            Product SelectedProduct = _context.Products.SingleOrDefault(p => p.ProductId == model.ProductId);
            if(SelectedProduct.ProductQuantity < model.OrderQuantity) {
                ModelState.AddModelError("OrderQuantity", "Not Enough Product in Stock");
            }
            if(ModelState.IsValid) {
                Order NewOrder = new Order {
                    OrderQuantity = model.OrderQuantity,
                    CreatedAt = DateTime.Now,
                    CustomerId = model.CustomerId,
                    Customer = CurrentCustomer,
                    ProductId = model.ProductId,
                    Product = SelectedProduct
                };
                _context.Add(NewOrder);
                SelectedProduct.ProductQuantity -= model.OrderQuantity;
                _context.SaveChanges();
                return RedirectToAction("Orders");
            }
            List<Order> AllOrders = _context.Orders.ToList();
            List<Customer> AllCustomers = _context.Customers.OrderBy(c => c.CustomerName).ToList();
            List<Product> AllProducts = _context.Products.OrderBy(p => p.ProductName).ToList();
            ViewBag.AllOrders = AllOrders;
            ViewBag.AllCustomers = AllCustomers;
            ViewBag.AllProducts = AllProducts;
            return View("Orders");
        }

        [HttpPost]
        [Route("NewCustomer")]
        public IActionResult NewCustomer(Customer model) {
            Customer CheckName = _context.Customers.SingleOrDefault(c => c.CustomerName == model.CustomerName);
            if(CheckName != null) {
                ModelState.AddModelError("CustomerName", "Name already in use");
            }
            if(ModelState.IsValid) {
                Customer NewCustomer = new Customer {
                    CustomerName = model.CustomerName,
                    CreatedAt = DateTime.Now
                };
                _context.Add(NewCustomer);
                _context.SaveChanges();
            }
            return RedirectToAction("Customers");
        }

        [HttpPost]
        [Route("NewProduct")]
        public IActionResult NewProduct(Product model) {
            Product CheckName = _context.Products.SingleOrDefault(p => p.ProductName == model.ProductName);
            if(CheckName != null) {
                ModelState.AddModelError("Product", "Name already in use");
            }
            if(ModelState.IsValid) {
                _context.Add(model);
                _context.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        [HttpGet]
        [Route("Remove/{CustomerId}")]
        public IActionResult Remove(int CustomerId) {
            Customer RetrievedCustomer = _context.Customers.SingleOrDefault(c => c.CustomerId == CustomerId);
            _context.Customers.Remove(RetrievedCustomer);
            _context.SaveChanges();
            return RedirectToAction("Customers");
        }
    }
}
