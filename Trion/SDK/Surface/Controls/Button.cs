using System.Drawing;

using Trion.SDK.Interfaces;
using Trion.SDK.Interfaces.Gui;

namespace Trion.SDK.Surface.Controls
{
    class Button : Control
    {
        #region Public Methods
        public override void Show()
        {
            Interface.Surface.SetDrawColor(BackColor);

            Interface.Surface.SetDrawFilledRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                Interface.Surface.SetTextColor(ForeColor);
                Interface.Surface.SetTextFont(Font.Size);
                Interface.Surface.GetTextSize(Font.Size, Text, out int TextWidth, out int TextHeight);
                Interface.Surface.SetTextPosition(Position.X + (Size.Width / 2) - (TextWidth / 2), Position.Y + (Size.Height / 2) - (TextHeight / 2));
                Interface.Surface.PrintText(Text);
            }

            MouseEvent();
        }
        #endregion
    }
}