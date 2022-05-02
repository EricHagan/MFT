using System.Collections.Generic;
using AForge.Video.DirectShow;

namespace MFT
{
    internal static class CameraCollection
    {
        public static IEnumerable<ICamera> GetCameras()
        {
            // AForge cameras
            var filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                yield return new AForgeCamera(new VideoCaptureDevice(filterInfo.MonikerString), filterInfo.Name);
        }
    }
}
