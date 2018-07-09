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
    public class SlidePhoto : Photo
    {
        [Required]
        [ForeignKey("Slide")]
        [DataMember]
        public int SlidePhotoID { get; set; }

        public virtual Slide Slide { get; set; }

    }
}
