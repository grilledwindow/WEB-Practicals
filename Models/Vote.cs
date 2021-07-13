using System.ComponentModel.DataAnnotations;

namespace Web_S10203108.Models
{
    public class Vote
    {
        [Display(Name = "Book ID")]
        public int BookId { get; set; }
        public string Justification { get; set; }
    }
}