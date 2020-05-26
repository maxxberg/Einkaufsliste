using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Einkaufsliste.ViewModels;

namespace Einkaufsliste.Services
{
    public interface INavService
    {
        bool CanGoBack { get; }
        Task GoBack();

        Task NavigateTo<TVM>()
            where TVM : BaseViewModel;

        Task NavigateTo<TVM, TParameter>(TParameter parameter)
            where TVM : BaseViewModel;

        void RemoveLastView();
        void NavigateToUri(Uri uri);

        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
