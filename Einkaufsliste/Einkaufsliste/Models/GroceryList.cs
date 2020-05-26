using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Einkaufsliste.Models
{
    public class GroceryList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count => Entries.Count;
        [OneToMany]
        public List<GroceryListEntry> Entries { get; set; }
    }
}
