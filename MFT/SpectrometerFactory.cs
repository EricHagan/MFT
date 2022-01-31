using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal static class SpectrometerFactory
    {
        public static ISpectrometer GetSpectrometer(SpectrometerTypes type)
        {
            switch (type)
            {
                case SpectrometerTypes.BROADCOM:
                    return new Qmini();
                case SpectrometerTypes.SIMULATED:
                    return new SimulatedSpectrometer();
                case SpectrometerTypes.CONTROLDEV:
                case SpectrometerTypes.SPECTRALIGHT:
                    throw new NotImplementedException();
                default:
                    throw new Exception($"Unknown type: '{type}'");
                    
            }
        }
        public static string GetName(SpectrometerTypes type)
        {
            switch (type)
            {
                case SpectrometerTypes.BROADCOM:
                    return "Broadcom";
                case SpectrometerTypes.SIMULATED:
                    return "Simulated";
                case SpectrometerTypes.CONTROLDEV:
                    return "Control Development";
                case SpectrometerTypes.SPECTRALIGHT:
                    return "Spectralight";
                default:
                    throw new Exception($"Unknown type: '{type}'");

            }
        }
    }
}
