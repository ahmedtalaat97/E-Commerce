using E_Commerce_Core.Enities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string email) : 
            base(order=>order.Email==email)
        {
            IncludeExpressions.Add(order => order.DeliveryMethod);
            IncludeExpressions.Add(order => order.OrderItems);

            OrderByDesc= o=>o.OrderDate;
        }


        public OrderSpecifications(Guid id ,string email) :
            base(order => order.Email == email && order.Id==id)
        {
            IncludeExpressions.Add(order => order.DeliveryMethod);
            IncludeExpressions.Add(order => order.OrderItems);

            
        }



    }
}
