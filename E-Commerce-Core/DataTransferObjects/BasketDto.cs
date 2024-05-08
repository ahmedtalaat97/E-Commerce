using E_Commerce_Core.Enities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.DataTransferObjects
{
    public class BasketDto
    {

        public string Id { get; set; }

        public int? DeliveryMethodId { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<BasketItemDto> BasketItems { get; set; } = new();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; } 
    }
}
