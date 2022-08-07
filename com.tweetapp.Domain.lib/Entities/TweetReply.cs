using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Domain.lib.Entities
{
    [Table("TweetReply")]
    public class TweetReply
    {
        public TweetReply()
        {
            ReplyTags = new List<ReplyTag>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(144)]
        public string ReplyMessage { get; set; }
        public int TweetId { get; set; }
        public virtual Tweet Tweet { get; set; }
        public int RepliedById { get; set; }
        public User RepliedBy { get; set; }
        public DateTime CreatedTime { get; set; }= DateTime.Now;
        public ICollection<ReplyTag> ReplyTags { get; set; }
    }
}
