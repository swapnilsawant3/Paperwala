using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Main_DistrbutorSaleProduct")]
    public class DisctributorSaleProductDTO
    {
        [Display(Name = "Paper Name")]
        [Validate_Main_DistributorSaleProduct(ErrorMessage = "Select Paper")]
        public int PaperId { get; set; }
        [Display(Name = "Paper Name")]
        public string PaperName { get; set; }
        [Display(Name = "Paper Rate")]

        [Required(ErrorMessage = "Please Enter Paper Rate")]
        public decimal PaperRate { get; set; }
        [Display(Name = "Paper Quantity")]
        [Required(ErrorMessage = "Please Enter Paper Quantity")]
        public int PaperQuantity { get; set; }
        [Display(Name = "Total Price ")]
        [Required(ErrorMessage = "Please Enter Total Price")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Pamphlet Quantity")]
        public int PamphletQuantity { get; set; }
        [Display(Name = "Pamphlet Rate")]
        public int PamphletRate { get; set; }
        [Display(Name = "Total Pamphlet Amount")]
        public decimal TotalPamphletAmount { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }
        [NotMapped]
        public IEnumerable<DistributorTransactionDTO> ListPaperbyCityId { get; set; }
        // public IEnumerable<CityDTO> ListCity { get; set; }
    }
    public class Validate_Main_DistributorSaleProduct : ValidationAttribute
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