﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFT
{
    public interface ISpectrumProcessor
    {
        Spectrum Process(Spectrum data);
        // todo: add Exposure Process(Exposure data);
        string GetDescription();
        SpectrumProcessorFactory.Types Type { get; }
    }
}
