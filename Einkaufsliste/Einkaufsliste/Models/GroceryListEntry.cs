using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Einkaufsliste.Models
{
    public class GroceryListEntry
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Done { get; set; }
        public GroceryList List { get; set; }
    }
}
