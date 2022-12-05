using System;
using System.Collections.Generic;

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

        public static double WavelengthsEqualPercentTolerance { get; set; } = 1.0;

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

        public static Spectrum operator /(Spectrum A, Spectrum B)
        {
            string errMsg;
            if (!A.IsGood(out errMsg))
                throw new Exception($"Problem with spectrum A: {errMsg}");
            if (!B.IsGood(out errMsg))
                throw new Exception($"Problem with spectrum A: {errMsg}");

            if (A.Values.Count != B.Values.Count)
                throw new Exception($"A.Values.Count ({A.Values.Count})" +
                    $" is not the same as B.Values.Count ({B.Values.Count})");
            if (!A.WavelengthsNm.EqualWithinTolerance(B.WavelengthsNm, WavelengthsEqualPercentTolerance))
                throw new Exception($"A.WavelengthsNm not within percent tolerance" +
                    $" of {WavelengthsEqualPercentTolerance} of B.WavelengthsNm");

            var output = new Spectrum();
            output.WavelengthsNm = A.WavelengthsNm;
            for (int i = 0; i < A.Values.Count; i++)
            {
                if (B.Values[i] == 0.0)
                    output.Values.Add(double.NaN);
                else
                    output.Values.Add(A.Values[i] / B.Values[i]);
            }
            return output;
        }

        public static Spectrum operator -(Spectrum A, Spectrum B)
        {
            string errMsg;
            if (!A.IsGood(out errMsg))
                throw new Exception($"Problem with spectrum A: {errMsg}");
            if (!B.IsGood(out errMsg))
                throw new Exception($"Problem with spectrum A: {errMsg}");

            if (A.Values.Count != B.Values.Count)
                throw new Exception($"A.Values.Count ({A.Values.Count})" +
                    $" is not the same as B.Values.Count ({B.Values.Count})");
            if (!A.WavelengthsNm.EqualWithinTolerance(B.WavelengthsNm, WavelengthsEqualPercentTolerance))
                throw new Exception($"A.WavelengthsNm not within percent tolerance" +
                    $" of {WavelengthsEqualPercentTolerance} of B.WavelengthsNm");

            var output = new Spectrum();
            output.WavelengthsNm = A.WavelengthsNm;
            for (int i = 0; i < A.Values.Count; i++)
            {
                    output.Values.Add(A.Values[i] - B.Values[i]);
            }
            return output;
        }

    }
}
