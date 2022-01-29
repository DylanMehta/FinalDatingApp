using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDatingApp.Shared.Domain
{
    public class Media : BaseDomainModel
    {
        public String Photo { get; set; }
        public int UserId { get; set; }
        public virtual Person User { get; set; }
    }
}
