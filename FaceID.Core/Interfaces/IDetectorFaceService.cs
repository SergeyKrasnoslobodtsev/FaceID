using FaceID.Recognizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceID.Core.Interfaces
{
    public interface IDetectorFaceService
    {
        public Task<IEnumerable<Location>> GetLocation(string frameBase64);
    }
}
