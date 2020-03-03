using System.Drawing;

using Trion.SDK.Interfaces;

namespace Trion.SDK.Surface.Controls
{
    internal sealed class Separator : Control
    {
        public override void Show()
        {
            Interface.Surface.SetDrawColor(BackColor);

            Interface.Surface.SetDrawOutlinedRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + 1);
        }
    }
}