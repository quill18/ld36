using System;
using System.Collections.Generic;

namespace LD36Quill18
{
    public class Map
    {
        public Map()
        {
            CurrentFloorIndex = 0;


            floors = new Floor[FloorMaps.floor.Length];

            for (int i = 0; i < floors.Length; i++)
            {
                floors[i] = new Floor(FloorMaps.floor[i], i);
            }
        }

        public int CurrentFloorIndex { get; set; }

        public Floor CurrentFloor { 
            get
            {
                return floors[CurrentFloorIndex];
            }
            set
            {
                for (int i = 0; i < floors.Length; i++)
                {
                    if (floors[i] == value)
                    {
                        CurrentFloorIndex = i;
                        return;
                    }
                }

                throw new Exception("Didn't find floor.");
            }
        }

        public int NumFloors
        {
            get
            {
                return floors.Length;
            }
        }

        public Floor GetFloor(int index)
        {
            if (index < 0 || index >= floors.Length)
            {
                throw new Exception();
            }

            return floors[index];
        }

        private Floor[] floors;

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

    }
}

