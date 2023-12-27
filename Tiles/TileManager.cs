using System.Security.Cryptography;

namespace Plague_Pilgrim
{
    /// <summary>
    /// Info required to init tile map
    /// </summary>
    struct TileMapInfo
    {
        public Point mTileMapSize;
        // Difficulty
        // Theme 
    }

    /// <summary>
    /// Class to manage tile map
    /// </summary>
    static class TileManager
    {
        #region rConstants

        static Point MAP_SIZE = new Point(20, 100);
        static int GROUND_MAX_WIDTH = 10;
        static int GROUND_MIN_WIDTH = 5;
        static int MIN_SECTION_WIDTH = 2;

        #endregion rConstants







        #region rMembers

        static Tile[,] mTileMap;
        static Vector2 mTileMapPos;
        static Tile mDefaultTile;
        static int mTileSize;

        #endregion rMembers








        #region rInitialisation

        /// <summary>
        /// Inititialse size and world position of tile map
        /// </summary>
        /// <param name="pos">Top left corner of tile map</param>
        public static void InitTileMap(Vector2 pos)
        {
            mTileMapPos = pos;
            mTileMap = new Tile[MAP_SIZE.X, MAP_SIZE.Y];
            mDefaultTile = new EmptyTile(Vector2.Zero);
            mTileSize = mDefaultTile.GetSize();
        }


        public static void LoadTileMap()
        {
            //int[] bankLeft = GetBankWidths();
            //int[] bankRight = GetBankWidths();

            for (int y = 0; y < mTileMap.GetLength(1); y++)
            {
                for (int x = 0; x < mTileMap.GetLength(0); x++)
                {
                    Vector2 tilePos = new Vector2(mTileMapPos.X + x * mTileSize, mTileMapPos.Y + y * mTileSize);

                    if (x == RandomManager.Next(0, mTileMap.GetLength(0)))
                    {
                        mTileMap[x, y] = new SlowTile(tilePos);
                    }
                    else
                    {
                        mTileMap[x, y] = new EmptyTile(tilePos);
                    }

                    mTileMap[x, y].LoadContent();
                }
            }
        }


        //private static int[] GetBankWidths()
        //{
        //    int[] widths = new int[mTileMap.GetLength(1)];

        //    // Set starting width
        //    int lastWidth = RandomManager.Next(GROUND_MIN_WIDTH, GROUND_MAX_WIDTH + 1);

        //    int nextMove = 0; // Used to determine which direction to go
        //    int sectionWidth = 0; // Used to keep track of the current sections width

        //    // Cycle through our widths
        //    for (int i = 0; i < widths.Length; i++)
        //    {
        //        // Flip coin to determine next move up or down
        //        nextMove = RandomManager.FlipCoin();

        //        //Only change the height if current height used more than the minimum required section width
        //        if (nextMove == 0 && lastWidth > GROUND_MIN_WIDTH && sectionWidth > MIN_SECTION_WIDTH)
        //        {
        //            lastWidth--;
        //            sectionWidth = 0;
        //        }
        //        else if (nextMove == 1 && lastWidth < GROUND_MAX_WIDTH && sectionWidth > MIN_SECTION_WIDTH)
        //        {
        //            lastWidth++;
        //            sectionWidth = 0;
        //        }

        //        sectionWidth++;
        //        widths[i] = lastWidth;
        //    }
        //    return widths;
        //}

        #endregion rInitialisation








        #region rUpdate

        public static void UpdateVisibleTiles(GameTime gameTime, Rect2f box)
        {
            Rectangle visibleTiles = PossibleIntersectTiles(box);

            for (int x = visibleTiles.X; x <= visibleTiles.X + visibleTiles.Width; x++)
            {
                for (int y = visibleTiles.Y; y <= visibleTiles.Y + visibleTiles.Height; y++)
                {
                    mTileMap[x, y].SetEnabled(true);
                }
            }

            foreach (Tile tile in mTileMap)
            {
                if (tile.IsEnabled())
                {
                    tile.Update(gameTime);

                    if (tile.GetBounds().min.Y > box.max.Y)
                    {
                        tile.SetEnabled(false);
                    }
                }
            }
        }

        #endregion rUpdate








        #region rDraw

        public static void Draw(DrawInfo info)
        {
            foreach (Tile tile in mTileMap)
            {
                if (tile.IsEnabled())
                {
                    Rectangle sourceRect = new Rectangle((int)tile.GetBounds().min.X, (int)tile.GetBounds().min.Y, mTileSize, mTileSize);
                    DrawTile(info, sourceRect, tile);
                }
            }
        }


        /// <summary>
        /// Draw a single tile
        /// </summary>
        /// <param name="info"></param>
        /// <param name="drawDestination"></param>
        /// <param name="tile"></param>
        private static void DrawTile(DrawInfo info, Rectangle drawDestination, Tile tile)
        {
            Draw2D.DrawTexture(info, tile.GetTexture(), new Vector2(drawDestination.X, drawDestination.Y));
        }

        #endregion rDraw








        #region rCollisions

        /// <summary>
        /// Check if an entity has collided with any tiles. Call tile response if they have.
        /// </summary>
        public static void ResolveEntityTileCollision(GameTime gameTime, MovingEntity entity)
        {
            Rect2f bounds = entity.ColliderBounds();
            Rect2f futureBounds = bounds + entity.VelocityToDisplacement(gameTime);

            // Gather possible collision tiles.
            Rectangle tileBounds = PossibleIntersectTiles(bounds + futureBounds);
            List<Tile> possibleCollisions = new List<Tile>();

            for (int x = tileBounds.X; x <= tileBounds.X + tileBounds.Width; x++)
            {
                for (int y = tileBounds.Y; y <= tileBounds.Y + tileBounds.Height; y++)
                {
                    if (mTileMap[x, y].IsEnabled() & Collision2D.RectVsRect(mTileMap[x, y].GetBounds(), entity.ColliderBounds()))
                    {
                        possibleCollisions.Add(mTileMap[x, y]);
                    }
                }
            }

            // Sort order of collisions based on proximity to player.
            if (possibleCollisions.Count > 0)
            {
                List<Tile> sortedList = SortCollisions(entity, possibleCollisions);

                foreach (Tile tile in sortedList)
                {
                    if (tile.IsSolid())
                    {
                        tile.OnEntityCollision(entity);
                    }
                    else
                    {
                        tile.OnEntityIntersect(entity);
                    }
                }
            }
        }


        /// <summary>
        /// Find rectangle containing all possible tiles that an object will intersect with
        /// </summary>
        /// <param name="box">Collision box of object to check</param>
        /// <returns>Rectangle of indices to tiles</returns>
        private static Rectangle PossibleIntersectTiles(Rect2f box)
        {
            // Get tilemap index of top left and bottom right tiles in box
            box.min = (box.min - mTileMapPos) / mTileSize;
            box.max = (box.max - mTileMapPos) / mTileSize;

            Point rectMin = new Point(Math.Max((int)box.min.X - 1, 0), Math.Max((int)box.min.Y - 1, 0));
            Point rectMax = new Point(Math.Min((int)box.max.X + 2, mTileMap.GetLength(0) - 1), Math.Min((int)box.max.Y + 2, mTileMap.GetLength(1) - 1));

            // Return a rectangle with size in number of tiles. (rectMin - rectMax gets the number of tiles between the two points rather than all tiles frome top left of tile map)
            return new Rectangle(rectMin, rectMax - rectMin);
        }



        /// <summary>
        /// Sorts possible tile collisions based on their proximity to the player.
        /// </summary>
        /// <param name="entity">Entity to check for collisions</param>
        /// <param name="tiles">Tiles near enough to be collided with</param>
        /// <returns>List of collisions from closest to furthest from entity</returns>
        private static List<Tile> SortCollisions(MovingEntity entity, List<Tile> tiles)
        {
            List<Tile> returnList = new List<Tile> { tiles[0] };

            for (int i = 1; i < tiles.Count(); i++)
            {
                float iDist = (tiles[i].GetCentre() - entity.GetCentre()).Length();
                float jDist = (returnList[i - 1].GetCentre() - entity.GetCentre()).Length();

                if (iDist <= jDist)
                {
                    returnList.Insert(i - 1, tiles[i]);
                }
                else
                {
                    returnList.Add(tiles[i]);
                }
            }

            return returnList;
        }

        #endregion rCollisionss






        #region rUtility

        /// <summary>
        /// Get size of tile map
        /// </summary>
        /// <returns>Rows and columns in tilemap</returns>
        public static Point GetSize()
        {
            return MAP_SIZE;
        }


        /// <summary>
        /// Return tile map width in pixels
        /// </summary>
        public static int GetWidth()
        {
            return mTileMap.GetLength(0) * mTileSize;
        }


        /// <summary>
        /// Return tile map height in pixels
        /// </summary>
        public static int GetHeight()
        {
            return mTileMap.GetLength(1) * mTileSize;
        }


        /// <summary>
        /// Get tile at a world position
        /// </summary>
        /// <param name="pos">Tile world coordinates</param>
        /// <returns>Tile reference</returns>
        public static Tile GetTile(Vector2 pos)
        {
            return GetTile(GetTileMapCoord(pos));
        }


        /// <summary>
        /// Get tile from its coordinate in tile map
        /// </summary>
        /// <param name="coord">Tile map coordintate of desired tile</param>
        /// <returns>Tile reference</returns>
        public static Tile GetTile(Point coord)
        {
            return GetTile(coord.X, coord.Y);
        }


        /// <summary>
        /// Get tile from its coordinate in tile map
        /// </summary>
        /// <param name="x">Tile x coordinate</param>
        /// <param name="y">Tile y coordinate</param>
        /// <returns>Tile reference</returns>
        public static Tile GetTile(int x, int y)
        {
            if (x >= 0 && x < mTileMap.GetLength(0) &&
                y >= 0 && y < mTileMap.GetLength(1))
            {
                return mTileMap[x, y];
            }

            return null;
        }


        /// <summary>
        /// Convert world space position to tile map
        /// </summary>
        /// <param name="pos">World space position</param>
        /// <returns><Tile map index. Note may be out of bounds/returns>
        public static Point GetTileMapCoord(Vector2 pos)
        {
            pos = pos - mTileMapPos;
            pos = pos / mTileSize;

            Point coord = new Point((int)Math.Floor(pos.X), (int)Math.Floor(pos.Y));

            return coord;
        }


        /// <summary>
        /// Convert index to tiles top left
        /// </summary>
        /// <param name="index">Tile map index of tile</param>
        /// <returns>Tiles top left corner position</returns>
        public static Vector2 GetTilePos(Point index)
        {
            Vector2 result = mTileMapPos;

            result.X += index.X * mTileSize;
            result.Y += index.Y * mTileSize;

            return result;
        }

        #endregion rUtility
    }
}
