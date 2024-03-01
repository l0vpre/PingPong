using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Constants;
namespace PingPong;

class Game
{
    public int width => 1250;
    public int height => 800;
    Font font;
    public GameScreen currentScreen;

    Platform startPlayer1;
    Platform startPlayer2;
    Platform player1;
    Platform player2;

    Vector2 startCSpeed;
    Circle startCircle;
    Vector2 circleSpeed;
    public Circle circle;

    bool collision;
    bool mode;
    bool start;
    int score1;
    int score2;

    public Game()
    {
        currentScreen = GameScreen.Menu;
        startPlayer1 = new Player(new(100, 150, 30, 200), KeyboardKey.Up, KeyboardKey.Down);
        player1 = startPlayer1;
        startPlayer2 = new Player(new(width - 100, 150, 30, 200), KeyboardKey.W, KeyboardKey.S);
        player2 = startPlayer2;
        startCSpeed = new Vector2(5, 5);
        startCircle = new(new Vector2(width / 2, height / 2), 30);
        circleSpeed = startCSpeed;
        circle = startCircle;
        collision = false;
        start = true;
        score1 = 0;
        score2 = 0;
        mode = false;
    }

    public void Run()
    {
        while (!WindowShouldClose())
        {
            Update();
            BeginDrawing();
            Draw();
            EndDrawing();
        }
        CloseWindow();
    }

    public void Setup()
    {
        InitWindow(width, height, "PingPong");
        SetTargetFPS(60);
        Load();
    }
    public void Load()
    {
        font = LoadFont("Assets/BACKTO1982.TTF");
    }


    public void Update()
    {
        switch (currentScreen)
        {
            case GameScreen.Menu:
                {
                    if (IsKeyPressed(KeyboardKey.S))
                    {
                        mode = false;
                        currentScreen = GameScreen.Gameplay;
                        startPlayer2 = new Bot(new(width - 100, 150, 30, 200));
                        player2 = startPlayer2;
                    }
                    else if (IsKeyPressed(KeyboardKey.M))
                    {
                        mode = true;
                        currentScreen = GameScreen.Gameplay;
                        startPlayer2 = new Player(new(width - 100, 150, 30, 200), KeyboardKey.W, KeyboardKey.S);
                        player2 = startPlayer2;
                    }
                }
                break;
            case GameScreen.Gameplay:
                {
                    if (IsKeyPressed(KeyboardKey.Enter))
                        currentScreen = GameScreen.Pause;

                    if (circle.Pos.X <= circle.Radius || circle.Pos.X >= width - circle.Radius)
                        currentScreen = GameScreen.Gameover;

                    player1.Update(0);
                    player2.Update(circle.Pos.Y);


                    if (start)
                    {
                        Random rand = new Random();
                        VectorLA vec = VectorLA.FromVector2(circleSpeed);
                        int n = rand.Next(3);
                        vec.Angle = rand.NextSingle() * DegToRad(4) + DegToRad(43) + PI / 2 * n;
                        circleSpeed = VectorLA.ToVector2(vec);
                        start = false;
                    }
                    circle.Pos.Y += circleSpeed.Y;
                    circle.Pos.X += circleSpeed.X;

                    if (circle.Pos.X <= 0 + circle.Radius || circle.Pos.X >= width - circle.Radius)
                        circleSpeed.X = -circleSpeed.X;

                    if (circle.Pos.Y <= 0 + circle.Radius || circle.Pos.Y >= height - circle.Radius)
                        circleSpeed.Y = -circleSpeed.Y;

                    collision = CheckCollisionCircleRec(circle.Pos, circle.Radius, player1.platform);
                    if (collision)
                    {
                        score1++;
                        circleSpeed.X = -circleSpeed.X;
                    }

                    collision = CheckCollisionCircleRec(circle.Pos, circle.Radius, player2.platform);
                    if (collision)
                    {
                        score2++;
                        circleSpeed.X = -circleSpeed.X;
                    }
                }
                break;
            case GameScreen.Pause:
                {
                    if (IsKeyPressed(KeyboardKey.Enter))
                        currentScreen = GameScreen.Gameplay;
                }
                break;
            case GameScreen.Gameover:
                {
                    if (IsKeyPressed(KeyboardKey.R))
                    {
                        circle = startCircle;
                        circleSpeed = startCSpeed;
                        player1 = startPlayer1;
                        player2 = startPlayer2;
                        score1 = 0;
                        score2 = 0;
                        start = true;
                        currentScreen = GameScreen.Gameplay;
                    }
                }
                break;
        }
    }

    public void Draw()
    {
        ClearBackground(Color.Beige);
        switch (currentScreen)
        {
            case GameScreen.Menu:
                {

                    DrawTextEx(font, "PING PONG", new Vector2(200, 200), 50, 50, Color.Black);
                    DrawTextEx(font, "single mode (s)", new Vector2(200, 350), 20, 10, Color.Black);
                    DrawTextEx(font, "multiplayer (m)", new Vector2(200, 450), 20, 10, Color.Black);
                }
                break;
            case GameScreen.Gameplay:
                {
                    DrawCircleV(circle.Pos, circle.Radius, Color.RayWhite);
                    DrawRectangleRec(player1.platform, Color.RayWhite);
                    DrawRectangleRec(player2.platform, Color.RayWhite);
                    DrawFPS(0, 0);
                    DrawTextEx(font, score1.ToString(), new Vector2(150, 50), 25, 20, Color.Brown);
                    DrawTextEx(font, score2.ToString(), new Vector2(550, 50), 25, 20, Color.Brown);

                }
                break;
            case GameScreen.Pause:
                {
                    DrawTextEx(font, "Pause", new Vector2(200, 200), 50, 20, Color.DarkGray);
                }
                break;
            case GameScreen.Gameover:
                {
                    DrawTextEx(font, "Gameover", new Vector2(200, 200), 50, 20, Color.Red);
                    DrawTextEx(font, "Restart? (R)", new Vector2(200, 300), 30, 15, Color.DarkGray);
                }
                break;
        }
    }

    static float DegToRad(float degree)
    {
        return degree / 180 * PI;
    }


}
