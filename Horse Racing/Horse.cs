using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Horse : GameObject
{
    private (int X, int Y) _direction;
    private (int X, int Y) _nextDirection;
    private static readonly string Fallback = "♞";
    public Horse(Scene scene,int StartX,int StartY) : base(scene)
    {
        Name = "Horse";
        _direction = (StartX, StartY);
        _nextDirection = (StartX, StartY);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteText(_direction.X,_direction.Y, Fallback, ConsoleColor.Gray, ConsoleColor.Black);
    }

    public override void Update(float deltaTime)
    {
        
    }
}
