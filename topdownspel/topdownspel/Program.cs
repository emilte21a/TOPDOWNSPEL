using Raylib_cs;
using System.Numerics;

const int screenWidth = 800;
const int screenHeight = 600;

Raylib.InitWindow(screenWidth, screenHeight, "Topdown game");
Raylib.SetTargetFPS(60);

List<enemyClass> enemies = new List<enemyClass>();
enemies.Add(new enemyClass());

enemies[0].enemyRec.y = 500;

TextureClass t = new(); 

int round = 1;

float speed = 4.5f;

int dmgTimer = 60;

Rectangle character = new Rectangle(0, 60, t.charTextures[0].width, t.charTextures[0].height);

enemyClass enemyRec = new enemyClass();

Rectangle finish = new Rectangle(750, 300, t.otherTextures[0].width, t.otherTextures[0].height);

Rectangle upgrade1 = new Rectangle(700, 0, 300, 100); //Rektangel för upgradering

Rectangle nextround = new Rectangle(700, 500, 300, 100); //Rektangel för nästa runda

Vector2 position = new Vector2(40, 300);

//Kamera följer spelarens position
Camera2D camera;
camera.zoom = 1;
camera.rotation = 0;
camera.offset = new Vector2(800 / 2, 600 / 2);

int gold = 0;

int hp = 100;

int i = 0;

void HandleTimer()
{
	gold = gold+2;
}
System.Timers.Timer timer = new (interval: 1000); //Timer med intervallet 1 sekund
timer.Elapsed += ( sender, e ) => HandleTimer();



Color myColor = new Color(0, 200, 30, 225);

string currentScene = "start"; //start, game, gameover

Vector2 fiendeMovement = new Vector2(1, 0);
float fiendeSpeed = 2;


float walkingX(float characterx, float speed)
{
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && characterx < (screenWidth - 80))
    {
        characterx += speed;
        
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && characterx > 0)
    {
        characterx -= speed;
    
    }
    return characterx;
}

static float walkingY(float charactery, float speed)
{ //Metod för player movement. Görs i en metod så att jag slipper göra onödig mängd kod
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && charactery > 5)
    {

        charactery -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && charactery + 80 < screenHeight)
    {

        charactery += speed;
    }
    return charactery;
}

while (Raylib.WindowShouldClose() == false)
{

    //LOGIK
    Vector2 characterPos = new Vector2(character.x, character.y);
    camera.target = characterPos;
    dmgTimer--;
    if (dmgTimer == 0){
        dmgTimer = 60;
    }
    

    if (currentScene == "game")
    {
        timer.Start();
        character.x = walkingX(character.x, speed);
        character.y = walkingY(character.y, speed);


        Vector2 playerPos = new Vector2(character.x, character.y);
        Vector2 fiendePos = new Vector2(enemyRec.enemyRec.x, enemyRec.enemyRec.y);
        Vector2 diff = playerPos - fiendePos;

        Vector2 fiendeDirection = Vector2.Normalize(diff);

        fiendeMovement = fiendeDirection * fiendeSpeed;

        enemyRec.enemyRec.x += fiendeMovement.X;
        enemyRec.enemyRec.y += fiendeMovement.Y;


        if (Raylib.CheckCollisionRecs(character, enemyRec.enemyRec) && dmgTimer == 1) //Gameover-scen när fienden och karaktären krockar
        {   
            hp=hp-25;
        }

        if (hp ==0)
        {
            currentScene = "gameover";
            hp=100;
            fiendeSpeed = 2;
            gold=0;
            speed= 4.5f;
            timer.Stop();
        }

        if (Raylib.CheckCollisionRecs(character, finish) && gold >= 10)
        {   
            gold = gold - 10*round;
            timer.Stop();
            round++;
            fiendeSpeed += 0.2f;
            currentScene = "upgrade";
        }

    }

    else if (currentScene == "start")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            currentScene = "game";
        }
    }

    else if (currentScene == "upgrade")
    {
        character.x = walkingX(character.x, speed);
        character.y = walkingY(character.y, speed);

        if (Raylib.CheckCollisionRecs(character, upgrade1) && gold >= 50)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
            speed = speed + 0.2f;
            gold = gold - 50;
            }
        }

        if (Raylib.CheckCollisionRecs(character, nextround))
        {
            currentScene = "game";
            character.x = 0;
            character.y = 0;
            enemyRec.enemyRec.x = 800;
            enemyRec.enemyRec.y = 600;
        }
    }

    else
    {


        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            character.x = 0;
            character.y = 0;
            enemyRec.enemyRec.x = 300;
            enemyRec.enemyRec.y = 300;
            currentScene = "start";
        }
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_ESCAPE))
    {
        break;
    }

    //=============================GRAFIK===============================

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.WHITE);


    if (currentScene == "game")
    {
        Raylib.BeginMode2D(camera);

        Raylib.DrawTexture(t.backgroundTextures[2], 0, 0, Color.WHITE);

        

        
        
        Raylib.DrawTexture(t.charTextures[1], (int)enemyRec.enemyRec.x, (int)enemyRec.enemyRec.y, Color.WHITE);

        Raylib.DrawTexture(t.charTextures[0],
        (int)character.x,
        (int)character.y,
        Color.WHITE);

         if (Raylib.CheckCollisionRecs(character, enemyRec.enemyRec) && dmgTimer == 1)
        {   
           Raylib.DrawTexture(t.charTextures[0], (int)character.x, (int)character.y,Color.RED);
        }

        Raylib.DrawTexture(t.otherTextures[0], 750, 300, Color.WHITE);

        Raylib.EndMode2D();

        Raylib.DrawText($"Round {round}", 400, 10, 30, Color.BLACK);

        Raylib.DrawText($"Gold:{gold}", 400, 50, 30, Color.BLACK);

        Raylib.DrawText($"HP:{hp}", 400, 90, 30, Color.BLACK);
    }

    else if (currentScene == "upgrade")
    {
        Raylib.DrawTexture(t.backgroundTextures[1], 0, 0, Color.WHITE );
        Raylib.DrawRectangle(700, 0, 300, 100, Color.GOLD);
        Raylib.DrawRectangle(700, 500, 300, 100, Color.LIME);
        Raylib.DrawTexture(t.charTextures[0], (int)character.x, (int)character.y, Color.WHITE);
        Raylib.DrawText("Play next round", 600, 450, 20, Color.BLACK);
        Raylib.DrawText("Increase speed with 0.2 (50 gold) Press SPACE to buy", 200, 100, 20, Color.WHITE);
        Raylib.DrawText($"Gold:{gold}", 50, 70, 30, Color.WHITE);
        Raylib.DrawText($"Speed:{speed}", 50, 110, 30, Color.WHITE);
    }

    else if (currentScene == "start")
    {
        Raylib.DrawTexture(t.backgroundTextures[0], 0, 0, Color.WHITE);
        Raylib.DrawText("Zombie Runner", 260, 225, 50, Color.ORANGE);
        Raylib.DrawText("Press SPACE to start", 270, 280, 20, Color.ORANGE);
        Raylib.DrawText("Press ESCAPE to quit", 30, 20, 20, Color.RED);
    }


    else
    {
        
        Raylib.DrawText("You lost!", 300, 225, 30, Color.ORANGE);
        Raylib.DrawText("Press ENTER to start again", 300, 260, 15, Color.ORANGE);
    }


    Raylib.EndDrawing();
}