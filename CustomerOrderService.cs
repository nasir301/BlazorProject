using BlazorApp1.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlazorApp1
{
    public class CustomerOrderService
    {
        ApplicationDbContext db;
        public CustomerOrderService(ApplicationDbContext _db)
        {
            db = _db;
        }

        public string AddCustomerOrderVm(CustomerOrderVm md2)
        {
            Customer m = new Customer() { CustomerID = md2.Customer.CustomerID, CustomerName = md2.Customer.CustomerName, BillingAddress = md2.Customer.BillingAddress, ShippingAddress = md2.Customer.ShippingAddress };
            db.Customer.Add(m);
            db.SaveChanges();
            db.Entry(m).State = EntityState.Detached;
            foreach (var c in md2.Order)
            {
                Order d = new Order() { OrderID = c.OrderID, CustomerID = c.CustomerID, OrderDate = c.OrderDate, cost = c.cost, rate = c.rate, picture = c.picture };
                db.Order.Add(d);
            }
            db.SaveChanges();

            return "1";
        }
        public string RemoveCustomerOrderVm(string id)
        {
            List<Order> st5 = db.Order.Where(xx => xx.CustomerID == id).ToList();
            db.Order.RemoveRange(st5);
            db.SaveChanges();
            Customer st6 = db.Customer.Find(id);
            if (st6 != null)
            {
                db.Customer.Remove(st6);
            }
            db.SaveChanges();

            return "1";
        }
        public string UpdateCustomerOrderVm(CustomerOrderVm md)
        {
            RemoveCustomerOrderVm(md.Customer.CustomerID);
            AddCustomerOrderVm(md);
            return "1";
        }
        public List<Customer> GetTwoTables()
        {
            List<Customer> md = new List<Customer>();

            md = (from d in db.Customer select d).ToList();
            //var jo = JsonSerializer.Deserialize<Master>(a);
            return md;
        }
        public CustomerOrderVm GetTwoTables2(string id)
        {
            CustomerOrderVm md = new CustomerOrderVm();
            Customer a = new Customer();
            a = (from d in db.Customer where d.CustomerID == id select d).FirstOrDefault();
            md.Customer = a;
            List<Order> b = new List<Order>();
            b = (from d in db.Order where d.CustomerID == id select d).ToList();
            md.Order = b;
            if (a != null) db.Entry(a).State = EntityState.Detached;
            return md;

        }

        public string GenerateCode()
        {
            string a1 = "";
            string b1 = "";

            try
            {
                var a = (from det in db.Customer select det.CustomerID.Substring(3)).Max();
                if (a == null)
                    a = "0";
                int b = int.Parse(a.ToString()) + 1;
                if (b < 10)
                {
                    b1 = "000" + b.ToString();
                }
                else if (b < 100)
                {
                    b1 = "00" + b.ToString();
                }
                else if (b < 1000)
                {
                    b1 = "0" + b.ToString();
                }
                else
                {
                    b1 = b.ToString();
                }
                a1 = "AC-" + b1.ToString();
            }
            catch (Exception ex)
            {
                a1 = "AC-0001";
            }
            return a1;
        }
        public string Child_Exists(string id)
        {
            var a = (from p in db.Order where p.OrderID == id select new { p.OrderID, }).FirstOrDefault();
            if (a != null)
                return "1";
            else
                return "0";
        }
    }
}

