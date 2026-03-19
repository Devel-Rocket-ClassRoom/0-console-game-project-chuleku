using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class RacingScenc : Scene
{
    private Track track;
    private Hurdle hurdle;
    public event GameAction PlayAgainRequested;
    private bool isGameOver;
    public RacingScenc()
    {
    }

    public override void Draw(ScreenBuffer buffer)
    {
        
    }

    public override void Load()
    {
        track = new Track(this);
        hurdle = new Hurdle(this);
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