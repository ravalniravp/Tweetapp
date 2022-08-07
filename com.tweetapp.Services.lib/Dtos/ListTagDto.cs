using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.Services.lib.Dtos
{
    public class ListTagDto
    {
        
        public int Id { get; set; }
        public string TagMessage { get; set; }
        public string TagedUser { get; set; }
        
    }
}
