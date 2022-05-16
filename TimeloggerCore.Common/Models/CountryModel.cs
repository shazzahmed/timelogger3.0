﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class CountryModel
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
