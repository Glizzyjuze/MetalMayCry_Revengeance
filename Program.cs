using System.Numerics;
using Raylib_cs;

Raylib.InitWindow(1920, 1080, "Game");
Raylib.SetTargetFPS(60);
Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);

Random generator = new Random();

string currentScene = "room1";

Rectangle avatar = new Rectangle(Raylib.GetScreenWidth() / 2 - 16, Raylib.GetScreenHeight() / 2 - 16, 32, 32);
Rectangle enemy = new Rectangle(Raylib.GetScreenWidth() / 2 - 16, Raylib.GetScreenHeight() / 2 - 16, 32, 32);

float speed = 8f;
float enemySpeed = 2;

List<Rectangle> walls = new();
List<Rectangle> wallsRoom1 = new List<Rectangle>();
List<Rectangle> wallsRoom2 = new List<Rectangle>();
List<Rectangle> wallsRoom3 = new List<Rectangle>();
List<Rectangle> wallsRoom3Alternate = new List<Rectangle>();
List<Rectangle> wallsRoom4 = new List<Rectangle>();
List<Rectangle> wallsRoom4Alternate = new List<Rectangle>();
List<Rectangle> wallsRoom5 = new List<Rectangle>();
List<Rectangle> wallsRoom5Alternate = new List<Rectangle>();
List<Rectangle> wallsBossRoom = new List<Rectangle>();

int thickness = 70;

wallsRoom1.Add(new Rectangle(0, 0, Raylib.GetScreenWidth(), thickness));
wallsRoom1.Add(new Rectangle(0, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth(), thickness));
wallsRoom1.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, 0, thickness, Raylib.GetScreenHeight()));
wallsRoom1.Add(new Rectangle(0, 0, thickness, Raylib.GetScreenHeight() - (Raylib.GetScreenHeight() / 3) * 2));
wallsRoom1.Add(new Rectangle(0, Raylib.GetScreenHeight() / 3 * 2, thickness, Raylib.GetScreenHeight()));

wallsRoom2.Add(new Rectangle(0, 0, Raylib.GetScreenWidth()/3, thickness));
wallsRoom2.Add(new Rectangle(Raylib.GetScreenWidth()/3 * 2, 0, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom2.Add(new Rectangle(0, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth(), thickness));
wallsRoom2.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, 0, thickness, Raylib.GetScreenHeight() - (Raylib.GetScreenHeight() / 3) * 2));
wallsRoom2.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, Raylib.GetScreenHeight() / 3 * 2, thickness, Raylib.GetScreenHeight() / 3));
wallsRoom2.Add(new Rectangle(0, 0, thickness, Raylib.GetScreenHeight()));

wallsRoom3.Add(new Rectangle(0, 0, Raylib.GetScreenWidth(), thickness));
wallsRoom3.Add(new Rectangle(0, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom3.Add(new Rectangle(Raylib.GetScreenWidth() / 3 * 2, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom3.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, 0, thickness, Raylib.GetScreenHeight()));
wallsRoom3.Add(new Rectangle(0, 0, thickness, Raylib.GetScreenHeight() / 3));
wallsRoom3.Add(new Rectangle(0, Raylib.GetScreenHeight() / 3 * 2, thickness, Raylib.GetScreenHeight() / 3));

wallsRoom3Alternate.Add(new Rectangle(0, 0, Raylib.GetScreenWidth(), thickness));
wallsRoom3Alternate.Add(new Rectangle(0, 0, thickness, Raylib.GetScreenHeight()));
wallsRoom3Alternate.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, 0, thickness, Raylib.GetScreenHeight() / 3));
wallsRoom3Alternate.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, Raylib.GetScreenHeight() / 3 * 2, thickness, Raylib.GetScreenHeight() / 3));
wallsRoom3Alternate.Add(new Rectangle(0, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom3Alternate.Add(new Rectangle(Raylib.GetScreenWidth() / 3 * 2, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth() / 3, thickness));

wallsRoom4.Add(new Rectangle(0, 0, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom4.Add(new Rectangle(Raylib.GetScreenWidth() / 3 * 2, 0, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom4.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, 0, thickness, Raylib.GetScreenHeight() / 3));
wallsRoom4.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, Raylib.GetScreenHeight() / 3 * 2, thickness, Raylib.GetScreenHeight() / 3));
wallsRoom4.Add(new Rectangle(0, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth(), thickness));
wallsRoom4.Add(new Rectangle(0, 0, thickness, Raylib.GetScreenHeight()));

wallsRoom4Alternate.Add(new Rectangle(0, 0, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom4Alternate.Add(new Rectangle(Raylib.GetScreenWidth() / 3 * 2, 0, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom4Alternate.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, 0, thickness, Raylib.GetScreenHeight()));
wallsRoom4Alternate.Add(new Rectangle(0, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth(), thickness));
wallsRoom4Alternate.Add(new Rectangle(0, 0, thickness, Raylib.GetScreenHeight() / 3));
wallsRoom4Alternate.Add(new Rectangle(0, Raylib.GetScreenHeight() / 3 * 2, thickness, Raylib.GetScreenHeight() / 3));

wallsRoom5.Add(new Rectangle(0, 0, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom5.Add(new Rectangle(Raylib.GetScreenWidth() / 3 * 2, 0, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom5.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, 0, thickness, Raylib.GetScreenHeight()));
wallsRoom5.Add(new Rectangle(Raylib.GetScreenWidth() / 3 * 2, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom5.Add(new Rectangle(0, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom5.Add(new Rectangle(0, 0, thickness, Raylib.GetScreenHeight()));

wallsRoom5Alternate.Add(new Rectangle(0, 0, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom5Alternate.Add(new Rectangle(Raylib.GetScreenWidth() / 3 * 2, 0, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom5Alternate.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, 0, thickness, Raylib.GetScreenHeight()));
wallsRoom5Alternate.Add(new Rectangle(Raylib.GetScreenWidth() / 3 * 2, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom5Alternate.Add(new Rectangle(0, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth() / 3, thickness));
wallsRoom5Alternate.Add(new Rectangle(0, 0, thickness, Raylib.GetScreenHeight()));

wallsBossRoom.Add(new Rectangle(0, 0, Raylib.GetScreenWidth(), thickness));
wallsBossRoom.Add(new Rectangle(Raylib.GetScreenWidth() - thickness, 0, thickness, Raylib.GetScreenHeight()));
wallsBossRoom.Add(new Rectangle(0, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth() / 3, thickness));
wallsBossRoom.Add(new Rectangle(Raylib.GetScreenWidth() / 3 * 2, Raylib.GetScreenHeight() - thickness, Raylib.GetScreenWidth() / 3, thickness));
wallsBossRoom.Add(new Rectangle(0, 0, thickness, Raylib.GetScreenHeight()));

int resultRoom3 = generator.Next(2);

Vector2 enemyMovement = new Vector2(1, 0);

while (!Raylib.WindowShouldClose())
{
    // LOGIK
    bool areOverlapping = false;

    Vector2 movement = Vector2.Zero;
    Vector2 enemyPos = new Vector2(enemy.x, enemy.y);
    Vector2 avatarPos = new Vector2(avatar.x, avatar.y);
    Vector2 diff = avatarPos - enemyPos;
    Vector2 enemyDirection = Vector2.Normalize(diff);
    enemyMovement = enemyDirection * enemySpeed;

    if (currentScene == "room1" || currentScene == "room2" || currentScene == "room3" || currentScene == "room4" || currentScene == "room5" || currentScene == "bossRoom")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -speed;
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement.Y = -speed;
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement.Y = speed;
        }

        avatar.x += movement.X;

        
        foreach (Rectangle wall in walls)
        {
            if (Raylib.CheckCollisionRecs(avatar, wall) == true)
            {
                areOverlapping = true;
            }
        }

        if (areOverlapping == true)
        {
            avatar.x -= movement.X;
        }

        avatar.y += movement.Y;

        areOverlapping = false;

        foreach (Rectangle wall in walls)
        {
            if (Raylib.CheckCollisionRecs(avatar, wall) == true)
            {
                areOverlapping = true;
            }
        }

        if (areOverlapping == true)
        {
            avatar.y -= movement.Y;
        }
    }

    //GRAFIK
    Raylib.BeginDrawing();

    if (currentScene == "room1")
    {
        walls = wallsRoom1;

        Raylib.ClearBackground(Color.BLACK);

        Raylib.DrawRectangleRec(avatar, Color.WHITE);


        foreach (Rectangle wall in walls)
        {
            Raylib.DrawRectangleRec(wall, Color.WHITE);
        }

        if (avatar.x <= 0)
        {
            currentScene = "room2";
            avatar.x = Raylib.GetScreenWidth() - 32;
            avatar.y = Raylib.GetScreenHeight() / 2 - 16;
        }
    }

    if (currentScene == "room2")
    {
        walls = wallsRoom2;

        Raylib.ClearBackground(Color.BLACK);

        Raylib.DrawRectangleRec(avatar, Color.WHITE);

        Raylib.DrawRectangleRec(enemy, Color.GRAY);

        enemy.x += enemyMovement.X;
        enemy.y += enemyMovement.Y;

        foreach (Rectangle wall in walls)
        {
            Raylib.DrawRectangleRec(wall, Color.WHITE);
        }

        if (avatar.y <= 0)
        {
            currentScene = "room3";
            avatar.x = Raylib.GetScreenWidth() / 2 - 16;
            avatar.y = Raylib.GetScreenHeight() - 32;
        }

        if (avatar.x >= Raylib.GetScreenWidth() - 31)
        {
            currentScene = "room1";
            avatar.x = 1;
            avatar.y = Raylib.GetScreenHeight() / 2 - 16;
        }
    }

    if (currentScene == "room3")
    {
        if (resultRoom3 == 0 || walls == wallsRoom4)
        {
            walls = wallsRoom3;
        }
        
        if (resultRoom3 == 1 || walls == wallsRoom4Alternate)
        {
            walls = wallsRoom3Alternate;
        }

        Raylib.ClearBackground(Color.BLACK);

        Raylib.DrawRectangleRec(avatar, Color.WHITE);

        foreach (Rectangle wall in walls)
        {
            Raylib.DrawRectangleRec(wall, Color.WHITE);
        }

        if (walls == wallsRoom3)
        {
            if (avatar.x <= 0)
            {
                currentScene = "room4";
                avatar.x = Raylib.GetScreenWidth() - 33;
                avatar.y = Raylib.GetScreenHeight() / 2 -16;
            }
        }

        if (walls == wallsRoom3Alternate)
        {
            if (avatar.x >= Raylib.GetScreenWidth() - 32)
            {
                currentScene = "room4";
                avatar.x = 1;
                avatar.y = Raylib.GetScreenHeight() / 2 - 16;
            }
        }

        if (avatar.y >= Raylib.GetScreenHeight() + 32)
        {
            currentScene = "room2";
            avatar.x = Raylib.GetScreenWidth() / 2 - 16;
            avatar.y = 1;
        }
    }

    if (currentScene == "room4")
    {
        if (walls == wallsRoom3 || walls == wallsRoom5)
        {
            walls = wallsRoom4;
        }

        if (walls == wallsRoom3Alternate || walls == wallsRoom5Alternate)
        {
            walls = wallsRoom4Alternate;
        }

        Raylib.ClearBackground(Color.BLACK);

        Raylib.DrawRectangleRec(avatar, Color.WHITE);

        foreach (Rectangle wall in walls)
        {
            Raylib.DrawRectangleRec(wall, Color.WHITE);
        }

        if (walls == wallsRoom4)
        {
            if (avatar.y <= 0)
            {
                currentScene = "room5";
                avatar.x = Raylib.GetScreenWidth() / 2 - 16;
                avatar.y = Raylib.GetScreenHeight() - 33;
            }

            if (avatar.x >= Raylib.GetScreenWidth() - 32)
            {
                currentScene = "room3";
                avatar.x = 1;
                avatar.y = Raylib.GetScreenHeight() / 2 - 16;
            }
        }

        if (walls == wallsRoom4Alternate)
        {
            if (avatar.y <= 0)
            {
                currentScene = "room5";
                avatar.x = Raylib.GetScreenWidth() / 2 - 16;
                avatar.y = Raylib.GetScreenHeight() - 33;
            }

            if (avatar.x <= 0)
            {
                currentScene = "room3";
                avatar.x = Raylib.GetScreenWidth() - 33;
                avatar.y = Raylib.GetScreenHeight() / 2 - 16;
            }
        }
    }

    if (currentScene == "room5")
    {
        if (walls == wallsRoom4)
        {
            walls = wallsRoom5;
        }

        if (walls == wallsRoom4Alternate)
        {
            walls = wallsRoom5Alternate;
        }

        Raylib.ClearBackground(Color.BLACK);

        Raylib.DrawRectangleRec(avatar, Color.WHITE);

        foreach (Rectangle wall in walls)
        {
            Raylib.DrawRectangleRec(wall, Color.WHITE);
        }

        if (avatar.y <= 0)
        {
            currentScene = "bossRoom";
            avatar.y = Raylib.GetScreenHeight() - 33;
            avatar.x = Raylib.GetScreenWidth() / 2 - 16;
        }

        if (avatar.y >= Raylib.GetScreenHeight() - 32)
        {
            currentScene = "room4";
            avatar.x = Raylib.GetScreenWidth() / 2 - 16;
            avatar.y = 1;
        }
    }

    if (currentScene == "bossRoom")
    {
        walls = wallsBossRoom;

        Raylib.ClearBackground(Color.BLACK);

        Raylib.DrawRectangleRec(avatar, Color.WHITE);

        foreach (Rectangle wall in walls)
        {
            Raylib.DrawRectangleRec(wall, Color.WHITE);
        }

        if (avatar.y >= Raylib.GetScreenHeight() - 32)
        {
            avatar.y -= movement.Y;
        }
    }

    Raylib.EndDrawing();
}