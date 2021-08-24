using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.IO;
using System.Windows;

namespace FaceID.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            if (!Directory.Exists(@".\Modules"))
                Directory.CreateDirectory(@".\Modules");
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
