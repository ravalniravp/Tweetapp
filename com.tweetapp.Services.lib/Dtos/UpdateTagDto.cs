using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Dtos
{
    public class UpdateTagDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TagMessage { get; set; }
        public int TagPerosnId { get; set; }
    }
}
