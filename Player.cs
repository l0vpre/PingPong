using Raylib_cs;
using static Raylib_cs.Raylib;
namespace PingPong;

class Player : Platform
{
    KeyboardKey Up;
    KeyboardKey Down;
    public Player(Rectangle rectangle, KeyboardKey up, KeyboardKey down) : base(rectangle)
    {
        Up = up;
        Down = down;
    }

    public override void Update(float circleY)
    {
        if (IsKeyDown(Up) && platform.Y > 0)
            platform.Y -= 10;

        if (IsKeyDown(Down) && platform.Y + platform.Height < 800) //height
            platform.Y += 10;
    }
}
