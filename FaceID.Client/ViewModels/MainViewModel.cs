using FaceID.Client.Common;
using FaceID.Mvvm;
using ModernWpf.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace FaceID.Client.ViewModels
{
    public class MainViewModel : RegionViewModelBase
    {
        private Frame _frame;
        private NavigationView _navigationView;
        private bool _isBackEnabled;
        private NavigationViewItem _selected;
        private IRegionManager _regionManager;

        public ICommand ItemInvokedCommand => new DelegateCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked);

        public bool IsBackEnabled
        {
            get { return _isBackEnabled; }
            set { SetProperty(ref _isBackEnabled, value); }
        }

        public NavigationViewItem Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }
        public MainViewModel(IRegionManager regionManager) : base(regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RequestNavigate(Regions.ContentRegion, "CollectionCamera");
        }

        public void Initialize(Frame frame, NavigationView navigationView)
        {
            _frame = frame;
            _navigationView = navigationView;
            _frame.NavigationFailed += (sender, e) =>
            {
                throw e.Exception;
            };
            _frame.Navigated += Frame_Navigated; ;
            _navigationView.BackRequested += OnBackRequested; ;
        }

        private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            _frame.GoBack();
        }

        private NavigationViewItem GetSelectedItem(object menuItems, Type pageType)
        {
            var items = menuItems as IList<object>;
            foreach (var item in items.OfType<NavigationViewItem>())
            {
                if (IsMenuItemForPageType(item, pageType))
                {
                    return item;
                }

                var selectedChild = GetSelectedItem(item.MenuItems, pageType);
                if (selectedChild != null)
                {
                    return selectedChild;
                }
            }

            return null;
        }

        private bool IsMenuItemForPageType(NavigationViewItem menuItem, Type sourcePageType)
        {
            var sourcePageKey = sourcePageType.Name;
            sourcePageKey = sourcePageKey.Substring(0, sourcePageKey.Length - 4);
            var pageKey = menuItem.GetValue(NavHelper.NavigateToProperty) as string;
            return pageKey == sourcePageKey;
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            IsBackEnabled = _frame.CanGoBack;


            var selectedItem = GetSelectedItem(_navigationView.MenuItems, e.GetType());
            if (selectedItem != null)
            {
                Selected = selectedItem;
            }
        }

        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer is NavigationViewItem selectedItem)
            {
                var pageKey = selectedItem.GetValue(NavHelper.NavigateToProperty) as string;
                _regionManager.RequestNavigate(Regions.ContentRegion, pageKey);
            }
        }

    }
}
