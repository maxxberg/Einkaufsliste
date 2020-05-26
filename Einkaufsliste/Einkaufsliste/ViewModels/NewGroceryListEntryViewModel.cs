using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Einkaufsliste.Models;
using Einkaufsliste.Services;
using Xamarin.Forms;

namespace Einkaufsliste.ViewModels
{
    public class NewGroceryListEntryViewModel : BaseValidationViewModel
    {
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

        public override void Init()
        {

        }

        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(Save, CanSave));

        void Save()
        {
            GroceryListEntry newEntry = new GroceryListEntry()
            {
                Name = Name,
                Count = Count,
                Done = Done,
            };
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(Name) && !HasErrors;
    }
}
