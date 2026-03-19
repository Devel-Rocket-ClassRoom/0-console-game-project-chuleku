using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class RacingScene : Scene
{
    private Track track;
    private Hurdle hurdle;
    private Horse horse;
    private Horse horse1;
    private Horse horse2;
    public event GameAction PlayAgainRequested;
    private bool isGameOver;
    public RacingScene()
    {
    }

    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);
    }

    public override void Load()
    {
        horse = new Horse(this,17,3);
        horse1 = new Horse(this,13,5);
        horse2 = new Horse(this,9,7);
        track = new Track(this);
        hurdle = new Hurdle(this);
        hurdle.Spawn(200);
        AddGameObject(horse);
        AddGameObject(horse1);
        AddGameObject(horse2);
        AddGameObject(track);
        AddGameObject(hurdle);
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
        UpdateGameObjects(deltaTime);
        
    }
}