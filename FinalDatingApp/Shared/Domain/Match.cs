using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDatingApp.Shared.Domain
{
    public class Match : BaseDomainModel
    {
        public Boolean AcceptOrNot { get; set; }
        public int FirstPersonId { get; set; }
        public int SecondPersonId { get; set; }

        [ForeignKey("FirstPersonId")]
        public virtual Person FirstPerson { get; set; }
        [ForeignKey("SecondPersonId")]
        public virtual Person SecondPerson { get; set; }
    }
}
