using System;
using System.Collections.Generic;
using System.Text;

namespace Einkaufsliste.Models
{
    public class GroceryList
    {
        public string Name { get; set; }
        public int Count => Entries.Count;
        public List<GroceryListEntry> Entries { get; set; }
    }
}
