using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorimetry;

namespace Tester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int j = 0; j < 1000; ++j)
            {
                {
                    bool success = false;
                    Console.WriteLine("10. SmoothSpectrum with points = 1");
                    try
                    {
                        var input = new double[] { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1, 0.9, 0.8, 0.7, 0.6, 0.5, 0.4, 0.3, 0.2, 0.1, 0 };
                        var smoothoutput = Functions.SmoothSpectrum(input, 1, 1);
                        if (input.Length != smoothoutput.Length)
                            success = false;
                        for (int i = 0; i < input.Length; ++i)
                        {
                            if (input[i] != smoothoutput[i])
                                success = false;
                        }
                        success = true;
                    }
                    catch
                    {
                        success = false;
                    }
                    Console.WriteLine(success ? "Success" : "False");
                }
                {
                    bool success = false;
                    Console.WriteLine("20. SmoothSpectrum with points = 2");
                    try
                    {
                        var input = new double[] { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1, 0.9, 0.8, 0.7, 0.6, 0.5, 0.4, 0.3, 0.2, 0.1, 0 };
                        var smoothoutput = Functions.SmoothSpectrum(input, 2, 1);
                        if (input.Length != smoothoutput.Length)
                            success = false;
                        for (int i = 1; i < input.Length; ++i)
                        {
                            if (smoothoutput[i] != (input[i - 1] + input[i]) / 2)
                                success = false;
                        }
                        success = true;
                    }
                    catch
                    {
                        success = false;
                    }
                    Console.WriteLine(success ? "Success" : "False");
                }
            }
        }
    }
}
