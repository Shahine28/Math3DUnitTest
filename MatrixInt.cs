namespace Maths_Matrices.Tests
{
    public class MatrixInt
    {
        private int[,] _data;
        public int NbLines { get; }
        public int NbColumns { get; }
        
        public MatrixInt(int n, int m)
        {
            NbLines = n;
            NbColumns = m;
            _data = new int[n, m]; // auto-rempli de 0
        }
        
        public MatrixInt(int[,] values)
        {
            NbLines = values.GetLength(0);
            NbColumns = values.GetLength(1);
            _data = new int[NbLines, NbColumns];
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    _data[i, j] = values[i, j];
                }
            }
        }
        
        public MatrixInt(MatrixInt other)
        {
            NbLines = other.NbLines;
            NbColumns = other.NbColumns;
            _data = new int[NbLines, NbColumns];
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    _data[i, j] = other[i, j];
                }
            }
        }
        
        public int this[int i, int j]
        {
            get => _data[i, j];
            set => _data[i, j] = value;
        }

        public int[] this[int i]
        {
            get => _data[i, 0];
            set => _data[i] = value;
        }

        public static MatrixInt Identity(int size)
        {
            int[,] data = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    data[i, j] = (i == j) ? 1 : 0;
                }
            }
            return new MatrixInt(data);
        }

        public bool IsIdentity()
        {
            if (NbLines != NbColumns) return false;
            for(int i = 0; i < NbLines; i++)
            {
                for(int j = 0; j < NbColumns; j++)
                {
                    if (i==j && _data[i, j] != 1) return false;
                    if (i != j && _data[i, j] != 0) return false;
                }
            }
            
            return true;
        }
        
        
        public int[,] ToArray2D()
        {
            int[,] clone = new int[NbLines, NbColumns];
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    clone[i, j] = _data[i, j];
                }
            }
            return clone;
        }
        
        public MatrixInt Multiply(int scalar)
        {
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    _data[i, j] *= scalar;
                }
            }
            return this;
        }
        
        public static MatrixInt Multiply(MatrixInt matrix, int scalar)
        {
            MatrixInt result = new MatrixInt(matrix);
            return result.Multiply(scalar);
        }
        
        public static MatrixInt operator *(MatrixInt matrix, int scalar)
        {
            return Multiply(matrix, scalar);
        }
        
        public static MatrixInt operator *(int scalar, MatrixInt matrix)
        {
            return Multiply(matrix, scalar);
        }
        
        public static MatrixInt operator -(MatrixInt matrix)
        {
            return Multiply(matrix, -1);
        }
        
        public MatrixInt Add(MatrixInt other)
        {
            if (NbLines != other.NbLines || NbColumns != other.NbColumns)
            {
                throw new MatrixSumException();
            }
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    _data[i, j] += other[i, j];
                }
            }
            return this;
        }

        public static MatrixInt Add(MatrixInt matrix1, MatrixInt matrix2)
        {
            return matrix1.Add(matrix2);
        }

        public static MatrixInt operator +(MatrixInt matrix1, MatrixInt matrix2)
        {
            return Add(matrix1, matrix2);
        }

        public static MatrixInt operator -(MatrixInt matrix1, MatrixInt matrix2)
        {
            if (matrix1.NbLines != matrix2.NbLines || matrix1.NbColumns != matrix2.NbColumns)
            {
                throw new ArgumentException("Matrices must have the same dimensions for addition.");
            }

            for (int i = 0; i < matrix1.NbLines; i++)
            {
                for (int j = 0; j < matrix1.NbColumns; j++)
                {
                    matrix1[i, j] -= matrix2[i, j];
                }
            }
            return matrix1;
        }

        public MatrixInt Multiply(MatrixInt matrix)
        {
            return Multiply(this, matrix);
        }
        
        public static MatrixInt Multiply(MatrixInt matrix1, MatrixInt matrix2)
        {
            if (matrix1.NbColumns != matrix2.NbLines)
            {
                throw new MatrixMultiplyException();
            }
            MatrixInt result = new MatrixInt(matrix1.NbLines, matrix2.NbColumns);
            for (int i = 0; i < matrix1.NbLines; i++)
            {
                for (int j = 0; j < matrix2.NbColumns; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < matrix1.NbColumns; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        public static MatrixInt operator *(MatrixInt matrix1, MatrixInt matrix2)
        {
            return Multiply(matrix1, matrix2);
        }

        public MatrixInt Transpose()
        {
            return Transpose(this);
        }
        
        public static MatrixInt Transpose(MatrixInt matrix)
        {  
            MatrixInt result = new MatrixInt(matrix.NbColumns, matrix.NbLines);
            for (int i = 0; i < matrix.NbLines; i++)
            {
                for (int j = 0; j < matrix.NbColumns; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }
            return result;
        }
    }
}