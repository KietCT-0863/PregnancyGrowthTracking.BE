using Microsoft.AspNetCore.Http;
using PregnancyGrowthTracking.DAL.DTOs.Vnpay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services.Vnpay
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}