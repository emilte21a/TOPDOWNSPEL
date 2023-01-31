using Raylib_cs;
using System.Numerics;


public class methodClass{
    public static void HandleTimer() //Timerfunktion: ökar guld med 1 varje sekund
    {
	    VariableClass.gold = VariableClass.gold+VariableClass.extragold; //Guld ökar med extragold per sekund
    }


    public static void runningLogic() //Bestämmer vilken frame som ska visas under springanimationen
    {
    VariableClass.timer1 += 0.06f; //Timer ökar med 0.06 varje frame. 60 gånger per sekund
        
    if (VariableClass.timer1 > 0.4f) //Om timer är större än 0.4. Timer är 0 och frame är lika med +1. 
    {
    VariableClass.timer1 = 0.0f;
    VariableClass.frame +=1; //En ny frame innebär en ny "plats" på spritesheetet. Varje frame har ett värde på 80 pixlar.
        }
    VariableClass.frame = VariableClass.frame % VariableClass.maxFrames; //Frame ökar tills maxframes uppnås, alltså 8.
    }

    //Resettar alla variabler om du exempelvis dör eller vinner.
    public static void resetVariables(){
    VariableClass.extragold = 0; 
    VariableClass.gold = 0; 
    VariableClass.hp = 100;
    VariableClass.beginGame = false;
    VariableClass.currentDifficulty = "easy";
    VariableClass.diffInt = 2;
}
}

