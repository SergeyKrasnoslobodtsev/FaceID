using FaceID.Mvvm;
using ModuleCameraDevices.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleCameraDevices
{
    public class ModuleCameraDevicesModule : IModule
    {
        private IRegionManager _regionManager;

        public ModuleCameraDevicesModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, nameof(CollectionCamera));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CollectionCamera>();
        }
    }
}