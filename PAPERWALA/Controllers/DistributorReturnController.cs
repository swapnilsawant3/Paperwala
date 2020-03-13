using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAPERWALA.Filters;
using PAPERWALA.Models;
using PAPERWALA.Repository;

namespace PAPERWALA.Controllers
{
    public class DistributorReturnController : Controller
    {

        IDistributorReturn objdistributorReturn;
        //IDistributorReturnProduct objdistributorReturnproduct;
        IRetailerMaster objretailer;
        IPaperRateMaster objpaperrate;

        public DistributorReturnController()
        {
            objdistributorReturn = new DistributorReturn();
            objretailer = new RetailerMaster();
            objpaperrate = new PaperRateMaster();
            //objdistributorReturnproduct = new DistributorReturnProduct();
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            return View(objdistributorReturn.GetDistributorReturns(id));
        }


        [ChildActionOnly]
        public ActionResult BindProduct()
        {

            return PartialView(objdistributorReturn.GetDistributorReturnProducts(Session["ReturnOrder"].ToString()));
        }

        [HttpGet]
        public ActionResult Create(string tri)

        {
            DistributorReturnDTO objDS = new DistributorReturnDTO();
            objDS.ListRetailer = BindListRetailer_DReturn(tri);
            //BindListRetailer(objDT.DistributorId.ToString());
            ViewData["SelectedRetailer"] = string.Empty;


            return View(objDS);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistributorReturnDTO obj, FormCollection frm, string tri)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(obj.RetailerId)))
                {
                    ModelState.AddModelError("Error", "Please Select Retailer");
                }
                else
                {

                    obj.DistributorReturnId = 0;
                    obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.CreatedDate = cstTime;


                    objdistributorReturn.InsertDistributorReturn(obj);
                    TempData["MessageRegistration"] = "Data Saved Successfully!";
                    //  ViewData["VReturnOrder"]= obj.ReturnOrder;
                    TempData["AddPaperTrn"] = "AddPapaerTrn";
                    Session["TransDate"] = obj.TransDate.ToShortDateString();
                    ViewBag.VRetailerId = obj.RetailerId;
                    Session["ReturnOrder"] = obj.ReturnOrder;
                    Session["PrvBalanceRetailer"] = obj.PrvBalanceAmount;
                    return RedirectToAction("AddProductT");


                }
                obj.ListRetailer = BindListRetailer_DReturn(tri);
                // obj.ListPaperbyCityId = BindListPaper(tri);
                //obj.ListCity = BindListCity();
                ViewData["SelectedrRetailer"] = obj.RetailerId;
                //ViewData["SelectedPaper"] = obj.PaperId;
                //ViewData["SelectedCity"] = obj.CityId;
                return View(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult AddProduct()
        {
            DistributorReturnDTO model = new DistributorReturnDTO();
            //model.ReturnOrder = Session["VReturnOrder"].ToString();
            //model.TransDate = Convert.ToDateTime(Session["VTrnDate"].ToString());
            //model.RetailerId =Convert.ToInt32(Session["VRetailerId"]);
            return PartialView("AddProduct", model);
        }
        [HttpGet]
        public ActionResult AddProductT(string tri)
        {
            DistributorReturnProductDTO objDT = new DistributorReturnProductDTO();
            objDT.ListPaperbyDistId = BindListPaperL(tri);
            objDT.ReturnOrder = Session["ReturnOrder"].ToString();
            string selectDate = Session["TransDate"].ToString();
            string D = Convert.ToDateTime(selectDate).ToString("yyyy-MM-dd");
            DateTime FD = Convert.ToDateTime(D);
            objDT.TransDateP = FD;

            //objDT.re = Convert.ToInt32(Session["VRetailerId"]);
            ViewData["SelectedPaper"] = string.Empty;

            return View(objDT);
        }

        [HttpPost]
        public ActionResult AddProductT(DistributorReturnProductDTO obj, FormCollection frm, string tri)
        {
            try
            {

                if (string.IsNullOrEmpty(Convert.ToString(obj.PaperId)))
                {
                    ModelState.AddModelError("Error", "Please Select Paper Name");
                }
                else
                {

                    obj.DistributorReturnProductId = 0;
                    obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.CreatedDate = cstTime;


                    objdistributorReturn.InsertDistributorReturnProduct(obj);
                    TempData["MessageRegistration"] = "Data Saved Successfully!";
                    TempData["AddPaperTrn"] = "AddPapaerTrn";
                    TempData["UpdtDistTrn"] = "UpdtDistTrn";
                    // DistributorReturnDTO DS = new DistributorReturnDTO();
                    return RedirectToAction("AddProductT", obj);


                }
                // obj.ListRetailer = BindListRetailer(tri);
                obj.ListPaperbyDistId = BindListPaperL(tri);
                //obj.ListCity = BindListCity();
                // ViewData["SelectedrRetailer"] = obj.RetailerId;
                ViewData["SelectedPaper"] = obj.PaperId;
                //ViewData["SelectedCity"] = obj.CityId;
                return PartialView("Create", objdistributorReturn);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public ActionResult UpdateDistTrans(string tri)

        {
            DistributorReturnDTO objDS = new DistributorReturnDTO();
            // objDS.ListRetailer = BindListRetailer_DReturn(tri);
            //BindListRetailer(objDT.DistributorId.ToString());
            ViewData["SelectedRetailer"] = string.Empty;
            var TotalFinalAMT = GetFinalAmount_By_ReturnOrder(tri);
            TempData["TFianlAMT"] = TotalFinalAMT;
            return View(objDS);
        }

        [HttpPost]
        public ActionResult UpdateDistTrans(DistributorReturnDTO obj, FormCollection frm, string tri)
        {
            try
            {

                if (string.IsNullOrEmpty(Convert.ToString(obj.PaperId)))
                {
                    ModelState.AddModelError("Error", "Please Select Paper Name");
                }
                else
                {

                    obj.DistributorReturnProductId = 0;
                    obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.CreatedDate = cstTime;


                    objdistributorReturn.UpdateDistributorReturn(obj);
                    TempData["MessageRegistration"] = "Data Saved Successfully!";
                    TempData["AddPaperTrn"] = "AddPapaerTrn";
                    TempData["UpdtDistTrn"] = "UpdtDistTrn";
                    // DistributorReturnDTO DS = new DistributorReturnDTO();
                    return RedirectToAction("Details");


                }
                // obj.ListRetailer = BindListRetailer(tri);
                // obj.ListPaperbyDistId = BindListPaperL(tri);
                //obj.ListCity = BindListCity();
                // ViewData["SelectedrRetailer"] = obj.RetailerId;
                // ViewData["SelectedPaper"] = obj.PaperId;
                //ViewData["SelectedCity"] = obj.CityId;
                return PartialView("Create", objdistributorReturn);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public ActionResult DeleteReturnProduct(string ID)
        {
            objdistributorReturn.DeleteDistributorReturnProduct(ID);
            return RedirectToAction("AddProductT");
        }
        [HttpGet]
        public ActionResult DeleteReturn(string ID)
        {
            objdistributorReturn.DeleteDistributorReturn(ID);
            return RedirectToAction("Details");
        }

        public List<DistributorReturnDTO> BindListRetailer_DReturn(string distId)
        {
            List<DistributorReturnDTO> listritailer = new List<DistributorReturnDTO>()
            { new DistributorReturnDTO  { RetailerId = 0, RetailerName = "Select" } };

            foreach (var item in objdistributorReturn.GetRetailerByDistributerId(distId))
            {
                DistributorReturnDTO RT = new DistributorReturnDTO();
                RT.RetailerId = item.RetailerId;
                RT.RetailerName = item.RetailerName;
                listritailer.Add(RT);
            }

            return listritailer;

        }
        public JsonResult GetRetailerPrevBal(string RetailerId)
        {
            var RetailerPrevBaldata = objretailer.GetPrevBalalnceByRetailerId(RetailerId);
            return Json(RetailerPrevBaldata, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public IEnumerable<PaperDTO> BindListPaperL(string CityId)
        {
            List<PaperDTO>
            ListPaper = new List<PaperDTO>() { new PaperDTO { PaperId = 0, PaperName = "Select" } };

            foreach (var item in objdistributorReturn.GetPaperByCityId(CityId))
            {
                PaperDTO P = new PaperDTO();
                P.PaperId = item.PaperId;
                P.PaperName = item.PaperName;
                ListPaper.Add(P);
            }

            return ListPaper;
        }
        public JsonResult GetPaperRate(string PaperId, string Tdate)
        {
            var PaperRatedata = objpaperrate.GetPaperRateByPaperId(PaperId, Tdate);
            return Json(PaperRatedata, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public string GetFinalAmount_By_ReturnOrder(string ReturnOrder)
        {
            return objdistributorReturn.GetFinalAmount_By_ReturnOrder(Session["ReturnOrder"].ToString());
        }

        public string GetFinalAmount_By_ReturnOrder_API(string ReturnOrder)
        {
            return objdistributorReturn.GetFinalAmount_By_ReturnOrder_API(ReturnOrder);
        }
        [NonAction]
        public string GetPreviousBalance_By_RetailerID(string RetailerID)
        {
            return objretailer.GetPrevBalalnceByRetailerId(RetailerID);
        }
        //[HttpGet]
        //[ChildActionOnly]
        //public PartialViewResult MyPartialView()
        //{
        //    DistributorReturnProductDTO model = new DistributorReturnProductDTO();
        //    return PartialView("~/Views/DistributorReturnProduct/Create.cshtml", model);
        //}
        // GET: DistributorReturn
        public ActionResult Index()
        {
            return View();
        }
    }
}