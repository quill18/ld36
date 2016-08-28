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
            Height = lines.Length-1;

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
                    if (x < lines[y+1].Length)
                    {
                        // actual line doesn't have enough text. Assume floor.
                        c = lines[y+1][x];

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

        public int ViewOffsetX { get; protected set; }
        public int ViewOffsetY { get; protected set; }
        private int centerThresholdX = 24;
        private int centerThresholdY = 7;

        public Tile Upstair   { get; set; }
        public Tile Downstair { get; set; }

        /// <summary>
        /// Redraws the tile at world coord x and y.
        /// </summary>
        /// <param name="x">Tile coord (not view).</param>
        /// <param name="y">Tile coord (not view).</param>
        public void RedrawTileAt(int x, int y)
        {
            x += ViewOffsetX;
            y += ViewOffsetY;
            RedrawRequests.Rects.Add(new Rect(x, y, 1, 1));
        }

        public void Recenter(bool forced = false)
        {
            if (PlayerCharacter.Instance.Floor != this)
            {
                return;
            }

            // Center the viewpoint such that the player in the middle
            // of the rendering area.
            Rect area = Game.Instance.LevelRenderArea;

            // Viewport's center spot
            int cX = area.Width / 2 + area.Left;
            int cY = area.Height / 2 + area.Top;

            // How far from this is the player's current rendered position?
            int dX = cX - PlayerCharacter.Instance.X - ViewOffsetX;
            int dY = cY - PlayerCharacter.Instance.Y - ViewOffsetY;

            if (forced == false)
            {
                if (Math.Abs(dX) < centerThresholdX && Math.Abs(dY) < centerThresholdY)
                    return;
            }

            // Update offset so player is in the center!
            ViewOffsetX += dX;
            ViewOffsetY += dY;
            RedrawRequests.FullRedraw();
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || x >= this.Width || y < 0 || y >= this.Height)
            {
                //throw new Exception();
                return null;
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
                    tiles[x, y].Draw(ViewOffsetX, ViewOffsetY);
                }
            }

            RedrawCharacters();
        }

        public void RedrawCharacters()
        {
            // Draw characters
            foreach (Character ch in characters)
            {
                if (ch.Tile.WasSeen)
                {
                    FrameBuffer.Instance.SetChixel(ch.X + ViewOffsetX, ch.Y + ViewOffsetY, ch.Chixel);
                    RedrawRequests.Rects.Add(new Rect(ch.X + ViewOffsetX, ch.Y + ViewOffsetY, 1, 1));
                }
            }
        }

        public void RedrawRequestedArea()
        {
            foreach (Rect r in RedrawRequests.Rects)
            {
                for (int x = r.Left; x < r.Left + r.Width; x++)
                {
                    for (int y = r.Top; y < r.Top + r.Height; y++)
                    {
                        int tX = x - ViewOffsetX;
                        int tY = y - ViewOffsetY;

                        if(tX > 0 && tX < Width && tY > 0 && tY < Height)
                        {
                            tiles[tX, tY].Draw(ViewOffsetX, ViewOffsetY);
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
            Recenter();
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

                int numEnemyTurns = 1;
                if (PlayerCharacter.Instance.Energy <= 0)
                    numEnemyTurns = 2;

                for (int i = 0; i < numEnemyTurns; i++)
                {
                    HashSet<Character> chars_temp = new HashSet<Character>(characters);
                    foreach (Character ch in chars_temp)
                    {
                        if (Game.Instance.PlayerCharacter == null)
                            return;
                        
                        if (ch != Game.Instance.PlayerCharacter)
                        {
                            ch.Update();
                        }
                    }
                }
            }

            RedrawCharacters();
        }
    }
}

