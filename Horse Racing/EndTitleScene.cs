using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class EndTitleScene : Scene
{
    private int money;
    private int amount;
    private int totalmoney;
    public event GameAction StartRequested;
    private IReadOnlyList<int> ranks;
    private int horsenum;
    public EndTitleScene(int initialMoney,int bettingmoney,int Endtotalmoney, IReadOnlyList<int> rankList,int readhorse)
    {
        money = initialMoney;
        amount = bettingmoney;
        totalmoney = Endtotalmoney;
        ranks = rankList ?? new List<int>().AsReadOnly();
        horsenum = readhorse;
    }
    public override void Draw(ScreenBuffer buffer)
    {

        buffer.WriteTextCentered(10, "=== 게임 종료 ===", ConsoleColor.White);
        buffer.WriteTextCentered(12, $"원래 보유액: {money}", ConsoleColor.White);
        buffer.WriteTextCentered(14, $"배팅 액: {amount}", ConsoleColor.White);
        buffer.WriteTextCentered(16, $"고른말: {horsenum}번마", ConsoleColor.White);
        if (ranks[0] == horsenum)
        {
            buffer.WriteTextCentered(18, $"1등 배팅액x2 현재 보유액 {totalmoney}", ConsoleColor.White);
        }
        if (ranks[1] == horsenum)
        {
            buffer.WriteTextCentered(18, $"2등.. 배팅액/2  현재 보유액 {totalmoney}", ConsoleColor.White);
        }
        if (ranks[2] == horsenum)
        {
            buffer.WriteTextCentered(18, $"3등ㅠㅠㅠ 배팅액을 전부 잃었습니다.  현재 보유액 {totalmoney}", ConsoleColor.Red);
        }
        if (totalmoney <= 0)
        {
            buffer.WriteTextCentered(20, "보유금이 0원입니다. Enter를 누르면 게임이 종료됩니다.", ConsoleColor.Red);
            
        }
        else if(totalmoney>0)
        {
            buffer.WriteTextCentered(20, "Enter 키를 눌러 다시 시작 하십시오.", ConsoleColor.White);
        }
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
