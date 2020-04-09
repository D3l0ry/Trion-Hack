using System;
using System.Drawing;
using System.Runtime.InteropServices;

using Trion.SDK.Surface.Drawing;
using Trion.SDK.VMT;

namespace Trion.SDK.Interfaces.Gui
{
    internal unsafe class ISurface : VMTable
    {
        #region Initializations
        public ISurface(IntPtr Base) : base(Base)
        {
        }
        #endregion

        #region Enums
        public enum FontFeature
        {
            FONT_FEATURE_ANTIALIASED_FONTS = 1,
            FONT_FEATURE_DROPSHADOW_FONTS = 2,
            FONT_FEATURE_OUTLINE_FONTS = 6,
        };
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetDrawColorDelegate(void* Class, int Red, int Green, int Blue, int Alpha);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void DrawFilledRectDelegate(void* Class, int X1, int Y1, int X2, int Y2);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void DrawOutlinedRectDelegate(void* Class, int X1, int Y1, int X2, int Y2);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetTextFontDelegate(void* Class, uint Font);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetTextColorDelegate(void* Class, int Red, int Green, int Blue, int Alpha);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetTextPositionDelegate(void* Class, int X, int Y);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void PrintTextDelegate(void* Class, [MarshalAs(UnmanagedType.LPWStr)]string Text, int Length, int DrawType);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void GetScreenSizeDelegate(void* Class, out int Width, out int Height);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void* UnlockCursorDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void* LockCursorHookDelegate();

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void* LockCursorOriginalDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate uint CreateFontDelegate(void* Class);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool SetFontGlyphSetDelegate(void* Class, uint Font, string FontName, int Tall, int Weight, int Blur, int ScanLines, Font.FontFlags Flags, int RangeMin, int RangeMax);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void GetTextSizeDelegate(void* Class, uint Font, [MarshalAs(UnmanagedType.LPWStr)]string Text, out int Width, out int Height);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetDrawOutlinedCircleDelegate(void* Class,int X, int Y, int Radius, int Seg);
        #endregion

        #region Virtual Methods
        public void SetDrawColor(Color Color) => CallVirtualFunction<SetDrawColorDelegate>(15)(this, Color.R, Color.G, Color.B, 255);

        public void SetDrawFilledRect(int X1, int Y1, int X2, int Y2) => CallVirtualFunction<DrawFilledRectDelegate>(16)(this, X1, Y1, X2, Y2);

        public void SetDrawOutlinedRect(int X1, int Y1, int X2, int Y2) => CallVirtualFunction<DrawOutlinedRectDelegate>(18)(this, X1, Y1, X2, Y2);

        public void SetTextFont(uint Font) => CallVirtualFunction<SetTextFontDelegate>(23)(this, Font);

        public void SetTextColor(Color Color) => CallVirtualFunction<SetTextColorDelegate>(25)(this, Color.R, Color.G, Color.B, 255);

        public void SetTextPosition(int X, int Y) => CallVirtualFunction<SetTextPositionDelegate>(26)(this, X, Y);

        public void PrintText(string Text) => CallVirtualFunction<PrintTextDelegate>(28)(this, Text, Text.Length, 0);

        public void GetScreenSize(out int Width, out int Height) => CallVirtualFunction<GetScreenSizeDelegate>(44)(this, out Width, out Height);

        public void* UnlockCursor() => CallVirtualFunction<UnlockCursorDelegate>(66)(this);

        public void* LockCursor() => CallOriginalFunction<LockCursorOriginalDelegate>(67)(this);

        public uint CreateFont() => CallVirtualFunction<CreateFontDelegate>(71)(this);

        public bool SetFontGlyphSet(uint Font, string FontName, int Tall, int Weight, int Blur, int ScanLines, Font.FontFlags Flags, int RangeMin = 0, int RangeMax = 0) => CallVirtualFunction<SetFontGlyphSetDelegate>(72)(this,Font, FontName, Tall, Weight, Blur, ScanLines, Flags, RangeMin, RangeMax);

        public void GetTextSize(uint Font, string Text, out int Width, out int Height) => CallVirtualFunction<GetTextSizeDelegate>(79)(this, Font,Text, out Width, out Height);

        public void SetDrawOutlinedCircle(int X, int Y, int Radius, int Seg) => CallVirtualFunction<SetDrawOutlinedCircleDelegate>(103)(this, X, Y, Radius, Seg);
        #endregion
    }
}