using System.Collections.Generic;
using DlibDotNet;
;

namespace FaceID.Recognizer.Extensions
{
    /// <summary>
    /// Абстрактный базовый класс, который предоставляет функциональные возможности для определения местоположения лиц по изображению.
    /// </summary>
    public abstract class BaseFaceDetector : DisposableObject
    {

        #region Methods

        internal IEnumerable<Location> Detect(Image image, int numberOfTimesToUpsample)
        {
            return this.RawDetect(image.Matrix, numberOfTimesToUpsample);
        }

        /// <summary>
        /// Возвращает перечисляемую коллекцию местоположений лиц, соответствующих всем лицам на указанном изображении.
        /// </summary>
        /// <param name="matrix">Матрица содержит лицо.</param>
        /// <param name="numberOfTimesToUpsample">Коэффициент дискретизации.</param>
        /// <returns>Список координат лица</returns>
        protected abstract IEnumerable<Location> RawDetect(MatrixBase matrix, int numberOfTimesToUpsample);

        #endregion

    }
}
