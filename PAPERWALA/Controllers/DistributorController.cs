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
    
    public class DistributorController : Controller
    {

        IStateMaster objstate;
        ICityMaster objcity;
        IDistributorMaster objdistributor;

        public DistributorController()
        {
            objstate = new StateMaster();
            objcity = new CityMaster();
            objdistributor = new DistributorMaster();
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View(objdistributor.GetDistributors());
        }
        [HttpGet]
        public ActionResult Create()
        {
            DistributorDTO objDT = new DistributorDTO();

            objDT.ListState = BindListState();
            objDT.ListCity = BindListCity();
            ViewData["SelectedState"] = string.Empty;
            ViewData["SelectedCity"] = string.Empty;

            return View(objDT);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistributorDTO obj, FormCollection frm)
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

                    obj.DistributorId = 0;
                    obj.CreatedBy = Convert.ToInt32(Session["UserID"]);

                    DateTime timeUtc = System.DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    obj.CreatedDate = cstTime;

                    
                    objdistributor.InsertDistributor(obj);
                    TempData["MessageRegistration"] = "Data Saved Successfully!";
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

            var model = objdistributor.GetDistributorByID(id);
            model.ListState = BindListState();
            model.ListCity = BindListCity();
            ViewData["SelectedState"] = model.StateId;
            ViewData["SelectedCity"] = model.CityId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DistributorDTO obj, FormCollection collection, string actionType)
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

                    objdistributor.UpdateDistributor(obj);
                    TempData["MessageUpdate"] = "Distributor Details Updated Successfully.";
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
            objdistributor.DeleteDistributor(ID);
            return RedirectToAction("Details");
        }

        [HttpGet]
        public ActionResult DistributorChangePassword()
        {

            DistributorDTO objDT = new DistributorDTO();
            return View(objDT);
        }

        [HttpPost]
        public ActionResult DistributorChangePassword(DistributorDTO obj, FormCollection frm)
        {
           

                objdistributor.ChangePassword(obj);
                TempData["MessageRegistration"] = "Data Saved Successfully!";
           
            
            return RedirectToAction("DistributorChangePassword");
        }

        public JsonResult GetPassword(string DistributorID)
        {
            var DPassword = objdistributor.GetPassword(DistributorID);
            return Json(DPassword, JsonRequestBehavior.AllowGet);
        }
        // GET: Distributor
        public ActionResult Index()
        {
            return View();
        }
    }
}