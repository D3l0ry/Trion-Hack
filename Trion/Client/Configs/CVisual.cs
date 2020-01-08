using System;

using Trion.SDK.Interfaces.Client;

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

        public bool FullBloom;

        public int GlowStyle;
        #endregion

        #region Misc
        public bool Radar;
        public bool BunnyHop = true;
        public bool AutoStrafe = true;
        public bool RevealRanks;
        public bool NoFlash;
        public bool MoonWalk;
        public float ViewModelFov;
        public bool WaterMark = true;
        #endregion

        #region Initializations
        public CVisual()
        {
            Red = 255 / 255f;
            Green = 0 / 255f;
            Blue = 0 / 255f;
            Alpha = 255 / 255f;
        }
        #endregion

        #region Operators
        public static implicit operator IGlowObjectManager.GlowColor(CVisual Value) => new IGlowObjectManager.GlowColor(Value.Red, Value.Green, Value.Blue, Value.Alpha);
        #endregion
    }
}