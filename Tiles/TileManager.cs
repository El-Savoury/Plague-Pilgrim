using System.Security.Cryptography.X509Certificates;

namespace Plague_Pilgrim
{
    /// <summary>
    /// Class to manage tile map
    /// </summary>
    internal class TileManager
    {
        #region rConstants

        int TILE_SIZE = 32;

        #endregion rConstants






        #region rMembers

        Tile[,] mTileMap;
        Vector2 mTileMapPos;

        #endregion rMembers







        #region rInitialisation

        /// <summary>
        /// Constructor
        /// </summary>
        public TileManager()
        {
        }


        /// <summary>
        /// Inititialse size and world position of tile map
        /// </summary>
        /// <param name="pos">Top left corner of tile map</param>
        /// <param name="mapSize">Width and height of tile map in tiles</param>
        public void InitTileMap(Vector2 pos, Point mapSize)
        {
            mTileMapPos = pos;
            mTileMap = new Tile[mapSize.X, mapSize.Y];
        }


        public void LoadTileMap()
        {
            for (int x = 0; x < mTileMap.GetLength(0); x++)
            {
                for (int y = 0; y < mTileMap.GetLength(1); y++)
                {
                    int index = x + y * TILE_SIZE;

                    mTileMap[x, y] = new GroundTile(GetTileTopLeft(new Point(x,y)), this);
                    mTileMap[x, y].LoadContent();
                }
            }
        }

        #endregion rInitialisation



        #region rUpdate

        public void Update(GameTime gameTime)
        {
        }

        #endregion rUdate







        #region rDraw

        public void Draw(DrawInfo info)
        {
            Point offset = new Point((int)mTileMapPos.X, (int)mTileMapPos.Y);

            for (int x = 0; x < mTileMap.GetLength(0); x++)
            {
                for (int y = 0; y < mTileMap.GetLength(1); y++)
                {
                    Rectangle drawRect = new Rectangle(offset.X + x * TILE_SIZE, offset.Y + y * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                    DrawTile(info, drawRect, mTileMap[x, y]);
                }
            }
        }


        /// <summary>
        /// Draw a single tile
        /// </summary>
        /// <param name="info"></param>
        /// <param name="drawDestination"></param>
        /// <param name="tile"></param>
        private void DrawTile(DrawInfo info, Rectangle drawDestination, Tile tile)
        {
            Rectangle sourceRect = new Rectangle(tile.mTileMapIndex.X * TILE_SIZE, tile.mTileMapIndex.Y * TILE_SIZE, TILE_SIZE, TILE_SIZE);
            Draw2D.DrawTexture(info, tile.mTexture, new Vector2(drawDestination.X, drawDestination.Y));
        }

        #endregion rDraw




        #region rUtility

        /// <summary>
        /// Get tile at a world position
        /// </summary>
        /// <param name="pos">Tile world coordinates</param>
        /// <returns>Tile reference</returns>
        public Tile GetTile(Vector2 pos)
        {
            return GetTile(GetTileMapCoord(pos));
        }


        /// <summary>
        /// Get tile from its coordinate in tile map
        /// </summary>
        /// <param name="coord">Tile map coordintate of desired tile</param>
        /// <returns>Tile reference</returns>
        public Tile GetTile(Point coord)
        {
            return GetTile(coord.X, coord.Y);
        }


        /// <summary>
        /// Get tile from its coordinate in tile map
        /// </summary>
        /// <param name="x">Tile x coordinate</param>
        /// <param name="y">Tile y coordinate</param>
        /// <returns>Tile reference</returns>
        public Tile GetTile(int x, int y)
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
        public Point GetTileMapCoord(Vector2 pos)
        {
            pos = pos - mTileMapPos;
            pos = pos / TILE_SIZE;

            Point coord = new Point((int)Math.Floor(pos.X), (int)Math.Floor(pos.Y));

            return coord;
        }


        /// <summary>
        /// Convert index to tiles top left
        /// </summary>
        /// <param name="index">Tile map index of tile</param>
        /// <returns>Tiles top left corner position</returns>
        public Vector2 GetTileTopLeft(Point index)
        {
            Vector2 result = mTileMapPos;

            result.X += index.X * TILE_SIZE;
            result.Y += index.Y * TILE_SIZE;

            return result;
        }

        #endregion rUtility
    }
}
