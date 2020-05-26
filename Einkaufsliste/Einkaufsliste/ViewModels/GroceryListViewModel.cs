using System;
using System.Collections.Generic;
using System.Text;
using Einkaufsliste.Models;
using Einkaufsliste.Services;

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
        }
    }
}
