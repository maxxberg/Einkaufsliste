using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Einkaufsliste.Models;
using Einkaufsliste.Services;
using Einkaufsliste.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Einkaufsliste.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewGroceryListPage : ContentPage
    {
        NewGroceryListViewModel ViewModel => BindingContext as NewGroceryListViewModel;
        public NewGroceryListPage()
        {
            InitializeComponent();
            BindingContextChanged += NewGroceryListPage_BindingContextChanged;
            BindingContext = new NewGroceryListViewModel(DependencyService.Get<INavService>());
        }

        private void NewGroceryListPage_BindingContextChanged(object sender, EventArgs e)
        {

            ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
        }

        private void ViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            var propHasErrors = (ViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;
            switch (e.PropertyName)
            {
                case nameof(ViewModel.Name): name.LabelColor = propHasErrors ? Color.Red : Color.Black; 
                    break;
                default: 
                    break;
            }
        }
        
    }
}