using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Iransabzcnc.Entities
{
    public class CompanyService
    {
        [Required]
        public int CompanyServiceID { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string BriefDescription { get; set; }
        [Required]
        [StringLength(5000)]
        [AllowHtml]
        public string FullDescription { get; set; }

        public virtual ServiceIconPhoto ServiceIconPhoto { get; set; }
        public virtual ICollection<ServicePhoto> ServicePhotos { get; set; }
    }
}
