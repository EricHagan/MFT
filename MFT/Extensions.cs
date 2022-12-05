using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal static class Extensions
    {
        public static bool EqualWithinTolerance(this List<double> A, List<double> B, double percentTolerance)
        {
            if (A == B)
                return true;
            if (A == null && B == null)
                return true;
            if (A == null ^ B == null)
                return false;
            if (A.Count != B.Count)
                return false;
            for (int i = 0; i < A.Count; i++)
            {
                double percentDifference = PercentDifference(A[i], B[i]);
                if (percentDifference > percentTolerance)
                    return false;
            }
            return true;
        }

        static double PercentDifference(double x, double y)
        {
            // if they're both NaN, let's call that a 0% difference:
            if (double.IsNaN(x) && double.IsNaN(y))
                return 0.0;

            // if only one is NaN, let's call that a 100% difference:
            if ((double.IsNaN(x) && !double.IsNaN(y)) || (!double.IsNaN(x) && double.IsNaN(y)))
                return 100.0;

            double basis;
            if (x != 0)
                basis = x;
            else if (y != 0)
                basis = y;
            else
                return 0.0; // both 0
            return Math.Abs(x - y) / basis * 100;
        }
    }
}
