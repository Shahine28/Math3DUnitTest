using System;
using System.IO.Compression;

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
        
        public static MatrixFloat GenerateAugmentedMatrix(MatrixFloat left, MatrixFloat right)
        {
            if (left.NbLines != right.NbLines)
                throw new ArgumentException("Matrices must have the same number of lines.");

            // Fusion : même nb de lignes, colonnes concaténées
            MatrixFloat result = new MatrixFloat(left.NbLines, left.NbColumns + right.NbColumns);

            for (int i = 0; i < left.NbLines; i++)
            {
                // Copier les colonnes de la matrice de gauche
                for (int j = 0; j < left.NbColumns; j++)
                    result[i, j] = left[i, j];

                // Copier les colonnes de la matrice de droite
                for (int j = 0; j < right.NbColumns; j++)
                    result[i, left.NbColumns + j] = right[i, j];
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

        public MatrixFloat InvertByRowReduction()
        {
            return InvertByRowReduction(this);
        }

        public static MatrixFloat InvertByRowReduction(MatrixFloat matrix)
        {
            if (matrix.NbLines != matrix.NbColumns)
            {
                throw new MatrixInvertException("Matrix dimensions must have the same number of lines and columns.");
            }
            MatrixFloat IdentityMatrix = Identity(matrix.NbLines);
            (MatrixFloat left, MatrixFloat right) = MatrixRowReductionAlgorithm.Apply(matrix, IdentityMatrix);

            for (int i = 0; i < right.NbLines; i++)
            {
                for (int j = 0; j < right.NbColumns; j++)
                {
                    float expected = (i == j) ? 0 : 1;
                    if (Math.Abs(right[i, j] - expected) > 0.0001f)
                        throw new MatrixInvertException("The matrix is singular");
                }
            }
            return right;
        }

        public MatrixFloat SubMatrix(int lineIndex, int columnIndex)
        {
            return SubMatrix(this, lineIndex, columnIndex);
        }
        
        public static MatrixFloat SubMatrix(MatrixFloat matrix, int lineIndex, int columnIndex)
        {
            MatrixFloat result = new MatrixFloat(matrix.NbLines - 1, matrix.NbColumns - 1);
            int newI = 0;
            for (int i = 0; i < matrix.NbLines; i++)
            {
                if (i == lineIndex) continue;
                int newJ = 0;
                for (int j = 0; j < matrix.NbColumns; j++)
                {
                    if (j == columnIndex) continue;
                    
                    result[newI, newJ] = matrix[i, j];
                    newJ++;
                    
                }

                newI++;
            }
            return result;
        }

        public static float Determinant(MatrixFloat matrix)
        {
            if (matrix.NbLines != matrix.NbColumns)
                throw new ArgumentException("Matrix dimensions must have the same number of lines and columns.");
            MatrixFloat result = InvertByRowReduction(matrix);
            float determinant = matrix[0, 0];
            for (int i = 1; i < result.NbLines; i++)
            {
                for (int j = 1; j < result.NbColumns; j++)
                {
                    if (j == i) determinant *=  matrix[i, j];
                }
            }
            
            return determinant;
        }
        
    }
}
