using System;
using System.Runtime.InteropServices;

namespace AndroidToolkit.Memory
{
    public static class MemoryManager
    {
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int setProcessWorkingSetSize(
          IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public static void SetProcessWorkingSetSize(IntPtr proc, int min, int max)
        {
            setProcessWorkingSetSize(proc, min, max);
        }
    }
}
