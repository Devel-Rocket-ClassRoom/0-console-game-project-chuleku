using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Horse1 : GameObject
{
    private const float K_MoveInterval = 0.50f;
    private (int X, int Y) _direction;
    private (int X, int Y) _nextDirection;
    private float _moveTimer;
    private bool _IsMoving;
    private static readonly string Fallback = "♞";
    public Horse1(Scene scene, int StartX, int StartY) : base(scene)
    {
        Name = "Horse1";
        _direction = (StartX, StartY);
        _nextDirection = (1, 0);
        _IsMoving = false;

    }
    public (int X, int Y) Position => _direction;
    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteText(_direction.X, _direction.Y, Fallback, ConsoleColor.Yellow, ConsoleColor.Black);
    }
    public void MoveStep()
    {

        int Mx = _direction.X + _nextDirection.X;
        int My = _direction.Y + _nextDirection.Y;

        Mx = Math.Clamp(Mx, Track.Left, Track.Right);
        My = Math.Clamp(My, Track.Top, Track.Bottom);
        var hurdle = Scene.FindGameObject("Hurdle") as Hurdle;
        var otherhorse = Scene.FindGameObject("Horse") as Horse;
        var otherhorse2 = Scene.FindGameObject("Horse2") as Horse2;

        if (hurdle != null && hurdle.IsAt(Mx, My))
        {
            int dx = _nextDirection.X;
            int dy = _nextDirection.Y;
            var candUp = (x: _direction.X + dx, y: _direction.Y + (dy - 1));
            var candDown = (x: _direction.X + dx, y: _direction.Y + (dy + 1));

            bool IsValid((int x, int y) c)
            {
                if (c.x < Track.Left || c.x > Track.Right || c.y < Track.Top || c.y > Track.Bottom) return false;
                if (hurdle.IsSqawn(c.x, c.y)) return false;
                if (hurdle.IsAt(c.x, c.y)) return false;
                return true;
            }

            if (IsValid(candUp))
            {
                _direction = (candUp.x, candUp.y);
                return;
            }
            if (IsValid(candDown))
            {
                _direction = (candDown.x, candDown.y);
                return;
            }
            return;
        }
        if (otherhorse != null && otherhorse.IsOther(Mx, My))
        {
            int dx = _nextDirection.X;
            int dy = _nextDirection.Y;
            var candUp = (x: _direction.X + dx, y: _direction.Y + (dy - 1));
            var candDown = (x: _direction.X + dx, y: _direction.Y + (dy + 1));

            bool IsValid((int x, int y) c)
            {
                if (c.x < Track.Left || c.x > Track.Right || c.y < Track.Top || c.y > Track.Bottom) return false;
                if (otherhorse.IsOther(c.x, c.y)) return false;
                return true;
            }

            if (IsValid(candUp))
            {
                _direction = (candUp.x, candUp.y);
                return;
            }
            if (IsValid(candDown))
            {
                _direction = (candDown.x, candDown.y);
                return;
            }
            return;
        }
        if (otherhorse2 != null && otherhorse2.IsOther(Mx, My))
        {
            int dx = _nextDirection.X;
            int dy = _nextDirection.Y;
            var candUp = (x: _direction.X + dx, y: _direction.Y + (dy - 1));
            var candDown = (x: _direction.X + dx, y: _direction.Y + (dy + 1));

            bool IsValid((int x, int y) c)
            {
                if (c.x < Track.Left || c.x > Track.Right || c.y < Track.Top || c.y > Track.Bottom) return false;
                if (otherhorse2.IsOther(c.x, c.y)) return false;
                return true;
            }

            if (IsValid(candUp))
            {
                _direction = (candUp.x, candUp.y);
                return;
            }
            if (IsValid(candDown))
            {
                _direction = (candDown.x, candDown.y);
                return;
            }
            return;
        }


        CheckConer(Mx, My);

        _direction.X = Mx;
        _direction.Y = My;

    }
    public bool CheckConer(int nx, int ny)
    {
        HashSet<(int x, int y)> cornerCells = new HashSet<(int x, int y)>();
        int startX = Track.secondRight;
        int startY = Track.secondTop;
        int length = Track.Right - Track.secondRight;
        for (int i = 0; i <= length; i++)
        {
            cornerCells.Add((startX + i, startY - i));
        }
        for (int i = 0; i <= length; i++)
        {
            cornerCells.Add((startX + i + 1, startY - i + 1));
        }
        if (cornerCells.Contains((nx, ny)))
        {
            _nextDirection = (0, 1);
            return true;
        }
        else if (nx > startX + 7 && ny < startY)
        {
            _nextDirection = (0, 1);
            return true;
        }
        HashSet<(int x, int y)> rightbottomconercells = new HashSet<(int x, int y)>();
        int RBX = Track.secondRight;
        int RBY = Track.secondBottom;
        int RBLength = Track.Right - Track.secondRight;
        for (int i = 0; i <= RBLength; i++)
        {
            rightbottomconercells.Add((RBX + i, RBY + i));
        }
        for (int i = 0; i <= length; i++)
        {
            rightbottomconercells.Add((RBX + i + 1, RBY + i + 1));
        }
        if (rightbottomconercells.Contains((nx, ny)))
        {
            _nextDirection = (-1, 0); return true;
        }
        else if (nx > RBX + 5 && ny > RBY + 6)
        {
            _nextDirection = (-1, 0); return true;
        }
        HashSet<(int x, int y)> leftbottomConerCells = new HashSet<(int x, int y)>();
        int LBX = Track.secondLeft;
        int LBY = Track.secondBottom;
        int LBLength = Track.secondLeft - Track.Left;
        for (int i = 0; i <= LBLength; i++)
        {
            leftbottomConerCells.Add((LBX - i, LBY + i));
        }
        for (int i = 0; i <= LBLength; i++)
        {
            leftbottomConerCells.Add((LBX - i - 1, LBY + i + 1));
        }
        if (leftbottomConerCells.Contains((nx, ny)))
        {
            _nextDirection = (0, -1);
            return true;
        }
        else if (nx < LBX - 5 && ny > LBY)
        {
            _nextDirection = (0, -1);
            return true;
        }
        else if (nx < LBX - 10 && ny > Track.secondTop)
        {
            _nextDirection = (0, -1);
            return true;
        }
        HashSet<(int x, int y)> LeftTopConerCells = new HashSet<(int x, int y)>();
        int LTX = Track.secondLeft;
        int LTY = Track.secondTop;
        int LTLength = Track.secondLeft - Track.Left;
        for (int i = 0; i <= LTLength; i++)
        {
            LeftTopConerCells.Add((LTX - i, LTY - i));
        }
        for (int i = 0; i <= LTLength; i++)
        {
            LeftTopConerCells.Add((LTX - i - 1, LTY - i - 1));
        }
        if (LeftTopConerCells.Contains((nx, ny)))
        {
            _nextDirection = (1, 0);
            return true;
        }
        else if (nx < LTX - 5 && ny < LTY)
        {
            _nextDirection = (1, 0);
            return true;
        }
        return false;
    }
    public bool IsOther(int x, int y)
    {
        HashSet<(int x, int y)> check = new HashSet<(int x, int y)>();
        check.Add((_direction.X, _direction.Y));
        return check.Contains((x, y));

    }

    public void StartMoving()
    {
        _IsMoving = true;
    }
    public void StopMoving()
    {
        _IsMoving = false;
    }
    public void SetDirection(int dx, int dy)
    {
        _nextDirection = (dx, dy);
    }
    public void GoalLine(int x, int y)
    {
        _direction.X = x;
        _direction.Y = y;
        _nextDirection = (1, 0);
    }

    public override void Update(float deltaTime)
    {

        if (!_IsMoving) return;

        _moveTimer += deltaTime;
        if (_moveTimer >= K_MoveInterval)
        {
            _moveTimer = 0f;
            return;
        }
        MoveStep();
    }
}
