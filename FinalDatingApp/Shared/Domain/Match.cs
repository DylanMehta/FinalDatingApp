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
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }

        [ForeignKey("FirstUserId")]
        public virtual Person FirstUser { get; set; }
        [ForeignKey("SecondUserId")]
        public virtual Person SecondUser { get; set; }
    }
}
