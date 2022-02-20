using System.ComponentModel;
using System.Diagnostics;

namespace MFT
{
    /// <summary>
    /// Use to check if the process is being run inside the Visual Studio Designer
    /// From https://stackoverflow.com/questions/2427381/how-to-detect-that-c-sharp-windows-forms-code-is-executed-within-visual-studio
    /// </summary>
    internal static class DesignTimeHelper
    {
        public static bool IsInDesignMode
        {
            get
            {
                bool isInDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime || Debugger.IsAttached == true;

                if (!isInDesignMode)
                {
                    using (var process = Process.GetCurrentProcess())
                    {
                        return process.ProcessName.ToLowerInvariant().Contains("devenv");
                    }
                }

                return isInDesignMode;
            }
        }
    }
}
