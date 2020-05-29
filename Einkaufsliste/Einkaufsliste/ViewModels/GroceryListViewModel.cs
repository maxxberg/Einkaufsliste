using System;
using System.Collections.Generic;
using System.Text;
using Einkaufsliste.Models;
using Einkaufsliste.Repositories;
using Einkaufsliste.Services;
using Xamarin.Forms;

namespace Einkaufsliste.ViewModels
{
    public class GroceryListViewModel : BaseViewModel<GroceryList>
    {
        private GroceryList _groceryList;

        public GroceryList GroceryList
        {
            get => _groceryList;
            set
            {
                _groceryList = value;
                OnPropertyChanged();
            }
        }

        public GroceryListViewModel(INavService navService) : 
            base(navService)
        {
        }

        public override void Init(GroceryList parameter)
        {
            GroceryList = parameter;
            Resolver.Resolve<IGroceryListRepository>().OnListUpdated += Repository_OnListUpdated;
        }

        private void Repository_OnListUpdated(object sender, GroceryList e)
        {
            if (e.Id == GroceryList.Id)
                GroceryList = e;
        }

        public Command<GroceryList> NewCommand => new Command<GroceryList>(async list => await NavService.NavigateTo<NewGroceryListEntryViewModel, GroceryList>(list));
    }
}
