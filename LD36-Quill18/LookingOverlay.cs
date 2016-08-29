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
            int viewOffsetX = Game.Instance.Map.CurrentFloor.ViewOffsetX;
            int viewOffsetY = Game.Instance.Map.CurrentFloor.ViewOffsetY;

            // Draw the reticle
            //FrameBuffer.Instance.SetChixel(X - 1+viewOffsetX, Y+viewOffsetY, '(');
            //FrameBuffer.Instance.SetChixel(X + 1+viewOffsetX, Y+viewOffsetY, ')');
            Chixel ch = FrameBuffer.Instance.GetChixel(X + viewOffsetX, Y + viewOffsetY);
            if (ch == null)
                return; // out of bounds
            
            ch.BackgroundColor = ConsoleColor.Green;
            ch.Dirty = true;

            //Rect r = new Rect(X - 1 + viewOffsetX, Y + viewOffsetY, 3, 1);
            Rect r = new Rect(X + viewOffsetX, Y + viewOffsetY, 1, 1);

            RedrawRequests.Rects.Add(r);
        }

    }
}

