using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace LD36Quill18
{
    public class Floor
    {

        public Floor(string floorMap, int floorIndex)
        {
            this.FloorIndex = floorIndex;
            this.characters = new HashSet<Character>();

            string[] lines = floorMap.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            // First, figure out the size of the floor
            Height = lines.Length;

            foreach (string s in lines)
            {
                if (s.Length > Width)
                {
                    Width = s.Length;
                }
            }

            tiles = new Tile[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    char c = ' ';
                    if (x < lines[y].Length)
                    {
                        // actual line doesn't have enough text. Assume floor.
                        c = lines[y][x];

                    }
                    tiles[x, y] = new Tile(x, y, this, c);
                }
            }
        }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public int FloorIndex { get; protected set; }

        private Tile[,] tiles;
        private HashSet<Character> characters;

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || x >= this.Width || y < 0 || y >= this.Height)
            {
                throw new Exception();
            }

            return tiles[x, y];
        }

        public void AddCharacter(Character ch)
        {
            characters.Add(ch);
        }

        public void RemoveCharacter(Character ch)
        {
            characters.Remove(ch);
        }

        public void RedrawFullMap()
        {
            // Redraw the whole map
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    tiles[x, y].Draw();
                }
            }

            RedrawCharacters();
        }

        void RedrawCharacters()
        {
            // Draw characters
            foreach (Character ch in characters)
            {
                FrameBuffer.Instance.SetChixel(ch.X, ch.Y, ch.Chixel);
                RedrawRequests.Rects.Add(new Rect(ch.X, ch.Y, 1, 1));
            }
        }

        void RedrawRequestedArea()
        {
            foreach (Rect r in RedrawRequests.Rects)
            {
                for (int x = r.Left; x < r.Left + r.Width; x++)
                {
                    for (int y = r.Top; y < r.Top + r.Height; y++)
                    {
                        if (x < this.Width && y < this.Height)
                        {
                            try
                            {
                                tiles[x, y].Draw();
                            }
                            catch (Exception e)
                            {
                                Game.Instance.DebugMessage(e.ToString());
                            }
                        }
                    }
                }
            }

            RedrawRequests.Clear();
        }

        /// <summary>
        /// Called each "tick" of the game.  Which is...different from drawing?
        /// </summary>
        public void Update(bool doTick)
        {
            // First, redraw all floor tiles "under" characters,
            // and anywhere else that may have been overlayed
            RedrawRequestedArea();

            if (doTick)
            {
                // Process player first
                if (Game.Instance.PlayerCharacter != null)
                {
                    Game.Instance.PlayerCharacter.Update();
                }

                // Process characters (who may move)
                HashSet<Character> chars_temp = new HashSet<Character>(characters);
                foreach (Character ch in chars_temp)
                {
                    if (ch != Game.Instance.PlayerCharacter)
                    {
                        ch.Update();
                    }
                }
            }

            RedrawCharacters();
        }
    }
}

