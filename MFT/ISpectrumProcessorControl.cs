using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal interface ISpectrumProcessorControl
    {
        bool Quiet { get; set; }
        void UpdateForm();
        void UpdateFromForm();
        ISpectrumProcessor GetProcessor();
    }
}
