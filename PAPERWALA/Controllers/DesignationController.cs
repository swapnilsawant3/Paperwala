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
    public class DesignationController : Controller
    {
        IDesignationMaster objIDesignationMaster;
        public DesignationController()
        {
            objIDesignationMaster = new DesignationMaster();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new DesignationDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DesignationDTO objDesignationMasterDTO)
        {
            if (ModelState.IsValid)
            {
                objDesignationMasterDTO.CreatedBy = Convert.ToInt32(Session["UserID"]);
                DateTime timeUtc = System.DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                objDesignationMasterDTO.CreatedDate = cstTime;

                objIDesignationMaster.InsertDesignation(objDesignationMasterDTO);
                TempData["MessageRegistration"] = "Data Saved Successfully!";
                return RedirectToAction("Create");
            }
            else
            {
                // TempData["MessageRegistration"] = "Please enter Division Name!";
                ModelState.AddModelError("", "Please enter Designation Name ");
            }

            // ModelState.Remove("DivisionName");

            return View(objDesignationMasterDTO);
        }

        [HttpPost]
        public ActionResult DesignationNameExists(string DesignationName)
        {
            var result = objIDesignationMaster.DesignationNameExists(DesignationName);
            return Json(!result, JsonRequestBehavior.AllowGet);
            // return Json(objIDivisionMaster.DivisionNameExists(DivisionName));
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View(objIDesignationMaster.GetDesignations());
        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            var Model = objIDesignationMaster.GetDesignationByID(ID);
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DesignationDTO objDesignationMasterDTO)
        {
            objDesignationMasterDTO.ModifiedBy = Convert.ToInt32(Session["UserID"]);

            DateTime timeUtc = System.DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            objDesignationMasterDTO.ModifiedDate = cstTime;

            objIDesignationMaster.UpdateDesignation(objDesignationMasterDTO);
            TempData["MessageUpdate"] = "Designation Updated Successfully.";
            return RedirectToAction("Details");
        }

        [HttpGet]
        public ActionResult Delete(string ID)
        {
            objIDesignationMaster.DeleteDesignation(ID);
            return RedirectToAction("Details");
        }

    }
}