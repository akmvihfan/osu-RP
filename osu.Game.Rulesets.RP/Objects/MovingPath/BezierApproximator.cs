﻿//Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
//Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using OpenTK;

namespace osu.Game.Rulesets.RP.Objects.MovingPath
{
    /// <summary>
    /// </summary>
    public class BezierApproximator
    {
        private const float TOLERANCE = 0.25f;
        private const float TOLERANCE_SQ = TOLERANCE * TOLERANCE;
        private readonly int count;
        private readonly List<Vector2> controlPoints;
        private readonly Vector2[] subdivisionBuffer1;
        private readonly Vector2[] subdivisionBuffer2;

        public BezierApproximator(List<Vector2> controlPoints)
        {
            this.controlPoints = controlPoints;
            count = controlPoints.Count;

            subdivisionBuffer1 = new Vector2[count];
            subdivisionBuffer2 = new Vector2[count * 2 - 1];
        }

        /// <summary>
        ///     Creates a piecewise-linear approximation of a bezier curve, by adaptively repeatedly subdividing
        ///     the control points until their approximation error vanishes below a given threshold.
        /// </summary>
        /// <returns>A list of vectors representing the piecewise-linear approximation.</returns>
        public List<Vector2> CreateBezier()
        {
            var output = new List<Vector2>();

            if (count == 0)
                return output;

            var toFlatten = new Stack<Vector2[]>();
            var freeBuffers = new Stack<Vector2[]>();

            // "toFlatten" contains all the curves which are not yet approximated well enough.
            // We use a stack to emulate recursion without the risk of running into a stack overflow.
            // (More specifically, we iteratively and adaptively refine our curve with a 
            // <a href="https://en.wikipedia.org/wiki/Depth-first_search">Depth-first search</a>
            // over the tree resulting from the subdivisions we make.)
            toFlatten.Push(controlPoints.ToArray());

            var leftChild = subdivisionBuffer2;

            while (toFlatten.Count > 0)
            {
                var parent = toFlatten.Pop();
                if (IsFlatEnough(parent))
                {
                    // If the control points we currently operate on are sufficiently "flat", we use
                    // an extension to De Casteljau's algorithm to obtain a piecewise-linear approximation
                    // of the bezier curve represented by our control points, consisting of the same amount
                    // of points as there are control points.
                    Approximate(parent, output);
                    freeBuffers.Push(parent);
                    continue;
                }

                // If we do not yet have a sufficiently "flat" (in other words, detailed) approximation we keep
                // subdividing the curve we are currently operating on.
                var rightChild = freeBuffers.Count > 0 ? freeBuffers.Pop() : new Vector2[count];
                Subdivide(parent, leftChild, rightChild);

                // We re-use the buffer of the parent for one of the children, so that we save one allocation per iteration.
                for (var i = 0; i < count; ++i)
                    parent[i] = leftChild[i];

                toFlatten.Push(rightChild);
                toFlatten.Push(parent);
            }

            output.Add(controlPoints[count - 1]);
            return output;
        }

        /// <summary>
        ///     Make sure the 2nd order derivative (approximated using finite elements) is within tolerable bounds.
        ///     NOTE: The 2nd order derivative of a 2d curve represents its curvature, so intuitively this function
        ///     checks (as the name suggests) whether our approximation is _locally_ "flat". More curvy parts
        ///     need to have a denser approximation to be more "flat".
        /// </summary>
        /// <param name="controlPoints">The control points to check for flatness.</param>
        /// <returns>Whether the control points are flat enough.</returns>
        private static bool IsFlatEnough(Vector2[] controlPoints)
        {
            for (var i = 1; i < controlPoints.Length - 1; i++)
                if ((controlPoints[i - 1] - 2 * controlPoints[i] + controlPoints[i + 1]).LengthSquared > TOLERANCE_SQ * 4)
                    return false;

            return true;
        }

        /// <summary>
        ///     Subdivides n control points representing a bezier curve into 2 sets of n control points, each
        ///     describing a bezier curve equivalent to a half of the original curve. Effectively this splits
        ///     the original curve into 2 curves which result in the original curve when pieced back together.
        /// </summary>
        /// <param name="controlPoints">The control points to split.</param>
        /// <param name="l">Output: The control points corresponding to the left half of the curve.</param>
        /// <param name="r">Output: The control points corresponding to the right half of the curve.</param>
        private void Subdivide(Vector2[] controlPoints, Vector2[] l, Vector2[] r)
        {
            var midpoints = subdivisionBuffer1;

            for (var i = 0; i < count; ++i)
                midpoints[i] = controlPoints[i];

            for (var i = 0; i < count; i++)
            {
                l[i] = midpoints[0];
                r[count - i - 1] = midpoints[count - i - 1];

                for (var j = 0; j < count - i - 1; j++)
                    midpoints[j] = (midpoints[j] + midpoints[j + 1]) / 2;
            }
        }

        /// <summary>
        ///     This uses <a href="https://en.wikipedia.org/wiki/De_Casteljau%27s_algorithm">De Casteljau's algorithm</a> to obtain
        ///     an optimal
        ///     piecewise-linear approximation of the bezier curve with the same amount of points as there are control points.
        /// </summary>
        /// <param name="controlPoints">The control points describing the bezier curve to be approximated.</param>
        /// <param name="output">The points representing the resulting piecewise-linear approximation.</param>
        private void Approximate(Vector2[] controlPoints, List<Vector2> output)
        {
            var l = subdivisionBuffer2;
            var r = subdivisionBuffer1;

            Subdivide(controlPoints, l, r);

            for (var i = 0; i < count - 1; ++i)
                l[count + i] = r[i + 1];

            output.Add(controlPoints[0]);
            for (var i = 1; i < count - 1; ++i)
            {
                var index = 2 * i;
                var p = 0.25f * (l[index - 1] + 2 * l[index] + l[index + 1]);
                output.Add(p);
            }
        }
    }
}