using System;
using System.Collections.Generic;
using System.Text;

namespace Euler81
{
    public class Matrix
    {
        public long[,] Values { get; private set; }

        public int NumRows { get; private set; }

        public int NumCols { get; private set; }

        public Matrix(long[,] Values, int NumCols, int NumRows)
        {
            this.Values = Values;
            this.NumCols = NumCols;
            this.NumRows = NumRows;
        }

        public long this[int col, int row]
        {
            get
            {
                return Values[col, row];
            }
        }
    }
}
