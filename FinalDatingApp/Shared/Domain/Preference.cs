using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDatingApp.Shared.Domain
{
    public class Preference : BaseDomainModel
    {
        [Required]
        public String TargetGender { get; set; }
    }
}
