using Microsoft.AspNetCore.Mvc;
using StudentCampusPortal.DAL;
using StudentCampusPortal.Models;
using System;

namespace StudentCampusPortal.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly ServiceRequestDAL _dal;

        public ServiceRequestController(ServiceRequestDAL dal)
        {
            _dal = dal;
        }

        public IActionResult Index()
        {
            var list = _dal.GetAll();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var req = _dal.GetById(id);
            if (req == null)
            {
                return NotFound();
            }
            return View(req);
        }

        public IActionResult Create()
        {
            return View(new ServiceRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceRequest request)
        {
            if (ModelState.IsValid)
            {
                _dal.Insert(request);
                TempData["SuccessMessage"] = "Service request submitted successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        public IActionResult UpdateStatus(int id)
        {
            var req = _dal.GetById(id);
            if (req == null)
            {
                return NotFound();
            }

            ViewBag.RequestId = id;
            ViewBag.CurrentStatus = req.Status;
            ViewBag.StudentName = req.StudentName;
            ViewBag.ServiceType = req.ServiceType;
            return View(req);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                _dal.UpdateStatus(id, status);
                TempData["SuccessMessage"] = $"Request #{id} status updated to '{status}'!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Please select a valid status.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _dal.Delete(id);
            TempData["SuccessMessage"] = $"Request #{id} deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
