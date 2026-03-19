using Framework.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

public class Track : GameObject
{
    public const int Left = 1;
    public const int Top = 2;
    public const int Right = 98;
    public const int Bottom = 27;
    public const int secondLeft = 31;
    public const int secondTop = 11;
    public const int secondRight = 68;
    public const int secondBottom = 18;

    public Track(Scene scene) : base(scene)
    {   
        Name = "Track";
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.DrawBox(Left - 1, Top - 1, Right - Left + 3, Bottom - Top + 3, ConsoleColor.White);
        buffer.DrawBox(secondLeft-1,secondTop-1, secondRight-secondLeft+3,secondBottom-secondTop+3, ConsoleColor.White);
        buffer.DrawVLine(secondLeft-1, Top, secondTop-3);
        buffer.DrawVLine(secondLeft, Top, secondTop-3);
        buffer.DrawHLine(Left, secondTop - 1, secondLeft - 2);
        buffer.DrawHLine(Left, secondTop, secondLeft - 2);
    }

    public override void Update(float deltaTime)
    {
        
    }
}