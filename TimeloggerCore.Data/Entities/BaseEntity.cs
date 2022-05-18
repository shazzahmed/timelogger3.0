﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Data.Entities
{
    class BaseEntity
    {
        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
