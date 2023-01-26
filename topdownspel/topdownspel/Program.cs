using Raylib_cs;
using System.Numerics;

//Konstant int för skrämbredden och höden så att jag slipper ändra båda två där de används

Raylib.InitWindow(VariableClass.screenWidth, VariableClass.screenHeight, "Topdown game");
Raylib.SetTargetFPS(60);

List<enemyClass> enemies = new List<enemyClass>();
enemies.Add(new enemyClass()); //Skapar en ny enemy från enemyclass

Color alpha = new Color(0, 0, 0, 0); //Tagna från Theo
int alphavariable = 0; //En helsvart färg samt en variable för opacityn på färgen

TextureClass t = new(); //Textureklass för generella textures
RunningClass r = new(); //Textureklass för springtexturer
Rectangle characterRec = new Rectangle(0, 60, t.charTextures[0].width, t.charTextures[0].height);
Rectangle finish = new Rectangle(randomClass.exitPosX, randomClass.exitPosY, t.otherTextures[0].width-20, t.otherTextures[0].height-20);

enemyClass enemyRec = new enemyClass();

//Kamera följer spelarens position
Camera2D camera = new();
camera.zoom = 1;
camera.rotation = 0;
camera.offset = new Vector2(VariableClass.screenWidth / 2, VariableClass.screenHeight / 2);


System.Timers.Timer timer = new (interval: 1000); //Timer med intervallet 1 sekund
timer.Elapsed += ( sender, e ) => methodClass.HandleTimer();

void resetCharPos()
{
    characterRec.x = 0;
    characterRec.y = 0;
}

//Fiende egenskaper

float speed = 4.5f; //Karaktär speed

static float walkingX(float characterx, float speed) //Fick denna koden från Theo och optimiserade den för mitt egna spel
{
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && characterx < (VariableClass.screenWidth - 80))
    {
        characterx += speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && characterx > 0)
    {
        characterx -= speed;
    }
    return characterx;
}

//Om W-knappen trycks så rör sig karaktären uppåt 

static float walkingY(float charactery, float speed)
{ //Metod för player movement. Görs i en metod så att jag slipper göra onödig mängd kod varje gång jag vill att karaktären ska röra på sig
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && charactery > 8) //Om W-knappen är nedtryck och karaktärens y-position är större än 8, så ska karaktärens position minska med speed.
    {
        charactery -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && charactery + 80 < VariableClass.screenHeight) //Om S-knappen är nedtryck och karaktärens y-position+80 är mindre än skärmens höjd, så ska karaktärens position öka med speed.
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

static float skipY(float charactery)
{
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
    {
        charactery += 100;
        if (charactery > 700 )
    {
        charactery = 500;
    }
    }
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
    {
        charactery -= 100;

        if (charactery < 500)
    {
        charactery = 700;
    }
    }
    return charactery;
}

characterRec.y = 500; 
while (Raylib.WindowShouldClose() == false)
{
    //LOGIK
    Vector2 characterPos = new Vector2(characterRec.x, characterRec.y);
    camera.target = characterPos;

    

    if (VariableClass.currentScene == "game") 
    {
        timer.Start(); //Starta timer
        characterRec.x = walkingX(characterRec.x, speed); 
        characterRec.y = walkingY(characterRec.y, speed);

        Vector2 playerPos = new Vector2(characterRec.x, characterRec.y);
        Vector2 fiendePos = new Vector2(enemyRec.enemyRec.x, enemyRec.enemyRec.y);
        Vector2 diff = playerPos - fiendePos;
        Vector2 fiendeDirection = Vector2.Normalize(diff);
        
        movementClass.enemyMovement = fiendeDirection * movementClass.enemySpeed;
        enemyRec.enemyRec.x += movementClass.enemyMovement.X;
        enemyRec.enemyRec.y += movementClass.enemyMovement.Y;
            
        VariableClass.dmgTimer--;   //Dmgtimer minskar med ett varje frame
        if (VariableClass.dmgTimer == 0){ //Om Dmgtimer är lika med 0 så ska dmgtimer vara lika med 60
            VariableClass.dmgTimer = 60;
        }

        if (Raylib.CheckCollisionRecs(characterRec, enemyRec.enemyRec) && VariableClass.dmgTimer == 60) //Gameover-scen när fienden och karaktären krockar
        {   
            VariableClass.hp-=25;
        }

        if (VariableClass.hp ==0) 
        {
            methodClass.resetVariables();
            alphavariable = 0;
            movementClass.enemySpeed=2;
            VariableClass.currentScene = "gameover";
            enemyRec.enemyRec.x = 1024;
            enemyRec.enemyRec.y = 1024;
            speed= 4.5f;
            timer.Stop();
        }

        if (Raylib.CheckCollisionRecs(characterRec, finish) && VariableClass.gold >= 5*VariableClass.round)
        {   
            VariableClass.gold = VariableClass.gold - 5*VariableClass.round;
            timer.Stop();
            VariableClass.round++;
            movementClass.enemySpeed += 0.5f;
            characterRec.x = 150;
            characterRec.y = 0;
            finish.x = randomClass.rnd.Next(512, 1000);
            finish.y = randomClass.rnd.Next(512, 1000);
            VariableClass.currentScene = "upgrade";
        }

        if (VariableClass.round==6)
        {
            VariableClass.currentScene="winScene";
            alphavariable = 0;
        }
    }

    else if (VariableClass.currentScene == "start")
    {   
        if (Raylib.CheckCollisionRecs(characterRec, rectangleClass.playGame))
        {
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_ENTER))
            {
                VariableClass.beginGame=true;
                characterRec.y = 500;
            }

            if (VariableClass.beginGame == true && alphavariable<255)
            {
                alphavariable++;
                alpha.a = (byte)alphavariable;
            }
            
            if (alphavariable==255)
            {
                VariableClass.currentScene = "game";
                resetCharPos();
            }
        }
            else if (Raylib.CheckCollisionRecs(characterRec, rectangleClass.difficulty))
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                VariableClass.currentScene = "chooseDiff";
                }
            }

            else if(Raylib.CheckCollisionRecs(characterRec, rectangleClass.exitGame))
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER)) 
                {
                break;
                }
            }
        characterRec.y = skipY(characterRec.y);
        characterRec.x = 20;
    }

    else if (VariableClass.currentScene== "chooseDiff")
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
        {
            VariableClass.diffInt++;   
            if (VariableClass.diffInt > 4)
            {
                VariableClass.diffInt=2;
            }
        }
        
        //om nuvarande scenen är chooseDiff och D-knappen trycks så ökar diffInt med 1.
        //om diffInt är större än 4 så blir den lika med 2

        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
        {
            VariableClass.diffInt--;   
            if (VariableClass.diffInt < 2)
            {
                VariableClass.diffInt=4;
            }
        }
        
        //om diffInt är mindre än 2 så blir den lika med 4

        switch (VariableClass.diffInt)
        {
            case 2:
            VariableClass.currentDifficulty= "easy";
            movementClass.enemySpeed=2;
            break;

            case 3:
            VariableClass.currentDifficulty= "medium";
            movementClass.enemySpeed=3;
            break;

            case 4:
            VariableClass.currentDifficulty= "hard";
            movementClass.enemySpeed=4;
            break;
        }

        //Om diffInt är detsamma som 2 så är svårigheten easy. 
        //Om diffInt är detsamma som 3 så är svårigheten medium. 
        //Om diffInt är detsamma som 4 så är svårigheten svår. 


        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            VariableClass.currentScene = "start";
        }
        //Om ENTER-knappen trycks så blir currentScene start scenen
    }

    else if (VariableClass.currentScene == "upgrade")
    {   
        characterRec.y = 700;
        characterRec.x = skipX(characterRec.x);

        if (Raylib.CheckCollisionRecs(characterRec, rectangleClass.upgrade1) && VariableClass.gold >= 10*VariableClass.plusSpeedAmount)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                speed = speed + 0.2f;
                VariableClass.gold = VariableClass.gold - 10*VariableClass.plusSpeedAmount;
                VariableClass.plusSpeedAmount++;
            }
        }

        if (Raylib.CheckCollisionRecs(characterRec, rectangleClass.upgrade2) && VariableClass.gold >= 5*VariableClass.round && VariableClass.hp<100)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                VariableClass.hp = 100;
                VariableClass.gold = VariableClass.gold - 5*VariableClass.round;
            }
        }

        if (Raylib.CheckCollisionRecs(characterRec, rectangleClass.upgrade3) && VariableClass.gold >= 10*VariableClass.plusGoldAmount)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                VariableClass.extragold++;
                VariableClass.gold = VariableClass.gold - 10*VariableClass.plusGoldAmount;
                VariableClass.plusGoldAmount++;
            }
        }

        if (Raylib.CheckCollisionRecs(characterRec, rectangleClass.nextround))
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                VariableClass.currentScene = "game";
                resetCharPos();
            }
        }
    }

    else if (VariableClass.currentScene == "winScene")
    {
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_ENTER)) 
        {
            resetCharPos();
            VariableClass.currentScene = "start";
            characterRec.y = 500;
            methodClass.resetVariables();
        }
    }

    else
    {
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_ENTER)) 
        {
            resetCharPos();
            characterRec.y = 500;
            VariableClass.currentScene = "start";
            methodClass.resetVariables();
        }
    }

    //=============================GRAFIK===============================

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    if (VariableClass.currentScene == "game")
    {   
        Raylib.DrawRectangle(0, 0, 1080, 1080, Color.DARKGRAY);
        Raylib.BeginMode2D(camera); //Starta 2D läge
        Raylib.DrawTexture(t.backgroundTextures[2], 0, 0, Color.WHITE); //Spelbakgrund
        Raylib.DrawTexture(t.otherTextures[0], (int)finish.x, (int)finish.y, Color.WHITE);

        
        foreach (var enemy in enemies)
        {
            Raylib.DrawTexture(t.charTextures[VariableClass.diffInt], (int)enemyRec.enemyRec.x, (int)enemyRec.enemyRec.y, Color.WHITE); //Fiende texturen 
        }
        
        methodClass.runningLogic(); //Funktion för hur snabbt rektangeln ska röra på sig

        VariableClass.coinTimer--;

        if (VariableClass.coinTimer < 0)
        {
            Rectangle coinRec = new Rectangle(randomClass.pickupPosX, randomClass.pickupPosY, 32, 20);
            Vector2 coinposition = new(randomClass.pickupPosX, randomClass.pickupPosY);
            Raylib.DrawTextureRec(t.otherTextures[4], rectangleClass.coinRecAnim, coinposition, Color.WHITE);

            if (Raylib.CheckCollisionRecs(characterRec, coinRec)){
                VariableClass.gold++;
                VariableClass.coinTimer = 120;
                randomClass.pickupPosX = randomClass.rnd.Next(0, 1030);
                randomClass.pickupPosY = randomClass.rnd.Next(0, 1030);
            }
            //Om karaktären och guldcoinen kolliderar så får man en guld och cointimern resetar samt så förändras coin positionen till en random position mellan 0<x<1031 och 0<y<1031
        }

        //Nedan är själva animationen som använder en sourcerec för att rita ut specifika positioner på ett spritesheet
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            Raylib.DrawTextureRec(r.runningTexture, rectangleClass.sourceRec, characterPos, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            Raylib.DrawTextureRec(r.runningTexture, rectangleClass.sourceRec1, characterPos, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            Raylib.DrawTextureRec(r.runningTexture2, rectangleClass.sourceRec, characterPos, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            Raylib.DrawTextureRec(r.runningTexture3, rectangleClass.sourceRec, characterPos, Color.WHITE);
        }
        
        else //Annars om inte ovanstående saker stämmer så ritas idle karaktär texturen
        {
        Raylib.DrawTexture(t.charTextures[0], //Karaktär texturen
        (int)characterRec.x,
        (int)characterRec.y,
        Color.WHITE);
        }

        //Kollar ifall karaktären och fienden har kolliderat och att dmgtimer är 1. Detta gör så att man endast kan ta skada en gång i sekunden
        if (Raylib.CheckCollisionRecs(characterRec, enemyRec.enemyRec) && VariableClass.dmgTimer == 1) 
        {   
           Raylib.DrawTexture(t.charTextures[0], (int)characterRec.x, (int)characterRec.y,Color.RED);
        }
        Raylib.EndMode2D(); //Tillåter texterna nedan att ha en fast positions på skärmen.

        Raylib.DrawTexture(t.otherTextures[2], 760, 900, Color.WHITE);
        Raylib.DrawTexture(t.otherTextures[3], 400, 0, Color.WHITE);
        Raylib.DrawRectangle(15, 15, 85, 25, Color.WHITE);
        Raylib.DrawFPS(20, 20);
        Raylib.DrawText($"{VariableClass.round}", 555, 40, 30, Color.BLACK);
        Raylib.DrawText($"Gold:{VariableClass.gold}", 850, 990, 30, Color.BLACK);
        Raylib.DrawText($"HP:{VariableClass.hp}", 850, 940, 30, Color.BLACK);

        if (alphavariable>0) //Taget från Theo
        {
            alphavariable--;
            alpha.a = (byte)alphavariable;
            Raylib.DrawRectangle(0, 0, 1080, 1080, alpha);
        }
    }

    else if (VariableClass.currentScene == "chooseDiff")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawRectangle(0, 0, 1080, 1080, Color.BLACK);
        Raylib.DrawTexture(t.charTextures[VariableClass.diffInt], 530, 540, Color.WHITE);
        Raylib.DrawText(VariableClass.currentDifficulty, 50, 50, 60, Color.ORANGE);
        Raylib.DrawTexture(t.otherTextures[8], 150, 800, Color.WHITE);
        Raylib.DrawTexture(t.otherTextures[7], 780, 800, Color.WHITE);
        Raylib.DrawText("Press ENTER to choose", 430, 900, 20, Color.WHITE);
    }

    else if (VariableClass.currentScene == "upgrade")
    {
        Raylib.DrawTexture(t.backgroundTextures[1], 0, 0, Color.WHITE ); //Bakgrundstextur

        Raylib.DrawRectangle(150, 700, 100, 100, Color.GOLD);  //Speed
        Raylib.DrawText("1", 200, 750, 20, Color.WHITE);

        Raylib.DrawRectangle(300, 700, 100, 100, Color.GOLD); //Hp
        Raylib.DrawText("2", 350, 750, 20, Color.WHITE);

        Raylib.DrawRectangle(450, 700, 100, 100, Color.GOLD); //+Gold
        Raylib.DrawText("3", 500, 750, 20, Color.WHITE);

        Raylib.DrawRectangle(600, 700, 100, 100, Color.LIME); //Nästa runda
        Raylib.DrawText("4", 650, 750, 20, Color.WHITE);

        Raylib.DrawTexture(t.otherTextures[1], (int)characterRec.x, (int)characterRec.y, Color.WHITE);
        Raylib.DrawText($"1. Speed+0.2 ({10*VariableClass.plusSpeedAmount}) Press SPACE to buy", 540, 150, 20, Color.WHITE);
        Raylib.DrawText($"2. Full health ({5*VariableClass.round}) Press SPACE to buy", 540, 200, 20, Color.WHITE);
        Raylib.DrawText($"3. +1 gold/sec ({10*VariableClass.plusGoldAmount}) Press SPACE to buy", 540, 250, 20, Color.WHITE);
        Raylib.DrawText("4. Play next round", 540, 300, 20, Color.WHITE);
        Raylib.DrawText($"Gold: {VariableClass.gold}", 150, 80, 30, Color.WHITE);
        Raylib.DrawText($"Gold/Sec: {VariableClass.extragold}", 150, 160, 30, Color.WHITE);
        Raylib.DrawText($"Speed: {speed}", 150, 240, 30, Color.WHITE);
        Raylib.DrawText($"Health: {VariableClass.hp}", 150, 320, 30, Color.WHITE);
    }   
    else if (VariableClass.currentScene == "start")
    {
        Raylib.DrawTexture(t.backgroundTextures[0], 0, 0, Color.WHITE);
        Raylib.DrawText("Zombie Runner", 18, 227, 70, Color.BROWN);
        Raylib.DrawText("Zombie Runner", 20, 225, 70, Color.ORANGE);
        Raylib.DrawText("Start", 60, 517, 26, Color.ORANGE);
        Raylib.DrawText("Difficulty", 35, 617, 26, Color.ORANGE);
        Raylib.DrawText("Exit", 60, 717, 26, Color.RED);
        Raylib.DrawTexture(t.otherTextures[5], (int)characterRec.x, (int)characterRec.y, Color.WHITE);
        Raylib.DrawText($"Difficulty:{VariableClass.currentDifficulty}", 700, 600, 40, Color.WHITE);
        Raylib.DrawText("Press T to see instructions!", 20, 400, 40, Color.WHITE);
        
        if (Raylib.IsKeyDown(KeyboardKey.KEY_T))
        {
            Raylib.DrawTexture(t.otherTextures[6],100, 0, Color.WHITE);
            Raylib.DrawText("Use WASD to move around", 170, 350, 20, Color.WHITE);
            Raylib.DrawText("Escape the Zombie by gathering enough coins to reach the next round!", 170, 400, 20, Color.WHITE);
            Raylib.DrawText("It costs 5 coins per round to exit! Reach round 5 in order to win.", 170, 450, 20, Color.WHITE);
            Raylib.DrawText("Every round the cost to exit to the upgrade-room increases with 5 coins!", 170, 500, 20, Color.WHITE);
            Raylib.DrawText("The zombie will get faster and faster every round!", 170, 550, 20, Color.WHITE);
            Raylib.DrawText("Be vary of what you spend your gold on!", 170, 600, 20, Color.WHITE);
        }
        Raylib.DrawRectangle(0, 0, 1080, 1080, alpha);
    }

    else if (VariableClass.currentScene == "winScene"){
        Raylib.DrawTexture(t.backgroundTextures[4], 0, 0, Color.WHITE);
        Raylib.DrawText("You won, Congratulations!", 20, 225, 50, Color.ORANGE);
        Raylib.DrawText("Press ENTER to start again", 20, 280, 30, Color.ORANGE);
        Raylib.DrawText("Press ESCAPE to exit", 20, 310, 30, Color.ORANGE);
    }
    else
    {
        Raylib.DrawTexture(t.backgroundTextures[3], 0, 0, Color.WHITE);
        Raylib.DrawText("You lost!", 20, 225, 50, Color.ORANGE);
        Raylib.DrawText("Press ENTER to start again", 20, 280, 30, Color.ORANGE);
        Raylib.DrawText("Press ESCAPE to exit", 20, 310, 30, Color.ORANGE);
    }
    Raylib.EndDrawing();
}