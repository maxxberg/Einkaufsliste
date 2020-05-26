using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Einkaufsliste.Models;
using Einkaufsliste.Services;
using Einkaufsliste.ViewModels;
using Einkaufsliste.Views;
using Xamarin.Forms;

namespace Einkaufsliste
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel(DependencyService.Get<INavService>());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel?.Init();
        }
    }
}
