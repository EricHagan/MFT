using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    public class Spectrum
    {
        public Spectrum(List<double> wavelengthNm, List<double> values)
        {
            WavelengthsNm = wavelengthNm;
            Values = values;
        }

        public Spectrum()
            : this(new List<double>(), new List<double>()) { }

        public List<double> WavelengthsNm { get; private set; }
        public List<double> Values { get; private set; }

        public bool IsGood()
        {
            if (WavelengthsNm == null)
                return false;
            if (Values == null)
                return false;
            if (WavelengthsNm.Count != Values.Count)
                return false;
            return true;
        }
    }
}
