using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Mst_Retailer")]
    public class RetailerDTO
    {
        [Key]
        public int RetailerId { get; set; }

        [Display(Name = " Retailer Name")]
        [Required(ErrorMessage = "Please Enter Retailer Name ")]
        public string RetailerName { get; set; }

        [Display(Name = " Retailer Address")]
        [Required(ErrorMessage = "Please Enter Retailer Address ")]
        public string RetailerAddress { get; set; }

        [Display(Name = " Pincode ")]
        [Required(ErrorMessage = "Please Enter Pincode")]
        public int Pincode { get; set; }

        [Display(Name = "Area")]
        public string Area { get; set; }

        [Display(Name = " Mobile Number ")]
        //[RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong Mobile Number")]
        [Required(ErrorMessage = "Please Enter Mobile  Number ")]
        public string MobileNo { get; set; }

        [Display(Name = " Alt. Mobile Number ")]
        public string AltMobileNo { get; set; }


        [Display(Name = "City Name")]
        [Validate_Retailer(ErrorMessage = "Select City")]
        public int? CityId { get; set; }
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Display(Name = "State Name")]
        [Validate_Retailer(ErrorMessage = "Select Sate")]
        public int? StateId { get; set; }
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        [Display(Name = " Email Id")]
        public string Email { get; set; }



        [Display(Name = " Birth Date")]

        public DateTime DateofBirth { get; set; }

        [Display(Name = " Remaining Amount")]
        public decimal RemainingAMT { get; set; }

        

        [Display(Name = " Distributor Name")]
        public int DistributorId { get; set; }

       

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }

        [NotMapped]
        
        public IEnumerable<StateDTO> ListState { get; set; }
        public IEnumerable<CityDTO> ListCity { get; set; }

    }
    public class Validate_Retailer : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                return false;
            else
                return true;
        }
    }
}