using Framework.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

public class Track : GameObject
{
    Random random = new Random();

    
    
    public Track(Scene scene) : base(scene)
    {
        Name = "Track";
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.DrawBox(0, 0, buffer.Width, buffer.Height, ConsoleColor.Cyan);
    }

    public override void Update(float deltaTime)
    {
        
    }
}