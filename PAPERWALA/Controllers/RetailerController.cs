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
    public class RetailerController : Controller
    {

        IStateMaster objstate;
        ICityMaster objcity;
        IRetailerMaster objretailer;
      
        public RetailerController()
        {
            objstate = new StateMaster();
            objcity = new CityMaster();
            objretailer = new RetailerMaster();
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            return View(objretailer.GetRetailers(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            RetailerDTO objRT = new RetailerDTO();

            objRT.ListState = BindListState();
            objRT.ListCity = BindListCity();
            ViewData["SelectedState"] = string.Empty;
            ViewData["SelectedCity"] = string.Empty;

            return View(objRT);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RetailerDTO obj, FormCollection frm)
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

                    obj.RetailerId= 0;
                    obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.CreatedDate = cstTime;

                    TempData["MessageRegistration"] = "Data Saved Successfully!";
                    objretailer.RetailerExists(obj.RetailerName,obj.StateId,obj.CityId);
                    objretailer.InsertRetailer(obj);
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

        [HttpGet]
        public ActionResult Edit(string id)
        {

            var model = objretailer.GetRetailerByID(id);
            model.ListState = BindListState();
            model.ListCity = BindListCity();
            ViewData["SelectedState"] = model.StateId;
            ViewData["SelectedCity"] = model.CityId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RetailerDTO obj, FormCollection collection, string actionType)
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

                    objretailer.UpdateRetailer(obj);
                    TempData["MessageUpdate"] = "Retailer Details Updated Successfully.";
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



        [HttpGet]
        public ActionResult Delete(string ID)
        {
            objretailer.DeleteRetailer(ID);
            return RedirectToAction("Details");
        }

        // GET: Retailer
        public ActionResult Index()
        {
            return View();
        }
    }
}