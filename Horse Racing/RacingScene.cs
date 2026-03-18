using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class RacingScenc : Scene
{
    private Track track;
    public RacingScenc()
    {
    }

    public override void Draw(ScreenBuffer buffer)
    {
        track = new Track(this);
        AddGameObject(track);
    }

    public override void Load()
    {
        
    }

    public override void Unload()
    {
       
    }

    public override void Update(float deltaTime)
    {
       
    }
}