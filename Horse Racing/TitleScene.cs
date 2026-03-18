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
        buffer.WriteTextCentered(14, "배팅 할 말을 선택 해주세요.", ConsoleColor.White);
        buffer.WriteTextCentered(17, "Press Number Key: 1, 2, 3 ", ConsoleColor.White);
    }

    public override void Load()
    {
        
    }

    public override void Unload()
    {
       
    }

    public override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.NumPad1))
        {
            StartRequested?.Invoke();
        }
        else if(Input.IsKeyDown(ConsoleKey.NumPad2))
        {
            StartRequested?.Invoke();
        }
        else if(Input.IsKeyDown (ConsoleKey.NumPad3))
        {
            StartRequested?.Invoke();
        }
    }
}