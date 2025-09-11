namespace Maths_Matrices.Tests
{
    public class MatrixElementaryOperations
    {
        // --- Version MatrixInt ---
        public static void SwapLines(MatrixInt matrix, int indexLine1, int indexLine2)
        {
            int[] Line1 = matrix.GetLine(indexLine1);
            int[] Line2 = matrix.GetLine(indexLine2);

            matrix.SetLine(indexLine1, Line2);
            matrix.SetLine(indexLine2, Line1);
        }

        public static void SwapColumns(MatrixInt matrix, int indexColumn1, int indexColumn2)
        {
            int[] Column1 = matrix.GetColumn(indexColumn1);
            int[] Column2 = matrix.GetColumn(indexColumn2);

            matrix.SetColumn(indexColumn1, Column2);
            matrix.SetColumn(indexColumn2, Column1);
        }

        public static void MultiplyLine(MatrixInt matrix, int lineIndex, int scalar)
        {
            if (scalar == 0) throw new MatrixScalarZeroException();
            int[] line1 = matrix.GetLine(lineIndex);
            for (int i = 0; i < line1.Length; i++)
            {
                line1[i] *= scalar;
            }
            matrix.SetLine(lineIndex, line1);
        }

        public static void MultiplyColumn(MatrixInt matrix, int columnIndex, int scalar)
        {
            if (scalar == 0) throw new MatrixScalarZeroException();
            int[] column1 = matrix.GetColumn(columnIndex);
            for (int i = 0; i < column1.Length; i++)
            {
                column1[i] *= scalar;
            }
            matrix.SetColumn(columnIndex, column1);
        }

        public static void AddLineToAnother(MatrixInt matrix, int lineIndexToAdd, int lineIndexToAddTo, int scalar)
        {
            if (scalar == 0) throw new MatrixScalarZeroException();
            int[] line1 = matrix.GetLine(lineIndexToAdd);
            int[] line2 = matrix.GetLine(lineIndexToAddTo);
            for (int i = 0; i < line2.Length; i++)
            {
                line2[i] += line1[i] * scalar;
            }
            matrix.SetLine(lineIndexToAddTo, line2);
        }

        public static void AddColumnToAnother(MatrixInt matrix, int columnIndexToAdd, int columnIndexToAddTo, int scalar)
        {
            if (scalar == 0) throw new MatrixScalarZeroException();
            int[] column1 = matrix.GetColumn(columnIndexToAdd);
            int[] column2 = matrix.GetColumn(columnIndexToAddTo);
            for (int i = 0; i < column2.Length; i++)
            {
                column2[i] += column1[i] * scalar;
            }
            matrix.SetColumn(columnIndexToAddTo, column2);
        }

        // --- Version MatrixFloat ---
        public static void SwapLines(MatrixFloat matrix, int indexLine1, int indexLine2)
        {
            float[] Line1 = matrix.GetLine(indexLine1);
            float[] Line2 = matrix.GetLine(indexLine2);

            matrix.SetLine(indexLine1, Line2);
            matrix.SetLine(indexLine2, Line1);
        }

        public static void SwapColumns(MatrixFloat matrix, int indexColumn1, int indexColumn2)
        {
            float[] Column1 = matrix.GetColumn(indexColumn1);
            float[] Column2 = matrix.GetColumn(indexColumn2);

            matrix.SetColumn(indexColumn1, Column2);
            matrix.SetColumn(indexColumn2, Column1);
        }

        public static void MultiplyLine(MatrixFloat matrix, int lineIndex, float scalar)
        {
            if (scalar == 0f) throw new MatrixScalarZeroException();
            float[] line1 = matrix.GetLine(lineIndex);
            for (int i = 0; i < line1.Length; i++)
            {
                line1[i] *= scalar;
            }
            matrix.SetLine(lineIndex, line1);
        }

        public static void MultiplyColumn(MatrixFloat matrix, int columnIndex, float scalar)
        {
            if (scalar == 0f) throw new MatrixScalarZeroException();
            float[] column1 = matrix.GetColumn(columnIndex);
            for (int i = 0; i < column1.Length; i++)
            {
                column1[i] *= scalar;
            }
            matrix.SetColumn(columnIndex, column1);
        }

        public static void AddLineToAnother(MatrixFloat matrix, int lineIndexToAdd, int lineIndexToAddTo, float scalar)
        {
            if (scalar == 0f) throw new MatrixScalarZeroException();
            float[] line1 = matrix.GetLine(lineIndexToAdd);
            float[] line2 = matrix.GetLine(lineIndexToAddTo);
            for (int i = 0; i < line2.Length; i++)
            {
                line2[i] += line1[i] * scalar;
            }
            matrix.SetLine(lineIndexToAddTo, line2);
        }

        public static void AddColumnToAnother(MatrixFloat matrix, int columnIndexToAdd, int columnIndexToAddTo, float scalar)
        {
            if (scalar == 0f) throw new MatrixScalarZeroException();
            float[] column1 = matrix.GetColumn(columnIndexToAdd);
            float[] column2 = matrix.GetColumn(columnIndexToAddTo);
            for (int i = 0; i < column2.Length; i++)
            {
                column2[i] += column1[i] * scalar;
            }
            matrix.SetColumn(columnIndexToAddTo, column2);
        }
    }
}
