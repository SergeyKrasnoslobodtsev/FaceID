
using OpenCvSharp;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Image = System.Windows.Controls.Image;

namespace ModuleCameraDevices.Controls
{
    public class CameraStreaming : IDisposable
    {
        private System.Drawing.Bitmap _lastFrame;
        private Task _previewTask;
        static string path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Models").LocalPath + "\\";
        private CancellationTokenSource _cancellationTokenSource;
        private readonly Image _imageControlForRendering;
        private readonly int _frameWidth;
        private readonly int _frameHeight;

        public string _connection;
        public CameraStreaming(
            Image imageControlForRendering,
            int frameWidth,
            int frameHeight,
            string connection)
        {
            _imageControlForRendering = imageControlForRendering;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _connection = connection;
        }
        public async Task Start()
        {
            if (_previewTask != null && !_previewTask.IsCompleted)
            {
                return;
            }

            var initializationSemaphore = new SemaphoreSlim(0, 1);

            _cancellationTokenSource = new CancellationTokenSource();
            _previewTask = Task.Run(async () =>
            {
                try
                {

                    var videoCapture = new VideoCapture(_connection);

                    if (!videoCapture.IsOpened())
                    {
                        throw new ApplicationException("Нет соединения с ip камерой");
                    }

                    using (var frame = new Mat())
                    {
                        while (!_cancellationTokenSource.IsCancellationRequested)
                        {
                            videoCapture.Read(frame);

                            Cv2.Resize(frame, frame, new Size(_frameWidth, _frameHeight), 0, 0, InterpolationFlags.Linear);
                            var rects = Task.Run(async () => await DetectorAsync(frame));
                            foreach (var rect in await rects)
                                Cv2.Rectangle(frame, rect, Scalar.Red);

                            //var buffer = new VectorOfByte();
                            //CvInvoke.Imencode(".jpg", frame, buffer);  //Must use .jpg not jpg
                            //byte[] jpgBytes = buffer.ToArray();


                            if (!frame.Empty())
                            {
                                if (initializationSemaphore != null)
                                    initializationSemaphore.Release();
                                var lastFrameBitmapImage = OpenCvSharp.WpfExtensions.BitmapSourceConverter.ToBitmapSource(frame);
                                lastFrameBitmapImage.Freeze();
                                _imageControlForRendering.Dispatcher.Invoke(() => _imageControlForRendering.Source = lastFrameBitmapImage);
                            }
                            await Task.Delay(10);
                        }
                    }

                    videoCapture?.Dispose();
                }
                finally
                {
                    if (initializationSemaphore != null)
                        initializationSemaphore.Release();
                }

            }, _cancellationTokenSource.Token);

            await initializationSemaphore.WaitAsync();
            initializationSemaphore.Dispose();
            initializationSemaphore = null;

            if (_previewTask.IsFaulted)
            {
                await _previewTask;
            }
        }

        private Task<Rect[]> DetectorAsync(Mat frame)
        {
            return Task.Run(() =>
            {
                using (var cascade = new CascadeClassifier(path + "haarcascade_frontalface_alt2.xml"))
                using (var grayMat = new Mat())
                {
                    Cv2.CvtColor(frame, grayMat, ColorConversionCodes.BGR2GRAY);

                    return cascade.DetectMultiScale(grayMat, 1.6, 5, HaarDetectionTypes.ScaleImage, new Size(80,80));
                }
            });
        }
        public async Task Stop()
        {
            if (_cancellationTokenSource.IsCancellationRequested)
                return;

            if (!_previewTask.IsCompleted)
            {
                _cancellationTokenSource.Cancel();

                await _previewTask;
            }
        }


        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _lastFrame?.Dispose();
        }
    }
}

