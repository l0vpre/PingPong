using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Constants;

namespace PingPong;
class Program
{
    static void Main(string[] args)
    {
        int width = 1250;
        int height = 800;
        InitWindow(width, height, "PingPong");
        SetTargetFPS(60);
        Font font = LoadFont("Assets/BACKTO1982.TTF");
        GameScreen currentScreen = GameScreen.Menu;

        Rectangle player1 = new(100, 150, 30, 200);
        Rectangle player2 = new(width - 100, 150, 30, 200);
        Rectangle startPlayer1 = player1;
        Rectangle startPlayer2 = player2;

        Vector2 circleSpeed = new Vector2(6, 6);
        Circle circle = new(new Vector2(width / 2, height / 2), 30);
        Vector2 startCSpeed = circleSpeed;
        Circle startCircle = circle;

        bool collision = false;
        bool start = true;
        int score1 = 0;
        int score2 = 0;

        while (!WindowShouldClose())
        {
            switch (currentScreen)
            {
                case GameScreen.Menu:
                    {
                        if (IsKeyPressed(KeyboardKey.S))
                            currentScreen = GameScreen.Gameplay;
                    }
                    break;
                case GameScreen.Gameplay:
                    {
                        if (IsKeyPressed(KeyboardKey.Enter))
                            currentScreen = GameScreen.Pause;

                        if (circle.Pos.X <= circle.Radius || circle.Pos.X >= width - circle.Radius)
                            currentScreen = GameScreen.Gameover;

                        if (IsKeyDown(KeyboardKey.Up) && player1.Y > 0)
                            player1.Y -= 10;

                        if (IsKeyDown(KeyboardKey.Down) && player1.Y + player1.Height < height)
                            player1.Y += 10;

                        if (IsKeyDown(KeyboardKey.W) && player2.Y > 0)
                            player2.Y -= 10;

                        if (IsKeyDown(KeyboardKey.S) && player2.Y + player2.Height < height)
                            player2.Y += 10;

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

                        collision = CheckCollisionCircleRec(circle.Pos, circle.Radius, player1);
                        if (collision)
                        {
                            score1++;
                            circleSpeed.X = -circleSpeed.X;
                        }

                        collision = CheckCollisionCircleRec(circle.Pos, circle.Radius, player2);
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

            BeginDrawing();
            ClearBackground(Color.Beige);
            switch (currentScreen)
            {
                case GameScreen.Menu:
                    {
                        DrawTextEx(font, "PING PONG", new Vector2(200, 200), 50, 50, Color.Black);
                    }
                    break;
                case GameScreen.Gameplay:
                    {
                        DrawCircleV(circle.Pos, circle.Radius, Color.RayWhite);
                        DrawRectangleRec(player1, Color.RayWhite);
                        DrawRectangleRec(player2, Color.RayWhite);
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
            EndDrawing();
        }
        CloseWindow();
    }

    static float DegToRad(float degree)
    {
        return degree / 180 * PI;
    }
}



