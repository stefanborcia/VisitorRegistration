﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorDataAccess.Entities
{
    public class Employee : SoftDelete
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CompanyId { get; set; }

    }
}
