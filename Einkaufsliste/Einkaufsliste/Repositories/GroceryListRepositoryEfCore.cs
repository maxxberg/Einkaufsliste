using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Einkaufsliste.Models;
using Microsoft.EntityFrameworkCore;

namespace Einkaufsliste.Repositories
{
    public class GroceryListRepositoryEfCore : DbContext, IGroceryListRepository
    {
        private ObservableCollection<GroceryList> _localGroceryLists;

        private ObservableCollection<GroceryList> LocalGroceryLists =>
            _localGroceryLists ?? (_localGroceryLists = Lists.Local.ToObservableCollection());
        public DbSet<GroceryList> Lists { get; set; }
        public DbSet<GroceryListEntry> Entries { get; set; }

        private bool isInitialized = false;

        public GroceryListRepositoryEfCore() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var databasePath = Path.Combine(documentPath, "GroceryList.db");
            //var databasePath = "GroceryList.db";
            optionsBuilder.UseSqlite(
                $"Data Source = {databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GroceryList>()
                .HasMany(t => t.Entries)
                .WithOne(k => k.List);
        }

        private async Task Init()
        {
            if (isInitialized)
                return;
            //this.Database.EnsureDeletedAsync().Wait();
            await this.Database.MigrateAsync();
            isInitialized = true;
        }

        public event EventHandler<GroceryList> OnListAdded;
        public event EventHandler<GroceryList> OnListUpdated;
        public async Task<IList<GroceryList>> GetLists()
        {
            Init().Wait();
            Lists.Load();
            return LocalGroceryLists;
        }

        public async Task AddList(GroceryList list)
        {
            await Init();
            Lists.Add(list);
            OnListAdded?.Invoke(this, list);
            await SaveChangesAsync();
        }

        public async Task UpdateList(GroceryList list)
        {
            await Init();
            Lists.Update(list);
            OnListUpdated?.Invoke(this, list);
            await SaveChangesAsync();
        }

        public async Task AddOrUpdate(GroceryList list)
        {
            await Init();
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
