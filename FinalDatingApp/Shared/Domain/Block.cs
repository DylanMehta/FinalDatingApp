using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDatingApp.Shared.Domain
{
    public class Block : BaseDomainModel
    {
        [Required]
        public int BlockerId { get; set; }

        [Required]
        public int BlockedId { get; set; }

        [ForeignKey("BlockerId")]
        public virtual Person BlockerPerson { get; set; }
        [ForeignKey("BlockedId")]
        public virtual Person BlockedPerson { get; set; }
    }
}