using System.Drawing;

using Trion.SDK.Interfaces;

namespace Trion.SDK.Surface.Controls
{
    internal sealed class CheckBox : Control
    {
        public bool Checked { get; set; }

        public override void Show()
        {
            Interface.Surface.GetTextSize(Font.Id, Text, out int width, out int height);

            int positionX = width + Position.X + 5;
            int rectX2 = positionX + Size.Width;
            int rectY2 = Position.Y + Size.Height;
            int checkX1 = positionX + 1;
            int checkY1 = Position.Y + 1;
            int checkX2 = positionX + (Size.Width / 2);
            int checkY2 = Position.Y + Size.Height - 1;

            Color activeColor = Color.Red;

            if (Checked)
            {
                checkX1 = positionX + (Size.Width / 2);
                checkX2 = positionX + Size.Width - 1;

                activeColor = Color.Green;
            }

            Interface.Surface.SetTextFont(Font.Id);
            Interface.Surface.SetTextColor(ForeColor);
            Interface.Surface.SetTextPosition(Position.X, Position.Y + 1);
            Interface.Surface.PrintText(Text);

            Interface.Surface.SetDrawColor(Color.White);
            Interface.Surface.SetDrawOutlinedRect(positionX - 1, Position.Y - 1, rectX2 + 1, rectY2 + 1);
            Interface.Surface.SetDrawColor(BackColor);
            Interface.Surface.SetDrawFilledRect(positionX, Position.Y, rectX2, rectY2);
            Interface.Surface.SetDrawColor(activeColor);
            Interface.Surface.SetDrawFilledRect(checkX1, checkY1, checkX2, checkY2);

            MouseEvent(positionX, Position.Y, rectX2, rectY2);
        }
    }
}