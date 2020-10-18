using System.ComponentModel.DataAnnotations;

namespace JamboPay.ViewModels
{
    public class ServiceViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public double CommissionPercentage { get; set; }
    }
}