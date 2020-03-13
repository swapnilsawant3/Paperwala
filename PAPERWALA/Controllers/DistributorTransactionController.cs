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
    public class DistributorTransactionController : Controller
    {


       IDistributorTransactions objdistributortransactions;
        ICityMaster objcity;
        IRetailerMaster objretailer;
        IPaperRateMaster objpaperrate;
        
        public DistributorTransactionController()
        {
            objdistributortransactions = new DistributorTransactions();
            objcity = new CityMaster();
            objretailer = new RetailerMaster();
            objpaperrate = new PaperRateMaster();
        }
        [HttpGet]
        public ActionResult Details(string id)
        {
            return View(objdistributortransactions.GetDistributorTransactions(id));
        }

        [HttpGet]
        public ActionResult Create(string tri)
        {
            DistributorTransactionDTO objDT = new DistributorTransactionDTO();
            objDT.ListRetailer = BindListRetailer(tri);
                //BindListRetailer(objDT.DistributorId.ToString());
           objDT.ListPaperbyCityId= BindListPaper(tri);
            ViewData["SelectedRetailer"] = string.Empty;
            ViewData["SelectedPaper"] = string.Empty;

            return View(objDT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistributorTransactionDTO obj, FormCollection frm, string tri)
        {
            try
            {


                if (string.IsNullOrEmpty(Convert.ToString(obj.RetailerId)))
                {
                    ModelState.AddModelError("Error", "Please Select State");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.PaperId)))
                {
                    ModelState.AddModelError("Error", "Please Select Paper Name");
                }
               

                else
                {

                    obj.TransDistributorId = 0;
                    obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.CreatedDate = cstTime;

                    TempData["MessageRegistration"] = "Data Saved Successfully!";
                    objdistributortransactions.InsertDistributorTransaction(obj);
                    return RedirectToAction("Create");


                }
                obj.ListRetailer = BindListRetailer(tri);
                obj.ListPaperbyCityId = BindListPaper(tri);
                //obj.ListCity = BindListCity();
                ViewData["SelectedrRetailer"] = obj.RetailerId;
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
            objdistributortransactions.DeleteDistributorTransaction(ID);
            return RedirectToAction("Details");
        }

        public List<DistributorTransactionDTO>  BindListRetailer(string distId)
        {
            List<DistributorTransactionDTO> listritailer = new List<DistributorTransactionDTO>()
            { new DistributorTransactionDTO  { RetailerId = 0, RetailerName = "Select" } };
            
            foreach (var item in objdistributortransactions.GetRetailerByDistributerId(distId))
            {
                DistributorTransactionDTO RT = new DistributorTransactionDTO();
                RT.RetailerId = item.RetailerId;
                RT.RetailerName = item.RetailerName;
                listritailer.Add(RT);
            }
           
            return listritailer;
            
        }

       [NonAction]
        public List<DistributorTransactionDTO> BindListPaper(string CityId)
        {
            List<DistributorTransactionDTO>
            ListPaper = new List<DistributorTransactionDTO>() { new DistributorTransactionDTO { PaperId = 0, PaperName = "Select" } };

            foreach (var item in objdistributortransactions.GetPaperByCityId(CityId))
            {
                DistributorTransactionDTO P = new DistributorTransactionDTO();
                P.PaperId = item.PaperId;
                P.PaperName = item.PaperName;
                ListPaper.Add(P);
            }

            return ListPaper;
        }

        public JsonResult GetPaperRate(string PaperId,string Tdate)
        {
            var PaperRatedata = objpaperrate.GetPaperRateByPaperId(PaperId ,Tdate);
            return Json(PaperRatedata, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRetailerPrevBal(string RetailerId)
        {
            var RetailerPrevBaldata = objretailer.GetPrevBalalnceByRetailerId(RetailerId);
            return Json(RetailerPrevBaldata, JsonRequestBehavior.AllowGet);
        }
        // GET: DistributorTransaction
        public ActionResult Index()
        {
            return View();
        }
    }
}