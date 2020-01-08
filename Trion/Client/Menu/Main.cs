using System.Drawing;
using System.Threading;

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

            OldButton = ProfileNavigate;
        }

        private void Main_KeyHold(Control ClassEvent)
        {
            Visible = !Visible;

            Thread.Sleep(150);
        }

        private void VisualNavigate_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent);

        private void SkinChangerNavigate_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent);

        private void ProfileNavigate_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent);

        private void AimBotNavigate_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent);

        private void MiscNavigate_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent);

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