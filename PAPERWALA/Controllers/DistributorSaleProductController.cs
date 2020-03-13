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
    public class DistributorSaleProductController : Controller
    {


        IDistributorSaleProduct objdistributorsaleproduct;
        IDistributorSale objdistributorsale;
        //ICityMaster objcity;
        // IRetailerMaster objretailer;
        IPaperRateMaster objpaperrate;
        public DistributorSaleProductController()
        {
            objdistributorsaleproduct = new DistributorSaleProduct();
            objdistributorsale = new DistributorSale();
            // objcity = new CityMaster();
            // objretailer = new RetailerMaster();
            objpaperrate = new PaperRateMaster();
        }
        [HttpGet]
        public ActionResult Details(string id)
        {
            return View(objdistributorsaleproduct.GetDistributorSaleProducts(id));
        }

        [HttpGet]

        public ActionResult Create(string tri)
        {
            DistributorSaleProductDTO objDT = new DistributorSaleProductDTO();
            objDT.ListPaperbyDistId = BindListPaperL(tri);
            //BindListRetailer(objDT.DistributorId.ToString());
           // objDT.ListPaperbyCityId = BindListPaper(tri);
            //ViewData["SelectedRetailer"] = string.Empty;
            ViewData["SelectedPaper"] = string.Empty;

            return View("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistributorSaleProductDTO obj, FormCollection frm, string tri)
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

                    TempData["MessageRegistration"] = "Data Saved Successfully!";
                    objdistributorsaleproduct.InsertDistributorSaleProduct(obj);
                    return PartialView("Create", objdistributorsale);


                }
               // obj.ListRetailer = BindListRetailer(tri);
                obj.ListPaperbyDistId = BindListPaperL(tri);
                //obj.ListCity = BindListCity();
               // ViewData["SelectedrRetailer"] = obj.RetailerId;
                ViewData["SelectedPaper"] = obj.PaperId;
                //ViewData["SelectedCity"] = obj.CityId;
                return View(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Delete(string ID)
        {
            objdistributorsaleproduct.DeleteDistributorSaleProduct(ID);
            return RedirectToAction("Details");
        }
        [NonAction]
        public IEnumerable<PaperDTO> BindListPaperL(string CityId)
        {
            List<PaperDTO>
            ListPaper = new List<PaperDTO>() { new PaperDTO { PaperId = 0, PaperName = "Select" } };

            foreach (var item in objdistributorsaleproduct.GetPaperByCityId(CityId))
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
        [ChildActionOnly]
        public ActionResult MyPartialView()
        {
            DistributorSaleProductDTO model = new DistributorSaleProductDTO();
            return PartialView("~/Views/DistributorSaleProduct/Create.cshtml",model);
        }
        // GET: DistributorSaleProduct
        public ActionResult Index()
        {
            return View();
        }
    }
}