using GamePlay.SnakeObjects;
using Map.MapGeneration;
using UnityEngine;

namespace Managers
{
    public class CollisionManager
    {
        private VisualMap _visualMap;

        public CollisionManager(VisualMap visualMap)
        {
            _visualMap = visualMap;
        }

        public bool IsCollided(Vector2Int pos, Snake snake)
        {
            return pos.x < 0 
                   || pos.y <0 
                   || pos.x >= _visualMap.DataMap.Width 
                   || pos.y >= _visualMap.DataMap.Height
                   || !_visualMap.DataMap.GetTile(pos).CanEnter
                   || snake.CheckSelfCollision();
        } 
    }
}