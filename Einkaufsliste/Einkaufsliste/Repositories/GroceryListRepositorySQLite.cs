using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Einkaufsliste.Models;
using Xamarin.Forms;
using SQLite;
using System.IO;

namespace Einkaufsliste.Repositories
{
    public class GroceryListRepositorySQLite : IGroceryListRepository
    {
        public event EventHandler<GroceryList> OnListAdded;
        public event EventHandler<GroceryList> OnListUpdated;

        private SQLiteAsyncConnection _connection;

        private async Task CreateConnection()
        {
            if (_connection != null)
            {
                return;
            }

            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "GroceryList.db");
            _connection = new SQLiteAsyncConnection(databasePath);
            await _connection.CreateTableAsync<GroceryList>();
            await _connection.CreateTableAsync<GroceryListEntry>();

            if (await _connection.Table<GroceryList>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new GroceryList()
                {
                    Name = "Welcome",
                    Entries = new List<GroceryListEntry>()
                    {
                        new GroceryListEntry()
                        {
                            Name = "Your first Entry",
                            Count = 1,
                            Done = true
                        }
                    }
                });
            }
        }

        public async Task<List<GroceryList>> GetLists()
        {
            await CreateConnection();
            return await _connection.Table<GroceryList>().ToListAsync();
        }

        public async Task AddList(GroceryList list)
        {
            await CreateConnection();
            await _connection.InsertAsync(list);
            OnListAdded?.Invoke(this, list);
        }

        public async Task UpdateList(GroceryList list)
        {
            await CreateConnection();
            await _connection.UpdateAsync(list);
            OnListUpdated?.Invoke(this, list);
        }

        public async Task AddOrUpdate(GroceryList list)
        {
            if (list.Id == 0)
            {
                await AddList(list);
            }
            else
            {
                await UpdateList(list);
            }
        }
    }
}
