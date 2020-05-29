using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Einkaufsliste.Repositories;
using Einkaufsliste.Services;
using Einkaufsliste.ViewModels;
using Xamarin.Forms;

namespace Einkaufsliste
{
    public abstract class Bootstrapper
    {
        protected ContainerBuilder ContainerBuilder { get; private set; }

        public Bootstrapper()
        {
            Initialize();
            FinishInitialization();
        }

        protected virtual void Initialize()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            ContainerBuilder = new ContainerBuilder();

            foreach (var type in currentAssembly.DefinedTypes.Where(e =>
                e.IsSubclassOf(typeof(Page))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            foreach (var type in currentAssembly.DefinedTypes.Where(e =>
                e.IsSubclassOf(typeof(BaseViewModel))))
            {
                ContainerBuilder.RegisterType(type.AsType())
                    .WithParameter(new TypedParameter(typeof(INavService), DependencyService.Get<INavService>()));
            }

            //ContainerBuilder.RegisterType<GroceryListRepositorySQLite>()
            //    .SingleInstance();
            ContainerBuilder.RegisterType<GroceryListRepositoryEfCore>()
                .SingleInstance()
                .As<IGroceryListRepository>();

        }

        private void FinishInitialization()
        {
            var container = ContainerBuilder.Build();
            Resolver.Initialize(container);
        }
    }
}
