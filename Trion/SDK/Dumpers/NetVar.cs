using System;
using System.Runtime.InteropServices;
using Trion.SDK.Interfaces;
using Trion.SDK.Interfaces.Client;

namespace Trion.SDK.Dumpers
{
    internal unsafe class NetVar
    {
        private readonly IBaseClientDLL.RecvTable*[] mr_Tables;

        public uint SequencePtr;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetViewModelSequenceHookDelegate(ref IBaseClientDLL.RecvProxyData Data, void* Struct, void* Out);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetViewModelSequenceOriginalDelegate(ref IBaseClientDLL.RecvProxyData Data, void* Struct, void* Out);

        public NetVar()
        {
            int Length = 0;
            IBaseClientDLL.ClientClass* ClientClass = Interface.BaseClientDLL.GetAllClasses();

            if (ClientClass == null)
            {
                return;
            }

            while (ClientClass != null)
            {
                Length++;
                ClientClass = ClientClass->Next;
            }

            mr_Tables = new IBaseClientDLL.RecvTable*[Length];

            Length = 0;
            ClientClass = Interface.BaseClientDLL.GetAllClasses();
            while (ClientClass != null)
            {
                mr_Tables[Length] = ClientClass->RecVTable;

                Length++;
                ClientClass = ClientClass->Next;
            }
        }

        public int this[string Table, string Prop] => GetProp(Table, Prop);

        public int GetOffset(string TableName, string PropName) => GetProp(TableName, PropName);

        public void HookProp<T>(string TableName, string PropName, T Funct, ref uint OldPtr)
        {
            IBaseClientDLL.RecvProp* Prop = (IBaseClientDLL.RecvProp*)IntPtr.Zero;
            GetProp(TableName, PropName, &Prop);

            if (Prop == null)
            {
                return;
            }

            OldPtr = (uint)Prop->proxy;
            Prop->proxy = Marshal.GetFunctionPointerForDelegate(Funct).ToPointer();
        }

        private IBaseClientDLL.RecvTable* GetTable(string TableName)
        {
            if (mr_Tables.Length == 0)
            {
                return (IBaseClientDLL.RecvTable*)IntPtr.Zero;
            }

            foreach (IBaseClientDLL.RecvTable* Table in mr_Tables)
            {
                if (Table == null)
                {
                    continue;
                }

                if (Marshal.PtrToStringAnsi((IntPtr)Table->netTableName) == TableName)
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

                if (Marshal.PtrToStringAnsi((IntPtr)RecvProp->name) != PropName)
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
    }
}