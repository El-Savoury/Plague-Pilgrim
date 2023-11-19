namespace Plague_Pilgrim
{
    enum CollisionType
    {
        Top,
        Left,
        Right,
        Bottom,
    }

    /// <summary>
    /// Results of a ray collision
    /// </summary>
    struct CollisionResults
    {
        public Vector2 EntryPoint;
        public Vector2 ExitPoint;
        public Vector2 normal;
        public float? t; // Time to point of contact from ray's origin, null if no contact

        public static CollisionResults None
        {
            get
            {
                CollisionResults none;

                none.EntryPoint = Vector2.Zero;
                none.ExitPoint = Vector2.Zero;
                none.normal = Vector2.Zero;
                none.t = null;

                return none;
            }
        }
        public bool Collided
        {
            get
            {
                return t.HasValue && t <= 1;
            }
        }
    }





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

        public Rect2f(Vector2 _min, Texture2D texture)
        {
            min = _min;
            max = new Vector2(_min.X + texture.Width, _min.Y + texture.Height);
        }

        public Rect2f(Vector2 _min, float width, float height)
        {
            min = _min;
            max = new Vector2(_min.X + width, _min.Y + height);
        }

        public float Width
        {
            get { return Math.Abs(max.X - min.X); }
        }

        public float Height
        {
            get { return Math.Abs(max.Y - min.Y); }
        }

        public Vector2 Centre
        {
            get { return (min + max) / 2.0f; }
        }

        public static Rect2f operator +(Rect2f a, Rect2f b)
        {
            float minX = Math.Min(a.min.X, b.min.X);
            float minY = Math.Min(a.min.Y, b.min.Y);
            float maxX = Math.Max(a.max.X, b.max.X);
            float maxY = Math.Max(a.max.Y, b.max.Y);

            return new Rect2f(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        public static Rect2f operator +(Rect2f rect, Vector2 vec)
        {
            rect.min += vec;
            rect.max += vec;

            return rect;
        }

        public Vector2 min;
        public Vector2 max;
    }






    /// <summary>
    /// Utility methods for handling collisions
    /// </summary>
    static class Collision2D
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
        /// <returns>True if they overlap (exclusive)</returns>
        public static bool RectVsRect(Rect2f rect1, Rect2f rect2)
        {
            return rect1.min.X < rect2.max.X && rect2.min.X < rect1.max.X &&
                rect1.min.Y < rect2.max.Y && rect2.min.Y < rect1.max.Y;
        }


        /// <summary>
        /// Checks if a ray intersects a rectangle
        /// </summary>
        /// <param name="ray">Ray to check</param>
        /// <param name="rect">Rectangle to check</param>
        /// <returns>Collision results of intersection</returns>
        public static CollisionResults RayVsRect(Ray2f ray, Rect2f rect)
        {
            CollisionResults results = new CollisionResults();

            //if (PointVsRect(new Point((int)ray.origin.X, (int)ray.origin.Y), rect)) { return CollisionResults.None; }

            if (ray.direction.Length() > 0)
            {
                // Get the intersection points wherever they land on each axis 
                Vector2 tNear = (rect.min - ray.origin) / ray.direction;
                Vector2 tFar = (rect.max - ray.origin) / ray.direction;

                // Swap if far intersection point is closer than near point i.e the ray is going upwards 
                if (tNear.X > tFar.X) { Utility.Swap(ref tNear.X, ref tFar.X); }
                if (tNear.Y > tFar.Y) { Utility.Swap(ref tNear.Y, ref tFar.Y); }

                // Return none if no collision with rectangle edges
                if (tNear.X > tFar.Y || tNear.Y > tFar.X) return CollisionResults.None;

                // If there is a collision, calculate the near and far times to the actual intersections with the rectangle
                float tHitNear = Math.Max(tNear.X, tNear.Y);
                float tHitFar = Math.Min(tFar.X, tFar.Y);

                // Ignore collisions that happen in the negative direction i.e behind the rays origin
                if (tHitFar <= 0.0f) return CollisionResults.None;

                // Assign the results of the collision
                results.EntryPoint = ray.origin + tHitNear * ray.direction;
                results.ExitPoint = ray.origin + tHitFar * ray.direction;
                results.t = tHitNear;

                // Construct collision normal
                if (tNear.X > tNear.Y)
                {
                    if (ray.direction.X < 0) { results.normal = new Vector2(1, 0); }
                    else { results.normal = new Vector2(-1, 0); }
                }
                else if (tNear.X < tNear.Y)
                {
                    if (ray.direction.Y < 0) { results.normal = new Vector2(0, 1); }
                    else { results.normal = new Vector2(0, -1); }
                }
            }

            return results;
        }


        /// <summary>
        /// Compare if moving a rectangle will hit another rectangle
        /// </summary>s
        /// <param name="movingRect">Rectangle that will be moving</param>
        /// <param name="displacement">How far it is moving</param>
        /// <param name="targetRect">Rectangle that is static</param>
        /// <returns>Collision results of collision</returns>
        public static CollisionResults MovingRectVsRect(Rect2f movingRect, Vector2 displacement, Rect2f targetRect)
        {
            //Expand target rect
            Vector2 sizeVec = new Vector2(movingRect.Width * 0.5f, movingRect.Height * 0.5f);

            targetRect.min -= sizeVec;
            targetRect.max += sizeVec;

            return RayVsRect(new Ray2f(new Vector2(movingRect.Centre.X, movingRect.Centre.Y), displacement), targetRect);
        }
    }
}