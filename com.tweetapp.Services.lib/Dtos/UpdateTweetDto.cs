using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Dtos
{
    public class UpdateTweetDto
    {
        public UpdateTweetDto()
        {
            Tags = new List<UpdateTagDto>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(144)]
        public string Message { get; set; }
        public ICollection<UpdateTagDto> Tags { get; set; }
    }
}
