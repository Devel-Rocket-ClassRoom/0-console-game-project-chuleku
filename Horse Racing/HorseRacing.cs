using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class HorseRacing : GameApp
{
    private readonly SceneManager<Scene> _scenes = new SceneManager<Scene>();
    
    public HorseRacing() : base(120, 30) { }
    public HorseRacing(int width, int height) : base(120, 30)
    {
    }

    protected override void Draw()
    {
        _scenes.CurrentScene?.Draw(Buffer);
    }

    protected override void Initialize()
    {
        ChangeToTitle();
    }

    protected override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Escape))
        {
            Quit();
            return;
        }
        _scenes.CurrentScene?.Update(deltaTime);
    }
    private void ChangeToTitle()
    {
        var title = new TitleScene();
        title.StartRequested += ChangeToRacing;
        _scenes.ChangeScene(title);
    }
    private void ChangeToRacing(int initialMoney)
    {
        var betting = new BettingScene(initialMoney);
        betting.StartRequested += () =>
        {
            var racing = new RacingScene(betting.Money, betting.BettingMoney);
            racing.PlayAgainRequested += () =>
            {
                var end = new EndTitleScene(betting.Money, betting.BettingMoney, racing.Money, racing.ranklist, racing.readCheckhorse);
                if (racing.Money <= 0)
                {
                    end.StartRequested += () => Quit();
                }
                else
                {
                    end.StartRequested += () => ChangeToRacing(racing.Money);
                }
                _scenes.ChangeScene(end);
            };
            _scenes.ChangeScene(racing);
        };
        _scenes.ChangeScene(betting);
    }
}