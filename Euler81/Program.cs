using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using static Euler.Extension;
using static Euler.Sequence;
using System.Diagnostics.CodeAnalysis;

namespace Euler81
{
    class Program
    {
        static long[,] LoadMatrix(string matrixName, int cols, int rows) {
            var ret = new long[cols,rows];

            var assembly = typeof(Euler81.Program).Assembly;

            using(Stream resourceStream = assembly.GetManifestResourceStream(matrixName))
            using(StreamReader reader = new StreamReader(resourceStream))
            {
                for (int row = 0; row < rows; ++row)
                {
                    var lineTokens = reader.ReadLine().Split(",");
                    for (int col = 0; col < cols; ++col) 
                    {
                        ret[row,col] = Convert.ToInt64(lineTokens[col].Trim());
                    }
                }

                return ret;
            }
        }

        class SearchState : IComparable<SearchState>
        {
            public long[,] matrix;
            public (int col, int row) currentLocation;
            public HashSet<(int col, int row)> locationsInPath;
            public long pathSum;
            public long heuristicValue;

            public int CompareTo([AllowNull] SearchState other) =>
                this.heuristicValue > other.heuristicValue ? 1 : this.heuristicValue == other.heuristicValue ? 0 : -1;
        }

        static IEnumerable<SearchState> NeighborFunc(SearchState s, int numCols, int numRows, Func<SearchState, long> heuristicFunc, long[,] minPathLengths)
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
                yield return newState;
            }
        }

        static long Min(long[,] matrix) 
        {
            long min = matrix[0,0];
            foreach (var value in matrix)
            {
                min = Math.Min(min,value);
            }

            return min;
        }

        static long[] DiagonalMins(long[,] matrix, int numCols, int numRows)
        {
            var ret = new long[numCols + numRows - 1];

            for (int i = 0; i < ret.Length; ++i)
            {
                ret[i] = long.MaxValue;
                for (int row = 0; row <= numRows; ++row)
                {
                    int col = i-row;
                    if (col >= 0 && col<numCols && row<numRows)
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

            int numRows = 80;
            int numCols = 80;

            var matrix = LoadMatrix("Euler81.p081_matrix.txt", numCols, numRows);
            var minPathLengths = new long[numCols, numRows];
            
            for (int col = 0; col < numCols; ++col)
            {
                for (int row = 0; row < numRows; ++row)
                {
                    minPathLengths[col,row] = long.MaxValue;
                }
            }

            // int numRows = 5;
            // int numCols = 5;
            // var matrix = LoadMatrix("Euler81.exampleMatrix.txt", numCols, numRows);

            var diagonalMins = DiagonalMins(matrix, numCols, numRows);
            var diagonalMinSums = diagonalMins.Reverse().PartialSums().Reverse().Append(0).ToArray();

            Func<SearchState, long> heuristicFunc = s => s.pathSum + diagonalMinSums[s.currentLocation.col + s.currentLocation.row+1];
            //Func<SearchState, long> heuristicFunc = s => s.pathSum;   // assume all values are minimum value
            Func<SearchState, IEnumerable<SearchState>> neighborFunc = s => NeighborFunc(s,numCols, numRows, heuristicFunc,minPathLengths);
            Func<SearchState, bool> goalFunc = s => s.currentLocation.col == numCols - 1 && s.currentLocation.row == numRows - 1;

            Euler.MinHeap<SearchState> searchFrontier = new Euler.MinHeap<SearchState>();

            var startState = new SearchState
            {
                matrix = matrix,
                currentLocation = (col: 0, row: 0),
                locationsInPath = new HashSet<(int col, int row)>((col: 0, row: 0).Yield()),
                pathSum = matrix[0,0]
            };
            startState.heuristicValue = heuristicFunc(startState);

            searchFrontier.Add(startState);
            int numInspected = 0;

            while(searchFrontier.Count > 0)
            {
                SearchState next;
                using(profiler.Time("Pop search state"))
                {
                    next = searchFrontier.Pop();
                }
                ++numInspected;

                if (goalFunc(next))
                {
                    Console.WriteLine($"Goal reached. Path sum: {next.pathSum}");
                    break;
                }
                else
                {
                    using(profiler.Time("Get neighbors and add to heap"))
                    searchFrontier.AddRange(neighborFunc(next));
                }
            }

            profiler.Print();         
        }
    }
}
