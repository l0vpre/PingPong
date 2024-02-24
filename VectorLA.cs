using System.Numerics;
using static Constants;

namespace PingPong;

struct VectorLA
{
    public float Length;
    public float Angle;

    public VectorLA(float length, float angle)
    {
        Length = length;
        Angle = angle;
    }

    public static VectorLA FromVector2(Vector2 vec)
    {
        float length = (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
        float angle = (float)Math.Atan(vec.Y / vec.X);
        if (vec.X < 0 && vec.Y > 0)
            angle += PI;

        if (vec.X < 0 && vec.Y < 0)
            angle -= PI;

        return new VectorLA(length, angle);
    }

    public static Vector2 ToVector2(VectorLA vec)
    {
        float x = vec.Length * (float)Math.Cos(vec.Angle);
        float y = vec.Length * (float)Math.Sin(vec.Angle);
        return new Vector2(x, y);
    }
}

