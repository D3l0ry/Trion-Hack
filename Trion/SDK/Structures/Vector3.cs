﻿using System.Runtime.InteropServices;

namespace Trion.SDK.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Vector3
    {
        public float X, Y, Z;

        public Vector3(float Value) : this(Value, Value, Value) { }

        public Vector3(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public static Vector3 Zero => new Vector3();

        public static Vector3 UnitX => new Vector3(1.0f, 0f, 0f);

        public static Vector3 UnitY => new Vector3(0f, 1.0f, 0f);

        public static Vector3 UnitZ => new Vector3(0f, 0f, 1.0f);

        public void NormalizeAngle()
        {
            if (X > 89.0f)
            {
                X = 89.0f;
            }

            if (X < -89.0f)
            {
                X = -89.0f;
            }

            while (Y > 180.0f)
            {
                Y = 180.0f;
            }

            while (Y < -180.0f)
            {
                Y = -180.0f;
            }
        }

        public bool Equals(Vector3 Other) => X == Other.X && Y == Other.Y && Z == Other.Z;

        public override bool Equals(object obj) => (object)this == obj;

        public static Vector3 operator +(Vector3 One, Vector3 Two) => new Vector3(One.X + Two.X, One.Y + Two.Y, One.Z + Two.Z);

        public static Vector3 operator -(Vector3 One) => Zero - One;

        public static Vector3 operator -(Vector3 One, Vector3 Two) => new Vector3(One.X - Two.X, One.Y - Two.Y, One.Z - Two.Z);

        public static Vector3 operator *(Vector3 One, Vector3 Two) => new Vector3(One.X * Two.X, One.Y * Two.Y, One.Z * Two.Z);

        public static Vector3 operator *(Vector3 One, float Two) => One * new Vector3(Two);

        public static Vector3 operator *(float One, Vector3 Two) => new Vector3(One) * Two;

        public static Vector3 operator /(Vector3 One, Vector3 Two) => new Vector3(One.X / Two.X, One.Y / Two.Y, One.Z / Two.Z);

        public static Vector3 operator /(Vector3 One, float Two)
        {
            float Div = 1.0f / Two;

            return new Vector3(One.X * Div, One.Y * Div, One.Z * Div);
        }

        public static bool operator ==(Vector3 One, Vector3 Two) => One.X == Two.X && One.Y == Two.Y && One.Z == Two.Z;

        public static bool operator !=(Vector3 One, Vector3 Two) => One.X != Two.X || One.Y != Two.Y || One.Z != Two.Z;
    }
}