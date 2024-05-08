using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.DataTransferObjects
{
    public class OrderDto
    {

        public string BasketId { get; set; }

        public string Email { get; set;}

        public int? DeliveryMethodId { get; set; }


        public AdressDto ShippingAdress { get; set; }
    }
}
