using System;
using System.Runtime.InteropServices;

using Trion.SDK.Interfaces;
using Trion.SDK.Interfaces.Client;

namespace Trion.SDK.Dumpers
{
    internal unsafe class NetVar
    {
        #region Variables
        private IBaseClientDLL.RecvTable*[] Tables { get; }
        #endregion

        #region Props
        public uint SequencePtr;
        #endregion

        #region Initializations
        public NetVar()
        {
            int Length = 0;
            var ClientClass = Interface.BaseClientDLL.GetAllClasses();
            if (ClientClass == null)
            {
                return;
            }

            while (ClientClass != null)
            {
                Length++;
                ClientClass = ClientClass->Next;
            }
            Tables = new IBaseClientDLL.RecvTable*[Length];

            Length = 0;
            ClientClass = Interface.BaseClientDLL.GetAllClasses();
            while (ClientClass != null)
            {
                Tables[Length] = ClientClass->RecVTable;

                Length++;
                ClientClass = ClientClass->Next;
            }
        }
        #endregion

        #region Indexers
        public int this[string Table, string Prop] => GetProp(Table, Prop);
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetViewModelSequenceHookDelegate(ref IBaseClientDLL.RecvProxyData Data, void* Struct, void* Out);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetViewModelSequenceOriginalDelegate(ref IBaseClientDLL.RecvProxyData Data, void* Struct, void* Out);
        #endregion

        #region Public Methods
        public int GetOffset(string TableName, string PropName) => GetProp(TableName, PropName);

        public void HookProp<T>(string TableName,string PropName, T Funct,ref uint OldPtr)
        {
            IBaseClientDLL.RecvProp* Prop = (IBaseClientDLL.RecvProp*)0;
            GetProp(TableName, PropName, &Prop);

            if (Prop == null)
            {
                return;
            }

            OldPtr = (uint)Prop->proxy;
            Prop->proxy = Marshal.GetFunctionPointerForDelegate(Funct).ToPointer();
        }
        #endregion

        #region Private Methods
        private IBaseClientDLL.RecvTable* GetTable(string TableName)
        {
            if (Tables.Length == 0)
            {
                return (IBaseClientDLL.RecvTable*)IntPtr.Zero;
            }

            foreach (var Table in Tables)
            {
                if (Table == null)
                {
                    continue;
                }

                if (Marshal.PtrToStringAnsi((IntPtr)Table->netTableName).Equals(TableName))
                {
                    return Table;
                }
            }

            return (IBaseClientDLL.RecvTable*)IntPtr.Zero;
        }

        private int GetProp(string TableName, string PropName, IBaseClientDLL.RecvProp** Prop = null)
        {
            IBaseClientDLL.RecvTable* RecvTable = GetTable(TableName);
            if (RecvTable == null)
            {
                return 0;
            }

            return GetProp(RecvTable, PropName, Prop);
        }

        private int GetProp(IBaseClientDLL.RecvTable* RecvTable, string PropName, IBaseClientDLL.RecvProp** Prop)
        {
            int ExtraOffset = 0;

            for (int Index = 0; Index < RecvTable->propCount; Index++)
            {
                IBaseClientDLL.RecvProp* RecvProp = &RecvTable->props[Index];
                IBaseClientDLL.RecvTable* Child = RecvProp->dataTable;

                if (Child != null && (Child->propCount > 0))
                {
                    int Temp = GetProp(Child, PropName, Prop);

                    if (Temp != 0)
                    {
                        ExtraOffset += RecvProp->offset + Temp;
                    }
                }

                if (!Marshal.PtrToStringAnsi((IntPtr)RecvProp->name).Equals(PropName))
                {
                    continue;
                }

                if (Prop != null)
                {
                    *Prop = RecvProp;
                }

                return RecvProp->offset + ExtraOffset;
            }

            return ExtraOffset;
        }
        #endregion
    }
}