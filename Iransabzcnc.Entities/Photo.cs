using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Iransabzcnc.Entities
{
    [DataContract]
    public class Photo
    {
        [Required]
        [StringLength(255)]
        [DataMember]
        public string FileName { get; set; }
        [Required]
        [StringLength(255)]
        [DataMember]
        public string Title { get; set; }
    }
}
