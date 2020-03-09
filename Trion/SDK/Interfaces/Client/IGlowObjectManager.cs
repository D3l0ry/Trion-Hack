using System.Runtime.InteropServices;

using Trion.Client.Configs;

namespace Trion.SDK.Interfaces.Client
{
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct IGlowObjectManager
    {
        public struct GlowObjectDefinition
        {
            public void* entity;

            public float Red;
            public float Green;
            public float Blue;
            public float Alpha;

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

        [FieldOffset(0x0)]
        private GlowObjectDefinition* GlowObjectDefinitions;

        [FieldOffset(0xC)]
        public int Size;

        public void SetEntity(int index, void* entity) => GlowObjectDefinitions[index].entity = entity;

        public void SetColor(int index, bool isHealth = false, int health = 0)
        {
            if(isHealth)
            {
                float hp = health / 100f;

                GlowObjectDefinitions[index].Red = 1 - hp;
                GlowObjectDefinitions[index].Green = hp;
                GlowObjectDefinitions[index].Blue = 0;
            }
            else
            {
                GlowObjectDefinitions[index].Red = ConfigManager.CVisual.Red;
                GlowObjectDefinitions[index].Green = ConfigManager.CVisual.Green;
                GlowObjectDefinitions[index].Blue = ConfigManager.CVisual.Blue;
            }

            GlowObjectDefinitions[index].Alpha = ConfigManager.CVisual.Alpha;
        }

        public void SetRenderFlags(int index,bool occuluded, bool unocculuded)
        {
            GlowObjectDefinitions[index].renderWhenOccluded = occuluded;
            GlowObjectDefinitions[index].renderWhenUnoccluded = unocculuded;
        }
    }
}