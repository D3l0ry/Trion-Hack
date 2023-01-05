using System.Drawing;

using Trion.Client.Configs;
using Trion.SDK.Surface;
using Trion.SDK.Surface.Controls;

namespace Trion.Client.Menu.Panels
{
    internal partial class VisualPanel : Panel
    {
        public VisualPanel(Point mainPosition, Point panelPosition) => Initialization(panelPosition, mainPosition);

        private void GlowEnable_MouseLeftClick(Control ClassEvent) => ConfigManager.CVisual.GlowEnable = ((CheckBox)ClassEvent).Checked = !((CheckBox)ClassEvent).Checked;

        private void GlowHPEnable_MouseLeftClick(Control ClassEvent) => ConfigManager.CVisual.GlowHPEnable = ((CheckBox)ClassEvent).Checked = !((CheckBox)ClassEvent).Checked;

        private void GlowFullBloomEnable_MouseLeftClick(Control ClassEvent) => ConfigManager.CVisual.GlowFullBloom = ((CheckBox)ClassEvent).Checked = !((CheckBox)ClassEvent).Checked;
    }
}