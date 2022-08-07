using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Domain.lib.Entities
{
    [Table("Tweets")]
    public class Tweet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(144)]
        public string Message { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UpdatedById { get; set; }
        public User UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public ICollection<Tag> Tags { get; set; }
        public ICollection<TweetReply> TweetReplies { get; set; }
        public ICollection<Likes> Likes { get; set; }
    }
}
