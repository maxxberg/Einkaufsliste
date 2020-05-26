using System;
using System.Collections.Generic;
using System.Text;

namespace Einkaufsliste.Models
{
    public class GroceryListEntry
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Done { get; set; }
    }
}
