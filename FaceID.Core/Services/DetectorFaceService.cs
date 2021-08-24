using FaceID.Core.Interfaces;
using FaceID.Recognizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceID.Core.Services
{
    public class DetectorFaceService : IDetectorFaceService
    {
        public Task<IEnumerable<Location>> GetLocation(string frameBase64)
        {
            throw new NotImplementedException();
        }
    }
}
