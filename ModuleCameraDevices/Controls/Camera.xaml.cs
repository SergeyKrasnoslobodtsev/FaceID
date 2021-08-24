using System;
using System.Windows;
using System.Windows.Controls;

namespace ModuleCameraDevices.Controls
{
    /// <summary>
    /// Логика взаимодействия для Camera.xaml
    /// </summary>
    public partial class Camera : UserControl
    {
        private CameraStreaming _webcamStreaming;
        private string _connection;
        int _width, _height;
        public Image GetImage { get; set; }
        public Camera(string connection, int width = 640, int height = 480)
        {
            InitializeComponent();
            _connection = connection;
            _width = width;
            _height = height;
            OnStart();
        }

        public async void OnStart()
        {
            cameraLoading.Visibility = Visibility.Visible;
            webcamContainer.Visibility = Visibility.Collapsed;
            webcamContainer.Width = _width;
            webcamContainer.Height = _height;
            if (_webcamStreaming == null)
            {
                _webcamStreaming?.Dispose();
                _webcamStreaming = new CameraStreaming(
                    imageControlForRendering: webcamPreview,
                    frameWidth: _width,
                    frameHeight: _height,
                    _connection);
                GetImage = webcamPreview;
            }

            try
            {
                await _webcamStreaming.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            cameraLoading.Visibility = Visibility.Collapsed;
            webcamContainer.Visibility = Visibility.Visible;
        }

        public async void OnStop()
        {
            try
            {
                await _webcamStreaming.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Dispose()
        {
            _webcamStreaming?.Dispose();
        }

        ~Camera()
        {
            OnStop();
            Dispose();
        }
    }
}
