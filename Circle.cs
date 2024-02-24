using System.Numerics;
namespace PingPong;

struct Circle
{
    public Vector2 Pos;
    public float Radius;

    public Circle(Vector2 pos, float radius)
    {
        Pos = pos;
        Radius = radius;
    }
}
