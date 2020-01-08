using System.Drawing;

using Trion.Client.Menu.Panels;
using Trion.SDK.Interfaces;
using Trion.SDK.Surface.Controls;
using Trion.SDK.Surface.Drawing;

namespace Trion.Client.Menu
{
    internal partial class Main
    {
        #region Labels
        private Label TrionLogo;
        private Label VisualNavigate;
        private Label SkinChangerNavigate;
        private Label ProfileNavigate;
        private Label AimBotNavigate;
        private Label MiscNavigate;
        #endregion

        #region Panels
        PProfile PProfile;
        #endregion

        private void Initialization()
        {
            Interface.Surface.GetScreenSize(out int ScreenWidth, out int ScreenHeight);

            TrionLogo = new Label();
            VisualNavigate = new Label();
            SkinChangerNavigate = new Label();
            ProfileNavigate = new Label();
            AimBotNavigate = new Label();
            MiscNavigate = new Label();

            TrionLogo.Position = new Point(25, 10);
            TrionLogo.ForeColor = Color.White;
            TrionLogo.Font = new Font();
            TrionLogo.Visible = true;

            ///Visual
            VisualNavigate.Size = new Size(100, 25);
            VisualNavigate.Position = new Point(140, -25);
            VisualNavigate.BackColor = Color.FromArgb(38, 44, 58);
            VisualNavigate.ForeColor = Color.White;
            VisualNavigate.Visible = false;
            VisualNavigate.MouseLeftClick += VisualNavigate_MouseLeftClick;

            ///SkinChanger
            SkinChangerNavigate.Size = new Size(100, 25);
            SkinChangerNavigate.Position = new Point(250, -25);
            SkinChangerNavigate.BackColor = Color.FromArgb(38, 44, 58);
            SkinChangerNavigate.ForeColor = Color.White;
            SkinChangerNavigate.Visible = false;
            SkinChangerNavigate.MouseLeftClick += SkinChangerNavigate_MouseLeftClick;

            ///Profile
            ProfileNavigate.Size = new Size(150, 25);
            ProfileNavigate.Position = new Point(360, -25);
            ProfileNavigate.BackColor = Color.FromArgb(38, 44, 58);
            ProfileNavigate.ForeColor = Color.FromArgb(152, 241, 221);
            ProfileNavigate.Visible = false;
            ProfileNavigate.MouseLeftClick += ProfileNavigate_MouseLeftClick;

            ///AimBot
            AimBotNavigate.Size = new Size(100, 25);
            AimBotNavigate.Position = new Point(520, -25);
            AimBotNavigate.BackColor = Color.FromArgb(38, 44, 58);
            AimBotNavigate.ForeColor = Color.White;
            AimBotNavigate.Visible = false;
            AimBotNavigate.MouseLeftClick += AimBotNavigate_MouseLeftClick;

            ///Misc
            MiscNavigate.Size = new Size(100, 25);
            MiscNavigate.Position = new Point(630, -25);
            MiscNavigate.BackColor = Color.FromArgb(38, 44, 58);
            MiscNavigate.ForeColor = Color.White;
            MiscNavigate.Visible = false;
            MiscNavigate.MouseLeftClick += MiscNavigate_MouseLeftClick;

            ///Main
            Name = "Main";
            Size = new System.Drawing.Size(700, 370);
            BackColor = Color.FromArgb(24, 25, 38);
            Position = new Point((ScreenWidth / 2) - (Size.Width / 2), (ScreenHeight / 2) - (Size.Height / 2));
            KeyHold += Main_KeyHold;

            this["TrionLogo", "TRION"] = TrionLogo;
            this["VisualNavigate", "Visual"] = VisualNavigate;
            this["SkinChangerNavigate", "SkinChanger"] = SkinChangerNavigate;
            this["ProfileNavigate", "Profile"] = ProfileNavigate;
            this["AimBotNavigate", "AimBot"] = AimBotNavigate;
            this["MiscNavigate", "Misc"] = MiscNavigate;

            this["CheckBox", "Check"] = new CheckBox()
            {
                BackColor = Color.FromArgb(24, 25, 38),
                Checked = true,
                Font = new Font("Tahoma", 5, Font.FontFlags.FONTFLAG_ITALIC),
                Position = new Point(25, 25),
                Size = new Size(35, 25),
                Visible = true
            };

            this["PProfile",true] = new PProfile(Position);
        }
    }
}