using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorimetry
{
    public struct XYZPoint
    {
        public XYZPoint(double _X, double _Y, double _Z)
        {
            X = _X;
            Y = _Y;
            Z = _Z;
        }

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
    }
}
