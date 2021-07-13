using System;
using System.ComponentModel.DataAnnotations;

namespace Web_S10203108.Models
{
    public class VoteDetails
    {
        private DateTime created_at;
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Justification { get; set; }
        public DateTime CreatedAt
        {
            get { return created_at; }
            set { created_at = value.ToLocalTime(); }
        }
    }
}