﻿using Raylib_cs;
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
        GameScreen currentScreen = GameScreen.Gameplay;

        Rectangle player1 = new(100, 150, 30, 200);
        Rectangle player2 = new(width - 100, 150, 30, 200);
        Vector2 circleSpeed = new Vector2(1, 1);
        Circle circle = new(new Vector2(width / 2, height / 2), 30);


        bool collision = false;
        bool start = true;

        while (!WindowShouldClose())
        {
            switch (currentScreen)
            {
                case GameScreen.Menu:
                    {

                    }
                    break;
                case GameScreen.Gameplay:
                    {
                        if (IsKeyPressed(KeyboardKey.Enter))
                            currentScreen = GameScreen.Pause;

                        if (IsKeyDown(KeyboardKey.Up) && player1.Y > 0)
                            player1.Y -= 10;

                        if (IsKeyDown(KeyboardKey.Down) && player1.Y + player1.Height < height)
                            player1.Y += 10;

                        if (IsKeyDown(KeyboardKey.W) && player2.Y > 0)
                            player2.Y -= 10;

                        if (IsKeyDown(KeyboardKey.S) && player2.Y + player2.Height < height)
                            player2.Y += 10; ;

                        if (start)
                        {
                            Random rand = new Random();
                            VectorLA vec = VectorLA.FromVector2(circleSpeed);
                            int n = rand.Next(3);
                            //vec.Angle = rand.Next(43, 47) + 90 * n;
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

                            circleSpeed.X = -circleSpeed.X;
                        }
                        collision = CheckCollisionCircleRec(circle.Pos, circle.Radius, player2);
                        if (collision)
                            circleSpeed.X = -circleSpeed.X;
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

                    }
                    break;
            }

            BeginDrawing();
            ClearBackground(Color.Beige);
            switch (currentScreen)
            {
                case GameScreen.Menu:
                    {

                    }
                    break;
                case GameScreen.Gameplay:
                    {
                        DrawCircleV(circle.Pos, circle.Radius, Color.RayWhite);
                        DrawRectangleRec(player1, Color.RayWhite);
                        DrawRectangleRec(player2, Color.RayWhite);
                    }
                    break;
                case GameScreen.Pause:
                    {

                    }
                    break;
                case GameScreen.Gameover:
                    {

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



