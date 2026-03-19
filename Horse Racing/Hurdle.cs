using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Hurdle : GameObject
{
    private (int x,int y) _position;
    private const float K_MoveHurdle = 0.20f;
    private readonly LinkedList<(int x, int y)> _fall = new LinkedList<(int x, int y)>();

    private float _moveTimer;
    public Hurdle(Scene scene) : base(scene)
    {
        Name = "Hurdle";
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.SetCell(_position.x, _position.y, '*', ConsoleColor.Red);
    }
    
    public override void Update(float deltaTime)
    {
        
    }
}