using System.Drawing;

using Trion.SDK.Surface.Drawing;
using Trion.SDK.WinAPI;
using Trion.SDK.WinAPI.Enums;
using Trion.SDK.WinAPI.Structures;

namespace Trion.SDK.Surface
{
    internal abstract unsafe class Control
    {
        public delegate void Key(Control classEvent);
        public delegate void Mouse(Control classEvent);

        public event Mouse MouseLeftClick;
        public event Mouse MouseRightClick;
        public event Mouse MouseMove;
        public event Key KeyDown;
        public event Key KeyHold;

        public string Name { get; set; }

        public string Text { get; set; }

        public Size Size { get; set; }

        public Point Position { get; set; }

        public Font Font { get; set; }

        public Color BackColor { get; set; }

        public Color ForeColor { get; set; }

        public bool Visible { get; set; }

        public abstract void Show();

        protected bool MouseInArea(int x1, int y1, int x2, int y2)
        {
            NativeMethods.GetCursorPos(out POINT Point);

            bool isPointX1 = Point.X >= x1;
            bool isPointX2 = Point.X <= x2;
            bool isPointY1 = Point.Y >= y1;
            bool isPointY2 = Point.Y <= y2;

            return isPointX1 && isPointY1 && isPointX2 && isPointY2;
        }

        protected void MouseEvent() => MouseEvent(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);

        protected void MouseEvent(int x1, int y1, int x2, int y2)
        {
            if (!MouseInArea(x1, y1, x2, y2))
            {
                return;
            }

            if ((NativeMethods.GetAsyncKeyState(KeyCode.MS_Click1) & 1) != 0)
            {
                MouseLeftClick?.Invoke(this);
            }
            else if ((NativeMethods.GetAsyncKeyState(KeyCode.MS_Click2) & 1) != 0)
            {
                MouseRightClick?.Invoke(this);
            }
            else if ((NativeMethods.GetAsyncKeyState(KeyCode.MS_Click1) & 0x8000) != 0)
            {
                MouseMove?.Invoke(this);
            }
        }

        protected void KeyEvent(KeyCode key)
        {
            if ((NativeMethods.GetAsyncKeyState(key) & 0x1) != 0)
            {
                KeyDown?.Invoke(this);
            }

            if ((NativeMethods.GetAsyncKeyState(key) & 0x8000) != 0)
            {
                KeyHold?.Invoke(this);
            }
        }
    }
}