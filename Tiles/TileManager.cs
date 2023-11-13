﻿namespace Plague_Pilgrim
{
    /// <summary>
    /// Info required to init tile map
    /// </summary>
    struct TileMapInfo
    {
        public Point mTileMapSize;
        // Difficulty
        // Texture pack
    }

    /// <summary>
    /// Class to manage tile map
    /// </summary>
    static class TileManager
    {
        #region rConstants

        static Point MAP_SIZE = new Point(20, 400);
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
        /// <param name="mapSize">Width and height of tile map in tiles</param>
        public static void InitTileMap(Vector2 pos)
        {
            mTileMapPos = pos;
            mTileMap = new Tile[MAP_SIZE.X, MAP_SIZE.Y];
            mDefaultTile = new EmptyTile(Vector2.Zero);
            mTileSize = mDefaultTile.GetSize();
        }


        public static void LoadTileMap()
        {
            int[] bankLeft = GetBankWidths();
            int[] bankRight = GetBankWidths();

            for (int y = 0; y < mTileMap.GetLength(1); y++)
            {
                for (int x = 0; x < mTileMap.GetLength(0); x++)
                {
                    Vector2 tilePos = new Vector2(mTileMapPos.X + x * mTileSize, mTileMapPos.Y - y * mTileSize);

                    mTileMap[x, y] = new EmptyTile(tilePos);
                    mTileMap[x, y].LoadContent();
                }
            }
        }


        private static int[] GetBankWidths()
        {
            int[] widths = new int[mTileMap.GetLength(1)];

            // Set starting width
            int lastWidth = RandomManager.Next(GROUND_MIN_WIDTH, GROUND_MAX_WIDTH + 1);

            int nextMove = 0; // Used to determine which direction to go
            int sectionWidth = 0; // Used to keep track of the current sections width

            // Cycle through our widths
            for (int i = 0; i < widths.Length; i++)
            {
                // Flip coin to determine next move up or down
                nextMove = RandomManager.FlipCoin();

                //Only change the height if current height used more than the minimum required section width
                if (nextMove == 0 && lastWidth > GROUND_MIN_WIDTH && sectionWidth > MIN_SECTION_WIDTH)
                {
                    lastWidth--;
                    sectionWidth = 0;
                }
                else if (nextMove == 1 && lastWidth < GROUND_MAX_WIDTH && sectionWidth > MIN_SECTION_WIDTH)
                {
                    lastWidth++;
                    sectionWidth = 0;
                }

                sectionWidth++;
                widths[i] = lastWidth;
            }
            return widths;
        }

        #endregion rInitialisation








        #region rUpdate

        public static void Update(GameTime gameTime)
        {
        }

        #endregion rUdate








        #region rDraw

        public static void Draw(DrawInfo info)
        {
            Point mapPos = new Point((int)mTileMapPos.X, (int)mTileMapPos.Y);

            foreach (Tile tile in mTileMap)
            {
                Rectangle sourceRect = new Rectangle((int)tile.GetBounds().min.X, (int)tile.GetBounds().min.Y, mTileSize, mTileSize);
                DrawTile(info, sourceRect, tile);
            }


            //for (int x = 0; x < mTileMap.GetLength(0); x++)
            //{
            //    for (int y = 0; y < mTileMap.GetLength(1); y++)
            //    {
            //        Rectangle drawRect = new Rectangle(mapPos.X + x * mTileSize, mapPos.Y - y * mTileSize, mTileSize, mTileSize);
            //        DrawTile(info, drawRect, mTileMap[x, y]);
            //    }
            //}
        }


        /// <summary>
        /// Draw a single tile
        /// </summary>
        /// <param name="info"></param>
        /// <param name="drawDestination"></param>
        /// <param name="tile"></param>
        private static void DrawTile(DrawInfo info, Rectangle drawDestination, Tile tile)
        {
            //Rectangle sourceRect = new Rectangle(tile.GetMapIndex().X * mTileSize, tile.GetMapIndex().Y * mTileSize, mTileSize, mTileSize);
            Draw2D.DrawTexture(info, tile.GetTexture(), new Vector2(drawDestination.X, drawDestination.Y));
        }

        #endregion rDraw








        #region rCollisions

        /// <summary>
        /// Resolve all collisions with an entity
        /// </summary>
        /// <param name="gameTime">Entity to collide</param>
        /// <param name="entity">Frame time</param>
        /// <param name="outputList">List of all collisions</param>
        public static void GatherCollisions(GameTime gameTime, MovingEntity entity, ref List<EntityCollision> outputList)
        {
            // Get players position in next frame
            Rect2f playerBounds = entity.ColliderBounds();
            Rect2f futurePlayerBounds = entity.ColliderBounds() + entity.VelocityToDisplacement(gameTime);

            // Define rectangle encompassing all tiles between the players current position and future position in next frame
            Rectangle tileBounds = PossibleIntersectTiles(playerBounds + futurePlayerBounds);

            // Check all tiles within rectangle for collisions
            for (int x = tileBounds.X; x <= tileBounds.X + tileBounds.Width; x++)
            {
                for (int y = tileBounds.Y; y <= tileBounds.Y + tileBounds.Height; y++)
                {
                    if (mTileMap[x, y].GetEnabled() == false) { continue; }

                    CollisionResults collisionResults = mTileMap[x, y].Collide(entity, gameTime);

                    if (collisionResults.Collided)
                    {
                        outputList.Add(new TileEntityCollision(collisionResults, new Point(x, y)));
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
