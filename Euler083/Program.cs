using System;
using System.Linq;
using System.Collections.Generic;

using Euler81;

using static Euler.Extension;
using static Euler.Sequence;
using Euler;

namespace Euler83
{
    public class Program
    {
        public static IEnumerable<SearchState> NeighborFunc(SearchState s, int numCols, int numRows, Func<SearchState, long> heuristicFunc, long[,] minPathLengths)
        {
            if(s.currentLocation.col < numCols - 1) {
                var newLocation = (col: s.currentLocation.col+1, row: s.currentLocation.row);
                var newState = new SearchState
                {
                    matrix = s.matrix,
                    currentLocation = newLocation,
                    locationsInPath = new HashSet<(int col, int row)>(s.locationsInPath.Append(newLocation)),
                    pathSum = s.pathSum + s.matrix[newLocation.col, newLocation.row]
                };
                newState.heuristicValue = heuristicFunc(newState);

                if (newState.pathSum < minPathLengths[newState.currentLocation.col, newState.currentLocation.row])
                {
                    minPathLengths[newState.currentLocation.col, newState.currentLocation.row] = newState.pathSum;
                    yield return newState;
                }
            }

            if(s.currentLocation.row < numRows - 1) {
                var newLocation = (col: s.currentLocation.col, row: s.currentLocation.row + 1);
                var newState = new SearchState
                {
                    matrix = s.matrix,
                    currentLocation = newLocation,
                    locationsInPath = new HashSet<(int col, int row)>(s.locationsInPath.Append(newLocation)),
                    pathSum = s.pathSum + s.matrix[newLocation.col, newLocation.row]
                };
                newState.heuristicValue = heuristicFunc(newState);

                if (newState.pathSum < minPathLengths[newState.currentLocation.col, newState.currentLocation.row])
                {
                    minPathLengths[newState.currentLocation.col, newState.currentLocation.row] = newState.pathSum;
                    yield return newState;
                }
            }

            if(s.currentLocation.row > 0) {
                var newLocation = (col: s.currentLocation.col, row: s.currentLocation.row - 1);
                var newState = new SearchState
                {
                    matrix = s.matrix,
                    currentLocation = newLocation,
                    locationsInPath = new HashSet<(int col, int row)>(s.locationsInPath.Append(newLocation)),
                    pathSum = s.pathSum + s.matrix[newLocation.col, newLocation.row]
                };
                newState.heuristicValue = heuristicFunc(newState);

                if (newState.pathSum < minPathLengths[newState.currentLocation.col, newState.currentLocation.row])
                {
                    minPathLengths[newState.currentLocation.col, newState.currentLocation.row] = newState.pathSum;
                    yield return newState;
                }
            }

            if(s.currentLocation.col > 0) {
                var newLocation = (col: s.currentLocation.col - 1, row: s.currentLocation.row);
                var newState = new SearchState
                {
                    matrix = s.matrix,
                    currentLocation = newLocation,
                    locationsInPath = new HashSet<(int col, int row)>(s.locationsInPath.Append(newLocation)),
                    pathSum = s.pathSum + s.matrix[newLocation.col, newLocation.row]
                };
                newState.heuristicValue = heuristicFunc(newState);

                if (newState.pathSum < minPathLengths[newState.currentLocation.col, newState.currentLocation.row])
                {
                    minPathLengths[newState.currentLocation.col, newState.currentLocation.row] = newState.pathSum;
                    yield return newState;
                }
            }
        }

        static void Main(string[] args)
        {
            var profiler = new Euler.Profiler();

            AStarWithMinPathLengthCheck(Euler81.Program.FullMatrix, profiler).ConsoleWriteLine();

            profiler.Print();
        }

        public static long AStarWithMinPathLengthCheck(Matrix matrix)
        {
            return AStarWithMinPathLengthCheck(matrix, IProfiler.Default);
        }

        public static long AStarWithMinPathLengthCheck(Matrix matrix, IProfiler profiler = null)
        {
            if (profiler == null) profiler = IProfiler.Default;

            var minPathLengths = new long[matrix.NumCols, matrix.NumRows];

            for (int col = 0; col < matrix.NumCols; ++col)
            {
                for (int row = 0; row < matrix.NumRows; ++row)
                {
                    minPathLengths[col, row] = long.MaxValue;
                }
            }

            var diagonalMins = Euler81.Program.DiagonalMins(matrix);
            var diagonalMinSums = diagonalMins.Reverse().PartialSums().Reverse().Append(0).ToArray();

            Func<SearchState, long> heuristicFunc = s => s.pathSum + diagonalMinSums[s.currentLocation.col + s.currentLocation.row + 1];
            //Func<SearchState, long> heuristicFunc = s => s.pathSum;   // assume all values are minimum value
            Func<SearchState, IEnumerable<SearchState>> neighborFunc = s => NeighborFunc(s, matrix.NumCols, matrix.NumRows, heuristicFunc, minPathLengths);
            Func<SearchState, bool> goalFunc = s => s.currentLocation.col == matrix.NumCols - 1 && s.currentLocation.row == matrix.NumRows - 1;

            Euler.MinHeap<SearchState> searchFrontier = new Euler.MinHeap<SearchState>();

            var startState = new SearchState
            {
                matrix = matrix,
                currentLocation = (col: 0, row: 0),
                locationsInPath = new HashSet<(int col, int row)>((col: 0, row: 0).Yield()),
                pathSum = matrix[0, 0]
            };
            startState.heuristicValue = heuristicFunc(startState);

            searchFrontier.Add(startState);
            int numInspected = 0;

            while (searchFrontier.Count > 0)
            {
                SearchState next;
                using (profiler.Time("Pop search state"))
                {
                    next = searchFrontier.Pop();
                }
                ++numInspected;

                if (numInspected % 1 == 0)
                {
                    // TODO: progress
                    //Console.WriteLine($"{numInspected}/{numInspected+searchFrontier.Count}: {next.currentLocation}, {next.locationsInPath.Count}, {next.pathSum}, {next.heuristicValue}");
                }

                if (goalFunc(next))
                {
                    // TODO: progress
                    //Console.WriteLine($"Goal reached. Path sum: {next.pathSum}");
                    return next.pathSum;
                }
                else
                {
                    using (profiler.Time("Get neighbors and add to heap"))
                        searchFrontier.AddRange(neighborFunc(next));
                }
            }

            throw new Exception("Goal state not reached!");
        }

        public static IEnumerable<EulerProblemInstance<long>> ProblemInstances
        {
            get
            {
                var factory = EulerProblemInstance<long>.InstanceFactoryWithCustomParameterRepresentation<Matrix>(typeof(Euler83.Program), 83);

                yield return factory(nameof(AStarWithMinPathLengthCheck), Euler81.Program.FullMatrix, "full matrix", 425185L).Canonical();
                yield return factory(nameof(AStarWithMinPathLengthCheck), Euler81.Program.ExampleMatrix, "example matrix", 2297L).Mini();
            }
        }
    }
}
