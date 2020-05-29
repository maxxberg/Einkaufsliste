using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Einkaufsliste.Models;

namespace Einkaufsliste.Repositories
{
    public interface IGroceryListRepository
    {
        event EventHandler<GroceryList> OnListAdded;
        event EventHandler<GroceryList> OnListUpdated;

        Task<IList<GroceryList>> GetLists();
        Task AddList(GroceryList list);
        Task UpdateList(GroceryList list);
        Task AddOrUpdate(GroceryList list);
    }
}
