using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace PAPERWALA.Models
{
    [Table("Trans_DistrbutorTransaction")]
    public class DistributorTransactionDTO
    {
        [Key]

        public int TransDistributorId { get; set; }
        [Display(Name = "Retailer Name")]
        [Validate_PaperRate(ErrorMessage = "Select Paper")]
        public int RetailerId { get; set; }
        [Display(Name = "Retailer Name")]
        public string RetailerName { get; set; }
        [Display(Name = "Paper Name")]
        [Validate_PaperRate(ErrorMessage = "Select Paper")]
        public int PaperId { get; set; }
        [Display(Name = "Paper Name")]
        public string PaperName { get; set; }
        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please Enter Transaction Date")]
        public DateTime TransDate { get; set; }
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
        [Display(Name = "Total Final Amount")]
        [Required(ErrorMessage = "Please Enter Total Final Amount")]
        public decimal TotalFinalAmount { get; set; }
        [Display(Name = "Total Paid Amount")]
        [Required(ErrorMessage = "Please Enter Total Paid Amount")]
        public decimal PaidAmount { get; set; }
        [Display(Name = "Balance  Amount")]
        [Required(ErrorMessage = "Please Enter Balance Amount")]
        public decimal BalanceAmount { get; set; }
        [Display(Name = "Previous Balance Amount")]
        [Required(ErrorMessage = "Please Enter Previous Balance Amount")]
        public decimal PrvBalanceAmount { get; set; }
        [Display(Name = "Final Balance Amount")]
        [Required(ErrorMessage = "Please Enter Final Balance Amount")]
        public decimal FinalBalaceAmount { get; set; }

        public int DistributorId { get; set; }
        public string CityId { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }

        [NotMapped]
        public List<DistributorTransactionDTO> ListRetailer { get; set; }
        public IEnumerable<DistributorTransactionDTO> ListPaperbyCityId { get; set; }
       // public IEnumerable<CityDTO> ListCity { get; set; }

    }

    public class Validate_TransDistributor : ValidationAttribute
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