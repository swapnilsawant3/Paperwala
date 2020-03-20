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
    public class StateController : Controller
    { 
        IStateMaster objIStateMaster;
        public StateController()
        {
            objIStateMaster = new StateMaster();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new StateDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StateDTO objStateMasterDTO)
        {
            if (ModelState.IsValid)
            {
                objStateMasterDTO.CreatedBy = Convert.ToInt32(Session["UserID"]);
                DateTime timeUtc = System.DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                objStateMasterDTO.CreatedDate = cstTime;

                objIStateMaster.InsertState(objStateMasterDTO);
                TempData["MessageRegistration"] = "Data Saved Successfully!";
                return RedirectToAction("Create");
            }
            else
            {
                // TempData["MessageRegistration"] = "Please enter Division Name!";
                ModelState.AddModelError("", "Please enter State Name ");
            }

            // ModelState.Remove("DivisionName");

            return View(objStateMasterDTO);
        }

        [HttpPost]
        public ActionResult StateNameExists(string StateName)
        {
            var result = objIStateMaster.StateNameExists(StateName);
            return Json(!result, JsonRequestBehavior.AllowGet);
            // return Json(objIDivisionMaster.DivisionNameExists(DivisionName));
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View(objIStateMaster.GetStates());
        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            var Model = objIStateMaster.GetStateByID(ID);
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StateDTO objStateMasterDTO)
        {
            objStateMasterDTO.ModifiedBy = Convert.ToInt32(Session["UserID"]);

            DateTime timeUtc = System.DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            objStateMasterDTO.ModifiedDate = cstTime;

            objIStateMaster.UpdateState(objStateMasterDTO);
            TempData["MessageUpdate"] = "State Updated Successfully.";
            return RedirectToAction("Details");
        }

        [HttpGet]
        public ActionResult Delete(string ID)
        {
            objIStateMaster.DeleteState(ID);
            return RedirectToAction("Details");
        }

       
    }

}