using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Dtos
{
    public class ListReplyDto
    {
        public int Id { get; set; }
        public string ReplyMessage { get; set; }
        public string ReplyBy { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
