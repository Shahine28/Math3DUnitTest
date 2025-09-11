using System;

namespace Maths_Matrices.Tests
{
    public class MatrixFloat
    {
        private float[,] _data;
        public int NbLines { get; }
        public int NbColumns { get; }
        
        public MatrixFloat(int n, int m)
        {
            NbLines = n;
            NbColumns = m;
            _data = new float[n, m];
        }

  
        public MatrixFloat(float[,] values)
        {
            NbLines = values.GetLength(0);
            NbColumns = values.GetLength(1);
            _data = new float[NbLines, NbColumns];
            for (int i = 0; i < NbLines; i++)
                for (int j = 0; j < NbColumns; j++)
                    _data[i, j] = values[i, j];
        }


        public MatrixFloat(MatrixFloat other)
        {
            NbLines = other.NbLines;
            NbColumns = other.NbColumns;
            _data = new float[NbLines, NbColumns];
            for (int i = 0; i < NbLines; i++)
                for (int j = 0; j < NbColumns; j++)
                    _data[i, j] = other[i, j];
        }


        public float this[int i, int j]
        {
            get => _data[i, j];
            set => _data[i, j] = value;
        }


        public float[] GetLine(int i)
        {
            float[] line = new float[NbColumns];
            for (int j = 0; j < NbColumns; j++)
                line[j] = _data[i, j];
            return line;
        }

        public void SetLine(int i, float[] values)
        {
            if (values.Length != NbColumns)
                throw new ArgumentException("Line length must match number of columns.");
            for (int j = 0; j < NbColumns; j++)
                _data[i, j] = values[j];
        }
        
        public float[] GetColumn(int j)
        {
            float[] col = new float[NbLines];
            for (int i = 0; i < NbLines; i++)
                col[i] = _data[i, j];
            return col;
        }

        public void SetColumn(int j, float[] values)
        {
            if (values.Length != NbLines)
                throw new ArgumentException("Column length must match number of lines.");
            for (int i = 0; i < NbLines; i++)
                _data[i, j] = values[i];
        }
        
        
        public static MatrixFloat Identity(int size)
        {
            float[,] data = new float[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    data[i, j] = (i == j) ? 1f : 0f;
            return new MatrixFloat(data);
        }

        public bool IsIdentity()
        {
            if (NbLines != NbColumns) return false;
            for (int i = 0; i < NbLines; i++)
                for (int j = 0; j < NbColumns; j++)
                {
                    if (i == j && _data[i, j] != 1f) return false;
                    if (i != j && _data[i, j] != 0f) return false;
                }
            return true;
        }

  
        public float[,] ToArray2D()
        {
            float[,] clone = new float[NbLines, NbColumns];
            for (int i = 0; i < NbLines; i++)
                for (int j = 0; j < NbColumns; j++)
                    clone[i, j] = _data[i, j];
            return clone;
        }

        
        public MatrixFloat Multiply(float scalar)
        {
            for (int i = 0; i < NbLines; i++)
                for (int j = 0; j < NbColumns; j++)
                    _data[i, j] *= scalar;
            return this;
        }

        public static MatrixFloat Multiply(MatrixFloat matrix, float scalar)
        {
            MatrixFloat result = new MatrixFloat(matrix);
            return result.Multiply(scalar);
        }

        public static MatrixFloat operator *(MatrixFloat matrix, float scalar) => Multiply(matrix, scalar);
        public static MatrixFloat operator *(float scalar, MatrixFloat matrix) => Multiply(matrix, scalar);
        public static MatrixFloat operator -(MatrixFloat matrix) => Multiply(matrix, -1f);


        public MatrixFloat Add(MatrixFloat other)
        {
            if (NbLines != other.NbLines || NbColumns != other.NbColumns)
                throw new ArgumentException("Matrix dimensions must match.");
            for (int i = 0; i < NbLines; i++)
                for (int j = 0; j < NbColumns; j++)
                    _data[i, j] += other[i, j];
            return this;
        }

        public static MatrixFloat Add(MatrixFloat m1, MatrixFloat m2) => new MatrixFloat(m1).Add(m2);
        public static MatrixFloat operator +(MatrixFloat m1, MatrixFloat m2) => Add(m1, m2);
        
        public static MatrixFloat operator -(MatrixFloat m1, MatrixFloat m2)
        {
            if (m1.NbLines != m2.NbLines || m1.NbColumns != m2.NbColumns)
                throw new ArgumentException("Matrix dimensions must match.");
            MatrixFloat result = new MatrixFloat(m1);
            for (int i = 0; i < m1.NbLines; i++)
                for (int j = 0; j < m1.NbColumns; j++)
                    result[i, j] -= m2[i, j];
            return result;
        }
        
        public MatrixFloat Multiply(MatrixFloat other) => Multiply(this, other);

        public static MatrixFloat Multiply(MatrixFloat m1, MatrixFloat m2)
        {
            if (m1.NbColumns != m2.NbLines)
                throw new ArgumentException("Invalid dimensions for multiplication.");
            MatrixFloat result = new MatrixFloat(m1.NbLines, m2.NbColumns);
            for (int i = 0; i < m1.NbLines; i++)
                for (int j = 0; j < m2.NbColumns; j++)
                {
                    float sum = 0f;
                    for (int k = 0; k < m1.NbColumns; k++)
                        sum += m1[i, k] * m2[k, j];
                    result[i, j] = sum;
                }
            return result;
        }

        public static MatrixFloat operator *(MatrixFloat m1, MatrixFloat m2) => Multiply(m1, m2);
        
        public MatrixFloat Transpose() => Transpose(this);

        public static MatrixFloat Transpose(MatrixFloat matrix)
        {
            MatrixFloat result = new MatrixFloat(matrix.NbColumns, matrix.NbLines);
            for (int i = 0; i < matrix.NbLines; i++)
                for (int j = 0; j < matrix.NbColumns; j++)
                    result[j, i] = matrix[i, j];
            return result;
        }
        
        public static MatrixFloat GenerateAugmentedMatrix(MatrixFloat matrix, MatrixFloat matrixColumn)
        {
            if (matrix.NbLines != matrixColumn.NbLines)
                throw new ArgumentException("Matrices must have the same number of lines.");
            if (matrixColumn.NbColumns != 1)
                throw new ArgumentException("Column matrix must have exactly 1 column.");

            MatrixFloat result = new MatrixFloat(matrix.NbLines, matrix.NbColumns + 1);

            for (int i = 0; i < matrix.NbLines; i++)
            {
                // Copier la matrice originale
                for (int j = 0; j < matrix.NbColumns; j++)
                    result[i, j] = matrix[i, j];

                // Ajouter la colonne supplémentaire
                result[i, matrix.NbColumns] = matrixColumn[i, 0];
            }

            return result;
        }

        public (MatrixFloat, MatrixFloat) Split(int splitColumnIndex)
        {
            if (splitColumnIndex < 0 || splitColumnIndex >= NbColumns - 1)
                throw new ArgumentException("Split index must be between 0 and NbColumns - 2.");

            // Partie gauche : colonnes [0 .. splitColumnIndex]
            MatrixFloat left = new MatrixFloat(NbLines, splitColumnIndex + 1);

            // Partie droite : colonnes [splitColumnIndex+1 .. NbColumns-1]
            MatrixFloat right = new MatrixFloat(NbLines, NbColumns - (splitColumnIndex + 1));

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j <= splitColumnIndex; j++)
                    left[i, j] = _data[i, j];

                for (int j = splitColumnIndex + 1; j < NbColumns; j++)
                    right[i, j - (splitColumnIndex + 1)] = _data[i, j];
            }

            return (left, right);
        }
        
    }
}
