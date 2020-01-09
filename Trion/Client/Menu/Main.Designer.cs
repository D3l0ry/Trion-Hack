using System.Drawing;

using Trion.SDK.Interfaces;
using Trion.SDK.Surface.Drawing;

namespace Trion.Client.Menu
{
    internal partial class Main
    {
        private void Initialization()
        {
            Interface.Surface.GetScreenSize(out int ScreenWidth, out int ScreenHeight);

            Text = "TRION";
            Size = new Size(700, 350);
            Position = new Point((ScreenWidth / 2) - (Size.Width/2), (ScreenHeight / 2) - (Size.Height/2));
            Font = new Font("Impact", 25, Font.FontFlags.FONTFLAG_ANTIALIAS);
            BackColor = Color.FromArgb(38, 44, 52);
            KeyDown += Main_KeyDown;
        }
    }
}