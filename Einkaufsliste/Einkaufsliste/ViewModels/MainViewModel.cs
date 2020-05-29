using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Einkaufsliste.Models;
using Einkaufsliste.Repositories;
using Einkaufsliste.Services;
using Xamarin.Forms;

namespace Einkaufsliste.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<GroceryList> _groceryLists;

        public ObservableCollection<GroceryList> GroceryLists
        {
            get => _groceryLists;
            set
            {
                _groceryLists = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(INavService navService)
            : base(navService)
        {
            GroceryLists = new ObservableCollection<GroceryList>();
        }

        public override void Init()
        {
            LoadEntries();
            Resolver.Resolve<IGroceryListRepository>().OnListAdded += Repository_OnListAdded;
            Resolver.Resolve<IGroceryListRepository>().OnListUpdated += Repository_OnListUpdated;
        }

        private void Repository_OnListUpdated(object sender, GroceryList e)
        {
            var list = GroceryLists.FirstOrDefault(t => t.Id == e.Id);
            if (list == null)
                return;
            list.Entries = e.Entries;
            list.Name = e.Name;
        }

        private void Repository_OnListAdded(object sender, GroceryList e)
        {
            GroceryLists.Add(e);
        }

        private async void LoadEntries()
        {
            //GroceryLists.Clear();
            GroceryLists = await Resolver.Resolve<IGroceryListRepository>().GetLists() as ObservableCollection<GroceryList>;
            //foreach (var list in await Resolver.Resolve<IGroceryListRepository>().GetLists())
            //{
            //    GroceryLists.Add(list);
            //}

            //GroceryLists.Add(new GroceryList()
            //{
            //    Name = "Rewe",
            //    Entries = new List<GroceryListEntry>
            //        {
            //            new GroceryListEntry()
            //            {
            //                Name = "Äpfel",
            //                Count = 5,
            //                Done = false,
            //            },
            //            new GroceryListEntry()
            //            {
            //                Name = "Birnen",
            //                Count = 10,
            //                Done = true,
            //            },
            //        }
            //}
            //);
        }

        public Command<GroceryList> ViewCommand => new Command<GroceryList>(async list => await NavService.NavigateTo<GroceryListViewModel, GroceryList>(list));

        public Command NewCommand => new Command(async () => await NavService.NavigateTo<NewGroceryListViewModel>());

    }
}
