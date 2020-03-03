using System.Drawing;
using Trion.Client.Menu.Panels;
using Trion.SDK.Interfaces;
using Trion.SDK.Surface.Controls;
using Trion.SDK.Surface.Drawing;

namespace Trion.Client.Menu
{
    internal partial class Main
    {
        #region Buttons
        private Button VisualButton;
        private Button ESPButton;
        private Button ChamsButton;
        private Button SkinButton;
        private Button MiscButton;
        private Button ProfileButton;
        #endregion

        private void Initialization()
        {
            Interface.Surface.GetScreenSize(out int ScreenWidth, out int ScreenHeight);

            #region Buttons
            VisualButton = new Button();
            ESPButton = new Button();
            ChamsButton = new Button();
            SkinButton = new Button();
            MiscButton = new Button();
            ProfileButton = new Button();
            #endregion

            VisualButton.BackColor = Color.FromArgb(18, 21, 26);
            VisualButton.ForeColor = Color.FromArgb(152, 241, 221);
            VisualButton.Font = new Font("Tahoma", 20, Font.FontFlags.FONTFLAG_ANTIALIAS);
            VisualButton.Position = new Point(0, 37);
            VisualButton.Size = new Size(200, 60);
            VisualButton.Visible = true;
            VisualButton.MouseLeftClick += VisualButton_MouseLeftClick;

            ESPButton.BackColor = Color.FromArgb(25, 30, 37);
            ESPButton.Font = new Font("Tahoma", 20, Font.FontFlags.FONTFLAG_ANTIALIAS);
            ESPButton.Position = new Point(0, 99);
            ESPButton.Size = new Size(200, 60);
            ESPButton.Visible = true;
            ESPButton.MouseLeftClick += ESPButton_MouseLeftClick;

            ChamsButton.BackColor = Color.FromArgb(25, 30, 37);
            ChamsButton.Font = new Font("Tahoma", 20, Font.FontFlags.FONTFLAG_ANTIALIAS);
            ChamsButton.Position = new Point(0, 161);
            ChamsButton.Size = new Size(200, 60);
            ChamsButton.Visible = true;
            ChamsButton.MouseLeftClick += ChamsButton_MouseLeftClick;

            SkinButton.BackColor = Color.FromArgb(25, 30, 37);
            SkinButton.Font = new Font("Tahoma", 20, Font.FontFlags.FONTFLAG_ANTIALIAS);
            SkinButton.Position = new Point(0, 223);
            SkinButton.Size = new Size(200, 60);
            SkinButton.Visible = true;
            SkinButton.MouseLeftClick += SkinButton_MouseLeftClick;

            MiscButton.BackColor = Color.FromArgb(25, 30, 37);
            MiscButton.Font = new Font("Tahoma", 20, Font.FontFlags.FONTFLAG_ANTIALIAS);
            MiscButton.Position = new Point(0, 285);
            MiscButton.Size = new Size(200, 60);
            MiscButton.Visible = true;
            MiscButton.MouseLeftClick += MiscButton_MouseLeftClick;

            ProfileButton.BackColor = Color.FromArgb(25, 30, 37);
            ProfileButton.Font = new Font("Tahoma", 20, Font.FontFlags.FONTFLAG_ANTIALIAS);
            ProfileButton.Position = new Point(0, 347);
            ProfileButton.Size = new Size(200, 60);
            ProfileButton.Visible = true;
            ProfileButton.MouseLeftClick += ProfileButton_MouseLeftClick;

            Text = "TRION";
            Size = new Size(800, 407);
            Position = new Point((ScreenWidth / 2) - (Size.Width / 2), (ScreenHeight / 2) - (Size.Height / 2));
            Font = new Font("Impact", 25, Font.FontFlags.FONTFLAG_ANTIALIAS);
            BackColor = Color.FromArgb(18, 21, 26);
            KeyDown += Main_KeyDown;

            this["VisualButton", "Visual"] = VisualButton;
            this["ESPButton", "ESP"] = ESPButton;
            this["ChamsButton", "Chams"] = ChamsButton;
            this["SkinButton", "Skins"] = SkinButton;
            this["MiscButton", "Misc"] = MiscButton;
            this["ProfileButton", "Profile"] = ProfileButton;

            this["VisualPanel", true] = new VisualPanel(Position, new Point(202, 37))
            {
                 Visible = true
            };
        }
    }
}