﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimaArrendamentos.Data.Entities
{
    public class LeaseMessage : IEntity
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public int LeaseId { get; set; }
    }
}
