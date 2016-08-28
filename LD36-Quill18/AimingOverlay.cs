using System;
using System.Collections.Generic;

namespace LD36Quill18
{
    public class AimingOverlay
    {
        public AimingOverlay()
        {
            X = PlayerCharacter.Instance.X;
            Y = PlayerCharacter.Instance.Y;
        }

        // Where we are aiming
        public int X;
        public int Y;

        public void Draw()
        {
            // Draw the path
            Utility.DrawPath( Utility.GeneratePath(PlayerCharacter.Instance.X, PlayerCharacter.Instance.Y, X, Y), true, ConsoleColor.Yellow );

            int viewOffsetX = Game.Instance.Map.CurrentFloor.ViewOffsetX;
            int viewOffsetY = Game.Instance.Map.CurrentFloor.ViewOffsetY;

            // Draw the reticle
            FrameBuffer.Instance.SetChixel(X - 1 + viewOffsetX, Y + viewOffsetY, '[');
            FrameBuffer.Instance.SetChixel(X + 1 + viewOffsetX, Y + viewOffsetY, ']');

            int leftMost = PlayerCharacter.Instance.X < X - 1 ?
                        PlayerCharacter.Instance.X : X - 1;
            int rightMost = PlayerCharacter.Instance.X > X + 1 ?
                        PlayerCharacter.Instance.X : X + 1;
            int topMost = PlayerCharacter.Instance.Y < Y ?
                        PlayerCharacter.Instance.Y : Y;
            int bottomMost = PlayerCharacter.Instance.Y > Y ?
                        PlayerCharacter.Instance.Y : Y;



            Rect r = new Rect(leftMost + viewOffsetX, topMost + viewOffsetY, rightMost - leftMost + 1, bottomMost - topMost + 1);

            RedrawRequests.Rects.Add(r);
        }


        void DrawPath2()
        {
            int dX = PlayerCharacter.Instance.X;
            int dY = PlayerCharacter.Instance.Y;

            while (dX != X || dY != Y)
            {
                char c;
                // Step once towards the target, then draw a character
                if (dX < X && dY < Y)
                {
                    // We are left and above
                    c = '\\'; // going down and to the right
                    dX += 1;
                    dY += 1;
                }
                else if (dX > X && dY > Y)
                {
                    // We are right and below
                    c = '\\'; // going up and to the left
                    dX -= 1;
                    dY -= 1;
                }
                else if (dY == Y)
                {
                    c = '-';
                    dX += dX > X ? -1 : 1;
                }
                else if (dX == X)
                {
                    c = '|';
                    dY += dY < Y ? -1 : 1;
                }
                else {
                    c = '/';
                    if (dX > X)
                    {
                        dX -= 1;
                        dY += 1;
                    }
                    else
                    {
                        dX += 1;
                        dY -= 1;
                    }
                }

                if (dX != X || dY != Y)
                {
                    FrameBuffer.Instance.SetChixel(dX, dY, c, ConsoleColor.Yellow);
                }

            }
        }
    }
}

