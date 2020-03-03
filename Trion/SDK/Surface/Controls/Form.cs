using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Trion.SDK.Interfaces;

namespace Trion.SDK.Surface.Controls
{
    internal class Form : Control
    {
        #region Variables
        public readonly List<Control> Controls = new List<Control>();
        #endregion

        #region Indexer
        public Control this[int Index]
        {
            get => Controls[Index];
            set => Controls.Add(value);
        }

        public Control this[string Name]
        {
            get
            {
                foreach (Control Control in Controls)
                {
                    if (Control.Name == Name)
                    {
                        return Control;
                    }
                }

                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value.Name))
                {
                    value.Name = Name;
                }

                value.Position.X += Position.X;
                value.Position.Y += Position.Y;

                Controls.Add(value);
            }
        }

        public Control this[string Name, string Text]
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value.Name))
                {
                    value.Name = Name;
                }
                if (string.IsNullOrWhiteSpace(value.Text))
                {
                    value.Text = Text;
                }

                value.Position.X += Position.X;
                value.Position.Y += Position.Y;

                Controls.Add(value);
            }
        }

        public Control this[string Name, bool IsPanel]
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value.Name))
                {
                    value.Name = Name;
                }

                Controls.Add(value);
            }
        }
        #endregion

        #region Public Methods
        public override void Show()
        {
            if (Visible)
            {
                Interface.Surface.SetDrawColor(BackColor);
                Interface.Surface.SetDrawFilledRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);

                if (!string.IsNullOrWhiteSpace(Text))
                {
                    Interface.Surface.GetTextSize(Font.Id, Text, out int TextWidth, out int TextHeight);

                    Interface.Surface.SetDrawColor(Color.FromArgb(25, 30, 37));
                    Interface.Surface.SetDrawFilledRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + TextHeight + 10);

                    Interface.Surface.SetTextColor(ForeColor);
                    Interface.Surface.SetTextFont(Font.Id);
                    Interface.Surface.SetTextPosition(Position.X + (Size.Width / 2) - (TextWidth / 2), Position.Y + 5);

                    Interface.Surface.PrintText(Text);
                }

                if (Controls.Count > 0)
                {
                    foreach (Control Element in Controls)
                    {
                        if (Element.Visible)
                        {
                            Element.Show();
                        }
                    }
                }

                MouseEvent();
            }

            KeyEvent(WinAPI.Enums.KeyCode.VK_HOME);
        }
        #endregion
    }
}