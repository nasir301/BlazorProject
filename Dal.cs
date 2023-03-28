using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1
{
    public class Customer
    {
        [Key]
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public IList<Order>? Order { get; set; }
    }

    public class Order
    {
        [Key]
        public string OrderID { get; set; }
        [ForeignKey("Order")]
        public string CustomerID { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        public decimal? cost { get; set; }
        public decimal? rate { get; set; }
        public string picture { get; set; }
        public Customer Customer { get; set; }
    }
    public class CustomerOrderVm
    {
        public CustomerOrderVm()
        {
            this.Customer = new Customer();
            this.Order = new List<Order>();
            /* do nothing */
        }
        public Customer Customer { get; set; }
        public List<Order> Order { get; set; }
    }
}
