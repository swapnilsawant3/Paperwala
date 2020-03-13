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
   // [MyExceptionHandler]
    public class PaperRateController : Controller
    {

        IStateMaster objstate;
        ICityMaster objcity;
        IPaperMaster objpaper;
        IPaperRateMaster objpaperrate;
        public PaperRateController()
        {
            objstate = new StateMaster();
            objcity = new CityMaster();
            objpaper = new PaperMaster();
            objpaperrate = new PaperRateMaster();
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View(objpaperrate.GetPaperRates());
        }

        [HttpGet]
        public ActionResult Create()
        {
            PaperRateDTO objPR = new PaperRateDTO();

            objPR.ListState = BindListState();
            objPR.ListPaper = BindListPaper();
            objPR.ListCity = BindListCity();
            ViewData["SelectedState"] = string.Empty;
            ViewData["SelectedPaper"] = string.Empty;
            ViewData["SelectedCity"] = string.Empty;

            return View(objPR);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaperRateDTO obj, FormCollection frm)
        {
            try
            {

                
                    if (string.IsNullOrEmpty(Convert.ToString(obj.StateId)))
                    {
                        ModelState.AddModelError("Error", "Please Select State");
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

                    obj.PaperRateId = 0;
                    obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.CreatedDate = cstTime;

                    TempData["MessageRegistration"] = "Data Saved Successfully!";
                    objpaperrate.InsertPaperRate(obj);
                    return RedirectToAction("Create");
                   

                }
                obj.ListState = BindListState();
                obj.ListPaper = BindListPaper();
                obj.ListCity = BindListCity();
                ViewData["SelectedState"] = obj.StateId;
                ViewData["SelectedPaper"] = obj.PaperId;
                ViewData["SelectedCity"] = obj.CityId;
                return View(obj);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {

            var model = objpaperrate.GetPaperRateByID(id);
            model.ListState = BindListState();
            model.ListCity = BindListCity();
            model.ListPaper = BindListPaper();
            ViewData["SelectedState"] = model.StateId;
            ViewData["SelectedCity"] = model.CityId;
            ViewData["SelectedPaper"] = model.PaperId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaperRateDTO obj, FormCollection collection, string actionType)
        {
            if (actionType == "Update")
            {

                if (string.IsNullOrEmpty(Convert.ToString(obj.StateId)))
                {
                    ModelState.AddModelError("Error", "Please Select State");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.CityId)))
                {
                    ModelState.AddModelError("Error", "Please Select City");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.PaperId)))
                {
                    ModelState.AddModelError("Error", "Please Select Paper");
                }
                else
                {

                    obj.ModifiedBy = Convert.ToInt32(Session["UserID"]);
                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.ModifiedDate = cstTime;

                    objpaperrate.UpdatePaperRate(obj);
                    TempData["MessageUpdate"] = "Paper Rate Details Updated  Successfully.";
                    return RedirectToAction("Details");


                    //  return RedirectToAction("Details");
                }

                return RedirectToAction("Details");
            }
            else
            {
                return RedirectToAction("Details");
            }
        }




        [NonAction] // if Method is not Action method then use NonAction
        public List<StateDTO> BindListState()
        {
            List<StateDTO> liststate = new List<StateDTO>()
            { new StateDTO  { StateId = 0, StateName = "Select" } };

            foreach (var item in objstate.GetStates())
            {
                StateDTO st = new StateDTO();
                st.StateId = item.StateId;
                st.StateName = item.StateName;
                liststate.Add(st);
            }
            return liststate;

        }


        [NonAction]
        public List<CityDTO> BindListCity()
        {
            List<CityDTO>
            ListCity = new List<CityDTO>() { new CityDTO { CityId = 0, CityName = "Select" } };

            foreach (var item in objcity.GetCitys())
            {
                CityDTO ct = new CityDTO();
                ct.CityId = item.CityId;
                ct.CityName = item.CityName;
                ListCity.Add(ct);
            }

            return ListCity;
        }

        public JsonResult GetCity(string StateId)
        {
            var citydata = objcity.GetCityByStateId(StateId);
            return Json(citydata, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public List<PaperDTO> BindListPaper()
        {
            List<PaperDTO>
            ListPaper = new List<PaperDTO>() { new PaperDTO { PaperId = 0, PaperName = "Select" } };

            foreach (var item in objpaper.GetPapers())
            {
                PaperDTO p = new PaperDTO();
                p.PaperId = item.PaperId;
                p.PaperName = item.PaperName;
                ListPaper.Add(p);
            }

            return ListPaper;
        }

        public JsonResult GetPaper(string CityId)
        {
            var plandata = objpaper.GetPaperByCityId(CityId);
            return Json(plandata, JsonRequestBehavior.AllowGet);
        }

        // GET: PaperRate
        public ActionResult Index()
        {
            return View();
        }
    }
}