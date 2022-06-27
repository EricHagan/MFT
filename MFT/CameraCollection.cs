using System.Collections.Generic;
using AForge.Video.DirectShow;

namespace MFT
{
    internal static class CameraCollection
    {
        public static IEnumerable<ICamera> GetCameras()
        {
            // AForge cameras
            if (aForgeCameras == null)
                aForgeCameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in aForgeCameras)
                yield return new AForgeCamera(new VideoCaptureDevice(filterInfo.MonikerString), filterInfo.Name);
        }
        static FilterInfoCollection aForgeCameras;
    }
}
