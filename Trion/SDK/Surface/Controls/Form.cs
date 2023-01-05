using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Trion.SDK.Interfaces;

namespace Trion.SDK.Surface.Controls
{
    internal class Form : Control
    {
        public readonly List<Control> Controls = new List<Control>();

        public Control this[string name]
        {
            get => Controls.Where(X => X.Name == name).FirstOrDefault();
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

                Controls.Add(value);
            }
        }

        public Control this[string name, string text]
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value.Name))
                {
                    value.Name = name;
                }

                if (!string.IsNullOrWhiteSpace(value.Text))
                {
                    value.Text = text;
                }

                Point position = value.Position;
                position.X += Position.X;
                position.Y += Position.Y;

                value.Position = position;

                Controls.Add(value);
            }
        }

        public Control this[string name, bool isPanel]
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value.Name))
                {
                    value.Name = name;
                }

                Controls.Add(value);
            }
        }

        public override void Show()
        {
            if (!Visible)
            {
                return;
            }

            Interface.Surface.SetDrawColor(BackColor);
            Interface.Surface.SetDrawFilledRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                Interface.Surface.GetTextSize(Font.Id, Text, out int textWidth, out int textHeight);

                Interface.Surface.SetDrawColor(Color.FromArgb(25, 30, 37));
                Interface.Surface.SetDrawFilledRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + textHeight + 10);

                Interface.Surface.SetTextColor(ForeColor);
                Interface.Surface.SetTextFont(Font.Id);
                Interface.Surface.SetTextPosition(Position.X + (Size.Width / 2) - (textWidth / 2), Position.Y + 5);

                Interface.Surface.PrintText(Text);
            }

            if (Controls.Count > 0)
            {
                foreach (Control element in Controls)
                {
                    if (!element.Visible)
                    {
                        continue;
                    }

                    element.Show();
                }
            }

            MouseEvent();

            KeyEvent(WinAPI.Enums.KeyCode.VK_HOME);
        }
    }
}