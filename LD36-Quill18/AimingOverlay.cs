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
            DrawPath();

            // Draw the reticle
            FrameBuffer.Instance.SetChixel(X - 1, Y, '[');
            FrameBuffer.Instance.SetChixel(X + 1, Y, ']');

            int leftMost = PlayerCharacter.Instance.X < X - 1 ?
                        PlayerCharacter.Instance.X : X - 1;
            int rightMost = PlayerCharacter.Instance.X > X + 1 ?
                        PlayerCharacter.Instance.X : X + 1;
            int topMost = PlayerCharacter.Instance.Y < Y ?
                        PlayerCharacter.Instance.Y : Y;
            int bottomMost = PlayerCharacter.Instance.Y > Y ?
                        PlayerCharacter.Instance.Y : Y;



            Rect r = new Rect(leftMost, topMost, rightMost - leftMost + 1, bottomMost - topMost + 1);

            RedrawRequests.Rects.Add(r);
        }

        public static Tile[] GeneratePath(int x0, int y0, int x1, int y1)
        {
            // https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm

            int xDir = x1 > x0 ? 1 : -1;
            int yDir = y1 > y0 ? 1 : -1;

            double deltaX = x1 - x0;
            double deltaY = y1 - y0;

            int y = y0;

            List<Tile> tiles = new List<Tile>();

            if (deltaX == 0)
            {
                if (y == y1)
                    return tiles.ToArray();

                y += yDir;

                // Just draw vertical line
                while (y != y1+yDir)
                {
                    tiles.Add( Game.Instance.Map.CurrentFloor.GetTile(x0, y) );

                    y += yDir;
                }
                return tiles.ToArray();
            }

            double error = -1.0;
            double deltaerr = Math.Abs(deltaY / deltaX);

            for (int x = x0+xDir; (xDir > 0) ? (x <= x1) : (x >= x1); x += xDir)
            {
                //FrameBuffer.Instance.SetChixel(x, y, '-', ConsoleColor.Yellow);
                tiles.Add(Game.Instance.Map.CurrentFloor.GetTile(x, y));

                error = error + deltaerr;
                while (error > -0.5)
                {
                    y += yDir;
                    //FrameBuffer.Instance.SetChixel(x, y, '|', ConsoleColor.Yellow);
                    tiles.Add(Game.Instance.Map.CurrentFloor.GetTile(x, y));
                    error = error - 1.0;
                }
            }

            // Add the final tile
            //tiles.Add(Game.Instance.Map.CurrentFloor.GetTile(x1, y1));

            return tiles.ToArray();


        }

        void DrawPath()
        {
            // Fix any issues with our targeting
            if (X < 0)
                X = 0;
            if (Y < 0)
                Y = 0;
            if (X >= Game.Instance.Map.CurrentFloor.Width)
                X = Game.Instance.Map.CurrentFloor.Width - 1;
            if (Y >= Game.Instance.Map.CurrentFloor.Height)
                Y = Game.Instance.Map.CurrentFloor.Height - 1;
            
            Tile[] tiles = GeneratePath(PlayerCharacter.Instance.X, PlayerCharacter.Instance.Y, X, Y);
            if (tiles.Length == 0)
            {
                return;
            }

            for (int i = 0; i < tiles.Length-1; i++)
            {
                FrameBuffer.Instance.SetChixel(tiles[i].X, tiles[i].Y, '-', ConsoleColor.Yellow);
            }


            // Draw final X
            //FrameBuffer.Instance.SetChixel(tiles[tiles.Length - 1].X, tiles[tiles.Length - 1].Y, 'X', ConsoleColor.Red);

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

