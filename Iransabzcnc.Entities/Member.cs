using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iransabzcnc.Entities
{
    public class Member
    {
        [Required]
        public int MemberId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string JobTitle { get; set; }

        public virtual MemberImage MemberImage { get; set; }
    }
}
