using E_Commerce_Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.Interfaces.Services
{
    public interface IPaymentService
    {

        public  Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto input);
        public  Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId);
        public  Task<OrderResultDto> UpdatePaymentStatusFailed(string PaymentIntentId);
        public  Task<OrderResultDto> UpdatePaymentStatusSucceeded(string PaymentIntentId);


    }
}
