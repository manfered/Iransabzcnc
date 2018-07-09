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
    public class ServiceIconPhoto : Photo
    {
        [DataMember]
        [Required]
        [ForeignKey("CompanyService")]
        public int ServiceIconPhotoId { get; set; }

        public virtual CompanyService CompanyService { get; set; }
    }
}
