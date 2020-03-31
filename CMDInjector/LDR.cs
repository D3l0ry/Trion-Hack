using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

using NativeManager.MemoryInteraction;
using NativeManager.WinApi;

using CMDInjector.Enums;

namespace CMDInjector
{
    internal unsafe class LDR
    {
        #region Variable
        private MemoryManager MemoryManager;

        private FileInfo DllInfo;

        private string ExportName;
        #endregion

        #region Initialization
        public LDR(string ProcessName, string DllName, string ExportName)
        {
            MemoryManager = new MemoryManager(ProcessName);

            DllInfo = new FileInfo(DllName);

            this.ExportName = ExportName;
        }
        #endregion

        #region Public Methods
        public ReturnCode Injecting()
        {
            if (!DllInfo.Exists)
            {
                return ReturnCode.FILE_NOT_FOUND;
            }

            Assembly.LoadFrom(DllInfo.Name);

            IntPtr AllocationMemory = MemoryManager.GetAllocator().Alloc((uint)DllInfo.FullName.Length);
            if (AllocationMemory == IntPtr.Zero)
            {
                return ReturnCode.ALLOCATION_MEMORY_ERROR;
            }

            if (!MemoryManager.BlockCopy(Encoding.UTF8.GetBytes(DllInfo.FullName), 0, AllocationMemory, 0, DllInfo.FullName.Length))
            {
                return ReturnCode.WRITE_LIBRARY_ERROR;
            }

            if (!MemoryManager.GetExecutor().Execute(Kernel32.GetProcAddress(Kernel32.GetModuleHandle("kernel32.dll"), "LoadLibraryA"), AllocationMemory))
            {
                return ReturnCode.INJECTING_ERROR;
            }

            MemoryManager.GetAllocator().Free(AllocationMemory);

            ProcessModule ProcessModule = MemoryManager.GetProcessInfo().GetModule(DllInfo.Name);
            if (ProcessModule != null)
            {
                if (!MemoryManager.GetExecutor().Execute(Kernel32.GetProcAddress(ProcessModule.BaseAddress, ExportName), IntPtr.Zero))
                {
                    return ReturnCode.EXPORT_FUNCTION_ERROR;
                }
            }

            return ReturnCode.INJECTION_SUCCESSFUL;
        }
        #endregion
    }
}