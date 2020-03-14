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
    public class WebDistributorReturnController : ApiController
    {
        static readonly IRetailerMaster objRetailerAll = new RetailerMaster();
        static readonly IDistributorReturn objDistributorReturn = new DistributorReturn();
       // static readonly IDistributorSaleProduct objDistributorSaleP = new DistributorSaleProduct();
        static readonly IPaperRateMaster objpaperrate = new PaperRateMaster();
        static readonly IPaperMaster objpaper = new PaperMaster();
        static readonly IDistributorMaster objdistributor = new DistributorMaster();


        [HttpGet]
        [Route("api/webdistributorreturn/GetPaperRate")]
        public string GetPaperRate(string PaperId, string Tdate)
        {
            return objpaperrate.GetPaperRateByPaperId(PaperId, Tdate);
        }

        [HttpGet]
        [Route("api/webdistributorreturn/BindReturnPaperList")]
        public IEnumerable<DistributorReturnDTO> BindReturnPaperList(string ReturnOrder)
        {
            return objDistributorReturn.GetDistributorReturnProducts(ReturnOrder);
        }

        [HttpGet]
        [Route("api/webdistributorreturn/GetFinalAmountByReturnOrder")]
        public string GetFinalAmountByReturnOrder(string ReturnOrder)
        {
            return objDistributorReturn.GetFinalAmount_By_ReturnOrder_API(ReturnOrder);
        }

        [HttpGet]
        [Route("api/webdistributorreturn/GetDistributorReturnDetails")]
        public IEnumerable<DistributorReturnDTO> GetDistributorReturnDetails(string DistributorId)
        {
            return objDistributorReturn.GetDistributorReturnsAPI(DistributorId);
        }

        [HttpGet]
        [Route("api/webdistributorreturn/GetSaleDetailsByReturnOrder")]
        public IEnumerable<DistributorReturnDTO> GetSaleDetailsByReturnOrder(string DistributorId, string ReturnOrder)
        {
            return objDistributorReturn.GetDistributorReturnsTransactionByReturnOrderAPI(DistributorId, ReturnOrder);
        }




        //POST SECTION   ---------------------------% ****************************************************



        [HttpPost]
        [Route("api/webdistributorreturn/CRMainReturnOrder")]
        public HttpResponseMessage CRMainReturnOrder([FromBody]DistributorReturnDTO DistributorReturn)
        {

            try
            {
                objDistributorReturn.InsertDistributorReturn(DistributorReturn);
                var message = Request.CreateResponse<DistributorReturnDTO>(HttpStatusCode.Created, DistributorReturn);
                message.Headers.Location = new Uri(Request.RequestUri + DistributorReturn.DistributorReturnId.ToString());
                return message;
                //string uri = Url.Link("DefaultApi", new { orderID = order.OrderId });
                //response.Headers.Location = new Uri(uri);
                //return response;
                //return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                // return 0;
            }

        }

        [HttpPost]
        [Route("api/webdistributorreturn/CRAddPaperReturn")]
        public HttpResponseMessage CRAddPaperReturn([FromUri]DistributorReturnProductDTO look)
        {

            try
            {
                objDistributorReturn.InsertDistributorReturnProduct(look);
                var message = Request.CreateResponse<DistributorReturnProductDTO>(HttpStatusCode.Created, look);
                message.Headers.Location = new Uri(Request.RequestUri + look.DistributorReturnProductId.ToString());
                return message;
                //string uri = Url.Link("DefaultApi", new { orderID = order.OrderId });
                //response.Headers.Location = new Uri(uri);
                //return response;

                //return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                // return 0;
            }

        }

        [HttpPost]
        [Route("api/webdistributorreturn/UpdateMainReturnTrans")]
        public HttpResponseMessage UpdateMainReturnTrans([FromBody]DistributorReturnDTO DistributorReturn)
        {

            try
            {
                objDistributorReturn.UpdateDistributorReturnAPI(DistributorReturn);
                var message = Request.CreateResponse<DistributorReturnDTO>(HttpStatusCode.Created, DistributorReturn);
                message.Headers.Location = new Uri(Request.RequestUri + DistributorReturn.DistributorReturnId.ToString());
                return message;
                //string uri = Url.Link("DefaultApi", new { orderID = order.OrderId });
                //response.Headers.Location = new Uri(uri);
                //return response;

                //return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                // return 0;
            }

        }



        [HttpDelete]
        [Route("api/webdistributorreturn/DeleteReturnPaper")]
        public HttpResponseMessage DeleteReturnPaper(string id)
        {

            try
            {
                objDistributorReturn.DeleteDistributorReturnProduct(id);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                // return 0;
            }

        }

        [HttpDelete]
        [Route("api/webdistributorreturn/DeleteMainReturnTran")]
        public HttpResponseMessage DeleteMainReturnTran(string id)
        {

            try
            {
                objDistributorReturn.DeleteDistributorReturn(id);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                // return 0;
            }

        }

    }
}
