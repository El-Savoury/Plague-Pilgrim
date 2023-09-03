namespace Plague_Pilgrim
{
    /// <summary>
    /// Represents a finite ray in world space
    /// </summary>
    struct Ray2f
    {
        public Ray2f(Vector2 _origin, Vector2 _direction)
        {
            origin = _origin;
            direction = _direction;
        }

        public Vector2 origin;
        public Vector2 direction;
    }





    /// <summary>
    /// Rectangle in world space used for colliders
    /// </summary>
    struct Rect2f
    {
        public Vector2 min;
        public Vector2 max;

        public Rect2f(Vector2 vec1, Vector2 vec2)
        {
            min = new Vector2(MathF.Min(vec1.X, vec2.X), MathF.Min(vec1.Y, vec2.Y));
            max = new Vector2(MathF.Max(vec1.X, vec2.X), MathF.Max(vec1.Y, vec2.Y));
        }

        public Rect2f(Rectangle rect)
        {
            min = new Vector2(rect.X, rect.Y);
            max = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
        }
    }






    /// <summary>
    /// Utility methods for handling collisions
    /// </summary>
    static class Collision
    {
        /// <summary>
        /// Check if a point is within a rectangle
        /// </summary>
        /// <param name="point">The point we are checking</param>
        /// <param name="rect">Rectangle to check against</param>
        /// <returns>True if point is inside rectangle (inclusive)</returns>
        public static bool PointVsRect(Point point, Rect2f rect)
        {
            return (point.X >= rect.min.X && point.Y >= rect.min.Y &&
                point.X <= rect.max.X && point.Y <= rect.max.Y);
        }


        /// <summary>
        /// Check if two rectangles overlap
        /// </summary>
        /// <param name="rect1">First rectangle</param>
        /// <param name="rect2">Second rectangle</param>
        /// <returns>True if they overlap (inclusive)</returns>
        public static bool RectVsRect(Rect2f rect1, Rect2f rect2)
        {
            return rect1.min.X <= rect2.max.X && rect2.min.X <= rect1.max.X &&
                rect1.min.Y <= rect2.max.Y && rect2.min.Y <= rect1.max.Y;
        }
    }
}
