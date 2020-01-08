namespace Trion.SDK.Interfaces.Client
{
    internal unsafe struct IGlowObjectManager
    {
        #region Variables
        private uint Class_Base;
        #endregion

        #region Initializations
        public IGlowObjectManager(uint Class) => Class_Base = Class;
        #endregion

        #region Operators
        public static implicit operator IGlowObjectManager(uint Value) => new IGlowObjectManager(Value);

        public static implicit operator uint(IGlowObjectManager Value) => Value.Class_Base;
        #endregion

        public struct GlowColor
        {
            #region Variables
            public float Red;
            public float Green;
            public float Blue;
            public float Alpha;
            #endregion

            public GlowColor(float red, float green, float blue, float alpha)
            {
                Red = red;
                Green = green;
                Blue = blue;
                Alpha = alpha;
            }
        }

        public struct GlowObjectDefinition
        {
            void* entity;

            public GlowColor Color;

            fixed byte pad[4];

            float m_flSomeFloat;

            float bloomAmount;

            float m_flAnotherFloat;

            public bool renderWhenOccluded;

            public bool renderWhenUnoccluded;

            public bool fullBloomRender;

            byte pad1;

            int fullBloomStencilTestValue;

            public int glowStyle;

            int splitScreenSlot;

            int nextFreeSlot;
        };
    }
}