using System.Drawing;

using Trion.SDK.Surface;
using Trion.SDK.Surface.Controls;

namespace Trion.Client.Menu
{
    internal partial class Main : Form
    {
        private Control OldButton;
        private Panel OldPanel;

        public Main()
        {
            Initialization();

            OldButton = VisualButton;
            OldPanel = (Panel)this["VisualPanel"];
        }

        private void Main_KeyDown(Control ClassEvent) => Visible = !Visible;

        private void VisualButton_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent, (Panel)this["VisualPanel"]);

        private void ESPButton_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent, (Panel)this["VisualPanel"]);

        private void ChamsButton_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent, (Panel)this["VisualPanel"]);

        private void ProfileButton_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent, (Panel)this["VisualPanel"]);

        private void MiscButton_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent, (Panel)this["VisualPanel"]);

        private void SkinButton_MouseLeftClick(Control ClassEvent) => NavigateActive(ClassEvent, (Panel)this["VisualPanel"]);

        private void NavigateActive(Control Navigate, Panel PanelNavigate)
        {
            if (OldButton != Navigate)
            {
                OldButton.BackColor = Color.FromArgb(25, 30, 37);
                OldButton.ForeColor = Color.White;
                OldButton = Navigate;
            }

            if (OldPanel != PanelNavigate)
            {
                OldPanel.Visible = false;
                OldPanel = PanelNavigate;
            }

            Navigate.BackColor = Color.FromArgb(18, 21, 26);
            Navigate.ForeColor = Color.FromArgb(152, 241, 221);
            PanelNavigate.Visible = true;
        }
    }
}