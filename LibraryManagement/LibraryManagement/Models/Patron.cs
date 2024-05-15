using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Patron
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactInformation { get; set; }
        public virtual List<BorrowingRecord> BorrowingRecords { get; set; }
    }
}
