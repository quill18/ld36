using System;
using System.Collections.Generic;

namespace LD36Quill18
{
    public class LookingOverlay
    {
        public LookingOverlay()
        {
            X = PlayerCharacter.Instance.X;
            Y = PlayerCharacter.Instance.Y;
        }

        // Where we are looking
        public int X;
        public int Y;

        public void Draw()
        {

            // Draw the reticle
            FrameBuffer.Instance.SetChixel(X - 1, Y, '(');
            FrameBuffer.Instance.SetChixel(X + 1, Y, ')');

            Rect r = new Rect(X-1, Y, 3, 1);

            RedrawRequests.Rects.Add(r);
        }

    }
}

