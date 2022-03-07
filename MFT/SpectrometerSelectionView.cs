using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal class SpectrometerSelectionView
    {
        public SpectrometerTypes Type { get; set; }

        public string Name => SpectrometerFactory.GetName(Type);

        public override string ToString() => Name;
    }
}
