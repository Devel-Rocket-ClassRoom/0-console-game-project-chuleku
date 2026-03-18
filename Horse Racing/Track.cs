using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Track : GameObject
{
    public const int firstTrackTop = 2;
    public const int firstTrackBottom = 6;
    public const int secondTrackTop = 7;
    public const int secondTrackBottom = 11;
    public const int thirdTrackTop = 12;
    public const int thirdTrackBottom = 16;
    public const int Left = 1;
    public const int Right = 100;
    public Track(Scene scene) : base(scene)
    {
        Name = "Track";
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.DrawBox(Left - 1, firstTrackTop - 1, Right - Left + 3, firstTrackBottom - firstTrackTop + 3);
        buffer.DrawBox(Left - 1, secondTrackTop - 1, Right - Left + 3, secondTrackBottom - secondTrackTop + 3);
        buffer.DrawBox(Left - 1, thirdTrackTop - 1, Right - Left + 3, thirdTrackBottom - thirdTrackTop + 3);
    }

    public override void Update(float deltaTime)
    {
        
    }
}