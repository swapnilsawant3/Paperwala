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
    [MyExceptionHandler]
    public class PaperAllocationDistributorController : Controller
    {
        IStateMaster objStateMaster;
        ICityMaster objICityMaster;
        IPaperMaster objPaperMaster;
        IDistributorMaster objDistributorMaster;
        IPaperAllocationDistributorMaster objPaperAllocationDist;

        public PaperAllocationDistributorController()
        {
            objICityMaster = new CityMaster();
            objPaperMaster = new PaperMaster();
            objDistributorMaster = new DistributorMaster();
            objPaperAllocationDist = new PaperAllocationDistributorMaster();
            objStateMaster = new StateMaster();
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View(objPaperAllocationDist.GetPaperAllocationDistributor());
        }


        [HttpGet]
        public ActionResult Create(String CityId)
        {
            PaperAllocationDistributorDTO objPA = new PaperAllocationDistributorDTO();
            objPA.ListState = BindListState();
            objPA.ListDistributor = BindListDistributor();
            objPA.ListPaper = BindListPaper(CityId);
            objPA.ListCity = BindListCity();
            ViewData["SelectedState"] = string.Empty;
            ViewData["SelectedPaper"] = string.Empty;
            ViewData["SelectedCity"] = string.Empty;

            return View(objPA);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaperAllocationDistributorDTO obj, FormCollection frm, String CityId)
        {
            try
            {


                if (string.IsNullOrEmpty(Convert.ToString(obj.DistributorId)))
                {
                    ModelState.AddModelError("Error", "Please Select Distributor");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.PaperId)))
                {
                    ModelState.AddModelError("Error", "Please Select Paper Name");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.CityId)))
                {
                    ModelState.AddModelError("Error", "Please Select City");
                }

                else
                {

                    obj.PaperAllocateId = 0;
                    obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.CreatedDate = cstTime;

                    objPaperAllocationDist.InsertPaperAllocationDistributor(obj);
                    TempData["MessageRegistration"] = "Data Saved Successfully!";
                   
                    return RedirectToAction("Create");


                }
                obj.ListDistributor = BindListDistributor();
                obj.ListPaper = BindListPaper(CityId);
                obj.ListCity = BindListCity();
                ViewData["SelectedDistributor"] = obj.DistributorId;
                ViewData["SelectedPaper"] = obj.PaperId;
                ViewData["SelectedCity"] = obj.CityId;
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
            objPaperAllocationDist.DeletePaperAllocationDistributor(ID);
            return RedirectToAction("Details");
        }

        [NonAction] // if Method is not Action method then use NonAction
        public List<StateDTO> BindListState()
        {
            List<StateDTO> liststate = new List<StateDTO>()
            { new StateDTO  { StateId = 0, StateName = "Select" } };

            foreach (var item in objStateMaster.GetStates())
            {
                StateDTO st = new StateDTO();
                st.StateId = item.StateId;
                st.StateName = item.StateName;
                liststate.Add(st);
            }
            return liststate;

        }

        [NonAction]
        public List<DistributorDTO> BindListDistributor()
        {
            List<DistributorDTO>
            ListDistributor = new List<DistributorDTO>() { new DistributorDTO { DistributorId = 0, DistributorName = "Select" } };

            foreach (var item in objDistributorMaster.GetDistributors())
            {
                DistributorDTO dt = new DistributorDTO();
                dt.DistributorId = item.DistributorId;
                dt.DistributorName = item.DistributorName;
                ListDistributor.Add(dt);
            }

            return ListDistributor;
        }


        [NonAction]
        public List<CityDTO> BindListCity()
        {
            List<CityDTO>
            ListCity = new List<CityDTO>() { new CityDTO { CityId = 0, CityName = "Select" } };

            foreach (var item in objICityMaster.GetCitys())
            {
                CityDTO ct = new CityDTO();
                ct.CityId = item.CityId;
                ct.CityName = item.CityName;
                ListCity.Add(ct);
            }

            return ListCity;
        }


        [NonAction]
        public List<PaperDTO> BindListPaper(string CityId)
        {
            List<PaperDTO>
            ListPaper = new List<PaperDTO>() { new PaperDTO { PaperId = 0, PaperName = "Select" } };

            foreach (var item in objPaperMaster.GetPaperByCityIdBySession(CityId))
            {
                PaperDTO p = new PaperDTO();
                p.PaperId = item.PaperId;
                p.PaperName = item.PaperName;
                ListPaper.Add(p);
            }

            return ListPaper;
        }


        public JsonResult GetCity(string StateId)
        {
            var citydata = objICityMaster.GetCityByStateId(StateId);
            return Json(citydata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPaper(string CityId)
        {
            var plandata = objPaperMaster.GetPaperByCityIdBySession(CityId);
            return Json(plandata, JsonRequestBehavior.AllowGet);
        }

        // GET: PaperAllocationDistributor
        public ActionResult Index()
        {
            return View();
        }
    }
}