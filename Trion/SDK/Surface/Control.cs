using System.Drawing;

using Trion.SDK.Surface.Drawing;
using Trion.SDK.WinAPI;
using Trion.SDK.WinAPI.Enums;
using Trion.SDK.WinAPI.Structures;

namespace Trion.SDK.Surface
{
    internal unsafe abstract class Control
    {
        #region Variables
        public string Name { get; set; }
        public string Text { get; set; }

        public Size Size;
        public Point Position;

        public Font Font;

        public Color BackColor = Color.Transparent;
        public Color ForeColor = Color.White;

        public bool Visible { get; set; }
        #endregion

        #region Delegates
        public delegate void Mouse(Control ClassEvent);

        public delegate void Key(Control ClassEvent);
        #endregion

        #region Events
        public event Mouse MouseLeftClick;
        public event Mouse MouseRightClick;
        public event Mouse MouseMove;

        public event Key KeyDown;
        public event Key KeyHold;
        #endregion

        #region Public Methods
        public abstract void Show();
        #endregion

        #region Private Methods
        protected bool MouseInArea(int x1, int y1, int x2, int y2)
        {
            NativeMethods.GetCursorPos(out POINT Point);

            return Point.X >= x1 && Point.Y >= y1 && Point.X <= x2 && Point.Y <= y2 ? true : false;
        }

        protected void MouseEvent() => MouseEvent(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);

        protected void MouseEvent(int x1, int y1, int x2, int y2)
        {
            if (MouseInArea(x1,y1,x2,y2))
            {
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
        }

        protected void KeyEvent(KeyCode Key)
        {
            if ((NativeMethods.GetAsyncKeyState(Key) & 0x1) != 0)
            {
                KeyDown?.Invoke(this);
            }
            if ((NativeMethods.GetAsyncKeyState(Key) & 0x8000) != 0)
            {
                KeyHold?.Invoke(this);
            }
        }
        #endregion
    }
}