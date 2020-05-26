using System;
using Einkaufsliste.Services;
using Einkaufsliste.ViewModels;
using Einkaufsliste.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NewGroceryListViewModel = Einkaufsliste.ViewModels.NewGroceryListViewModel;

namespace Einkaufsliste
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainPage = new NavigationPage(Resolver.Resolve<MainPage>());
            var navService = DependencyService.Get<INavService>() as XamarinFormsNavService;

            navService.XamarinFormsNav = mainPage.Navigation;
            navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navService.RegisterViewMapping(typeof(GroceryListViewModel), typeof(GroceryListPage));
            navService.RegisterViewMapping(typeof(NewGroceryListEntryViewModel), typeof(NewGroceryListEntryPage));
            navService.RegisterViewMapping(typeof(NewGroceryListViewModel), typeof(NewGroceryListPage));

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
