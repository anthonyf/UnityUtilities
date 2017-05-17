using AF.UnityUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace AF.UnityUtilities
{
    public static class UnityExtensions
    {
        public static Vector3 Round(this Vector3 vector, float nearest)
        {
            var factor = 1f / nearest;
            vector *= factor;
            vector = new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
            vector /= factor;
            return vector;
        }

        private const float _fastCalculatePathGridSize = .5f;
        private static Dictionary<Tuple3<Vector3, Vector3, int>, float> _fastCalculatePathCache = new Dictionary<Tuple3<Vector3, Vector3, int>, float>();
        public static float CalculatePathCost(Vector3 sourcePosition, Vector3 targetPosition, int areaMask)
        {
            sourcePosition = sourcePosition.Round(_fastCalculatePathGridSize);
            targetPosition = targetPosition.Round(_fastCalculatePathGridSize);
            var key = new Tuple3<Vector3, Vector3, int>(sourcePosition, targetPosition, areaMask);
            if (_fastCalculatePathCache.ContainsKey(key))
            {
                return _fastCalculatePathCache[key];
            }

            NavMeshPath path = new NavMeshPath();
            float cost;
            if (NavMesh.CalculatePath(sourcePosition, targetPosition, areaMask, path))
            {
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    cost = path.Cost();
                }
                else
                {
                    cost = float.MaxValue;
                }
            }
            else
            {
                cost = float.MaxValue;
            }

            _fastCalculatePathCache[key] = cost;
            return cost;

        }

        public static float Cost(this NavMeshPath path)
        {
            if (path.corners.Length < 2) return 0;

            float cost = 0;
            NavMeshHit hit;
            NavMesh.SamplePosition(path.corners[0], out hit, 0.1f, NavMesh.AllAreas);
            Vector3 rayStart = path.corners[0];
            int mask = hit.mask;
            int index = IndexFromMask(mask);

            for (int i = 1; i < path.corners.Length; ++i)
            {

                while (true)
                {
                    NavMesh.Raycast(rayStart, path.corners[i], out hit, mask);

                    cost += NavMesh.GetAreaCost(index) * hit.distance;

                    if (hit.mask != 0) mask = hit.mask;

                    index = IndexFromMask(mask);
                    rayStart = hit.position;

                    if (hit.mask == 0)
                    { //hit boundary; move startPoint of ray a bit closer to endpoint
                        rayStart += (path.corners[i] - rayStart).normalized * 0.01f;
                    }

                    if (!hit.hit) break;
                }
            }

            return cost;
        }

        private static int IndexFromMask(int mask)
        {
            for (int i = 0; i < 32; ++i)
            {
                if ((1 << i & mask) != 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void InvokeRepeating(this MonoBehaviour behaviour, Action method, float time, float repeatRate)
        {
            behaviour.InvokeRepeating(method.Method.Name, time, repeatRate);
        }

        public static void AllPairs<T>(this List<T> list, Action<T, T> callback)
        {
            for (var i = 0; i < list.Count - 1; i++)
            {
                var a = list[i];
                for (var j = i + 1; j < list.Count; j++)
                {
                    var b = list[j];
                    callback(a, b);
                }
            }
        }

        public static Color MakeHeatColor(float min, float max, float value, bool hotToCold = false)
        {
            var ratio = 2 * (value - min) / (max - min);
            var b = Mathf.Max(0, 1 - ratio);
            var r = Mathf.Max(0, ratio - 1);
            var g = 1 - b - r;
            return hotToCold ? new Color(b, g, r) : new Color(r, g, b);
        }

        public static float DistanceSquared(float x1, float y1, float x2, float y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }

        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return Mathf.Sqrt(DistanceSquared(x1, y1, x2, y2));
        }

        public static float ManhattanDistance(float x1, float y1, float x2, float y2)
        {
            return Mathf.Abs(x2 - x1) + Mathf.Abs(y2 - y1);
        }

        public static void MapNeighborIndexes(int x, int y, int w, int h, Action<int, int> fun)
        {
            for (int ox = -1; ox <= 1; ox++)
            {
                for (int oy = -1; oy <= 1; oy++)
                {
                    if (ox == 0 && oy == 0) continue;
                    int nx = x + ox;
                    int ny = y + oy;
                    if (nx >= 0 && nx < w && ny >= 0 && ny < h)
                    {
                        fun(nx, ny);
                    }
                }
            }
        }
        public static IEnumerable<TResult> XWise<TSource, TResult>(this IEnumerable<TSource> source, int x, Func<TSource[], TResult> resultSelector)
        {

            using (var it = source.GetEnumerator())
            {
                var items = new TSource[x];
                var isDone = false;
                while (!isDone)
                {
                    var itemCount = 0;
                    for (var i = 0; i < x && !isDone; i++)
                    {
                        if(it.MoveNext())
                        {
                            itemCount++;
                            items[i] = it.Current;
                        } else
                        {
                            isDone = true;
                        }
                    }
                    if (itemCount > 0)
                    {
                        yield return resultSelector(items);
                    }
                }
            }
        }

        public static void Pairwise<TSource>(this IEnumerable<TSource> source, Action<TSource, TSource> resultSelector)
        {
            var pairs = source.Pairwise((a, b) =>
            {
                resultSelector(a, b);
                return 0;
            });
            foreach(var p in pairs)
            {
                // force evaluation
            }
        }

        public static IEnumerable<TResult> Pairwise<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> resultSelector)
        {
            return source.XWise(2, items =>
            {
                return resultSelector(items[0], items[1]);
            });
        }

        public static void Tripletwise<TSource>(this IEnumerable<TSource> source, Action<TSource, TSource, TSource> resultSelector)
        {
            var triples = source.Tripletwise((a, b, c) =>
            {
                resultSelector(a, b, c);
                return 0;
            });
            foreach(var t in triples)
            {
                // force evaluation
            }
        }

        public static IEnumerable<TResult> Tripletwise<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource, TResult> resultSelector)
        {
            return source.XWise(3, items =>
            {
                return resultSelector(items[0], items[1], items[2]);
            });
        }
    }
}