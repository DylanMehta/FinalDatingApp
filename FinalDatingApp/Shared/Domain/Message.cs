using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDatingApp.Shared.Domain
{
    public class Message : BaseDomainModel
    {
        public String Text { get; set; }
        public int MatchId { get; set; }
        public virtual Match Match { get; set; } 
    }
}
