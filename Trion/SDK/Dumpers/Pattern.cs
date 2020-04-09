using System;
using System.Linq;
using System.Collections.Generic;

using Trion.SDK.WinAPI;
using Trion.SDK.WinAPI.Structures;

namespace Trion.SDK.Dumpers
{
    internal unsafe static class Pattern
    {
        public static void* Relative(int* Address) => (Address + 1) + *Address;

        public static void* Dereference(UIntPtr address, uint offset)
        {
            if(address == UIntPtr.Zero)
            {
                return (void*)0;
            }

            if(UIntPtr.Size == 8)
            {
                return (void*)(address + (int)((*(int*)((uint)address + offset) + offset) + sizeof(int)));
            }

            return (void*)*(ulong*)((uint)address + offset);
        }

        public static void* FindPattern(this IntPtr Module,string Pattern,int Offset = 0)
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

            NativeMethods.GetModuleInformation(NativeMethods.GetCurrentProcess(), (IntPtr)Module, out MODULEINFO MODULEINFO, sizeof(MODULEINFO));

            for(long PIndex = (uint)Module; PIndex<MODULEINFO.SizeOfImage;PIndex++)
            {
                bool Found = true;

                for(int MIndex = 0; MIndex<Patterns.Count;MIndex++)
                {
                    Found = Patterns[MIndex] == 0 || *(byte*)(PIndex + MIndex) == Patterns[MIndex];

                    if(!Found)
                    {
                        break;
                    }
                }

                if(Found)
                {
                    return (byte*)PIndex + Offset;
                }
            }

            return (void*)0;
        }
    }
}