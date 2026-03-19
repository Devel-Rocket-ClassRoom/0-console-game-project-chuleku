using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Hurdle : GameObject
{
    private (int x,int y) _position;
    private readonly Random _random = new Random();

    private const float K_MoveHurdle = 0.20f;
    private readonly LinkedList<(int x, int y)> _fall = new LinkedList<(int x, int y)>();

    private float _moveTimer;
    public Hurdle(Scene scene) : base(scene)
    {
        Name = "Hurdle";
    }

    public override void Draw(ScreenBuffer buffer)
    {
        foreach (var pos in _fall)
        {
            buffer.SetCell(pos.x, pos.y, '*', ConsoleColor.Red);
        }
    }
    public bool IsSqawn(int x, int y)
    {
        if (x >= Track.Left && x <= Track.secondLeft - 1 && y >= Track.Top && y <= Track.secondTop - 1)
        {
            return true;
        }
        if(x>=Track.secondLeft&&x<Track.secondRight+1&&y>=Track.secondTop-1&&y<=Track.secondBottom+1)
        {
            return true;
        }
        return false;

    }
    public void Spawn(int count)
    {
        if (count <= 0) return;
        int attempts = 0;
        while (_fall.Count < count && attempts < count * 10)
        {
            attempts++;
            int x = _random.Next(Track.Left, Track.Right + 1);
            int y = _random.Next(Track.Top, Track.Bottom + 1);
            if(IsSqawn(x,y))
            {
                continue;
            }
            if(_fall.Contains((x,y)))continue;

            _fall.AddLast((x, y));
        }
    }

    // 기존 오타 메서드와의 호환성(기존 호출이 있다면 유지)
    public void Sqawn(int count) => Spawn(count);
    public override void Update(float deltaTime)
    {
        _moveTimer += deltaTime;
        if(_moveTimer >K_MoveHurdle)
        {
            
            _moveTimer = 0f;
        }
    }
}