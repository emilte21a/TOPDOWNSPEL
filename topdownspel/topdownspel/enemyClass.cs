using System;
using Raylib_cs;

public class enemyClass
{
    public Texture2D enemyTexture = Raylib.LoadTexture("enemy.png");
    public Rectangle enemyRec = new Rectangle(800, 600, 80, 80);
}

public class TextureClass
{
    public List<Texture2D> backgroundTextures = new();
    public List<Texture2D> otherTextures = new();
    public List<Texture2D> charTextures = new();
    public TextureClass(){
        charTextures.Add(Raylib.LoadTexture("gubbe.png"));
        backgroundTextures.Add(Raylib.LoadTexture("bakgrundstart.png"));
        backgroundTextures.Add(Raylib.LoadTexture("upgradebakgrund.png"));
        backgroundTextures.Add(Raylib.LoadTexture("mainbackground.png"));
        otherTextures.Add(Raylib.LoadTexture("exitsign.png"));
    }
}

public class RunningClass
{
    
   public Texture2D runningTexture = Raylib.LoadTexture("spring.png");
   
}