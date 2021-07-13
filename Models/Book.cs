using System.ComponentModel.DataAnnotations;

namespace Web_S10203108.Models
{
    public class Book
    {
        [Display(Name = "Book ID")]
        public int Id { get; set; }
        [Display(Name = "ISBN")]
        public string Isbn { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Author")]
        public string Author { get; set; }
        [Display(Name = "Pages")]
        public int Pages { get; set; }
        [Display(Name = "Quantity Available")]
        public int Qty { get; set; }
        public string Justification { get; set; }
    }
}