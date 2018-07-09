﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iransabzcnc.Entities
{
    public class Slide
    {
        [Required]
        public int SlideID { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual SlidePhoto SlidePhoto { get; set; }

    }
}
