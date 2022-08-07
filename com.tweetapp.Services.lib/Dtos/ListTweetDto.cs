using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Dtos
{
    public class ListTweetDto
    {
        public ListTweetDto()
        {
            ReplyDtos = new List<ListReplyDto>();
        }
        public int Id { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public ICollection<ListTagDto> Tags { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<ListReplyDto> ReplyDtos { get; set; }
        public int Likes { get; set; }
    }
}
