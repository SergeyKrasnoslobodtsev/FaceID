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
        public ObservableCollection<Camera> GetDevices => new ObservableCollection<Camera>()
        {
           // new Camera("rtsp://admin:123456@643107a3a691.sn.mynetname.net:554", 854, 480, 1280, 720)),
            new Camera(@"C:\video_2.avi", 640, 360, 1280, 720)//1920, 1080   2304, 1296   1280, 720
        };

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
        }
    }
}
