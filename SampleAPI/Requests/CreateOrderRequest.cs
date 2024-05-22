using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Requests
{
    public class CreateOrderRequest
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Entry date is required")]
        public DateTime EntryDate { get; set; }

        [StringLength(100, ErrorMessage = "Description length cannot exceed 100 characters")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Name length cannot exceed 100 characters")]
        public string? Name { get; set; }

        public bool IsInvoiced { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
