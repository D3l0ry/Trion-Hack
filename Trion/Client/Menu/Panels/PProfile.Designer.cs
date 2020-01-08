using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Trion.SDK.Surface.Controls;

namespace Trion.Client.Menu.Panels
{
    internal partial class PProfile
    {
        #region Panel
        private Panel Info;
        #endregion

        private void Initialization(Point Pos)
        {
            Info = new Panel();

            ///Info
            Info.Size = new System.Drawing.Size(600, 180);
            Info.BackColor = Color.FromArgb(34, 47, 65);
            Info.Position = new Point(260, 10);
            Info.Visible = true;

            ///PProfile
            Size = new System.Drawing.Size(870, 380);
            BackColor = Color.FromArgb(38, 44, 58);
            Position = Pos;
            Visible = false;
            this["Info"] = Info;
            this["CheckBox"] = new CheckBox()
            {
                Size = new System.Drawing.Size(600, 180),
                BackColor = Color.Coral,
                Position = new Point(300, 20),
                Visible = true,
                Checked = true
            };
        }
    }
}