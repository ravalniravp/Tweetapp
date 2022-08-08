using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Dtos
{
    public class CreatetweeterDto
    {
        public CreatetweeterDto()
        {
            Tags = new List<CreatetagDto>();
        }

        [Required]
        [StringLength(144)]
        public string Message { get; set; }
        public ICollection<CreatetagDto> Tags { get; set; }

    }
}
