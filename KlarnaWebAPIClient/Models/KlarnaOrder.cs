using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KlarnaWebAPIClient.Models
{
    public class KlarnaOrder
    {
        [Display(Name = "timestamp")]
        public byte[] timestamp { get; set; }
        [Display(Name = "CompName")]
        public string CompName { get; set; }
        [Display(Name = "OrderNo_")]
        public string OrderNo_ { get; set; }
        [Display(Name = "KlarnaID")]
        public string KlarnaID { get; set; }
        [Display(Name = "Internal_Order_Id")]
        public string Internal_Order_Id { get; set; }
        [Display(Name = "Date Created")]
        public DateTime Date_Created { get; set; }
        [Display(Name = "Time Created")]
        public DateTime Time_Created { get; set; }
        [Display(Name = "Order Type")]
        public int OrderType { get; set; }
        [Display(Name = "Amount")]        
        public decimal Amount { get; set; }
        [Display(Name = "Vat Amount")]
        public decimal Vat_Amount { get; set; }
        [Display(Name = "Payment Status")]
        public int Status_Payment { get; set; }
        [Display(Name = "Payment Status Text")]
        public string Status_Payment_Text { get; set; }        
    }
}