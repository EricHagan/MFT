using System.Linq;
using Colorimetry;

namespace MFT
{
    internal class Color
    {
        public Color(Spectrum spectrum)
        {
            Spectrum = spectrum;
            
            double[] _XYZ = Functions.SpectrumToXYZ(
                spectrum.Values.ToArray(), spectrum.WavelengthsNm.Select(x => (int)x).ToArray());
            XYZ = new XYZPoint(_XYZ[0], _XYZ[1], _XYZ[2]);

            double[] _Lab = Functions.XYZtoLAB(_XYZ);
            Lab = new LabPoint(_Lab[0], _Lab[1], _Lab[2]);
        }

        public Spectrum Spectrum { get; private set; }
        public XYZPoint XYZ { get; private set; }
        public LabPoint Lab { get; private set; }

        public double DeltaE(Color reference, Functions.DeltaEcalcTypes DEtype = Functions.DeltaEcalcTypes.DE00)
        {
            return Functions.DeltaE(reference.Lab, this.Lab, DEtype);
        }
    }
}
