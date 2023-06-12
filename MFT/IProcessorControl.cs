using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    internal interface IProcessorControl
    {
        bool Quiet { get; set; }
        void UpdateForm();
        void UpdateFromForm();
        IProcessor GetProcessor();
    }
}
