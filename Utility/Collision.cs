namespace Plague_Pilgrim
{
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

    }
}
