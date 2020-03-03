using System.Drawing;

using Trion.SDK.Surface.Controls;
using Trion.SDK.Surface.Drawing;

namespace Trion.Client.Menu.Panels
{
    internal partial class VisualPanel
    {
        #region CheckBox
        private CheckBox GlowEnable;
        private CheckBox GlowHPEnable;
        private CheckBox GlowFullBloomEnable;
        #endregion

        private void Initialization(Point mainPosition, Point panelPosition)
        {
            #region CheckBox Initialization
            GlowEnable = new CheckBox();
            GlowHPEnable = new CheckBox();
            GlowFullBloomEnable = new CheckBox();
            #endregion

            #region CheckBox Config
            GlowEnable.BackColor = Color.FromArgb(18, 21, 26);
            GlowEnable.Position = new Point(5, 10);
            GlowEnable.Size = new Size(47, 19);
            GlowEnable.Text = "Enable";
            GlowEnable.Font = new Font("Tahoma", 17, Font.FontFlags.FONTFLAG_ANTIALIAS);
            GlowEnable.Visible = true;
            GlowEnable.MouseLeftClick += GlowEnable_MouseLeftClick;

            GlowHPEnable.BackColor = Color.FromArgb(18, 21, 26);
            GlowHPEnable.Position = new Point(5, 35);
            GlowHPEnable.Size = new Size(47, 19);
            GlowHPEnable.Font = new Font("Tahoma", 17, Font.FontFlags.FONTFLAG_ANTIALIAS);
            GlowHPEnable.Text = "HP";
            GlowHPEnable.Visible = true;
            GlowHPEnable.MouseLeftClick += GlowHPEnable_MouseLeftClick;

            GlowFullBloomEnable.BackColor = Color.FromArgb(18, 21, 26);
            GlowFullBloomEnable.Position = new Point(140, 35);
            GlowFullBloomEnable.Size = new Size(47, 19);
            GlowFullBloomEnable.Font = new Font("Tahoma", 17, Font.FontFlags.FONTFLAG_ANTIALIAS);
            GlowFullBloomEnable.Text = "FullBloom";
            GlowFullBloomEnable.Visible = true;
            GlowFullBloomEnable.MouseLeftClick += GlowFullBloomEnable_MouseLeftClick;
            #endregion

            Size = new Size(598, 370);
            BackColor = Color.FromArgb(18, 21, 26);
            Position.X = panelPosition.X + mainPosition.X;
            Position.Y = panelPosition.Y + mainPosition.Y;

            #region CheckBox Set
            this["GlowEnable"] = GlowEnable;
            this["GlowHPEnable"] = GlowHPEnable;
            this["GlowFullBloomEnable"] = GlowFullBloomEnable;
            #endregion

            #region Separator Set
            this["GlowSeparator"] = new Separator()
            {
                BackColor = Color.White,
                Size = new Size(598, 0),
                Position = new Point(0, 35),
                Visible = true
            };
            #endregion
        }
    }
}