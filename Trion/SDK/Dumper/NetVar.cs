using System;
using System.Runtime.InteropServices;

using Trion.SDK.Interfaces;
using Trion.SDK.Structures;

namespace Trion.SDK.Dumpers
{
    internal unsafe class NetVar
    {
        private readonly RecvTable*[] mr_Tables;

        public uint SequencePtr;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetViewModelSequenceHookDelegate(ref RecvProxyData Data, void* Struct, void* Out);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetViewModelSequenceOriginalDelegate(ref RecvProxyData Data, void* Struct, void* Out);

        public NetVar()
        {
            int Length = 0;
            ClientClass* ClientClass = Interface.BaseClientDLL.GetAllClasses();

            if (ClientClass == null)
            {
                return;
            }

            while (ClientClass != null)
            {
                Length++;
                ClientClass = ClientClass->Next;
            }

            mr_Tables = new RecvTable*[Length];

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

        public void HookProp<T>(string TableName, string PropName, T Funct, ref uint OldPtr)
        {
            RecvProp* Prop = (RecvProp*)IntPtr.Zero;

            GetProp(TableName, PropName, &Prop);

            if (Prop == null)
            {
                return;
            }

            OldPtr = (uint)Prop->proxy;
            Prop->proxy = Marshal.GetFunctionPointerForDelegate(Funct).ToPointer();
        }

        private RecvTable* GetTable(string TableName)
        {
            if (mr_Tables.Length == 0)
            {
                return (RecvTable*)IntPtr.Zero;
            }

            foreach (RecvTable* Table in mr_Tables)
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

            return (RecvTable*)IntPtr.Zero;
        }

        private int GetProp(string TableName, string PropName, RecvProp** Prop = null)
        {
            RecvTable* RecvTable = GetTable(TableName);

            if (RecvTable == null)
            {
                return 0;
            }

            return GetProp(RecvTable, PropName, Prop);
        }

        private int GetProp(RecvTable* RecvTable, string PropName, RecvProp** Prop)
        {
            int ExtraOffset = 0;

            for (int Index = 0; Index < RecvTable->propCount; Index++)
            {
                RecvProp* RecvProp = &RecvTable->props[Index];
                RecvTable* Child = RecvProp->dataTable;

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