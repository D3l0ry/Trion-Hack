using System.Drawing;

using Trion.SDK.Interfaces;

namespace Trion.SDK.Surface.Controls
{
    class CheckBox : Control
    {
        #region Properties
        public bool Checked { get; set; }
        #endregion

        public override void Show()
        {
            #region Variables
            int RectX2 = Position.X + Size.Width;
            int RectY2 = Position.Y + Size.Height;

            int CheckX1 = Position.X + 1;
            int CheckY1 = Position.Y + 1;
            int CheckX2 = Position.X + (Size.Width / 2);
            int CheckY2 = Position.Y + Size.Height - 1;

            Color ActiveColor = Color.Red;
            #endregion

            if (Checked)
            {
                CheckX1 = Position.X + (Size.Width / 2);
                CheckX2 = Position.X + Size.Width - 1;

                ActiveColor = Color.Green;
            }

            Interface.Surface.SetDrawColor(Color.White);
            Interface.Surface.SetDrawOutlinedRect(Position.X - 1, Position.Y - 1, RectX2 + 1,RectY2 + 1);

            Interface.Surface.SetDrawColor(BackColor);
            Interface.Surface.SetDrawFilledRect(Position.X, Position.Y, RectX2, RectY2);

            Interface.Surface.SetDrawColor(ActiveColor);
            Interface.Surface.SetDrawFilledRect(CheckX1, CheckY1, CheckX2, CheckY2);

            MouseEvent();
        }
    }
}