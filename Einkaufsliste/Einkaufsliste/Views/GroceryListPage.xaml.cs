﻿using System;
using System.Collections.Generic;
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
    public partial class GroceryListPage : ContentPage
    {
        public GroceryListPage()
        {
            InitializeComponent();
            BindingContext = Resolver.Resolve<GroceryListViewModel>();
        }
    }
}