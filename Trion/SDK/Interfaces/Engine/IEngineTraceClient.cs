using System;
using System.Runtime.InteropServices;
using Trion.SDK.Dumpers;
using Trion.SDK.Structures.Numerics;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Engine
{
    internal unsafe sealed class IEngineTraceClient : VMTable
    {
        #region Initialization
        public IEngineTraceClient(void* Base) : base(Base)
        {
        }

        public IEngineTraceClient(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void TraceRayDelegate(void* Class,ref Ray ray, uint mask, TraceFilter filter, ref Trace trace);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void TraceLineDelegate(Vector3 start, Vector3 end, uint mask, void* filter,int collision, ref Trace trace);
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential)]
        public struct Ray
        {
            public Ray(Vector3 src, Vector3 dst)
            {
                Start = src;
                Delta = dst - src;

                IsSwept = Delta.X != 0 || Delta.Y != 0 || Delta.Z != 0;

                pad = 0;

                IsRay = true;
            }

            public Vector3 Start;

            private float pad;

            public Vector3 Delta;

            private fixed byte pad2[40];

            private bool IsRay;

            private bool IsSwept;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TraceFilter
        {
            public TraceFilter(void* entity) => Skip = entity;

            public virtual bool ShouldHitEntity(void* entity) => entity != Skip;
            public virtual int GetTraceType() => 0;

            void* Skip;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Surface
        {
            char* name;

            short surfaceProps;

            ushort flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Trace
        {
            Vector3 StartPos;

            Vector3 EndPos;

            fixed byte pad[20];

            public float fraction;

            int contents;

            ushort dispFlags;

            bool allSolid;

            bool StartSolid;

            fixed byte pad1[4];

            Surface surface;

            int hitgroup;

            fixed byte pad2[4];

            void* entity;

            int hitbox;
        }
        #endregion

        #region Enums
        public enum HitGroup
        {
            Invalid = -1,
            Generic,
            Head,
            Chest,
            Stomach,
            LeftArm,
            RightArm,
            LeftLeg,
            RightLeg,
            Gear = 10
        }
        #endregion

        #region Methods
        public void TraceRay(ref Ray ray, uint mask, TraceFilter filter, ref Trace trace) => CallVirtualFunction<TraceRayDelegate>(5)(this, ref ray, mask,  filter, ref trace);

        public void TraceLine(Vector3 start, Vector3 end, uint mask, void* filter, int collision, ref Trace trace) => Marshal.GetDelegateForFunctionPointer<TraceLineDelegate>((IntPtr)Offsets.UTIL_TraceLine)(start,end,mask,filter,collision, ref trace);
        #endregion
    }
}