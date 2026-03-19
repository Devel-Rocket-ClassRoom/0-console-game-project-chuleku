using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Hurdle : GameObject
{
    private int _position;
    private const float K_MoveHurdle = 0.20f;
    private readonly LinkedList<(int x, int y)> _fall = new LinkedList<(int x, int y)>();
    private (int X, int Y) _direction;
    private (int X, int Y) _nextDirection;
    private float _moveTimer;
    public Hurdle(Scene scene) : base(scene)
    {
        Name = "Hurdle";
    }

    public override void Draw(ScreenBuffer buffer)
    {
        var fallhurdle = _fall.First;
        while (fallhurdle != null)
        {
            buffer.SetCell(fallhurdle.Value.x,fallhurdle.Value.y,'ㅡ',ConsoleColor.Red);
        }
    }

    public override void Update(float deltaTime)
    {
        
    }
}