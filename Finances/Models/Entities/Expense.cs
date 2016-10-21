using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finances.Models.Entities
{
    public class Expense
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public float value { get; set; }
        public int id_category { get; set; }
        public int id_establishment { get; set; }
        public int id_user { get; set; }
    }
}