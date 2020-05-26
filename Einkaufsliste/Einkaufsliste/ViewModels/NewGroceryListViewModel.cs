using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Einkaufsliste.Models;
using Einkaufsliste.Services;
using Xamarin.Forms;

namespace Einkaufsliste.ViewModels
{
    public class NewGroceryListViewModel : BaseValidationViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Validate(() => !string.IsNullOrWhiteSpace(_name),
                    "List name must be provided");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public NewGroceryListViewModel(INavService navService)
            : base(navService)
        {

        }

        public override void Init()
        {

        }


        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));

        private async Task Save()
        {
            GroceryList list = new GroceryList()
            {
                Name = Name,
                Entries = new List<GroceryListEntry>(),
            };
            
            await NavService.GoBack();
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(Name) && !HasErrors;
    }
}
