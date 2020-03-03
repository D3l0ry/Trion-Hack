using System;
using System.Collections.Generic;
using System.Text;
using Trion.SDK.Interfaces;
using Trion.SDK.Structures.Numerics;

namespace Trion.SDK.Structures.Modules
{
    internal static unsafe class Screen
    {
        public static bool WorldToScreen(Vector3 from, Vector3 to, Vector3 position, float* viewMatrix)
        {
            Interface.VEngineClient.GetScreenSize(out int Width, out int Height);

            to.X = viewMatrix[0] * from.X + viewMatrix[1] * from.Y + viewMatrix[2] * from.Z + viewMatrix[3];
            to.Y = viewMatrix[4] * from.X + viewMatrix[5] * from.Y + viewMatrix[6] * from.Z + viewMatrix[7];

            float World = viewMatrix[12] * from.X + viewMatrix[13] * from.Y + viewMatrix[14] * from.Z + viewMatrix[15];

            if (World < 0.01f)
            {
                return false;
            }

            to.X *= (1.0f / World);
            to.Y *= (1.0f / World);


            float X = Width / 2;
            float Y = Height / 2;

            X += 0.5f * to.X * Width + 0.5f;
            Y -= 0.5f * to.Y * Height + 0.5f;

            position.X = to.X = X;
            position.Y = to.Y = Y;

            return true;
        }

        public static Vector3 WorldToScreen(Vector3 target, float* viewMatrix)
        {
            Interface.VEngineClient.GetScreenSize(out int ScreenWidth, out int ScreenHeight);

            Vector3 WorldToScreenPosition;
            Vector3 To;

            float W = 0.0f;

            To.X = viewMatrix[0] * target.X + viewMatrix[1] * target.Y + viewMatrix[2] * target.Z + viewMatrix[3];
            To.Y = viewMatrix[4] * target.X + viewMatrix[5] * target.Y + viewMatrix[6] * target.Z + viewMatrix[7];
            To.Z = 0.0f;

            W = viewMatrix[12] * target.X + viewMatrix[13] * target.Y + viewMatrix[14] * target.Z + viewMatrix[15];

            if (W < 0.01f)
            {
                return new Vector3(0, 0,0);
            }

            To.X *= (1.0f / W);
            To.Y *= (1.0f / W);

            int Width = ScreenWidth;
            int Height = ScreenHeight;

            float X = Width / 2;
            float Y = Height / 2;

            X += 0.5f * To.X * Width + 0.5f;
            Y -= 0.5f * To.Y * Height + 0.5f;

            To.X = X;
            To.Y = Y;

            WorldToScreenPosition.X = To.X;
            WorldToScreenPosition.Y = To.Y;
            WorldToScreenPosition.Z = To.Z;

            return WorldToScreenPosition;
        }

        public static bool IsNullVector(this Vector3 player) => player == new Vector3(0, 0, 0) ? true : false;
    }
}