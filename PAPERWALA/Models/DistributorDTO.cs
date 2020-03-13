using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Mst_Distributor")]
    public class DistributorDTO
    {
        [Key]
        public int DistributorId { get; set; }

        [Required(ErrorMessage = "Please enter Distributor Name")]
        [Display(Name = "Distributor Name")]
        public string DistributorName { get; set; }
        [Required(ErrorMessage = "Please enter Contact Person Name")]
        [Display(Name = "Person Name")]
        public string ContactPersonName { get; set; }
        [Required(ErrorMessage = "Select Designation")]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Select Designation")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter Mobile Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong Mobile Number")]
        [Display(Name = "Mobile Number")]
        public string MobileNo { get; set; }

        [Display(Name = "Alteernet Number")]
        public string AltMobileNo { get; set; }

        [Required(ErrorMessage = "Select State")]
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }
        [Required(ErrorMessage = "Select City")]
        [Display(Name = "City")]
        public string CityId { get; set; }
        [Display(Name = "City")]
        public string CityName { get; set; }
        [Display(Name = "Area")]
        public string Area { get; set; }
        [Display(Name = "Pincode")]
        public int Pincode { get; set; }
        [Display(Name = "Email Id")]
        public string Email { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime? DateofBirth { get; set; }
        [Required(ErrorMessage = "Enter User Name")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Remaining Amount")]
        public decimal RemainingAMT { get; set; }


        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string DeleteStatus { get; set; }

        [Required(ErrorMessage = "Enter Old Password")]
        [Display(Name = " Old Password")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Enter New Password")]
        [Display(Name = " New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "New Password and Confirm Password must match.")]
        [Display(Name = " Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string ErrorMessage { get; set; }
        public string ConfirmMessage { get; set; }
        [NotMapped]
        public IEnumerable<DesignationDTO> ListDesignation { get; set; }

        [NotMapped]
        public IEnumerable<StateDTO> ListState { get; set; }

        [NotMapped]
        public IEnumerable<CityDTO> ListCity { get; set; }
    }
}