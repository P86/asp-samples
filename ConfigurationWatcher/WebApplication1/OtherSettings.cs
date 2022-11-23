using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class SecuritySettings
    {
        [Required]
        public string SecretKey { get; set; }
    }
}
