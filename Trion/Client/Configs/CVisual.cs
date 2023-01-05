using System;

namespace Trion.Client.Configs
{
    [Serializable]
    internal class CVisual
    {
        #region Glow
        public bool GlowEnable;
        public bool GlowHPEnable;

        public float Red;
        public float Green;
        public float Blue;
        public float Alpha;

        public bool GlowFullBloom;

        public int GlowStyle;
        #endregion

        #region Misc
        public bool Prime;
        public bool Radar;
        public bool NoFlash;
        public bool WaterMark;
        public bool RevealRanks;
        public float ViewModelFov = 30f;
        #endregion
    }
}