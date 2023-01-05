using System;
using System.Collections.Generic;
using System.Linq;

using Trion.SDK.WinAPI;
using Trion.SDK.WinAPI.Structures;

namespace Trion.SDK.Dumpers
{
    internal static unsafe class Pattern
    {
        public static void* FindPattern(this IntPtr Module, string Pattern, int Offset = 0)
        {
            List<int> Patterns = new List<int>();

            Pattern.Split(' ').All((X) =>
            {
                if (X.Equals("?"))
                {
                    Patterns.Add(0);
                }
                else
                {
                    Patterns.Add(Convert.ToInt32(X, 16));
                }

                return true;
            });

            NativeMethods.GetModuleInformation(NativeMethods.GetCurrentProcess(), Module, out MODULEINFO MODULEINFO, sizeof(MODULEINFO));

            for (long PIndex = (uint)Module; PIndex < MODULEINFO.SizeOfImage; PIndex++)
            {
                bool Found = true;

                for (int MIndex = 0; MIndex < Patterns.Count; MIndex++)
                {
                    Found = Patterns[MIndex] == 0 || *(byte*)(PIndex + MIndex) == Patterns[MIndex];

                    if (!Found)
                    {
                        break;
                    }
                }

                if (Found)
                {
                    return (byte*)PIndex + Offset;
                }
            }

            return (void*)0;
        }
    }
}