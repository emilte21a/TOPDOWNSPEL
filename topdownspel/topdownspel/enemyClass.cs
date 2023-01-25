using System;
using Raylib_cs;

public class TextureClass
{
    public List<Texture2D> backgroundTextures = new();
    public List<Texture2D> otherTextures = new();
    public List<Texture2D> charTextures = new();
    public TextureClass(){
        charTextures.Add(Raylib.LoadTexture("gubbe.png"));
        charTextures.Add(Raylib.LoadTexture("gubbeBack.png"));
        charTextures.Add(Raylib.LoadTexture("enemy.png"));
        charTextures.Add(Raylib.LoadTexture("enemymedium.png"));
        charTextures.Add(Raylib.LoadTexture("enemyhard.png"));
        backgroundTextures.Add(Raylib.LoadTexture("bakgrundstart.png"));
        backgroundTextures.Add(Raylib.LoadTexture("upgradebakgrund.png"));
        backgroundTextures.Add(Raylib.LoadTexture("mainbackground.png"));
        backgroundTextures.Add(Raylib.LoadTexture("gameOverscreen.png"));
        backgroundTextures.Add(Raylib.LoadTexture("winScreen.png"));
        otherTextures.Add(Raylib.LoadTexture("exitsign.png"));
        otherTextures.Add(Raylib.LoadTexture("selectText.png"));
        otherTextures.Add(Raylib.LoadTexture("hpochpengar.png"));
        otherTextures.Add(Raylib.LoadTexture("ROUNDD.png"));
        otherTextures.Add(Raylib.LoadTexture("goldPickup.png"));
        otherTextures.Add(Raylib.LoadTexture("ARROW.png"));
        otherTextures.Add(Raylib.LoadTexture("howToInstruct.png"));
    }
}

class enemyClass
{
    public Rectangle enemyRec = new Rectangle(800, 600, 80, 80);
}

public class RunningClass
{
    
   public Texture2D runningTexture = Raylib.LoadTexture("spring.png");
   public Texture2D runningTexture2 = Raylib.LoadTexture("springBak.png");
   public Texture2D runningTexture3 = Raylib.LoadTexture("springFram.png");
   
}