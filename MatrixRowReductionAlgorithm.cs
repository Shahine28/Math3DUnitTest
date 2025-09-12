namespace Maths_Matrices.Tests;

public class MatrixRowReductionAlgorithm
{
    public static (MatrixFloat, MatrixFloat) Apply(MatrixFloat a, MatrixFloat b)
    {
        MatrixFloat matrix = MatrixFloat.GenerateAugmentedMatrix(a, b);
        int i = 0;

        for (int j = 0; j < a.NbColumns; j++) // seulement sur les colonnes de A
        {
            int k = i;
            for (int v = i; v < matrix.NbLines; v++)
            {
                if (Math.Abs(matrix[v, j]) > Math.Abs(matrix[k, j]))
                    k = v;
            }
            
            if (i != k) 
                MatrixElementaryOperations.SwapLines(matrix, i, k);
            
            float pivot = matrix[i, j];
            if (Math.Abs(pivot) < 1e-6f) 
                continue; 

            MatrixElementaryOperations.MultiplyLine(matrix, i, 1f / pivot);
            
            for (int r = 0; r < matrix.NbLines; r++)
            {
                if (r == i) continue;
                float factor = matrix[r, j];
                if (Math.Abs(factor) > 1e-6f)
                    MatrixElementaryOperations.AddLineToAnother(matrix, i, r, -factor);
            }

            i++;
        }
        (MatrixFloat left, MatrixFloat right) = matrix.Split(a.NbColumns - 1);
        return (left, right);
    }

}