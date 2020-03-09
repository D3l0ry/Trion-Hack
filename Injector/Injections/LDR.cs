using System;
using System.Linq;
using System.Diagnostics;
using System.IO;

using Injector.Injections.Enums;
using Injector.Injections.Interfaces;

namespace Injector.Injections
{
    internal class LDR : IAttacker
    {
        #region Variable
        private Process[] ProcessInfo;

        private FileInfo DllInfo;

        private string ExportName;
        #endregion

        #region Inject Variable
        IntPtr HandleProcess;
        IntPtr AllocationMemory;
        IntPtr ProcessLoadLibrary;
        IntPtr CallFunctionPtr;

        private bool IsDisposable;
        #endregion

        #region Initialization
        public LDR(string ProcessName, string DllName, string ExportName = null)
        {
            ProcessInfo = Process.GetProcessesByName(ProcessName);

            DllInfo = new FileInfo(DllName);

            this.ExportName = ExportName;
        }
        #endregion

        #region Public Methods
        public ReturnCode Injecting()
        {
            #region DLL
            if (!DllInfo.Exists)
            {
                return ReturnCode.FILE_NOT_FOUND;
            }
            #endregion

            #region Process
            if (ProcessInfo.Length == 0)
            {
                return ReturnCode.PROCESS_NOT_FOUND;
            }
            #endregion

            #region Handle
            HandleProcess = NativeMethods.OpenProcess(ProcessAccessFlags.All, true, ProcessInfo[0].Id);

            if (HandleProcess == IntPtr.Zero)
            {
                return ReturnCode.OPEN_PROCESS_ERROR;
            }
            #endregion

            #region Allication Memory
            AllocationMemory = NativeMethods.VirtualAllocEx(HandleProcess, IntPtr.Zero, (uint)(DllInfo.FullName.Length + 1), AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ExecuteReadWrite);

            if (AllocationMemory == IntPtr.Zero)
            {
                return ReturnCode.ALLOCATION_MEMORY_ERROR;
            }
            #endregion

            #region Write Library
            if (!NativeMethods.WriteProcessMemory(HandleProcess, AllocationMemory, DllInfo.FullName, DllInfo.FullName.Length, IntPtr.Zero))
            {
                return ReturnCode.WRITE_LIBRARY_ERROR;
            }
            #endregion

            #region Load Library
            ProcessLoadLibrary = NativeMethods.CreateRemoteThread(HandleProcess, IntPtr.Zero, 0, NativeMethods.GetProcAddress(NativeMethods.GetModuleHandle("kernel32.dll"), "LoadLibraryA"), AllocationMemory, 0, IntPtr.Zero);

            if (ProcessLoadLibrary == IntPtr.Zero)
            {
                return ReturnCode.INJECTING_ERROR;
            }
            #endregion

            #region Call Function
            if (!string.IsNullOrWhiteSpace(ExportName))
            {
                if(!CallFunction())
                {
                    return ReturnCode.EXPORT_FUNCTION_ERROR;
                }
            }
            #endregion

            #region Free Memory
            if (!NativeMethods.VirtualFreeEx(HandleProcess, AllocationMemory, 0, AllocationType.Release))
            {
                IsDisposable = true;

                return ReturnCode.VIRTUAL_FREE_ERROR;
            }

            if (!NativeMethods.CloseHandle(ProcessLoadLibrary) || !NativeMethods.CloseHandle(CallFunctionPtr) || !NativeMethods.CloseHandle(HandleProcess))
            {
                IsDisposable = true;

                return ReturnCode.CLOSE_HANDLE_ERROR;
            }
            #endregion

            return ReturnCode.INJECTION_SUCCESSFUL;
        }

        public void Dispose()
        {
            if (IsDisposable)
            {
                NativeMethods.VirtualFreeEx(HandleProcess, AllocationMemory, 0, AllocationType.Release);
                NativeMethods.CloseHandle(ProcessLoadLibrary);
                NativeMethods.CloseHandle(CallFunctionPtr);
                NativeMethods.CloseHandle(HandleProcess);
            }
        }
        #endregion

        #region Private Methods
        private bool CallFunction()
        {
            foreach (IntPtr ExportFunction in from ProcessModule ProcessModule in ProcessInfo[0].Modules where ProcessModule.ModuleName == DllInfo.Name let ExportFunction = NativeMethods.GetProcAddress(ProcessModule.BaseAddress, ExportName) select ExportFunction)
            {
                if (ExportFunction == IntPtr.Zero)
                {
                    return false;
                }

                CallFunctionPtr = NativeMethods.CreateRemoteThread(HandleProcess, IntPtr.Zero, 0, ExportFunction, AllocationMemory, 0, IntPtr.Zero);
                if (CallFunctionPtr == IntPtr.Zero)
                {
                    return false;
                }

                break;
            }

            return true;
        }
        #endregion
    }
}