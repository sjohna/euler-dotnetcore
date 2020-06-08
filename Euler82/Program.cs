using System;
using System.Linq;
using System.Collections.Generic;

using static Euler.Extension;
using static Euler.Sequence;

using Euler81;


namespace Euler82
{
    class Program
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
        }

        static void Main(string[] args)
        {
            var profiler = new Euler.Profiler();

            int numRows = 80;
            int numCols = 80;
            var matrix = Euler81.Program.LoadMatrix("Euler81.p081_matrix.txt", numCols, numRows);

            // int numRows = 5;
            // int numCols = 5;
            // var matrix = Euler81.Program.LoadMatrix("Euler81.exampleMatrix.txt", numCols, numRows);

            var minPathLengths = new long[numCols, numRows];
            
            for (int col = 0; col < numCols; ++col)
            {
                for (int row = 0; row < numRows; ++row)
                {
                    minPathLengths[col,row] = long.MaxValue;
                }
            }

            // var diagonalMins = Euler81.Program.DiagonalMins(matrix, numCols, numRows);
            // var diagonalMinSums = diagonalMins.Reverse().PartialSums().Reverse().Append(0).ToArray();

            //Func<SearchState, long> heuristicFunc = s => s.pathSum + diagonalMinSums[s.currentLocation.col + s.currentLocation.row+1];
            Func<SearchState, long> heuristicFunc = s => s.pathSum;   // assume all values are minimum value
            Func<SearchState, IEnumerable<SearchState>> neighborFunc = s => NeighborFunc(s,numCols, numRows, heuristicFunc, minPathLengths);
            Func<SearchState, bool> goalFunc = s => s.currentLocation.col == numCols - 1;

            Euler.MinHeap<SearchState> searchFrontier = new Euler.MinHeap<SearchState>();

            for(int i = 0; i < numRows; ++i)
            {
                var startState = new SearchState
                {
                    matrix = matrix,
                    currentLocation = (col: 0, row: i),
                    locationsInPath = new HashSet<(int col, int row)>((col: 0, row: i).Yield()),
                    pathSum = matrix[0,i]
                };
                startState.heuristicValue = heuristicFunc(startState);
                
                searchFrontier.Add(startState);
            }
            int numInspected = 0;

            while(searchFrontier.Count > 0)
            {
                SearchState next;
                using(profiler.Time("Pop search state"))
                {
                    next = searchFrontier.Pop();
                }
                ++numInspected;

                if(numInspected % 1 == 0)
                {
                    Console.WriteLine($"{numInspected}/{numInspected+searchFrontier.Count}: {next.currentLocation}, {next.locationsInPath.Count}, {next.pathSum}, {next.heuristicValue}");
                }

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
