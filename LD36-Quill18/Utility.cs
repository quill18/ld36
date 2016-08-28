using System;
using System.Collections.Generic;
using System.Text;

namespace LD36Quill18
{
    static public class Utility
    {
        static public int CircleDistance(int x0, int y0, int x1, int y1)
        {
            int x = x0 - x1;
            int y = y0 - y1;

            return (int)Math.Sqrt(x * x + y * y);
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
                while (y != y1 + yDir)
                {
                    tiles.Add(Game.Instance.Map.CurrentFloor.GetTile(x0, y));

                    y += yDir;
                }
                return tiles.ToArray();
            }

            double error = -1.0;
            double deltaerr = Math.Abs(deltaY / deltaX);

            for (int x = x0 + xDir; (xDir > 0) ? (x <= x1) : (x >= x1); x += xDir)
            {
                //FrameBuffer.Instance.SetChixel(x, y, '-', ConsoleColor.Yellow);

                error = error + deltaerr;
                if (error < -0.5)
                {
                    tiles.Add(Game.Instance.Map.CurrentFloor.GetTile(x, y));
                }
                else
                {
                    while (error > -0.5)
                    {
                        y += yDir;
                        //FrameBuffer.Instance.SetChixel(x, y, '|', ConsoleColor.Yellow);
                        tiles.Add(Game.Instance.Map.CurrentFloor.GetTile(x, y));
                        error = error - 1.0;
                    }
                }
            }

            // Add the final tile
            //tiles.Add(Game.Instance.Map.CurrentFloor.GetTile(x1, y1));

            return tiles.ToArray();
        }

        static public void DrawPath(Tile[] tiles, bool stopAtWallsOrCharacters, ConsoleColor fg_color)
        {
            int viewOffsetX = Game.Instance.Map.CurrentFloor.ViewOffsetX;
            int viewOffsetY = Game.Instance.Map.CurrentFloor.ViewOffsetY;

            if (tiles.Length == 0)
            {
                return;
            }

            int leftMost = tiles[0].X;
            int rightMost = tiles[0].X;
            int topMost = tiles[0].Y;
            int bottomMost = tiles[0].Y;

            for (int i = 0; i < tiles.Length - 1; i++)
            {
                if (stopAtWallsOrCharacters && (tiles[i].IsLookable() == false || tiles[i].Character != null))
                    break;

                char c = '-';
                if (i == 0 && tiles.Length > 0)
                {
                    c = GetCharForBeam(tiles[i], tiles[i+1]);
                }
                else
                {
                    c = GetCharForBeam(tiles[i - 1], tiles[i]);
                }

                FrameBuffer.Instance.SetChixel(tiles[i].X + viewOffsetX, tiles[i].Y + viewOffsetY, c, fg_color);

                if (tiles[i].X < leftMost)
                    leftMost = tiles[i].X;
                if (tiles[i].X > rightMost)
                    rightMost = tiles[i].X;
                if (tiles[i].Y < topMost)
                    topMost = tiles[i].Y;
                if (tiles[i].Y > bottomMost)
                    bottomMost = tiles[i].Y;
            }

            Rect r = new Rect(leftMost + viewOffsetX, topMost + viewOffsetY, rightMost - leftMost + 1, bottomMost - topMost + 1);

            RedrawRequests.Rects.Add(r);


            // Draw final X
            //FrameBuffer.Instance.SetChixel(tiles[tiles.Length - 1].X, tiles[tiles.Length - 1].Y, 'X', ConsoleColor.Red);

        }

        static char GetCharForBeam(Tile prev, Tile curr)
        {

            if (curr.X > prev.X)
            {
                // Moving to the right
                if (curr.Y > prev.Y)
                {
                    // And down
                    return '\\';
                }
                else if (curr.Y < prev.Y)
                {
                    // And Up
                    return '/';
                }
            }
            else if (curr.X < prev.X)
            {
                // Moving to the left
                if (curr.Y > prev.Y)
                {
                    // And down
                    return '/';
                }
                else if (curr.Y < prev.Y)
                {
                    // And Up
                    return '\\';
                }
            }

            if (prev.X == curr.X)
            {
                return '|';
            }

            return '-';
        }

        static public string WordWrap(string s, int width=60)
        {
			if (s == null)
				return "";
			
            string[] words = s.Split(' ');

            StringBuilder newSentence = new StringBuilder();


            string line = "";
            foreach (string word in words)
            {
                if ((line + word).Length + 1 > width)
                {
                    newSentence.AppendLine(line);
                    line = "";
                }

                line += string.Format("{0} ", word);
            }

            if (line.Length > 0)
                newSentence.AppendLine(line);

            return newSentence.ToString();
        }
    }

}

