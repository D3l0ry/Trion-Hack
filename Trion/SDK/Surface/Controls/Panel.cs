using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Trion.SDK.Interfaces;

namespace Trion.SDK.Surface.Controls
{
    internal class Panel : Control
    {
        private readonly List<Control> _Controls = new List<Control>();

        public Control this[string name]
        {
            get => _Controls.Where(X => X.Name == name).FirstOrDefault();
            set
            {
                if (!string.IsNullOrWhiteSpace(value.Name))
                {
                    value.Name = name;
                }

                Point position = value.Position;
                position.X += Position.X;
                position.Y += Position.Y;

                value.Position = position;

                _Controls.Add(value);
            }
        }

        public override void Show()
        {
            Interface.Surface.SetDrawColor(BackColor);

            Interface.Surface.SetDrawFilledRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);

            foreach (Control element in _Controls)
            {
                if (!element.Visible)
                {
                    continue;
                }

                element.Show();
            }
        }
    }
}