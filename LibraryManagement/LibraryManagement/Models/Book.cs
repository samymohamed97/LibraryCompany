using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string PublicationYear { get; set; }
        [Required]
        public string ISBN { get; set; }
        public virtual List<BorrowingRecord> BorrowingRecords { get; set; }
    }
}
