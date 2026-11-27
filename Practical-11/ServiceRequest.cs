using System;
using System.ComponentModel.DataAnnotations;

namespace StudentCampusPortal.Models
{
    public class ServiceRequest
    {
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Service Type is required")]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Problem Description is required")]
        [Display(Name = "Problem Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Priority { get; set; } = "Medium";

        public string Status { get; set; } = "Pending";

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
