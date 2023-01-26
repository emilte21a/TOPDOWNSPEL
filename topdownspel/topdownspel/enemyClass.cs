using System;
using Raylib_cs;



class enemyClass
{
    
    public Rectangle enemyRec = new Rectangle(800, 600, 80, 80);

    public enemyClass()
    {
        int enemyPosX = randomClass.rnd.Next(512, 1030);
        enemyRec.x = enemyPosX;
    }
}

