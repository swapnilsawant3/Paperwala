using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Mst_PaperAllocateDistributor")]
    public class PaperAllocationDistributorDTO
    {
        [Key]
        public int PaperAllocateId { get; set; }

        [Display(Name = "Paper Name")]
        [Validate_PaperAllocation(ErrorMessage = "Select Paper")]
        public int? PaperId { get; set; }
        [Display(Name = "Paper Name")]
        public string PaperName { get; set; }

        [Display(Name = "Distributor Name")]
        [Validate_PaperAllocation(ErrorMessage = "Select State")]
        public int? DistributorId { get; set; }
        [Display(Name = "Distributor Name")]
        public string DistributorName { get; set; }

        [Display(Name = "State Name")]
        [Validate_PaperAllocation(ErrorMessage = "Select State")]
        public int? StateId { get; set; }
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        [Display(Name = "City Name")]
        public int CityId { get; set; }

        [Display(Name = "City Name")]
        [Required(ErrorMessage = "Select City Name")]
        public string CityName { get; set; }

        [NotMapped]
        public IEnumerable<StateDTO> ListState { get; set; }
        public IEnumerable<CityDTO> ListCity { get; set; }
        public IEnumerable<DistributorDTO> ListDistributor { get; set; }
        public IEnumerable<PaperDTO> ListPaper { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }
    }
    public class Validate_PaperAllocation : ValidationAttribute
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
