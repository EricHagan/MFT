using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal class Spectrum
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

        public bool IsGood(out string errMsg)
        {
            errMsg = "";
            if (WavelengthsNm == null)
            {
                errMsg = "WavelengthsNm list is null";
                return false;
            }
            if (Values == null)
            {
                errMsg = "Values list is null";
                return false;
            }
            if (WavelengthsNm.Count != Values.Count)
            {
                errMsg = "WavelengthsNm.Count != Values.Count";
                return false;
            }
            return true;
        }
    }
}
