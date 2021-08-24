using ModuleCameraDevices.Controls;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace ModuleCameraDevices.ViewModels
{
    public class CollectionCameraViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _connection = "rtsp://admin:123456@643107a3a691.sn.mynetname.net:554";
        private Camera camera;
        public string Connection
        {
            get { return _connection; }
            set { SetProperty(ref _connection, value); }
        }
        ObservableCollection<Camera> _device;
        public ObservableCollection<Camera> GetDevices
        {
            get { return _device; }
            set { SetProperty(ref _device, value); }
        }

        private bool _IsWaitCamera;
        public bool IsWaitCamera
        {
            get { return _IsWaitCamera; }
            set { SetProperty(ref _IsWaitCamera, value); }
        }

        //"rtsp://admin:123456@643107a3a691.sn.mynetname.net:554"
        public CollectionCameraViewModel()
        {
            Message = "View Devices from your Prism Module";
            GetDevices = new ObservableCollection<Camera>();
            camera = new Camera(@"E:\1.avi");
            GetDevices.Add(camera);
        }
    }
}
