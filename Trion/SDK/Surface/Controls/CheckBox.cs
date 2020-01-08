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
            Interface.Surface.SetDrawColor(BackColor);
            Interface.Surface.SetDrawFilledRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);

            Interface.Surface.SetDrawColor(Color.White);
            Interface.Surface.SetDrawOutlinedRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);

            if(Checked)
            {
                Interface.Surface.SetDrawColor(Color.Green);
                Interface.Surface.SetDrawFilledRect(Position.X + (Size.Width/2), Position.Y + 5, Position.X + Size.Width - 5, Position.Y + Size.Height - 5);
                return;
            }

            Interface.Surface.SetDrawColor(Color.Red);
            Interface.Surface.SetDrawFilledRect(Position.X + 5, Position.Y + 5, Position.X + (Size.Width / 2), Position.Y + Size.Height - 5);

            Interface.Surface.SetTextColor(ForeColor);
            //Interface.Surface.SetTextPosition();
        }
    }
}