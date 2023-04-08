using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    const int step = 2;

    struct Point
    {
        public int Cost;
        public Vector2 Position;
    }

    Queue<Vector3> ListToQueue(List<Vector2> path)
    {
        Queue<Vector3> newPath = new Queue<Vector3>();
        foreach (Vector2 point in path)
        {
            newPath.Enqueue(new Vector3(point.x, 1f, point.y));
        }
        return newPath;
    }

    Vector2 RoundCell(Vector3 point)
    {
        int nearestX = Mathf.RoundToInt(point.x * 0.5f) * step;
        int nearestY = Mathf.RoundToInt(point.z * 0.5f) * step;
        return new Vector2(nearestX, nearestY);
    }

    //Cost calculation method;
    void CalculateCost(Vector2 current, Vector2 final, out int cost)
    {
        cost = Mathf.RoundToInt(Vector2.Distance(current, final));
    }

    public IEnumerator Search(Vector3 start, Vector3 end, List<Vector2> map, Action<Queue<Vector3>> onPathDone)
    {

        Vector2 roundStart = RoundCell(start);
        Vector2 roundEnd = RoundCell(end);
        Vector2 startPathPosition = roundStart;
        Point currentPoint;
        int maximumCost = 6000;

        List<Vector2> path = new List<Vector2>();
        List<Point> visited = new List<Point>();

        void Visit(Vector2 current)
        {
            if (map.Contains(current)|| path.Contains(current))
            {
                visited.Add(new Point() { Cost = maximumCost, Position = current });
            }
            else
            {
                CalculateCost(current, roundEnd, out int cost);
                //Debug.Log($"Added Cost {cost}; Position {current}");
                visited.Add(new Point() { Cost = cost, Position = current });
            }
        }

        path.Add(startPathPosition);

        while (startPathPosition != roundEnd)
        {
            Vector2 up = new Vector2(startPathPosition.x, startPathPosition.y - step);
            Vector2 down = new Vector2(startPathPosition.x, startPathPosition.y + step);
            Vector2 left = new Vector2(startPathPosition.x + step, startPathPosition.y);
            Vector2 right = new Vector2(startPathPosition.x - step, startPathPosition.y);

            Visit(up);
            Visit(down);
            Visit(left);
            Visit(right);

            visited.Sort((p1, p2) => p1.Cost.CompareTo(p2.Cost));

            currentPoint = visited[0];
            path.Add(currentPoint.Position);
            startPathPosition = currentPoint.Position;
            Debug.Log(startPathPosition);
            visited.Remove(currentPoint);

            if (startPathPosition == roundEnd || currentPoint.Cost == 1)
            {
                // Path optimization
                for (int i = 0; i < path.Count - 2; i++)
                {
                    Vector2 direction = (path[i + 2] - path[i]).normalized;
                    float angle = Vector2.Angle(path[i + 1] - path[i], direction);
                    if (angle <= 5f)
                    {
                        path.RemoveAt(i + 1);
                        i--;
                    }
                }

                path.Remove(roundStart);
                //path.Reverse();
                Debug.Log("path end");
                onPathDone.Invoke(ListToQueue(path));
                yield break;
            }

            yield return null;
        }
    }
}
