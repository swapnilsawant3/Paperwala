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
    public class CityController : Controller
    {
        ICityMaster objICityMaster;
        IStateMaster objState;
        public CityController()
        {
            objICityMaster = new CityMaster();
            objState = new StateMaster();
        }

        [HttpGet]
        public ActionResult Create()
        {
            CityDTO objCM = new CityDTO();

            List<StateDTO> liststate = new List<StateDTO>();
            liststate = new List<StateDTO>()
            {
            new StateDTO { StateId = 0, StateName ="Select"}
            };

            foreach (var item in objState.GetStates())
            {
                StateDTO st = new StateDTO();
                st.StateId = item.StateId;
                st.StateName = item.StateName;

                liststate.Add(st);
            }
            objCM.ListState = liststate;
            return View(objCM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityDTO objCity, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (objCity.StateId == 0)
                    {
                        ModelState.AddModelError("StateMessage", "Please select State Name");
                        Method(objCity);
                        return View(objCity);
                    }
                    else
                    {
                        objCity.CityId = 0;
                        objCity.CreatedBy = Convert.ToInt32(Session["UserID"]);

                        DateTime timeUtc = System.DateTime.UtcNow;
                        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                        objCity.CreatedDate = cstTime;

                        TempData["MessageRegistration"] = "Data Saved Successfully!";
                        objICityMaster.InsertCity(objCity);
                        return RedirectToAction("Create");

                    }
                }
                else
                {
                    Method(objCity);
                    return View(objCity);
                }
            }
            catch
            {
                Method(objCity);
                return View(objCity);
            }
        }
        [HttpPost]
        public ActionResult CityNameExists(string CityName)
        {
            var result = objICityMaster.CityNameExists(CityName);
            return Json(!result, JsonRequestBehavior.AllowGet);
            // return Json(objIDepotMaster.DepotNameExists(DepotName));
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View(objICityMaster.GetCitys());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Model = objICityMaster.GetCityByID(Convert.ToString(id));
            EDITMethod(Model);

            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CityDTO objcity, FormCollection collection, string actionType)
        {
            if (actionType == "Update")
            {

                try
                {

                    if (ModelState.IsValid)
                    {
                        if (objcity.StateId == 0)
                        {
                            ModelState.AddModelError("StateMessage", "Please Select State Name");
                            Method(objcity);
                            return View(objcity);
                        }

                        else
                        {
                            objcity.ModifiedBy = Convert.ToInt32(Session["UserID"]);

                            DateTime timeUtc = System.DateTime.UtcNow;
                            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                            objcity.ModifiedDate = cstTime;

                            objICityMaster.UpdateCity(objcity);
                            TempData["MessageUpdate"] = "Depot Updated Successfully.";
                            return RedirectToAction("Details");

                        }
                    }
                    else
                    {
                        Method(objcity);
                        return View(objcity);
                    }
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }


        [HttpGet]
        public ActionResult Delete(string ID)
        {
            objICityMaster.DeleteCity(ID);
            return RedirectToAction("Details");
        }

        public void Method(CityDTO objcity)
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


            objcity.ListState = liststate;
        }

        public void EDITMethod(CityDTO objcity)
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


            objcity.ListState = liststate;
            objcity.StateId = objcity.StateId;

        }

        // GET: City
        public ActionResult Index()
        {
            return View();
        }
    }
}