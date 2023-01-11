using Raylib_cs;
using System.Numerics;

const int screenWidth = 1080;
const int screenHeight = 1080;

Raylib.InitWindow(screenWidth, screenHeight, "Topdown game");
Raylib.SetTargetFPS(60);

int round = 1;

int enemycount = round;

var enemies = new List<enemyClass>();


for (int i = 0; i == enemycount; i++)
{
    enemies.Add(new enemyClass());
}


TextureClass t = new(); 

RunningClass r = new();


float speed = 4.5f;

int dmgTimer = 60;

int extragold = 1;

Random rnd = new Random();
int exitPosX = rnd.Next(512,944);
int exitPosY = rnd.Next(512,944);

Rectangle characterRec = new Rectangle(0, 60, t.charTextures[0].width, t.charTextures[0].height);

enemyClass enemyRec = new enemyClass();

Rectangle finish = new Rectangle(exitPosX, exitPosY, t.otherTextures[0].width, t.otherTextures[0].height);

Rectangle upgrade1 = new Rectangle(150, 700, 100, 100); //Rektangel för extra speed

Rectangle upgrade2 = new Rectangle(300, 700, 100, 100); //Rektangel för full hp

Rectangle upgrade3 = new Rectangle(450, 700, 100, 100); //Rektangel för extra guld per sekund

Rectangle nextround = new Rectangle(600, 700, 300, 100); //Rektangel för nästa runda

Vector2 position = new Vector2(40, 300);





//Kamera följer spelarens position
Camera2D camera = new();
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

float timer1 = 0.0f;
int frame = 0;
int maxFrames = (8);

void runningLogic() //Bestämmer vilken frame som ska visas under springanimationen
{
    timer1 += 0.06f;
        
    if (timer1 > 0.4f)
    {
    timer1 = 0.0f;
    frame +=1;
        }
    frame = frame % maxFrames;

}


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
{ //Metod för player movement. Görs i en metod så att jag slipper göra onödig mängd kod varje gång jag vill att karaktären ska röra på sig
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && charactery > 8)
    {
        charactery -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && charactery + 80 < screenHeight)
    {

        charactery += speed;
    }
    return charactery;
}

static float skipX(float characterx)
{
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
    {
        characterx += 150;
        if (characterx > 600 )
    {
        characterx = 150;
    }
    }
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
    {
        characterx -= 150;

        if (characterx < 150)
    {
        characterx = 600;
    }
    }
    return characterx;
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

            enemySpeed += 0.5f;
            characterRec.x = 150;
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
        characterRec.y = 700;
        characterRec.x = skipX(characterRec.x);

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
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
            currentScene = "game";
            characterRec.x = 0;
            characterRec.y = 0;
            enemyRec.enemyRec.x = 1024;
            enemyRec.enemyRec.y = 1024;
            }
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
        
        Console.WriteLine(enemycount);
        foreach (var enemy in enemies)
        {
        Raylib.DrawTexture(enemyRec.enemyTexture, (int)enemyRec.enemyRec.x, (int)enemyRec.enemyRec.y, Color.WHITE); //Fiende texturen
            
        }
        

        
        runningLogic(); //Funktion för hur snabbt rektangeln ska röra på sig

        Rectangle sourceRec = new Rectangle(80*frame, 0, 80, 80); //Source rektangel för karaktäranimation under rörelse
        Rectangle sourceRec1 = new Rectangle(80*frame, 0, -80, 80); //Source rektangel för karaktär under rörelse i motsatt riktning

        //Nedan är själva animationen som använder en sourcerec för att rita ut specifika positioner på ett spritesheet
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            Raylib.DrawTextureRec(r.runningTexture, sourceRec, characterPos, Color.WHITE);
        }
        
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            Raylib.DrawTextureRec(r.runningTexture, sourceRec1, characterPos, Color.WHITE);
        }

        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            Raylib.DrawTextureRec(r.runningTexture2, sourceRec, characterPos, Color.WHITE);

        }

        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            Raylib.DrawTextureRec(r.runningTexture3, sourceRec, characterPos, Color.WHITE);
        }

        
        //Idle karaktär texturen som ritas ut när man inte rör sig
        else
        {
        Raylib.DrawTexture(t.charTextures[0], //Karaktär texturen
        (int)characterRec.x,
        (int)characterRec.y,
        Color.WHITE);
        }


        //Kollar ifall karaktären och fienden har kolliderat och att dmgtimer är 1. Detta gör så att man endast kan ta skada en gång i sekunden
        if (Raylib.CheckCollisionRecs(characterRec, enemyRec.enemyRec) && dmgTimer == 1) 
        {   
           Raylib.DrawTexture(t.charTextures[0], (int)characterRec.x, (int)characterRec.y,Color.RED);
        }

        Raylib.DrawTexture(t.otherTextures[0], (int)finish.x, (int)finish.y, Color.WHITE);
        
        

        Raylib.EndMode2D(); //Tillåter texterna nedan att ha en fast positions på skärmen.

        Raylib.DrawTexture(t.otherTextures[2], 760, 900, Color.WHITE);
        Raylib.DrawTexture(t.otherTextures[3], 400, 0, Color.WHITE);

        Raylib.DrawText($"{round}", 550, 40, 30, Color.BLACK);

        Raylib.DrawText($"Gold:{gold}", 850, 990, 30, Color.BLACK);

        Raylib.DrawText($"HP:{hp}", 850, 940, 30, Color.BLACK);
    }

    else if (currentScene == "upgrade")
    {
        Raylib.DrawTexture(t.backgroundTextures[1], 0, 0, Color.WHITE ); //Bakgrundstextur

        Raylib.DrawRectangle(150, 700, 100, 100, Color.GOLD); 
        Raylib.DrawText("1", 200, 750, 20, Color.WHITE);

        Raylib.DrawRectangle(300, 700, 100, 100, Color.GOLD);
        Raylib.DrawText("2", 350, 750, 20, Color.WHITE);

        Raylib.DrawRectangle(450, 700, 100, 100, Color.GOLD);
        Raylib.DrawText("3", 500, 750, 20, Color.WHITE);

        Raylib.DrawRectangle(600, 700, 100, 100, Color.LIME);
        Raylib.DrawText("4", 650, 750, 20, Color.WHITE);

        Raylib.DrawTexture(t.otherTextures[1], (int)characterRec.x, (int)characterRec.y, Color.WHITE);
        Raylib.DrawText("1. Speed+0.2 (50 gold) Press SPACE to buy", 424, 150, 20, Color.WHITE);
        Raylib.DrawText("2. Full health (25 gold) Press SPACE to buy", 424, 300, 20, Color.WHITE);
        Raylib.DrawText("3. +1 gold/sec (50 gold) Press SPACE to buy", 424, 450, 20, Color.WHITE);
        Raylib.DrawText("4. Play next round", 424, 600, 20, Color.WHITE);
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