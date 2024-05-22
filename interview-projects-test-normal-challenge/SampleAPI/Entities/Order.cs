using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime EntryDate { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }
        public bool IsInvoiced { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
