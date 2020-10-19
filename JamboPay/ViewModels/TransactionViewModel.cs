using System.ComponentModel.DataAnnotations;

namespace JamboPay.ViewModels
{
    public class TransactionViewModel
    {
        [Required]
        public string ServiceId { get; set; }
        [Required]
        public double Cost { get; set; }
    }
}