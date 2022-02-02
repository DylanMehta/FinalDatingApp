using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDatingApp.Shared.Domain
{
    public class Message : BaseDomainModel
    {
        [Required]
        public String Text { get; set; }
        
        [Required]
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }
    }
}
