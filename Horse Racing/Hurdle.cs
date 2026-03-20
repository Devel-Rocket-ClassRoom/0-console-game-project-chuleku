using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;
public class Hurdle : GameObject
{
    private (int x,int y) _position;
    private readonly Random _random = new Random();

    private const float K_MoveHurdle = 0.50f;
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
        if(x>=Track.secondLeft-1&&x<Track.secondRight+2&&y>=Track.secondTop-1&&y<=Track.secondBottom+1)
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
    public bool IsAt(int x, int y)
    {
        return _fall.Contains((x, y));
    }


    public void Sqawn(int count) => Spawn(count);

    public override void Update(float deltaTime)
    {

        _moveTimer += deltaTime;
        if (_moveTimer < K_MoveHurdle) return;
        _moveTimer = 0f;

        var occupied = new HashSet<(int x, int y)>(_fall);

        var node = _fall.First;
        while (node != null)
        {
            var current = node.Value;
 
            occupied.Remove(current);

        
            var candidates = new List<(int x, int y)>
            {
                current,
                (current.x - 1, current.y),
                (current.x + 1, current.y),
                (current.x, current.y - 1),
                (current.x, current.y + 1)
            };

            for (int i = 0; i < candidates.Count; i++)
            {
                int j = _random.Next(i, candidates.Count);
                var tmp = candidates[i];
                candidates[i] = candidates[j];
                candidates[j] = tmp;
            }

            (int x, int y) chosen = current;
            foreach (var cand in candidates)
            {
               
                if (cand.x < Track.Left || cand.x > Track.Right || cand.y < Track.Top || cand.y > Track.Bottom)
                    continue;
                
                if (IsSqawn(cand.x, cand.y)) continue;
          
                if (occupied.Contains(cand)) continue;

                chosen = cand;
                break;
            }

            node.Value = chosen;
            occupied.Add(chosen);

            node = node.Next;
        }
    }
}