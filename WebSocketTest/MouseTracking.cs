using System;
using System.Runtime.InteropServices;

namespace Server
{
    class MouseTracking
    {
        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public int X;
            public int Y;
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        public static void GetCursorPosition(out int X, out int Y)
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            X = lpPoint.X;
            Y = lpPoint.Y;
        }

    }
}
