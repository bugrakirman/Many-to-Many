using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    class Program
    {
        protected internal static MyContext db;

       
        static void Main(string[] args)
        {
            db = db ?? new MyContext();

            Product product = new Product()
            {
                //ProductID = 1 
            };         
            db.Products.Add(product);
            db.SaveChanges();
            var products = db.Set <Product>().ToList();
            Package package = new Package
            {
                 PackageID = 11, Size = 10,
                  ProductID=products.Where(x=>x.ProductID==product.ProductID).LastOrDefault().ProductID 
            };
            db.Packages.Add(package);
            db.SaveChanges();
            Order order = new Order
            {
                  //OrderID = 111
            };
            db.Orders.Add(order);
            db.SaveChanges();
            var orders = db.Set<Order>().ToList();
            var packages = db.Set<Package>().ToList();
            OrderDetails orderDetails = new OrderDetails
            {
                 //OrderDetailID = 1111,
                  PackageID=packages.Where(x=>x.PackageID==package.PackageID).LastOrDefault().PackageID,
                   OrderID= orders.Where(x => x.OrderID == order.OrderID).LastOrDefault().OrderID
            };
            db.OrderDetails.Add(orderDetails);
            db.SaveChanges();


            db.Products.FirstOrDefault().Qnt =Convert.ToInt32(db.Products.FirstOrDefault().Qnt) + package.Size * 5;
            int a = db.SaveChanges();
        }
    }
}
