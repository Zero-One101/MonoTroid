using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoTroid
{
    public class Polygon
    {
        private List<Vector2> points = new List<Vector2>();
        private List<Vector2> edges = new List<Vector2>();

        public List<Vector2> Points => points;

        public List<Vector2> Edges => edges;

        public void BuildEdges()
        {
            Vector2 p1;
            Vector2 p2;
            edges.Clear();

            for (var i = 0; i < points.Count; i++)
            {
                p1 = points[i];

                if (i + 1 >= points.Count)
                {
                    p2 = points[0];
                }
                else
                {
                    p2 = points[i + 1];
                }

                edges.Add(p2 - p1);
            }
        }

        public Vector2 Centre
        {
            get { return points.Aggregate((p1, p2) => p1 + p2) / points.Count; }
        }

        public Polygon(List<Vector2> points)
        {
            this.points = points;
            BuildEdges();
        }

        public void Offset(Vector2 v)
        {
            Offset(v.X, v.Y);
        }

        public void Offset(float x, float y)
        {
            for (var i = 0; i < points.Count; i++)
            {
                var p = points[i];
                points[i] = new Vector2(p.X + x, p.Y + y);
            }
        }

        public PolyCollisionResult CheckCollision(Polygon otherPoly, Vector2 velocity)
        {
            var result = new PolyCollisionResult
            {
                Intersecting = true,
                WillIntersect = true
            };

            var edgeCountA = edges.Count;
            var edgeCountB = otherPoly.Edges.Count;
            var mtv = float.PositiveInfinity;
            var translationAxis = new Vector2();
            var edge = new Vector2();

            // Loop through all the edges of both polys
            for (var i = 0; i < edgeCountA + edgeCountB; i++)
            {
                edge = i < edgeCountA ? edges[i] : otherPoly.Edges[i - edgeCountA];

                // Find the edge normal to use as an axis
                var axis = new Vector2(-edge.Y, edge.X);
                axis.Normalize();

                // Find the projection of the polygon on the current axis
                var minA = 0f;
                var maxA = 0f;
                var minB = 0f;
                var maxB = 0f;
                ProjectPolygon(axis, this, ref minA, ref maxA);
                ProjectPolygon(axis, otherPoly, ref minB, ref maxB);

                // Check if the poly projections are currently intersecting
                if (IntervalDistance(minA, maxA, minB, maxB) > 0)
                {
                    result.Intersecting = false;
                }

                // Find if the polygons WILL intersect
                // Project the velocity on the current axis
                var velProjection = Vector2.Dot(axis, velocity);

                // Get the projection of this during the movement
                if (velProjection < 0)
                {
                    minA += velProjection;
                }
                else
                {
                    maxA += velProjection;
                }

                var intervalDistance = IntervalDistance(minA, maxA, minB, maxB);
                if (intervalDistance > 0)
                {
                    result.WillIntersect = false;
                }

                // If the polygons are not intersecting and won't intersect, exit the loop
                if (!result.Intersecting && !result.WillIntersect) break;

                // Check if the current interval distance is the minimum. If so, store the interval
                // and the current distance. This will be used to calculate the MTV
                intervalDistance = Math.Abs(intervalDistance);
                if (intervalDistance < mtv)
                {
                    mtv = intervalDistance;
                    translationAxis = axis;

                    var d = Centre - otherPoly.Centre;

                    if (Vector2.Dot(d, translationAxis) < 0)
                    {
                        translationAxis *= -1;
                    }
                }
            }

            // The MTV can be used to push the polys apart
            if (result.WillIntersect)
            {
                result.MinimumTranslationVector = translationAxis * mtv;
            }

            return result;
        }

        // Calculates the projection of a polygon on an axis and returns it as a [min, max] interval
        private void ProjectPolygon(Vector2 axis, Polygon polygon, ref float min, ref float max)
        {
            // To project a point on an axis, use the dot product
            var d = Vector2.Dot(axis, polygon.Points[0]);
            min = max = d;

            foreach (var p in polygon.Points)
            {
                d = Vector2.Dot(p, axis);
                if (d < min)
                {
                    min = d;
                }
                else
                {
                    if (d > max)
                    {
                        max = d;
                    }
                }
            }
        }

        // Calculate the distance between [minA, maxA] and [minB, maxB]
        // The distance will be negative if the intervals overlap
        private float IntervalDistance(float minA, float maxA, float minB, float maxB)
        {
            if (minA < minB)
            {
                return minB - maxA;
            }

            return minA - maxB;
        }
    }

    public struct PolyCollisionResult
    {
        public bool WillIntersect;
        public bool Intersecting;

        /// <summary>
        /// The transistion to apply to a polygon to push the polygons apart
        /// </summary>
        public Vector2 MinimumTranslationVector;
    }
}
