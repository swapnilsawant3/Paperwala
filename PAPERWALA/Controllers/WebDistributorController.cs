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
    public class WebDistributorController : ApiController
    {
        
        static readonly IRetailerMaster objRetailerAll = new RetailerMaster();
        static readonly IDistributorSale objDistributorSale = new DistributorSale();
        static readonly IDistributorSaleProduct objDistributorSaleP = new DistributorSaleProduct();
        static readonly IPaperRateMaster objpaperrate = new PaperRateMaster();
        static readonly IPaperMaster objpaper = new PaperMaster();
        static readonly IDistributorMaster objdistributor = new DistributorMaster();

        // static readonly IPhotoGallery objPhotogall = new PhotoGallery();
        // static readonly INewsGallery objNewsgall = new NewsGallery();
        [HttpGet]
        [Route("api/webdistributor/RetailerList")]
        public IEnumerable<RetailerDTO> RetailerList(string disId)
        {
            return objRetailerAll.webGetRetailers(disId);
        }

        [HttpGet]
        [Route("api/webdistributor/RetailerById")]
        public IEnumerable<RetailerDTO> RetailerById(string id)
        {
            return objRetailerAll.webGetRetailerById(id);
        }

        [HttpGet]
        [Route("api/webdistributor/BindPaperListByCityId")]
        public IEnumerable<PaperDTO> BindPaperListByCityId(string CityId)
        {
            return objpaper.GetPaperByCityId(CityId);
        }

        [HttpGet]
        [Route("api/webdistributor/GetPaperRate")]
        public string GetPaperRate(string PaperId, string Tdate)
        {
            return objpaperrate.GetPaperRateByPaperId(PaperId, Tdate);
        }

        [HttpGet]
        [Route("api/webdistributor/BindSalePaperList")]
        public IEnumerable<DistributorSaleDTO> BindSalePaperList(string SaleOrder)
        {
            return objDistributorSale.GetDistributorSaleProducts(SaleOrder);
        }

        [HttpGet]
        [Route("api/webdistributor/GetFinalAmountBySaleOrder")]
        public string GetFinalAmountBySaleOrder(string SaleOrder)
        {
            return objDistributorSale.GetFinalAmount_By_SaleOrder_API(SaleOrder);
        }

        [HttpGet]
        [Route("api/webdistributor/GetRetailerPrevBal")]
        public string GetRetailerPrevBal(string RetailerId)
        {
            return objRetailerAll.GetPrevBalalnceByRetailerId(RetailerId);
        }

        [HttpGet]
        [Route("api/webdistributor/GetDistributorPassword")]
        public string GetDistributorPassword(string DistributorId)
        {
            return objdistributor.GetPasswordAPI(DistributorId);
        }

        [HttpGet]
        [Route("api/webdistributor/GetDistributorSaleDetails")]
        public IEnumerable<DistributorSaleDTO> GetDistributorSaleDetails(string DistributorId)
        {
            return objDistributorSale.GetDistributorSalesAPI(DistributorId);
        }

        [HttpGet]
        [Route("api/webdistributor/GetSaleDetailsBySaleOrder")]
        public IEnumerable<DistributorSaleDTO> GetSaleDetailsBySaleOrder(string DistributorId,string SaleOrder)
        {
            return objDistributorSale.GetDistributorSalesTransactionBySaleOrderAPI(DistributorId,SaleOrder);
        }
        [HttpGet]
        [Route("api/webdistributor/GetSaleMainDetailsBySaleOrder")]
        public IEnumerable<DistributorSaleDTO> GetSaleMainDetailsBySaleOrder( string SaleOrder)
        {
            return objDistributorSale.GetDistributorSalesMainTransactionBySaleOrderAPI( SaleOrder);
        }
        
        [HttpGet]
        [Route("api/webdistributor/GetRetailerCount")]
        public string GetRetailerCount(string DistributorId)
        {
            return objRetailerAll.GetRetailerCount(DistributorId);
        }

        [HttpGet]
        [Route("api/webdistributor/GetSumOfBalanceAmountByDistributorId")]
        public string GetSumOfBalanceAmountByDistributorId(string DistributorId)
        {
            return objdistributor.GetSumBalanceAmount(DistributorId);
        }

        //POST SECTION   ---------------------------% ****************************************************



        [HttpPost]
        [Route("api/webdistributor/CRMainOrder")]
        public HttpResponseMessage CRMainOrder([FromBody]DistributorSaleDTO Distributorsale)
        {

            try
            {
                objDistributorSale.InsertDistributorSale(Distributorsale);
                var message = Request.CreateResponse<DistributorSaleDTO>(HttpStatusCode.Created, Distributorsale);
                message.Headers.Location = new Uri(Request.RequestUri + Distributorsale.DistributorSaleId.ToString());
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
        [Route("api/webdistributor/CRAddPaper")]
        public HttpResponseMessage CRAddPaper([FromUri]DistributorSaleProductDTO look)
        {

            try
            {
                objDistributorSale.InsertDistributorSaleProduct(look);
                var message = Request.CreateResponse<DistributorSaleProductDTO>(HttpStatusCode.Created, look);
                message.Headers.Location = new Uri(Request.RequestUri + look.DistributorSaleProductId.ToString());
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
        [Route("api/webdistributor/UpdateMainSaleTrans")]
        public HttpResponseMessage UpdateMainSaleTrans([FromBody]DistributorSaleDTO Distributorsale)
        {

            try
            {
                objDistributorSale.UpdateDistributorSaleAPI(Distributorsale);
                var message = Request.CreateResponse<DistributorSaleDTO>(HttpStatusCode.Created, Distributorsale);
                message.Headers.Location = new Uri(Request.RequestUri + Distributorsale.DistributorSaleId.ToString());
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
        [Route("api/webdistributor")]
        public HttpResponseMessage Post([FromBody]RetailerDTO retailer)
        {

            try
            {
                objRetailerAll.InsertRetailer(retailer);
                var message = Request.CreateResponse<RetailerDTO>(HttpStatusCode.Created, retailer);
                message.Headers.Location = new Uri(Request.RequestUri + retailer.RetailerId.ToString());
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
        [Route("api/webdistributor/ChangePassword")]
        public HttpResponseMessage ChangePassword([FromBody]DistributorDTO Distributor)
        {

            try
            {
                objdistributor.ChangePasswordAPI(Distributor);
                var message = Request.CreateResponse<DistributorDTO>(HttpStatusCode.Created, Distributor);
                message.Headers.Location = new Uri(Request.RequestUri + Distributor.DistributorId.ToString());
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


        //PUT SECTION ------------------------------------****************************************************************

        [HttpPut]
        [Route("api/webdistributor")]
        public HttpResponseMessage Put(int id,[FromBody]RetailerDTO retailer)
        {

            try
            {
                objRetailerAll.UpdateRetailer(retailer);
                var message = Request.CreateResponse<RetailerDTO>(HttpStatusCode.OK, retailer);
                message.Headers.Location = new Uri(Request.RequestUri + retailer.RetailerId.ToString());
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



        //DELETE SECTION --------------------------------------------------------------*************************************************

        [HttpDelete]
        [Route("api/webdistributor")]
        public HttpResponseMessage Delete(string RetailerId)
        {

            try
            {
                objRetailerAll.DeleteRetailer(RetailerId);
                return Request.CreateResponse(HttpStatusCode.OK);
               // message.Headers.Location = new Uri(Request.RequestUri + retailer.RetailerId.ToString());
                 //message;
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

        [HttpGet]
        [Route("api/webdistributor")]
        public HttpResponseMessage DeleteRetailer(string RetailerId)
        {

            try
            {
                objRetailerAll.DeleteRetailer(RetailerId);
                return Request.CreateResponse(HttpStatusCode.OK);
                // message.Headers.Location = new Uri(Request.RequestUri + retailer.RetailerId.ToString());
                //message;
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
        [Route("api/webdistributor/DeleteSalePaper")]
        public HttpResponseMessage DeleteSalePaper(string id)
        {

            try
            {
                objDistributorSale.DeleteDistributorSaleProduct(id);
                return Request.CreateResponse(HttpStatusCode.OK);
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                // return 0;
            }

        }

        [HttpDelete]
        [Route("api/webdistributor/DeleteMainSaleTran")]
        public HttpResponseMessage DeleteMainSaleTran(string id)
        {

            try
            {
                objDistributorSale.DeleteDistributorSale(id);
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
