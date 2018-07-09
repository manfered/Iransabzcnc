using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Iransabzcnc.Entities
{
    [DataContract]
    public class MemberImage : Photo
    {
        [Required]
        [ForeignKey("Member")]
        [DataMember]
        public int MemberImageId { get; set; }

        public virtual Member Member { get; set; }
    }
}
