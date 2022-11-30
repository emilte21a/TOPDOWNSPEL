using Raylib_cs;
using System.Numerics;

const int screenWidth = 1024;
const int screenHeight = 1024;

Raylib.InitWindow(screenWidth, screenHeight, "Topdown game");
Raylib.SetTargetFPS(60);

List<enemyClass> enemies = new List<enemyClass>();
enemies.Add(new enemyClass());

enemies[0].enemyRec.y = 500;

TextureClass t = new(); 

RunningClass r = new();

int round = 1;

float speed = 4.5f;

int dmgTimer = 60;

int extragold = 1;

Random rnd = new Random();
int exitPosX = rnd.Next(512,1025);
int exitPosY = rnd.Next(512,1025);

Rectangle characterRec = new Rectangle(0, 60, t.charTextures[0].width, t.charTextures[0].height);

enemyClass enemyRec = new enemyClass();

Rectangle finish = new Rectangle(exitPosX, exitPosY, t.otherTextures[0].width, t.otherTextures[0].height);

Rectangle upgrade1 = new Rectangle(924, 0, 100, 100); //Rektangel för extra speed

Rectangle upgrade2 = new Rectangle(924, 150, 100, 100); //Rektangel för full hp

Rectangle upgrade3 = new Rectangle(924, 300, 100, 100); //Rektangel för extra guld per sekund

Rectangle nextround = new Rectangle(924, 924, 300, 100); //Rektangel för nästa runda

Vector2 position = new Vector2(40, 300);



//Kamera följer spelarens position
Camera2D camera;
camera.zoom = 1;
camera.rotation = 0;
camera.offset = new Vector2(screenWidth / 2, screenHeight / 2);

int gold = 0;

int hp = 100;

void HandleTimer() //Timerfunktion: ökar guld med 2 varje sekund
{
	gold = gold+extragold; //Guld ökar med extragold per sekund
}
System.Timers.Timer timer = new (interval: 1000); //Timer med intervallet 1 sekund
timer.Elapsed += ( sender, e ) => HandleTimer();

Color myColor = new Color(0, 200, 30, 225);

string currentScene = "start"; //start, game, gameover

//Fiende egenskaper
Vector2 enemyMovement = new Vector2(1, 0);
float enemySpeed = 2;


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
    Vector2 characterPos = new Vector2(characterRec.x, characterRec.y);
    camera.target = characterPos;
    dmgTimer--;
    if (dmgTimer == 0){ //Damage timer där fienden endast kan skada dig en gång varje sekund
        dmgTimer = 60;
    }
    

    if (currentScene == "game")
    {
        
        timer.Start();
        characterRec.x = walkingX(characterRec.x, speed);
        characterRec.y = walkingY(characterRec.y, speed);


        Vector2 playerPos = new Vector2(characterRec.x, characterRec.y);
        Vector2 fiendePos = new Vector2(enemyRec.enemyRec.x, enemyRec.enemyRec.y);
        Vector2 diff = playerPos - fiendePos;

        Vector2 fiendeDirection = Vector2.Normalize(diff);

        enemyMovement = fiendeDirection * enemySpeed;

        enemyRec.enemyRec.x += enemyMovement.X;
        enemyRec.enemyRec.y += enemyMovement.Y;

       

        if (Raylib.CheckCollisionRecs(characterRec, enemyRec.enemyRec) && dmgTimer == 1) //Gameover-scen när fienden och karaktären krockar
        {   
            hp=hp-25;
        }

        if (hp ==0)
        {
            currentScene = "gameover";
            enemyRec.enemyRec.x = 1024;
            enemyRec.enemyRec.y = 1024;
            hp=100;
            enemySpeed = 2;
            gold=0;
            speed= 4.5f;
            timer.Stop();
        }

        if (Raylib.CheckCollisionRecs(characterRec, finish) && gold >= 10)
        {   
            gold = gold - 10*round;
            timer.Stop();
            round++;
            enemySpeed += 0.2f;
            characterRec.x = 0;
            characterRec.y = 0;
            finish.x = rnd.Next(512, 1000);
            finish.y = rnd.Next(512, 1000);
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
        characterRec.x = walkingX(characterRec.x, speed);
        characterRec.y = walkingY(characterRec.y, speed);

        if (Raylib.CheckCollisionRecs(characterRec, upgrade1) && gold >= 50)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
            speed = speed + 0.2f;
            gold = gold - 50;
            }
        }

        if (Raylib.CheckCollisionRecs(characterRec, upgrade2) && gold >= 25 && hp<100)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
            hp = 100;
            gold = gold - 25;
            }
        }

        if (Raylib.CheckCollisionRecs(characterRec, upgrade3) && gold >= 50)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
            extragold++;
            gold = gold - 50;
            }
        }

        if (Raylib.CheckCollisionRecs(characterRec, nextround))
        {
            currentScene = "game";
            characterRec.x = 0;
            characterRec.y = 0;
            enemyRec.enemyRec.x = 1024;
            enemyRec.enemyRec.y = 1024;
        }
    }

    else
    {


        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            characterRec.x = 0;
            characterRec.y = 0;
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
        Raylib.BeginMode2D(camera); //Starta 2D läge

        Raylib.DrawTexture(t.backgroundTextures[2], 0, 0, Color.WHITE); //Spelbakgrund
        

        Raylib.DrawTexture(enemyRec.enemyTexture, (int)enemyRec.enemyRec.x, (int)enemyRec.enemyRec.y, Color.WHITE); //Fiende texturen

        Raylib.DrawTexture(t.charTextures[0], //Karaktär texturen
        (int)characterRec.x,
        (int)characterRec.y,
        Color.WHITE);

         if (Raylib.CheckCollisionRecs(characterRec, enemyRec.enemyRec) && dmgTimer == 1) 
        {   
           Raylib.DrawTexture(t.charTextures[0], (int)characterRec.x, (int)characterRec.y,Color.RED);
        }

        Raylib.DrawTexture(t.otherTextures[0], (int)finish.x, (int)finish.y, Color.WHITE);
        
        

        Raylib.EndMode2D(); //Tillåter texterna nedan att ha en fast positions på skärmen.

        Raylib.DrawText($"Round {round}", 400, 10, 30, Color.BLACK);

        Raylib.DrawText($"Gold:{gold}", 400, 50, 30, Color.BLACK);

        Raylib.DrawText($"HP:{hp}", 400, 90, 30, Color.BLACK);
    }

    else if (currentScene == "upgrade")
    {
        Raylib.DrawTexture(t.backgroundTextures[1], 0, 0, Color.WHITE );

        Raylib.DrawRectangle(924, 0, 100, 100, Color.GOLD); 
        Raylib.DrawRectangle(924, 150, 100, 100, Color.GOLD);
        Raylib.DrawRectangle(924, 300, 100, 100, Color.GOLD);
        Raylib.DrawRectangle(924, 924, 100, 100, Color.LIME);
        Raylib.DrawTexture(t.charTextures[0], (int)characterRec.x, (int)characterRec.y, Color.WHITE);
        Raylib.DrawText("Play next round", 600, 924, 20, Color.WHITE);
        Raylib.DrawText("Speed+0.2 (50 gold) Press SPACE to buy", 424, 50, 20, Color.WHITE);
        Raylib.DrawText("Full health (25 gold) Press SPACE to buy", 424, 200, 20, Color.WHITE);
        Raylib.DrawText("+1 gold/sec (50 gold) Press SPACE to buy", 424, 350, 20, Color.WHITE);
        Raylib.DrawText($"Gold: {gold}", 25, 40, 30, Color.WHITE);
        Raylib.DrawText($"Gold/Sec: {extragold}", 25, 80, 30, Color.WHITE);
        Raylib.DrawText($"Speed: {speed}", 25, 120, 30, Color.WHITE);
        Raylib.DrawText($"Health: {hp}", 25, 160, 30, Color.WHITE);
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