using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Dtos
{
    public class CreateReplyDto
    {
        [Required]
        [StringLength(144)]
        public string ReplyMessage { get; set; }
        public ICollection<CreateReplyTagDto> replyTagDtos{ get; set; }

    }
}
