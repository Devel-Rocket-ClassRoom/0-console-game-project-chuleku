using Framework.Engine;
using System;
using System.Collections.Generic;
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
    public RacingScene()
    {
    }

    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);
        buffer.WriteText(99, 0, "Who's Winner?", ConsoleColor.Yellow);
        buffer.WriteText(99, 2, "Press Number", ConsoleColor.Yellow);
        buffer.WriteText(99, 4, "(1, 2, 3)", ConsoleColor.Yellow);
    }

    public override void Load()
    {
        track = new Track(this);
        hurdle = new Hurdle(this);
        hurdle.Spawn(150);
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
        if (Input.IsKeyDown(ConsoleKey.NumPad1)|| Input.IsKeyDown(ConsoleKey.D1))
        {
            horse.StartMoving();
            horse1.StartMoving();
            horse2.StartMoving();
        }
        else if(Input.IsKeyDown(ConsoleKey.NumPad2)||Input.IsKeyDown(ConsoleKey.D2))
        {
            horse.StartMoving();
            horse1.StartMoving();
            horse2.StartMoving();
        }
        else if(Input.IsKeyDown(ConsoleKey.NumPad3)||Input.IsKeyDown(ConsoleKey.D3))
        {
            horse.StartMoving();
            horse1.StartMoving();
            horse2.StartMoving();
        }
        UpdateGameObjects(deltaTime);
        
    }
}