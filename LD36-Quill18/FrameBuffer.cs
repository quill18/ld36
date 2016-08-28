using System;

namespace LD36Quill18
{
    public class FrameBuffer
    {
        public FrameBuffer(int left, int top, int width, int height)
        {
            if (_instance != null)
            {
                //throw new Exception("Already have a framebuffer.");
            }
            else
            {
                _instance = this;
            }

            this.Left = left;
            this.Top = top;
            this.Width = width;
            this.Height = height;
            Clear();
        }

        static public FrameBuffer Instance
        {
            get { return _instance; }
        }

        static private FrameBuffer _instance;

        public void Clear()
        {
            Console.Clear();
            chixels = new Chixel[this.Width, this.Height];
        }

        public void DrawFrame()
        {
            bool forceDirty = false;

            if (lastWindowWidth != Console.WindowWidth || lastWindowHeight != Console.WindowHeight)
            {
                if (_instance == this)
                {
                    Console.Clear();
                }
                forceDirty = true;

                lastWindowWidth = Console.WindowWidth;
                lastWindowHeight = Console.WindowHeight;
            }

            for (int y = 0; y < Height; y++)
            {
                if (y < Console.WindowHeight)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (x < Console.WindowWidth)
                        {
                            Chixel ch = this.chixels[x, y];
                            if (ch != null && (ch.Dirty == true || forceDirty))
                            {
                                Console.SetCursorPosition(x + Left, y + Top);
                                Console.ForegroundColor = ch.ForegroundColor;
                                Console.BackgroundColor = ch.BackgroundColor;
                                Console.Write(ch.Glyph);
                                ch.Dirty = false;
                            }
                        }
                    }
                }
            }
        }

        public void SetChixel(int x, int y, Chixel chixel)
        {
            SetChixel(x, y, chixel.Glyph, chixel.ForegroundColor, chixel.BackgroundColor);
        }

        public void SetChixel(int x, int y, Char c, ConsoleColor fg_color = ConsoleColor.White, ConsoleColor bg_color = ConsoleColor.Black)
        {
            x -= Left;
            y -= Top;
            // check that the chixel is actually changed
            // if so, update values and set dirty

            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                //throw new Exception();
                return;
            }

            Chixel ch = this.chixels[x, y];
            if (ch != null && ch.Glyph == c && ch.ForegroundColor == fg_color && ch.BackgroundColor == bg_color)
            {
                // No change.
                return;
            }

            this.chixels[x, y] = new Chixel(c, fg_color, bg_color);
        }

        public void Write(int x, int y, string s, ConsoleColor fg_color=ConsoleColor.White, ConsoleColor bg_color=ConsoleColor.Black)
        {
            // TODO: Detect ANSI escapes and output as a single write.

            int initX = x;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\n')
                {
                    x = initX;
                    y++;
                    continue;
                }

                //if (s[i] == '\x1b')
               // {
                //    int ansiEnd = s.IndexOf('m', i);
                //}

                SetChixel(x, y, s[i], fg_color, bg_color);
                x++;
            }
        }

        private int Left;
        private int Top;
        public int Width { get; protected set; } 
        public int Height { get; protected set; }

        private Chixel[,] chixels;

        private int lastWindowWidth = -1;
        private int lastWindowHeight = -1;

    }
}

