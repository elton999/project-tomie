using System;
using Microsoft.Xna.Framework;

namespace UmbrellaToolsKit
{
    public static class MathHelper
    {
        public static Vector2 Rotate(Vector2 vector, float angle)
        {
            angle = Microsoft.Xna.Framework.MathHelper.ToRadians(angle);

            float x = vector.X * (float)Math.Cos(angle) - vector.Y * (float)Math.Cos(angle);
            float y = vector.X * (float)Math.Sin(angle) + vector.Y * (float)Math.Cos(angle);

            vector = new Vector2(x, y);

            return vector;
        }

        public static float MilliSecondsToSeconds(float milliSecondsToSeconds) => milliSecondsToSeconds / 1000.0f;
    }
}