using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Models
{
    public class TalkModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 20)]
        public string Abstract { get; set; }
        [Range(100, 300)]
        public int Level { get; set; }

        public SpeakerModel Speaker { get; set; }
    }
}
