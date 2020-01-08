using System;

namespace Trion.SDK.WinAPI.Variables
{
    internal unsafe struct HANDLE
    {
        #region Variables
        private void* Value;
        #endregion

        #region Initializations
        public HANDLE(void* Value) => this.Value = Value;

        public HANDLE(IntPtr Value) : this(Value.ToPointer()) { }

        public HANDLE(uint* Value) : this((void*)Value) { }

        public HANDLE(uint Value) : this((void*)Value) { }
        #endregion

        #region Indexer
        public HANDLE this[int Index]
        {
            get => (HANDLE)(*(uint**)Value)[Index];
            set => (*(uint**)Value)[Index] = (uint)value;
        }
        #endregion

        #region Operators
        public static implicit operator HANDLE(void* Value) => new HANDLE(Value);

        public static implicit operator HANDLE(IntPtr Value) => new HANDLE(Value);

        public static implicit operator HANDLE(uint Value) => new HANDLE(Value);

        public static implicit operator HANDLE(uint* Value) => new HANDLE(Value);


        public static implicit operator void*(HANDLE Value) => Value.Value;

        public static implicit operator IntPtr(HANDLE Value) => (IntPtr)Value.Value;

        public static implicit operator uint*(HANDLE Value) => (uint*)(uint)Value.Value;

        public static explicit operator uint(HANDLE Value) => (uint)Value.Value;

        public static implicit operator bool(HANDLE Value) => Value.Value != null ? true : false;
        #endregion

        #region Methods
        public bool IsZero => this;

        public HANDLE Zero => (void*)0;
        #endregion
    }

    internal static unsafe class HANDLE_Helper
    {
        public static HANDLE SetZero(this HANDLE HANDLE) => (void*)0;

        public static HANDLE Add(this HANDLE HANDLE, uint Offset) => (void*)((uint)HANDLE + Offset);

        public static HANDLE Subtract(this HANDLE HANDLE, uint Offset) => (void*)((uint)HANDLE - Offset);
    }
}