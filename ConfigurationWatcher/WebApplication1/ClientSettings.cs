using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class ClientSettings
    {
        [Required]
        public string BaseUrl { get; set; }
    }
}
