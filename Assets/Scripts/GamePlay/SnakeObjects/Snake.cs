using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.SnakeObjects
{
    public class Snake
    {
        public Direction CurrentDirection { get; set; }
        public LinkedList<Vector2Int> Body { get; } = new();
        public Vector2Int HeadPosition => Body.First.Value;
        public Vector2Int TailPosition => Body.Last.Value;
    
        public Snake(int initialLength, Vector2Int startingPos, Direction initialDirection)
        {
            CurrentDirection = initialDirection;

            Body.AddFirst(startingPos);
        
            switch (CurrentDirection)
            {
                case Direction.Up:
                    AddSegment(startingPos, initialLength-1, 0, 1);
                    break;
                case Direction.Right:
                    AddSegment(startingPos, initialLength-1, -1, 0);
                    break;
                case Direction.Down:
                    AddSegment(startingPos, initialLength-1, 0, -1);
                    break;
                case Direction.Left:
                    AddSegment(startingPos, initialLength-1, 1, 0);
                    break;
            }
        }

        private void AddSegment(Vector2Int position, int numberToAdd, int xInc, int yInc)
        {
            for (int i = 0; i < numberToAdd; i++)
            {
                position.x += xInc;
                position.y += yInc;
            
                LinkedListNode<Vector2Int> newNode = new LinkedListNode<Vector2Int>(position);
                Body.AddLast(newNode);
            }
        }

        public Vector2Int GetNextMovePosition() => GetNextMovePosition(CurrentDirection);

        private Vector2Int GetNextMovePosition(Direction direction)
        {
            int xOffset = 0;
            int yOffset = 0;
            
            switch (direction)
            {
                case Direction.Up:
                    yOffset = -1;
                    break;
                case Direction.Right:
                    xOffset = 1;
                    break;
                case Direction.Down:
                    yOffset = 1;
                    break;
                case Direction.Left:
                    xOffset = -1;
                    break;
            }

            return new Vector2Int(Body.First.Value.x + xOffset, Body.First.Value.y + yOffset);
        }
        public void Move()
        {
            Body.RemoveLast();
            Body.AddFirst(GetNextMovePosition());
        }

        public void MoveAndGrow()
        {
            Body.AddFirst(GetNextMovePosition());
        }

        public bool CheckSelfCollision()
        {
            Vector2Int nextMovePosition = GetNextMovePosition();
            var currentNode = Body.First;
            
            while (currentNode != null)
            {
                // Check for collision
                if (currentNode.Value == nextMovePosition)
                    return true;

                //break out before last segment
                if (currentNode.Next == null || currentNode.Next.Next == null)
                    break;

                currentNode = currentNode.Next;
            }

            return false;
        }

        public bool CanTurn(Direction newDirection)
        {
            return Body.First == null 
                   || Body.First.Next == null
                   || Body.First.Next.Value != GetNextMovePosition(newDirection);
        }
    }
}
