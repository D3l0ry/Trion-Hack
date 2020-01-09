using System.Drawing;

using Trion.SDK.Surface;
using Trion.SDK.Surface.Controls;

namespace Trion.Client.Menu
{
    internal partial class Main : Form
    {
        private Control OldButton;

        public Main()
        {
            Initialization();
        }

        private void Main_KeyDown(Control ClassEvent) => Visible = !Visible;

        private void NavigateActive(Control Navigate /*Panel NavigationPanel*/)
        {
            if (OldButton != Navigate)
            {
                OldButton.ForeColor = Color.White;
                OldButton = Navigate;
            }

            Navigate.ForeColor = Color.FromArgb(152, 241, 221);
            //NavigationPanel.Location = new Point(Navigate.Position.X, 25);
        }
    }
}