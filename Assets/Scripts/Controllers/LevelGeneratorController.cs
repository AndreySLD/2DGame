using UnityEngine.Tilemaps;
using UnityEngine;

namespace Platformer
{
    public class LevelGeneratorController
    {
        private Tilemap _tilemap;
        private Tile _groundTile;
        private int _mapHeight;
        private int _mapWidth;
        private bool _borders;
        private int _fillPercent;
        private int _smoothFactor;

        private int[,] _map;

        private const int _sideCount = 4;

        public LevelGeneratorController(LevelGeneratorView levelGeneratorView)
        {
            _tilemap = levelGeneratorView.Tilemap;
            _groundTile = levelGeneratorView.GroundTile;
            _mapHeight = levelGeneratorView.MapHeight;
            _mapWidth = levelGeneratorView.MapWidth;
            _borders = levelGeneratorView.Borders;
            _fillPercent = levelGeneratorView.FillPercent;
            _smoothFactor = levelGeneratorView.SmoothFactor;

            _map = new int[_mapWidth, _mapHeight];
        }
        public void Init()
        {
            FillMap();
            for (var i = 0; i < _smoothFactor; i++)
            {
                SmoothMap();
            }
            DrawTiles();
        }
        private void FillMap()
        {
            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    if (x == 0 || x == _mapWidth - 1 || y == 0 || y == _mapHeight - 1)
                    {
                        if (_borders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        _map[x, y] = Random.Range(0, 100) < _fillPercent ? 1 : 0;
                    }
                }
            }
        }
        private void SmoothMap()
        {
            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    int neighbourCount = GetNeighbours(x, y);
                    if (neighbourCount > _sideCount)
                    {
                        _map[x, y] = 1;
                    }
                    else if (neighbourCount < _sideCount)
                    {
                        _map[x, y] = 0;
                    }
                }
            }
        }
        private int GetNeighbours(int x, int y)
        {
            int neighbourAmount = 0;

            for (var gridX = x - 1; gridX < x + 1; gridX++)
            {
                for (var gridY = y - 1; gridY < y + 1; gridY++)
                {
                    if (gridX >= 0 && gridX < _mapWidth && gridY >= 0 && gridY < _mapHeight)
                    {
                        if (gridX != x || gridY != y)
                        {
                            neighbourAmount += _map[gridX, gridY];
                        }
                    }
                    else
                    {
                        neighbourAmount++;
                    }
                }
            }
            return neighbourAmount;
        }
        private void DrawTiles()
        {
            if (_map == null) return;
            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(-_mapWidth / 2 + x, -_mapHeight / 2 + y, 0);
                    if (_map[x, y] == 1)
                    {
                        _tilemap.SetTile(tilePosition, _groundTile);
                    }
                }
            }
        }
    }
}