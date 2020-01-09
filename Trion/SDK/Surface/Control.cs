using System.Drawing;

using Trion.SDK.Interfaces.Gui;
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
        protected bool MouseInArea(int X1,int Y1, int X2, int Y2)
        {
            NativeMethods.GetCursorPos(out POINT Point);

            return Point.X >= X1 && Point.Y >= Y1 && Point.X <= X2 && Point.Y <= Y2?true:false;
        }

        protected void MouseEvent()
        {
            if (MouseInArea(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height))
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
            else if((NativeMethods.GetAsyncKeyState(Key) & 0x8000) != 0)
            {
                KeyHold?.Invoke(this);
            }
        }
        #endregion
    }
}