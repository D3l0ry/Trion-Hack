using System.Drawing;

using Trion.SDK.Interfaces;

namespace Trion.SDK.Surface.Controls
{
    internal sealed class Label : Control
    {
        public override void Show()
        {
            Interface.Surface.GetTextSize(Font.Id, Text, out int width, out int height);

            Size newSize = Size;
            newSize.Width = width;
            newSize.Height = height;

            Size = newSize;

            Interface.Surface.SetTextFont(Font.Id);
            Interface.Surface.SetTextColor(ForeColor);
            Interface.Surface.SetTextPosition(Position.X, Position.Y);

            if (BackColor != Color.Transparent)
            {
                Interface.Surface.SetDrawColor(BackColor);
                Interface.Surface.SetDrawFilledRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);
            }

            Interface.Surface.PrintText(Text);
        }
    }
}