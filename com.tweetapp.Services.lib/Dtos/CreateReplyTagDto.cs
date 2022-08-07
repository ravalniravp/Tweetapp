using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Dtos
{
    public class CreateReplyTagDto
    {
        [Required]
        [StringLength(50)]
        public string TagMessage { get; set; }
        public int TagPersonId { get; set; }
    }
}
