using System;

public class VariableClass{



public static int dmgTimer = 60; //dmgTimer är en skadetimer som minskar med 60 varje sekund. Det tar en sekund för den att nå 0
public static int coinTimer = 120;//Cointimer som minskar med 60 varje sekund. Det tar två sekunder för den att nå 0
public static int extragold = 0; //Extra upgrade som man gan uppgradera för att få guld per sekund per sekund
public static int gold = 0; //Valutan i spelet
public static int hp = 100; //Health points
public static bool beginGame = false; //Bool som sätter igång spelet
public static string currentDifficulty = "easy"; //Svårighetsgrad på spelet
public static int diffInt = 2; //Svårighetsgrad int (DifficultyInt förkortat)
public static int plusGoldAmount = 1; //Kostnaden guld/sek uppgradering. Kostanden multipliceras med plusGoldAmount som ökar varje gång du uppgraderar den
public static int plusSpeedAmount = 1; //Kostnaden hastighet uppgradering. Kostanden multipliceras med plusSpeedAmount som ökar varje gång du uppgraderar den
public static int round = 1; //Runda som ökar varje gåmng du kolliderar med finish rektangeln
public static string currentScene = "start"; //start, game, gameover

//Konstanta integers för skärmhöjden samt bredden.
public const int screenWidth = 1080; 
public const int screenHeight = 1080;

//Variabler för spring-animationen
public static float timer1 = 0.0f;
public static int frame = 0;
public static int maxFrames = (8);
}


public class randomClass{
public static Random rnd = new Random();
 
public static int exitPosX = rnd.Next(540,944);//Exit-sign x positionen
public static int exitPosY = rnd.Next(540,900);//Exit-sign y positionen
public static int pickupPosX = rnd.Next(0, 1030);  //Guld X position
public static int pickupPosY = rnd.Next(0, 1030);  //Guld Y position
}