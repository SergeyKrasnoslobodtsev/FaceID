using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
using ModuleCameraDevices.Extensions;
using System;
using System.Drawing;
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

                    if (!videoCapture.IsOpened)
                    {
                        throw new ApplicationException("Нет соединения с ip камерой");
                    }

                    using (var frame = new Mat())
                    {
                        while (!_cancellationTokenSource.IsCancellationRequested)
                        {
                            videoCapture.Read(frame);
                            GpuMat cudaMat = new GpuMat();
                            cudaMat.Upload(frame);
                            CudaInvoke.Resize(cudaMat, cudaMat, new Size(_frameWidth, _frameHeight));
                            cudaMat.Download(frame);
                           
                            using(var cascade = new CascadeClassifier(path + "haarcascade_frontalface_alt2.xml"))
                            using(var grayMat = new Mat())
                            {
                                CvInvoke.CvtColor(frame, grayMat, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                                var rects = cascade.DetectMultiScale(grayMat, 1.3, 4, new Size(20,20));
                                foreach (var rect in rects)
                                    CvInvoke.Rectangle(frame, rect, new MCvScalar(255, 0, 0));
                            }
                                
                            





                            //var buffer = new VectorOfByte();
                            //CvInvoke.Imencode(".jpg", frame, buffer);  //Must use .jpg not jpg
                            //byte[] jpgBytes = buffer.ToArray();


                            if (!frame.IsEmpty)
                            {
                                if (initializationSemaphore != null)
                                    initializationSemaphore.Release();
                                _lastFrame = BitmapExtension.ToBitmap(frame);
                                var lastFrameBitmapImage = BitmapExtension.ToBitmapSource(_lastFrame);
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

