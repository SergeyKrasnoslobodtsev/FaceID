using DlibDotNet;
using DlibDotNet.Extensions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FaceID.Recognizer
{
    /// <summary>
    /// Specifies the file format of the image.
    /// </summary>
    public enum ImageFormat
    {

        /// <summary>
        /// Specifies that the bitmap (BMP) image format.
        /// </summary>
        Bmp,

        /// <summary>
        /// Specifies that the Joint Photographic Experts Group (JPEG) image format.
        /// </summary>
        Jpeg,

        /// <summary>
        /// Specifies that the W3C Portable Network Graphics (PNG) image format.
        /// </summary>
        Png,

    }

    /// <summary>
    /// Specifies the image mode.
    /// </summary>
    public enum Mode
    {

        /// <summary>
        /// Specifies that the rgb (8-bit Red, Green and Blue, 3 channels) image mode.
        /// </summary>
        Rgb,

        /// <summary>
        /// Specifies that the greyscale image mode.
        /// </summary>
        Grayscale

    }
    /// <summary>
    /// Represents a image data. This class cannot be inherited.
    /// </summary>
    public sealed class Image : DisposableObject
    {

        #region Fields

        private readonly MatrixBase _Matrix;

        private readonly Mode _Mode;

        #endregion

        #region Constructors

        internal Image(MatrixBase matrix, Mode mode)
        {
            this._Matrix = matrix;
            this._Mode = mode;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        /// <exception cref="ObjectDisposedException">This object is disposed.</exception>
        public int Height
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Matrix.Rows;
            }
        }

        internal MatrixBase Matrix => this._Matrix;

        internal Mode Mode => this._Mode;

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        /// <exception cref="ObjectDisposedException">This object is disposed.</exception>
        public int Width
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Matrix.Columns;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves this <see cref="Image"/> to the specified file.
        /// </summary>
        /// <param name="filename">A string that contains the name of the file to which to save this <see cref="Image"/>.</param>
        /// <param name="format">The <see cref="ImageFormat"/> for this <see cref="Image"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filename"/> is null.</exception>
        /// <exception cref="ObjectDisposedException">This object is disposed.</exception>
        public void Save(string filename, ImageFormat format)
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            this.ThrowIfDisposed();

            var directory = Path.GetDirectoryName(filename);
            if (!Directory.Exists(directory) && !string.IsNullOrWhiteSpace(directory))
                Directory.CreateDirectory(directory);

            switch (format)
            {
                case ImageFormat.Bmp:
                    DlibDotNet.Dlib.SaveBmp(this._Matrix, filename);
                    break;
                case ImageFormat.Jpeg:
                    DlibDotNet.Dlib.SaveJpeg(this._Matrix, filename);
                    break;
                case ImageFormat.Png:
                    DlibDotNet.Dlib.SavePng(this._Matrix, filename);
                    break;
            }
        }

        /// <summary>
        /// Converts this <see cref="Image"/> to a GDI+ <see cref="Bitmap"/>.
        /// </summary>
        /// <returns>A <see cref="Bitmap"/> that represents the converted <see cref="Image"/>.</returns>
        /// <exception cref="ObjectDisposedException">This object is disposed.</exception>
        /// <exception cref="NotSupportedException">A Greyscale image is not supported.</exception>
        public Bitmap ToBitmap()
        {
            this.ThrowIfDisposed();

            if (this.Mode == Mode.Grayscale)
                throw new NotSupportedException();

            return ((Matrix<RgbPixel>)this._Matrix).ToBitmap();
        }

        //public Image AsString(string base64)
        //{
        //    this.ThrowIfDisposed();

        //    if (this.Mode == Mode.Grayscale)
        //        throw new NotSupportedException();

            
        //}

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            this._Matrix?.Dispose();
        }

        #endregion

        #endregion

    }
}
