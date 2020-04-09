using System;
using System.Collections.Generic;
using System.Text;

namespace Trion.SDK.Structures
{
    internal unsafe struct ListPtr<T> where T:unmanaged
    {
        public T* ObjectPtr;
        public int AllocationCount;
        public int GrowSize;
        public int Size;
        public T* ElementsPtr;

        public ref T this[int index] => ref ObjectPtr[index];
    }
}