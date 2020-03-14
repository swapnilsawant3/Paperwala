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
    public class LineController : Controller
    {
        ICityMaster objCityMaster;
        IStateMaster objState;
        ILineMaster objLineMaster;

        public LineController()
        {
            objCityMaster = new CityMaster();
            objState = new StateMaster();
            objLineMaster = new LineMaster();
        }

        [HttpGet]
        public ActionResult Create()
        {
            LineDTO objLine = new LineDTO();

            objLine.ListState = BindListState();
            objLine.ListCity = BindListCity();
            ViewData["SelectedState"] = string.Empty;
            ViewData["SelectedCity"] = string.Empty;
            return View(objLine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LineDTO obj, FormCollection collection)
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

                    obj.LineId = 0;
                    obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.CreatedDate = cstTime;

                    TempData["MessageRegistration"] = "Data Saved Successfully!";
                    objLineMaster.InsertLine(obj);
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

            foreach (var item in objCityMaster.GetCitys())
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
            var citydata = objCityMaster.GetCityByStateId(StateId);
            return Json(citydata, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View(objLineMaster.GetLines());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = objLineMaster.GetLineByID(Convert.ToString(id));
            model.ListState = BindListState();
            model.ListCity = BindListCity();
            
            ViewData["SelectedState"] = model.StateId;
            ViewData["SelectedCity"] = model.CityId;
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LineDTO obj, FormCollection collection, string actionType)
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
                
                else
                {

                    obj.ModifiedBy = Convert.ToInt32(Session["UserID"]);
                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.ModifiedDate = cstTime;

                    objLineMaster.UpdateLine(obj);
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

        [HttpPost]
        public ActionResult LineNameExists(string LineName)
        {
            var result = objLineMaster.LineExists(LineName);
            return Json(!result, JsonRequestBehavior.AllowGet);
            // return Json(objIDepotMaster.DepotNameExists(DepotName));
        }

        [HttpGet]
        public ActionResult Delete(string ID)
        {
            objLineMaster.DeleteLine(ID);
            return RedirectToAction("Details");
        }

        // GET: Line
        public ActionResult Index()
        {
            return View();
        }
    }
}