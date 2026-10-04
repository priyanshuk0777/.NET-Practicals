using System.Web.Mvc;
using P7.Models;

namespace P7.Controllers
{
    public class FeedbackController : Controller
    {
        public ActionResult Index()
        {
            return View(new Feedback());
        }

        [HttpPost]
        public ActionResult Submit(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please correct the errors below.";
                return View("Index", feedback);
            }

            ViewBag.SuccessMessage =
                "Thank you! Your feedback has been submitted successfully.";

            ViewBag.Name = feedback.Name;
            ViewBag.Category = feedback.Category;
            ViewBag.Rating = feedback.Rating;

            return View("Index", new Feedback());
        }
    }
}
```
