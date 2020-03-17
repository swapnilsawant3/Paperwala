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
    public class DistributorSaleController : Controller
    {

        IDistributorSale objdistributorsale;
        IDistributorSaleProduct objdistributorsaleproduct;
        IRetailerMaster objretailer;
        IPaperRateMaster objpaperrate;
        //IDistributorSaleProduct objdistribuorsaleproduct;

        public DistributorSaleController()
        {
            objdistributorsale = new DistributorSale();
            objretailer = new RetailerMaster();
            objpaperrate = new PaperRateMaster();
            objdistributorsaleproduct = new DistributorSaleProduct();
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            return View(objdistributorsale.GetDistributorSales(id));
        }

         [ChildActionOnly]
            public ActionResult BindProduct()
            {

                return PartialView(objdistributorsale.GetDistributorSaleProducts(Session["SaleOrder"].ToString()));
            }

            [HttpGet]
            public ActionResult Create(string tri)

           {
                DistributorSaleDTO objDS = new DistributorSaleDTO();
                objDS.ListRetailer = BindListRetailer_DSale(tri);
                //BindListRetailer(objDT.DistributorId.ToString());
                ViewData["SelectedRetailer"] = string.Empty;
            

                return View(objDS);
            }
        

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(DistributorSaleDTO obj, FormCollection frm, string tri)
            {
                try
                {
                    if (string.IsNullOrEmpty(Convert.ToString(obj.RetailerId)))
                    {
                        ModelState.AddModelError("Error", "Please Select Retailer");
                    }
                    else
                    {

                        obj.DistributorSaleId = 0;
                        obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                        DateTime timeUtc = System.DateTime.UtcNow;
                        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                        obj.CreatedDate = cstTime;

                    
                        objdistributorsale.InsertDistributorSale(obj);
                        TempData["MessageRegistration"] = "Data Saved Successfully!";
                       //  ViewData["VSaleOrder"]= obj.SaleOrder;
                        TempData["AddPaperTrn"] = "AddPapaerTrn";
                        Session["TransDate"]= obj.TransDate.ToShortDateString();
                        ViewBag.VRetailerId = obj.RetailerId;
                        Session["SaleOrder"] = obj.SaleOrder;
                    Session["RetailerId"] = obj.RetailerId;
                    Session["PrvBalanceRetailer"] = obj.PrvBalanceAmount;
                        return RedirectToAction("AddProductT");


                    }
                    obj.ListRetailer = BindListRetailer_DSale(tri);
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
                DistributorSaleDTO model = new DistributorSaleDTO();
                //model.SaleOrder = Session["VSaleOrder"].ToString();
                //model.TransDate = Convert.ToDateTime(Session["VTrnDate"].ToString());
                //model.RetailerId =Convert.ToInt32(Session["VRetailerId"]);
                return PartialView("AddProduct", model);
            }
            [HttpGet]
            public ActionResult AddProductT(string tri)
            {
                DistributorSaleProductDTO objDT = new DistributorSaleProductDTO();
                objDT.ListPaperbyDistId = BindListPaperL(tri);
                objDT.SaleOrder = Session["SaleOrder"].ToString();
                string selectDate = Session["TransDate"].ToString();
                string D = Convert.ToDateTime(selectDate).ToString("yyyy-MM-dd");
                DateTime FD = Convert.ToDateTime(D);
                objDT.TransDateP = FD;

                //objDT.re = Convert.ToInt32(Session["VRetailerId"]);
                ViewData["SelectedPaper"] = string.Empty;

                return View(objDT);
            }

            [HttpPost]
            public ActionResult AddProductT(DistributorSaleProductDTO obj, FormCollection frm, string tri)
            {
                try
                {
                

                    if (string.IsNullOrEmpty(Convert.ToString(obj.PaperId)))
                    {
                        ModelState.AddModelError("Error", "Please Select Paper Name");
                    }
                    else
                    {

                        obj.DistributorSaleProductId = 0;
                        obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                        DateTime timeUtc = System.DateTime.UtcNow;
                        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                        obj.CreatedDate = cstTime;

                    
                        objdistributorsale.InsertDistributorSaleProduct(obj);
                   
                        TempData["MessageRegistration"] = "Data Saved Successfully!";
                        TempData["AddPaperTrn"] = "AddPapaerTrn";
                        TempData["UpdtDistTrn"] = "UpdtDistTrn";
                       // DistributorSaleDTO DS = new DistributorSaleDTO();
                        return RedirectToAction("AddProductT",obj);


                    }
                    // obj.ListRetailer = BindListRetailer(tri);
                    obj.ListPaperbyDistId = BindListPaperL(tri);
                    //obj.ListCity = BindListCity();
                    // ViewData["SelectedrRetailer"] = obj.RetailerId;
                    ViewData["SelectedPaper"] = obj.PaperId;
                    //ViewData["SelectedCity"] = obj.CityId;
                    return PartialView("Create",objdistributorsale);
                }
                catch (Exception)
                {
                    throw;
                }
            }


            [HttpGet]
            public ActionResult UpdateDistTrans(string tri)

            {
                DistributorSaleDTO objDS = new DistributorSaleDTO();
               // objDS.ListRetailer = BindListRetailer_DSale(tri);
                //BindListRetailer(objDT.DistributorId.ToString());
                ViewData["SelectedRetailer"] = string.Empty;
                var TotalFinalAMT = GetFinalAmount_By_SaleOrder(tri);
                TempData["TFianlAMT"] = TotalFinalAMT;
                return View(objDS);
            }

            [HttpPost]
            public ActionResult UpdateDistTrans(DistributorSaleDTO obj, FormCollection frm, string tri)
            {
                try
                {

                    if (string.IsNullOrEmpty(Convert.ToString(obj.PaperId)))
                    {
                        ModelState.AddModelError("Error", "Please Select Paper Name");
                    }
                    else
                    {

                        obj.DistributorSaleProductId = 0;
                        obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                        DateTime timeUtc = System.DateTime.UtcNow;
                        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                        obj.CreatedDate = cstTime;


                        objdistributorsale.UpdateDistributorSale(obj);
                       
                        TempData["MessageRegistration"] = "Data Saved Successfully!";
                        TempData["AddPaperTrn"] = "AddPapaerTrn";
                        TempData["UpdtDistTrn"] = "UpdtDistTrn";
                        // DistributorSaleDTO DS = new DistributorSaleDTO();
                        return RedirectToAction("Details");


                    }
                    // obj.ListRetailer = BindListRetailer(tri);
                   // obj.ListPaperbyDistId = BindListPaperL(tri);
                    //obj.ListCity = BindListCity();
                    // ViewData["SelectedrRetailer"] = obj.RetailerId;
                   // ViewData["SelectedPaper"] = obj.PaperId;
                    //ViewData["SelectedCity"] = obj.CityId;
                    return PartialView("Create", objdistributorsale);
                }
                catch (Exception)
                {
                    throw;
                }
            }


            [HttpGet]
            public ActionResult DeleteSaleProduct(string ID)
            {
                objdistributorsale.DeleteDistributorSaleProduct(ID);
                return RedirectToAction("AddProductT");
            }
            [HttpGet]
            public ActionResult DeleteSale(string ID)
            {
                objdistributorsale.DeleteDistributorSale(ID);
                return RedirectToAction("Details");
            }

            public List<DistributorSaleDTO> BindListRetailer_DSale(string distId)
            {
                List<DistributorSaleDTO> listritailer = new List<DistributorSaleDTO>()
                { new DistributorSaleDTO  { RetailerId = 0, RetailerName = "Select" } };

                foreach (var item in objdistributorsale.GetRetailerByDistributerId(distId))
                {
                    DistributorSaleDTO RT = new DistributorSaleDTO();
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

                foreach (var item in objdistributorsale.GetPaperByCityId(CityId))
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
            public string GetFinalAmount_By_SaleOrder(string SaleOrder)
            {
                return objdistributorsale.GetFinalAmount_By_SaleOrder(Session["SaleOrder"].ToString());
            }

            public string GetFinalAmount_By_SaleOrder_API(string SaleOrder)
            {
                return objdistributorsale.GetFinalAmount_By_SaleOrder_API(SaleOrder);
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
            //    DistributorSaleProductDTO model = new DistributorSaleProductDTO();
            //    return PartialView("~/Views/DistributorSaleProduct/Create.cshtml", model);
            //}
            // GET: DistributorSale
            public ActionResult Index()
            {
                return View();
            }
        }
    }