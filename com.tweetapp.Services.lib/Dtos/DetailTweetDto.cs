using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Dtos
{
    public class DetailTweetDto
    {
        
        public int Id { get; set; }
        public string Message { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual ICollection<DetailTagDto> Tags { get; set; }
    }
}
