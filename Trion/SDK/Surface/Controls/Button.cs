using Trion.SDK.Interfaces;

namespace Trion.SDK.Surface.Controls
{
    internal sealed class Button : Control
    {
        public override void Show()
        {
            Interface.Surface.SetDrawColor(BackColor);
            Interface.Surface.SetDrawFilledRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                Interface.Surface.SetTextColor(ForeColor);
                Interface.Surface.SetTextFont(Font.Id);
                Interface.Surface.GetTextSize(Font.Id, Text, out int textWidth, out int textHeight);
                Interface.Surface.SetTextPosition(Position.X + (Size.Width / 2) - (textWidth / 2), Position.Y + (Size.Height / 2) - (textHeight / 2));
                Interface.Surface.PrintText(Text);
            }

            MouseEvent();
        }
    }
}