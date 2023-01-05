using System;
using System.Drawing;
using System.Runtime.InteropServices;

using Trion.SDK.Surface.Drawing;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Gui
{
    internal unsafe class ISurface : VMTable
    {
        public enum FontFeature
        {
            FONT_FEATURE_ANTIALIASED_FONTS = 1,
            FONT_FEATURE_DROPSHADOW_FONTS = 2,
            FONT_FEATURE_OUTLINE_FONTS = 6,
        };

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetDrawColorDelegate(IntPtr Class, int Red, int Green, int Blue, int Alpha);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void DrawFilledRectDelegate(IntPtr Class, int X1, int Y1, int X2, int Y2);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void DrawOutlinedRectDelegate(IntPtr Class, int X1, int Y1, int X2, int Y2);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetTextFontDelegate(IntPtr Class, uint Font);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetTextColorDelegate(IntPtr Class, int Red, int Green, int Blue, int Alpha);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetTextPositionDelegate(IntPtr Class, int X, int Y);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void PrintTextDelegate(IntPtr Class, [MarshalAs(UnmanagedType.LPWStr)] string Text, int Length, int DrawType);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void GetScreenSizeDelegate(IntPtr Class, out int Width, out int Height);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr UnlockCursorDelegate(IntPtr Class);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate IntPtr LockCursorHookDelegate();

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr LockCursorOriginalDelegate(IntPtr Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate uint CreateFontDelegate(IntPtr Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool SetFontGlyphSetDelegate(IntPtr Class, uint Font, string FontName, int Tall, int Weight, int Blur, int ScanLines, Font.FontFlags Flags, int RangeMin, int RangeMax);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void GetTextSizeDelegate(IntPtr Class, uint Font, [MarshalAs(UnmanagedType.LPWStr)] string Text, out int Width, out int Height);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetDrawOutlinedCircleDelegate(IntPtr Class, int X, int Y, int Radius, int Seg);

        public ISurface(IntPtr classPtr) : base(classPtr) { }

        public void SetDrawColor(Color Color) => CallVirtualFunction<SetDrawColorDelegate>(15)(this, Color.R, Color.G, Color.B, 255);

        public void SetDrawFilledRect(int X1, int Y1, int X2, int Y2) => CallVirtualFunction<DrawFilledRectDelegate>(16)(this, X1, Y1, X2, Y2);

        public void SetDrawOutlinedRect(int X1, int Y1, int X2, int Y2) => CallVirtualFunction<DrawOutlinedRectDelegate>(18)(this, X1, Y1, X2, Y2);

        public void SetTextFont(uint Font) => CallVirtualFunction<SetTextFontDelegate>(23)(this, Font);

        public void SetTextColor(Color Color) => CallVirtualFunction<SetTextColorDelegate>(25)(this, Color.R, Color.G, Color.B, 255);

        public void SetTextPosition(int X, int Y) => CallVirtualFunction<SetTextPositionDelegate>(26)(this, X, Y);

        public void PrintText(string Text) => CallVirtualFunction<PrintTextDelegate>(28)(this, Text, Text.Length, 0);

        public void GetScreenSize(out int Width, out int Height) => CallVirtualFunction<GetScreenSizeDelegate>(44)(this, out Width, out Height);

        public IntPtr UnlockCursor() => CallVirtualFunction<UnlockCursorDelegate>(66)(this);

        public IntPtr LockCursor() => CallOriginalFunction<LockCursorOriginalDelegate>(67)(this);

        public uint CreateFont() => CallVirtualFunction<CreateFontDelegate>(71)(this);

        public bool SetFontGlyphSet(uint Font, string FontName, int Tall, int Weight, int Blur, int ScanLines, Font.FontFlags Flags, int RangeMin = 0, int RangeMax = 0) => CallVirtualFunction<SetFontGlyphSetDelegate>(72)(this, Font, FontName, Tall, Weight, Blur, ScanLines, Flags, RangeMin, RangeMax);

        public void GetTextSize(uint Font, string Text, out int Width, out int Height) => CallVirtualFunction<GetTextSizeDelegate>(79)(this, Font, Text, out Width, out Height);

        public void SetDrawOutlinedCircle(int X, int Y, int Radius, int Seg) => CallVirtualFunction<SetDrawOutlinedCircleDelegate>(103)(this, X, Y, Radius, Seg);
    }
}