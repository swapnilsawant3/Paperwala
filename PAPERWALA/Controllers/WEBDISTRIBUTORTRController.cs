using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PAPERWALA.Models;
using PAPERWALA.Repository;

namespace PAPERWALA.Controllers
{
    public class WEBDISTRIBUTORTRController : ApiController
    {
        static readonly IDistributorSale objDistributorSale = new DistributorSale();

        [HttpGet]
        [Route("api/webdistributor/GetSaleDetailsBySaleOrder")]
        public IEnumerable<DistributorSaleDTO> GetSaleDetailsBySaleOrder(string DistributorId, string SaleOrder)
        {
            return objDistributorSale.GetDistributorSalesTransactionBySaleOrderAPI(DistributorId, SaleOrder);
        }
    }
}
