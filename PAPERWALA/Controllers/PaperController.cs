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
    public class PaperController : Controller
    {
        IPaperMaster objPaperMaster;
        IStateMaster objState;
        ICityMaster objCity;
        public PaperController()
        {
            objPaperMaster = new PaperMaster();
            objState = new StateMaster();
            objCity = new CityMaster();
        }

        [HttpGet]
        public ActionResult Create()
        {
         
            PaperDTO  objP = new PaperDTO();

            objP.ListState = BindListState();
            objP.ListCity = BindListCity();
            ViewData["SelectedState"] = string.Empty;
            ViewData["SelectedCity"] = string.Empty;

            return View(objP);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaperDTO obj, FormCollection collection)
        {
            try
            {

                if (string.IsNullOrEmpty(Convert.ToString(obj.StateId)))
                {
                    ModelState.AddModelError("Error", "Please Select State");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.CityId)))
                {
                    ModelState.AddModelError("Error", "Please Select City");
                }

                else
                {

                    obj.PaperId = 0;
                    obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.CreatedDate = cstTime;

                    TempData["MessageRegistration"] = "Data Saved Successfully!";
                    objPaperMaster.InsertPaper(obj);
                    return RedirectToAction("Create");

                }
                obj.ListState = BindListState();
                obj.ListCity = BindListCity();
                ViewData["SelectedState"] = obj.StateId;
                ViewData["SelectedCity"] = obj.CityId;
                return View(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult PaperNameExists(string PaperName , int StateId)
        {
            var result = objPaperMaster.PaperNameExists(PaperName , StateId);
            return Json(!result, JsonRequestBehavior.AllowGet);
            // return Json(objIDepotMaster.DepotNameExists(DepotName));
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View(objPaperMaster.GetPapers());
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {

            var model = objPaperMaster.GetPaperByID(id);
            model.ListState = BindListState();
            model.ListCity = BindListCity();
            ViewData["SelectedState"] = model.StateId;
            ViewData["SelectedCity"] = model.CityId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaperDTO objpaper, FormCollection collection, string actionType)
        {
            if (actionType == "Update")
            {

                if (string.IsNullOrEmpty(Convert.ToString(objpaper.StateId)))
                {
                    ModelState.AddModelError("Error", "Please Select State");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(objpaper.CityId)))
                {
                    ModelState.AddModelError("Error", "Please Select City");
                }
                else
                {

                    objpaper.ModifiedBy = Convert.ToInt32(Session["UserID"]);
                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    objpaper.ModifiedDate = cstTime;

                    objPaperMaster.UpdatePaper(objpaper);
                    TempData["MessageUpdate"] = "Paper Details Updated  Successfully.";
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


        [HttpGet]
        public ActionResult Delete(string ID)
        {
            objPaperMaster.DeletePaper(ID);
            return RedirectToAction("Details");
        }

        public void Method(PaperDTO objpaper)
        {
            ModelState.Remove("ListState");
            List<StateDTO> liststate = new List<StateDTO>();
            liststate = new List<StateDTO>() { new StateDTO { StateId = 0, StateName = "Select" } };

            foreach (var item in objState.GetStates())
            {
                StateDTO sm = new StateDTO();
                sm.StateId = item.StateId;
                sm.StateName = item.StateName;
                liststate.Add(sm);
            }


            objpaper.ListState = liststate;
        }

        public void EDITMethod(PaperDTO objpaper)
        {
            ModelState.Remove("ListState");
            List<StateDTO> liststate = new List<StateDTO>();
            liststate = new List<StateDTO>() { new StateDTO { StateId = 0, StateName = "Select" } };

            foreach (var item in objState.GetStates())
            {
                StateDTO sm = new StateDTO();
                sm.StateId = item.StateId;
                sm.StateName = item.StateName;
                liststate.Add(sm);
            }


            objpaper.ListState = liststate;
            objpaper.StateId = objpaper.StateId;

        }


        [NonAction] // if Method is not Action method then use NonAction
        public List<StateDTO> BindListState()
        {
            List<StateDTO> liststate = new List<StateDTO>()
            { new StateDTO  { StateId = 0, StateName = "Select" } };

            foreach (var item in objState.GetStates())
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

            foreach (var item in objCity.GetCitys())
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
            var citydata = objCity.GetCityByStateId(StateId);
            return Json(citydata, JsonRequestBehavior.AllowGet);
        }



        // GET: Paper
        public ActionResult Index()
        {
            return View();
        }
    }
}