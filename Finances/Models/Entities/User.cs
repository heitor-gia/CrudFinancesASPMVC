﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finances.Models.Entities
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
    }
}