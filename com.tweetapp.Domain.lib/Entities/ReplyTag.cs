using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Domain.lib.Entities
{
    [Table("ReplyTag")]
    public class ReplyTag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TagMessage { get; set; }
        public int TweetId { get; set; }
        public Tweet Tweets { get; set; }
        public int TagedUserId { get; set; }
        public User TagedUser { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }
    }
}
