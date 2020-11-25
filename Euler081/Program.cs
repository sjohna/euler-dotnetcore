using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using static Euler.Extension;
using static Euler.Sequence;
using System.Diagnostics.CodeAnalysis;
using Euler;

namespace Euler81
{
    public class SearchState : IComparable<SearchState>
    {
        public Matrix matrix;
        public (int col, int row) currentLocation;
        public HashSet<(int col, int row)> locationsInPath;
        public long pathSum;
        public long heuristicValue;

        public int CompareTo([AllowNull] SearchState other) =>
            this.heuristicValue > other.heuristicValue ? 1 : this.heuristicValue == other.heuristicValue ? 0 : -1;
    }

    public class Program
    {
        private static Lazy<Matrix> exampleMatrix = new Lazy<Matrix>(() => LoadMatrix("exampleMatrix", 5, 5));
        private static Lazy<Matrix> fullMatrix = new Lazy<Matrix>(() => LoadMatrix("p081_matrix", 80, 80));

        public static Matrix ExampleMatrix => exampleMatrix.Value;
        public static Matrix FullMatrix => fullMatrix.Value;

        public static Matrix LoadMatrix(string matrixName, int cols, int rows) {
            var matrixValues = new long[cols,rows];

            string resourceName = $"Euler081.Matrices.{matrixName}.txt";

            var assembly = typeof(Euler81.Program).Assembly;

            using(Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
            using(StreamReader reader = new StreamReader(resourceStream))
            {
                for (int row = 0; row < rows; ++row)
                {
                    var lineTokens = reader.ReadLine().Split(",");
                    for (int col = 0; col < cols; ++col) 
                    {
                        matrixValues[col,row] = Convert.ToInt64(lineTokens[col].Trim());
                    }
                }

                return new Matrix(matrixValues, rows, cols);
            }
        }

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
        }

        public static long Min(long[,] matrix) 
        {
            long min = matrix[0,0];
            foreach (var value in matrix)
            {
                min = Math.Min(min,value);
            }

            return min;
        }

        public static long[] DiagonalMins(Matrix matrix)
        {
            var ret = new long[matrix.NumCols + matrix.NumRows - 1];

            for (int i = 0; i < ret.Length; ++i)
            {
                ret[i] = long.MaxValue;
                for (int row = 0; row <= matrix.NumRows; ++row)
                {
                    int col = i-row;
                    if (col >= 0 && col < matrix.NumCols && row < matrix.NumRows)
                    {
                        ret[i] = Math.Min(ret[i],matrix[col,row]);
                    }
                }
            }

            return ret;
        }

        static void Main(string[] args)
        {
            var profiler = new Euler.Profiler();

            AStarWithMinPathLengthCheck(FullMatrix, profiler).ConsoleWriteLine();

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

            var diagonalMins = DiagonalMins(matrix);
            var diagonalMinSums = diagonalMins.Reverse().PartialSums().Reverse().Append(0).ToArray();

            Func<SearchState, long> heuristicFunc = s => s.pathSum + diagonalMinSums[s.currentLocation.col + s.currentLocation.row + 1];
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

                if (goalFunc(next))
                {
                    // TODO: implement some sort of progress interface for this
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
                var factory = EulerProblemInstance<long>.InstanceFactoryWithCustomParameterRepresentation<Matrix>(typeof(Euler81.Program), 81);

                yield return factory(nameof(AStarWithMinPathLengthCheck), FullMatrix, "full matrix", 427337L).Canonical();
                yield return factory(nameof(AStarWithMinPathLengthCheck), ExampleMatrix, "example matrix", 2427L).Mini();
            }
        }
    }
}
