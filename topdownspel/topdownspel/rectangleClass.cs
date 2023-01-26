using Raylib_cs;
using System;


class rectangleClass{

public static Rectangle upgrade1 = new Rectangle(150, 700, 100, 100); //Rektangel för extra speed

public static Rectangle upgrade2 = new Rectangle(300, 700, 100, 100); //Rektangel för full hp

public static Rectangle upgrade3 = new Rectangle(450, 700, 100, 100); //Rektangel för extra guld per sekund

public static Rectangle nextround = new Rectangle(600, 700, 300, 100); //Rektangel för nästa runda

public static Rectangle playGame = new Rectangle(20, 500, 150, 50); //Rektangel för att kolla kollision när man ska starta spelet

public static Rectangle difficulty = new Rectangle(20, 600, 150, 50); //Rektangel för att kolla kollision när man ska byta svårighet

public static Rectangle exitGame = new Rectangle(20, 700, 150, 50); //Rektangel för att kolla kollision när man vill avsluta spelet

public static Rectangle sourceRec = new Rectangle(80*VariableClass.frame, 0, 80, 80); //Source rektangel för karaktäranimation under rörelse
public static Rectangle sourceRec1 = new Rectangle(80*VariableClass.frame, 0, -80, 80); //Source rektangel för karaktär under rörelse i motsatt riktning
public static Rectangle coinRecAnim = new Rectangle(20*VariableClass.frame, 0, 20, 32); //Source rektangel för guldcoinens spritesheet
}