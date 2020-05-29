using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Einkaufsliste.Models;
using Einkaufsliste.Repositories;
using Einkaufsliste.Services;
using Xamarin.Forms;

namespace Einkaufsliste.ViewModels
{
    public class NewGroceryListEntryViewModel : BaseValidationViewModel<GroceryList>
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

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Validate(() => !string.IsNullOrWhiteSpace(_name),
                    "Name must be provided");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private int _count;

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                Validate(() => _count > 0,
                    "Must be a number higher than 0");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private bool _done;

        public bool Done
        {
            get => _done;
            set
            {
                _done = value;
                OnPropertyChanged();
            }
        }

        public NewGroceryListEntryViewModel(INavService navService)
            : base(navService)
        {
            Done = false;
            Count = 1;
        }

        public override void Init(GroceryList parameter)
        {
            GroceryList = parameter;
        }

        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));

        private async Task Save()
        {
            GroceryListEntry newEntry = new GroceryListEntry()
            {
                Name = Name,
                Count = Count,
                Done = Done,
            };
            if (GroceryList.Entries == null)
                GroceryList.Entries = new List<GroceryListEntry>();
            GroceryList.Entries.Add(newEntry);
            await Resolver.Resolve<IGroceryListRepository>().AddOrUpdate(GroceryList);
            await NavService.GoBack();
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(Name) && !HasErrors;
    }
}
