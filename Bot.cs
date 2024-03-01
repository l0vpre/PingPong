using Raylib_cs;
namespace PingPong;

class Bot : Platform
{
    public Bot(Rectangle rec) : base(rec)
    {
    }

    public override void Update(float circleY)
    {
        platform.Y = circleY - (platform.Height / 2);
        if (platform.Y + platform.Height > 800)
            platform.Y = 800 - platform.Height;

        if (platform.Y < 0)
            platform.Y = 0;
    }

}
