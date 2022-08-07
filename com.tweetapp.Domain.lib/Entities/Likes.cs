using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Domain.lib.Entities
{
    [Table("Likes")]
    public class Likes
    {
        [Key]
        public int Id { get; set; }
        public Tweet Tweet { get; set; }
        public User User { get; set; }
        public bool? IsLike { get; set; }

    }
}
