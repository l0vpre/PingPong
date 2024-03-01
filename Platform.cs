using Raylib_cs;
namespace PingPong;

abstract class Platform
{
    public Rectangle platform;

    public Platform(Rectangle platform)
    {
        this.platform = platform;
    }

    public virtual void Update(float circleY)
    {
    }

}
