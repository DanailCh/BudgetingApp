﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.BillType
{
    public class BillTypeViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = String.Empty;
    }
}
