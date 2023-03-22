using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class TwoStepModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string TwoFactorCode { get; set; }
    }
}
