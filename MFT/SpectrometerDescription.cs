using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal class SpectrometerDescription
    {
        public SpectrometerTypes Type { get; set; }
        public override string ToString()
        {
            return SpectrometerFactory.GetName(Type);
        }
    }
}
