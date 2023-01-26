using System;
using Raylib_cs;

class TextureClass
{
    public List<Texture2D> backgroundTextures = new();
    public List<Texture2D> otherTextures = new();
    public List<Texture2D> charTextures = new();
    public TextureClass()
    {
        charTextures.Add(Raylib.LoadTexture("IMG/gubbe.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/gubbeBack.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/enemy.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/enemymedium.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/enemyhard.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/bakgrundstart.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/upgradebakgrund.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/mainbackground.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/gameOverscreen.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/winScreen.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/exitsign.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/selectText.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/hpochpengar.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/ROUNDD.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/goldPickup.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/ARROW.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/howToInstruct.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/Dkey.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/Akey.png"));
    }
}

public class RunningClass
{
    public Texture2D runningTexture = Raylib.LoadTexture("IMG/spring.png");
    public Texture2D runningTexture2 = Raylib.LoadTexture("IMG/springBak.png");
    public Texture2D runningTexture3 = Raylib.LoadTexture("IMG/springFram.png");

}