using System;
using System.ComponentModel.DataAnnotations;

namespace BargainBarterV2.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        [MinLength(1), MaxLength(500)]
        public string CommentText { get; set; }
        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}