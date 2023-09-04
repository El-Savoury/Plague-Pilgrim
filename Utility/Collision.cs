using System.Security.AccessControl;

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
    struct RayCollision
    {
        public Vector2 collisionPoint;
        public Vector2 normal;
        public float? t; // Time to point of contact from ray's origin, null if no contact

        public static RayCollision None
        {
            get
            {
                RayCollision none;

                none.collisionPoint = Vector2.Zero;
                none.normal = Vector2.Zero;
                none.t = null;

                return none;
            }
        }
        public bool Collided
        {
            get
            {
                return t.HasValue;
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


        public static RayCollision RayVsRect(Ray2f ray, Rect2f rect)
        {
            RayCollision results = new RayCollision();

            // Get the intersection points wherever they land on each axis 
            float Nx = (rect.min.X - ray.origin.X) - ray.direction.X;
            float Ny = (rect.min.Y - ray.origin.Y) - ray.direction.Y;

            float Fx = (rect.max.X - ray.origin.X) - ray.direction.X;
            float Fy = (rect.max.Y - ray.origin.Y) - ray.direction.Y;

            Vector2 tNear = new Vector2(Nx, Ny);
            Vector2 tFar = new Vector2(Fx, Fy);

            // Swap if far intersection point is closer than near point i.e the ray is going upwards 
            if (tNear.X > tFar.X) { Utility.Swap(ref tNear.X, ref tFar.X); }
            if (tNear.Y > tFar.Y) { Utility.Swap(ref tNear.Y, ref tFar.Y); }

            // Return false if no collision with rectangle edges
            if (tNear.X > tFar.Y || tNear.Y > tFar.X) return RayCollision.None;

            // If there is a collision, calculate the near and far times to the actual intersections with the rectangle
            float tHitNear = Math.Max(tNear.X, tNear.Y);
            float tHitFar = Math.Min(tFar.X, tFar.Y);

            // Ignore collisions that happen in the negative direction i.e behind the rays origin
            if (tHitFar < 0.0f) return RayCollision.None;

            // Assign the results of the collision
            results.collisionPoint = ray.origin + tHitNear * ray.direction;

            // Construct normal vector
            if (tNear.X > tNear.Y) // Hit from x axis first
            {
                if (ray.direction.X < 0) { results.normal = new Vector2(1, 0); }
                else { results.normal = new Vector2(-1, 0); }
            }
            else if (tNear.X > tNear.Y) // Hit from y axis first
            {
                if (ray.direction.Y < 0) { results.normal = new Vector2(0, 1); }
                else { results.normal = new Vector2(0, -1); }
            }

            return results;
        }
    }
}
