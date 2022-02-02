using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDatingApp.Shared.Domain
{
    public class Media : BaseDomainModel
    {
        [Required]
        public String Photo { get; set; }
        
        [Required]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
