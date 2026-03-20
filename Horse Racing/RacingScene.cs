using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

public class RacingScene : Scene
{
    private Track track;
    private Hurdle hurdle;
    private Horse horse;
    private Horse1 horse1;
    private Horse2 horse2;
    public event GameAction PlayAgainRequested;
    private bool isGameOver;
    private int Count1;
    private int Count2;
    private int Count3;
    private int amount;
    private int Checknum1;
    private int Checknum2;
    private int Checknum3;
    public List<int> ranklist = new List<int>();
    private int rankcount = 0;
    private int goalcount;
    private int money;
    public int readamount => amount;
    public int readCheckhorse;
   
    public RacingScene(int totalMoney,int betting)
    {
        money = totalMoney;
        amount = betting;   
    }
    public int Money => money;

    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);
        buffer.WriteText(100, 0, "어떤 말이 이길까?", ConsoleColor.Yellow);
        buffer.WriteText(100, 2, "Press Number", ConsoleColor.Yellow);
        buffer.WriteText(100, 3, "(1, 2, 3)", ConsoleColor.Yellow);
        buffer.WriteText(100, 5, "(TrackLap)", ConsoleColor.Blue);
        buffer.WriteText(100, 6, $"1번마: {Count1}", ConsoleColor.Green);
        buffer.WriteText(100, 7, $"2번마: {Count2}", ConsoleColor.Yellow);
        buffer.WriteText(100, 8, $"3번마: {Count3}", ConsoleColor.Gray);
        buffer.WriteText(100, 9, $"배팅금액: {amount}원", ConsoleColor.Gray);
        if(Checknum1==1)
        {
            buffer.WriteText(100, 10, $"고른말 : 1번마", ConsoleColor.Green);
        }
        if (Checknum2 == 1)
        {
            buffer.WriteText(100, 10, $"고른말 : 2번마", ConsoleColor.Yellow);
        }
        if (Checknum3 == 1)
        {
            buffer.WriteText(100, 10, $"고른말 : 3번마", ConsoleColor.Gray);
        }
        if(goalcount==3)
        {
            buffer.WriteText(100, 11, $"게임 종료.", ConsoleColor.Gray);
            buffer.WriteText(100, 12, $"Press Enter", ConsoleColor.Gray);
        }
    }
    public override void Load()
    {
        goalcount = 0;
        rankcount = 0;
        Count1 = 0;
        Count2 = 0;
        Count3 = 0;
        Checknum1 = 0;
        Checknum2 = 0;
        Checknum3 = 0;
        track = new Track(this);
        hurdle = new Hurdle(this);
        hurdle.Spawn(300);
        horse = new Horse(this,17,3);
        horse1 = new Horse1(this,13,4);
        horse2 = new Horse2(this,9,5);
       
        AddGameObject(track);
        AddGameObject(hurdle);
        AddGameObject(horse);
        AddGameObject(horse1);
        AddGameObject(horse2);
        isGameOver = false;

    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        if (isGameOver)
        {
            if (Input.IsKeyDown(ConsoleKey.Enter))
            {
                PlayAgainRequested?.Invoke();
            }
            return;
        }
        if (Input.IsKeyDown(ConsoleKey.NumPad1) || Input.IsKeyDown(ConsoleKey.D1))
        {
            horse.StartMoving();
            horse1.StartMoving();
            horse2.StartMoving();
            Checknum1++;
            readCheckhorse = 1;
        }
        else if (Input.IsKeyDown(ConsoleKey.NumPad2) || Input.IsKeyDown(ConsoleKey.D2))
        {
            horse.StartMoving();
            horse1.StartMoving();
            horse2.StartMoving();
            Checknum2++;
            readCheckhorse = 2;

        }
        else if (Input.IsKeyDown(ConsoleKey.NumPad3) || Input.IsKeyDown(ConsoleKey.D3))
        {
            horse.StartMoving();
            horse1.StartMoving();
            horse2.StartMoving();
            Checknum3++;
            readCheckhorse = 3;
        }
        if (track.Goal(horse.Position))
        {
            if (Count1 < 3)
            {
                Count1++;
                if (Count1 == 3)
                {
                    goalcount++;
                    rankcount = 1;
                    ranklist.Add(rankcount);
                    horse.StopMoving();
                    return;
                }
                horse.GoalLine(horse.Position.X, horse.Position.Y - 2);
                return;
            }
        }
        if (track.Goal(horse1.Position))
        {
            if (Count2 < 3)
            {
                Count2++;
                if (Count2 == 3)
                {
                    goalcount++;
                    rankcount = 2;
                    ranklist.Add(rankcount);
                    horse1.StopMoving();
                    return;
                }
                horse1.GoalLine(horse1.Position.X, horse1.Position.Y - 2);
                return;
            }
        }
        if (track.Goal(horse2.Position))
        {
            if (Count3 < 3)
            {
                Count3++;
                if (Count3 == 3)
                {
                    goalcount++;
                    rankcount = 3;
                    ranklist.Add(rankcount);
                    horse2.StopMoving();
                    return;
                }
                horse2.GoalLine(horse2.Position.X, horse2.Position.Y - 2);

                return;
            }
        }
        if (goalcount == 3)
        {
            if (Checknum1 == 1)
            {
                if (ranklist[0] == 1)
                {
                    money += amount;
                }
                else if (ranklist[1] == 1)
                {
                    money -= amount / 2;
                }
                else if (ranklist[2] == 1)
                {
                    money -= amount;
                }
            }
            if (Checknum2 == 1)
            {
                if (ranklist[0] == 2)
                {
                    money += amount;
                }
                else if (ranklist[1] == 2)
                {
                    money -= amount / 2;
                }
                else if (ranklist[2] == 2)
                {
                    money -= amount;
                }
            }
            if (Checknum3 == 1)
            {
                if (ranklist[0] == 3)
                {
                    money += amount;
                }
                else if (ranklist[1] == 3)
                {
                    money -= amount / 2;
                }
                else if (ranklist[2] == 3)
                {
                    money -= amount;
                }
            }

            isGameOver = true;
        }
        UpdateGameObjects(deltaTime);
    }

    
}