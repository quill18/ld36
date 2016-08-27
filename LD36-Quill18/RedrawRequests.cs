using System;
using System.Collections.Generic;

namespace LD36Quill18
{
    public static class RedrawRequests
    {
        static RedrawRequests()
        {
            Clear();
        }

        public static void Clear()
        {
            Rects = new List<Rect>();
        }

        public static void FullRedraw()
        {
            Clear();
            FrameBuffer.Instance.Clear();
            Rects.Add(new Rect(0, 0, FrameBuffer.Instance.Width, FrameBuffer.Instance.Height));
        }

        public static List<Rect> Rects;
    }

}

