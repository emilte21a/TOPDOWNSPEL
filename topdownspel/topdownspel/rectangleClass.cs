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

}