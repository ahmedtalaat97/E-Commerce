using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.Enities.OrderEntities
{
    public class Order : BaseEntity<Guid>
    {
        public string Email { get; set; }

        public DateTime OrderDate { get; set; }

        public ShippingAdress ShippingAdress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public int? DeliveryMethodId { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;


        public decimal SubTotal { get; set; }

        public string? PaymentIntentId {  get; set; }
        public string? BasketId { get; set; }

        public decimal Total() => SubTotal + DeliveryMethod.Price;
    }
}
