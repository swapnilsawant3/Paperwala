using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    public class DistributorReturnDTO
    {
        [Key]

        public int DistributorReturnId { get; set; }
        [Display(Name = "Return Order")]
        [Required(ErrorMessage = "Please Enter Return Order")]
        public string ReturnOrder { get; set; }
        [Display(Name = "Retailer Name")]
        [Validate_Main_DistributorSale(ErrorMessage = "Select Paper")]
        public int RetailerId { get; set; }
        [Display(Name = "Retailer Name")]
        public string RetailerName { get; set; }

        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please Enter Transaction Date")]
        public DateTime TransDate { get; set; }

        [Display(Name = "Paid Amount")]
        [Required(ErrorMessage = "Please Enter  Paid Amount")]
        public decimal PaidAmount { get; set; }

        [Display(Name = "Balance Amount")]
        [Required(ErrorMessage = "Please Enter  Balance Amount")]
        public decimal BalanceAmount { get; set; }

        [Display(Name = "Prv. Balance Amount")]
        [Required(ErrorMessage = "Please Enter Prv. Balance Amount")]
        public decimal PrvBalanceAmount { get; set; }
        //[Display(Name = "Final Balance Amount")]
        //[Required(ErrorMessage = "Please Enter Final Balance Amount")]
        //public decimal FinalBalaceAmount { get; set; }


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


        public int DistributorReturnProductId { get; set; }


        [Display(Name = "Paper Name")]
        [Validate_Main_DistributorReturn(ErrorMessage = "Select Paper")]
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
        [Display(Name = "Total Final Amount")]
        public decimal TotalFinalAmount { get; set; }

        [Display(Name = "Transaction Date")]
        public DateTime TransDateP { get; set; }


        [NotMapped]
        public List<DistributorReturnDTO> ListRetailer { get; set; }




    }
    public class DistributorReturnProductDTO
    {
        [Key]

        public int DistributorReturnProductId { get; set; }

        public string ReturnOrder { get; set; }
        [Display(Name = "Paper Name")]
        [Validate_Main_DistributorSale(ErrorMessage = "Select Paper")]
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
        [Display(Name = "PMP Qnt")]
        public int PamphletQuantity { get; set; }
        [Display(Name = "PMP Rat")]
        public int PamphletRate { get; set; }
        [Display(Name = "Total PMP AMT")]
        public decimal TotalPamphletAmount { get; set; }
        [Display(Name = "Total Final Amount")]
        public decimal TotalFinalAmount { get; set; }
        public int DistributorId { get; set; }

        [Display(Name = "Transaction Date")]
        public DateTime TransDateP { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }

        [NotMapped]

        public IEnumerable<PaperDTO> ListPaperbyDistId { get; set; }
        // public IEnumerable<CityDTO> ListCity { get; set; }
    }
    public class Validate_Main_DistributorReturn : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToInt32(value) == 0)
                return false;
            else
                return true;
        }
    }
}