using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class TitleScene : Scene
{
    public event GameAction StartRequested;
    public TitleScene()
    {

    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteTextCentered(10,"=== 경마 게임 ===",ConsoleColor.White);
        buffer.WriteTextCentered(14, "시작하려면 Enter키를 눌러주세요.", ConsoleColor.White);
        buffer.WriteTextCentered(20, "Press Enter to Start", ConsoleColor.White);
      
    }

    public override void Load()
    {
        
    }

    public override void Unload()
    {
       
    }

    public override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            StartRequested?.Invoke();
        }
    }
}