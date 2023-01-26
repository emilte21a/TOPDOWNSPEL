using System;

public class VariableClass{



public static int dmgTimer = 60; //dmgTimer är en skadetimer som minskar med 60 varje sekund. Det tar en sekund för den att nå 0
public static int coinTimer = 120;//Cointimer som minskar med 60 varje sekund. Det tar två sekunder för den att nå 0
public static int extragold = 0; //Extra upgrade som man gan uppgradera för att få guld per sekund per sekund
public static int gold = 0; //Valutan i spelet
public static int hp = 100;
public static bool beginGame = false;
public static string currentDifficulty = "easy";
public static int diffInt = 2;
public static int plusGoldAmount = 1;
public static int plusSpeedAmount = 1;
public static int round = 1; //Runda
public static string currentScene = "start"; //start, game, gameover

public const int screenWidth = 1080; 
public const int screenHeight = 1080;

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