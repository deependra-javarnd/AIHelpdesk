using System.ComponentModel.DataAnnotations;

namespace AIHelpdesk.API.DTOs
{
    public class CreateTicketRequest
    {
        [Required]
        [MaxLength(20)]
        [MinLength (5)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        [MinLength(5)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Priority { get; set; } = string.Empty;
    }
}
