﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class CountryCodeModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CountryShortCode { get; set; }
        public int CountryValue { get; set; }
    }
}
