using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Einkaufsliste.Services;
using Einkaufsliste.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Einkaufsliste.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewGroceryListEntryPage : ContentPage
    {
        NewGroceryListEntryViewModel ViewModel => BindingContext as NewGroceryListEntryViewModel;
        public NewGroceryListEntryPage()
        {
            InitializeComponent();
            BindingContextChanged += NewGroceryListEntryPage_BindingContextChanged;
            BindingContext = new NewGroceryListEntryViewModel(DependencyService.Get<INavService>());
        }

        private void NewGroceryListEntryPage_BindingContextChanged(object sender, EventArgs e)
        {
            ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
        }

        private void ViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            var propHasErrors = (ViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;

            switch (e.PropertyName)
            {
                case nameof(ViewModel.Name):
                    name.LabelColor = propHasErrors ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.Count):
                    count.LabelColor = propHasErrors ? Color.Red : Color.Black;
                    break;
                default:
                    break;
            }
        }
    }
}