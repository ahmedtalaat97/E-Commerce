using E_Commerce_Core.Enities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Specifications
{
    public class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId) 
            : base(order=>order.PaymentIntentId== PaymentIntentId)
        {
            IncludeExpressions.Add(o => o.DeliveryMethod);
        }
    }
}
