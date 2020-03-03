using System.Drawing;

using Trion.SDK.Interfaces;

namespace Trion.SDK.Surface.Controls
{
    internal sealed class CheckBox : Control
    {
        #region Properties
        public bool Checked { get; set; }
        #endregion

        public override void Show()
        {
            Interface.Surface.GetTextSize(Font.Id, Text, out int Width, out int Height);

            #region Variables
            int PositionX = Width + Position.X + 5;

            int RectX2 = PositionX + Size.Width;
            int RectY2 = Position.Y + Size.Height;

            int CheckX1 = PositionX + 1;
            int CheckY1 = Position.Y + 1;
            int CheckX2 = PositionX + (Size.Width / 2);
            int CheckY2 = Position.Y + Size.Height - 1;

            Color ActiveColor = Color.Red;
            #endregion

            if (Checked)
            {
                CheckX1 = PositionX + (Size.Width / 2);
                CheckX2 = PositionX + Size.Width - 1;

                ActiveColor = Color.Green;
            }

            #region Text
            Interface.Surface.SetTextFont(Font.Id);
            Interface.Surface.SetTextColor(ForeColor);
            Interface.Surface.SetTextPosition(Position.X, Position.Y + 1);

            Interface.Surface.PrintText(Text);
            #endregion

            #region CheckBox
            Interface.Surface.SetDrawColor(Color.White);
            Interface.Surface.SetDrawOutlinedRect(PositionX - 1, Position.Y - 1, RectX2 + 1,RectY2 + 1);

            Interface.Surface.SetDrawColor(BackColor);
            Interface.Surface.SetDrawFilledRect(PositionX, Position.Y, RectX2, RectY2);

            Interface.Surface.SetDrawColor(ActiveColor);
            Interface.Surface.SetDrawFilledRect(CheckX1, CheckY1, CheckX2, CheckY2);
            #endregion

            MouseEvent(PositionX,Position.Y,RectX2,RectY2);
        }
    }
}