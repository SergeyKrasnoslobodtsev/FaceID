using System;
using System.Collections.Generic;
using DlibDotNet;

namespace FaceID.Recognizer.Extensions
{
    /// <summary>
    /// Specifies the part of face.
    /// </summary>
    public enum FacePart
    {

        /// <summary>
        /// Specifies the chin.
        /// </summary>
        Chin,

        /// <summary>
        /// Specifies the left eyebrow.
        /// </summary>
        LeftEyebrow,

        /// <summary>
        /// Specifies the right eyebrow.
        /// </summary>
        RightEyebrow,

        /// <summary>
        /// Specifies the nose bridge.
        /// </summary>
        NoseBridge,

        /// <summary>
        /// Specifies the nose tip.
        /// </summary>
        NoseTip,

        /// <summary>
        /// Specifies the left eye.
        /// </summary>
        LeftEye,

        /// <summary>
        /// Specifies the right eye.
        /// </summary>
        RightEye,

        /// <summary>
        /// Specifies the top lip.
        /// </summary>
        TopLip,

        /// <summary>
        /// Specifies the bottom lip.
        /// </summary>
        BottomLip,

        /// <summary>
        /// Specifies the nose.
        /// </summary>
        Nose,

    }

    /// <summary>
    /// Represents an coordinate and index of face parts.
    /// </summary>
    public class FacePoint : IEquatable<FacePoint>
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FacePoint"/> class with the specified coordinates and index.
        /// </summary>
        /// <param name="point">The coordinate of face parts.</param>
        /// <param name="index">The index of face parts.</param>
        public FacePoint(Point point, int index)
        {
            this.Point = point;
            this.Index = index;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the coordinate of this <see cref="FacePoint"/>.
        /// </summary>
        public Point Point
        {
            get;
        }

        /// <summary>
        /// Gets the index of this <see cref="FacePoint"/>.
        /// </summary>
        public int Index
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares two <see cref="FacePoint"/> class for equality.
        /// </summary>
        /// <param name="other">The point to compare to this instance.</param>
        /// <returns><code>true</code> if both <see cref="FacePoint"/> class contain the same <see cref="Point"/> and <see cref="Index"/> values; otherwise, <code>false</code>.</returns>
        public bool Equals(FacePoint other)
        {
            return this.Point == other.Point &&
                   this.Index == other.Index;
        }

        #region Overrids

        /// <summary>
        /// Determines whether the specified <see cref="Object"/> is a <see cref="FacePoint"/> and whether it contains the same data as this <see cref="FacePoint"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><code>true</code> if <paramref name="obj"/> is a <see cref="FacePoint"/> and contains the same <see cref="Point"/> and <see cref="Index"/> values as this <see cref="FacePoint"/>; otherwise, <code>false</code>.</returns>
        public override bool Equals(object obj)
        {
            return obj is FacePoint && Equals((FacePoint)obj);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="FacePoint"/>.
        /// </summary>
        /// <returns>The hash code for this <see cref="FacePoint"/> class.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + this.Point.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Index.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares two <see cref="FacePoint"/> class for equality.
        /// </summary>
        /// <param name="point1">The first <see cref="FacePoint"/> class to compare.</param>
        /// <param name="point2">The second <see cref="FacePoint"/> class to compare.</param>
        /// <returns><code>true</code> if both the <see cref="Point"/> and <see cref="Index"/> of <paramref name="point1"/> and <paramref name="point2"/> are equal; otherwise, <code>false</code>.</returns>
        public static bool operator ==(FacePoint point1, FacePoint point2)
        {
            return point1.Equals(point2);
        }

        /// <summary>
        /// Compares two <see cref="FacePoint"/> class for inequality.
        /// </summary>
        /// <param name="point1">The first <see cref="FacePoint"/> class to compare.</param>
        /// <param name="point2">The second <see cref="FacePoint"/> class to compare.</param>
        /// <returns><code>true</code> if <paramref name="point1"/> and <paramref name="point2"/> have different <see cref="Point"/> or <see cref="Index"/>; <code>false</code> if <paramref name="point1"/> and <paramref name="point2"/> have the same <see cref="Point"/> and <see cref="Index"/>.</returns>
        public static bool operator !=(FacePoint point1, FacePoint point2)
        {
            return !(point1 == point2);
        }

        #endregion

        #endregion

    }
    /// <summary>
    /// An abstract base class that provides functionality to detect face parts locations from face image.
    /// </summary>
    public abstract class FaceLandmarkDetector : DisposableObject
    {

        #region Methods

        internal FullObjectDetection Detect(Image image, Location location)
        {
            return this.RawDetect(image.Matrix, location);
        }

        internal IEnumerable<Dictionary<FacePart, IEnumerable<FacePoint>>> GetLandmarks(IEnumerable<FacePoint[]> landmarkTuples)
        {
            return this.RawGetLandmarks(landmarkTuples);
        }

        /// <summary>
        /// Returns an object contains information of face parts corresponds to specified location in specified image.
        /// </summary>
        /// <param name="matrix">The matrix contains a face.</param>
        /// <param name="location">The location rectangle for a face.</param>
        /// <returns>An object contains information of face parts.</returns>
        protected abstract FullObjectDetection RawDetect(MatrixBase matrix, Location location);

        /// <summary>
        /// Returns an enumerable collection of dictionary of face parts locations (eyes, nose, etc).
        /// </summary>
        /// <param name="landmarkTuples">The enumerable collection of face parts location.</param>
        /// <returns>An enumerable collection of dictionary of face parts locations (eyes, nose, etc).</returns>
        protected abstract IEnumerable<Dictionary<FacePart, IEnumerable<FacePoint>>> RawGetLandmarks(IEnumerable<FacePoint[]> landmarkTuples);

        #endregion

    }
}
