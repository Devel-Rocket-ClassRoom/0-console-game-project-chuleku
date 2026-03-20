using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;


public class BettingScene : Scene
{
    public event GameAction StartRequested;
    private int money = 100000;
    private int readbet;
    public int Money => money;

    public int BettingMoney;


    private string _inputBuffer = "";
    public string _message { get; private set; } = "";
    public BettingScene(int CurrentMoney)
    {
        money = CurrentMoney;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteTextCentered(8, "배팅할 금액을 입력해주세요", ConsoleColor.White);
        buffer.WriteTextCentered(10, $"현재 금액: {Money}", ConsoleColor.Yellow);
        buffer.WriteTextCentered(12, $"배팅할 금액: {_inputBuffer}", ConsoleColor.Yellow);
        buffer.WriteTextCentered(14, $"Enter키를 입력하여 게임화면으로 이동.", ConsoleColor.Yellow);
        if (!string.IsNullOrEmpty(_message))
        {
            buffer.WriteTextCentered(18, _message, ConsoleColor.Red);
        }
    }


    public override void Load()
    {
        _inputBuffer = "";
        _message = "";
        BettingMoney = 0;
    }

    public override void Unload()
    {

    }

    public override void Update(float deltaTime)
    {
        var digitKeys = new[]
        {
            ConsoleKey.D0, ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4,
            ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9
        };
        var numpadKeys = new[]
        {
            ConsoleKey.NumPad0, ConsoleKey.NumPad1, ConsoleKey.NumPad2, ConsoleKey.NumPad3, ConsoleKey.NumPad4,
            ConsoleKey.NumPad5, ConsoleKey.NumPad6, ConsoleKey.NumPad7, ConsoleKey.NumPad8, ConsoleKey.NumPad9
        };
        for (int i = 0; i <= 9; i++)
        {
            if (Input.IsKeyDown(digitKeys[i]) || Input.IsKeyDown(numpadKeys[i]))
            {
                if (_inputBuffer.Length < 9) 
                    _inputBuffer += (char)('0' + i);
            }
        }

        if (Input.IsKeyDown(ConsoleKey.Backspace))
        {
            if (_inputBuffer.Length > 0)
                _inputBuffer = _inputBuffer.Substring(0, _inputBuffer.Length - 1);
        }
        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            if (int.TryParse(_inputBuffer, out int amount) && amount > 0 && amount <= Money)
            {
                BettingMoney = amount;
                _message = $"{BettingMoney}";
                readbet = BettingMoney;

                StartRequested?.Invoke();
            }
            else
            {
                _message = "잘못된 금액입니다. 현재 보유 금액 이하의 정수만 입력하세요.";
            }
        }
    }
}