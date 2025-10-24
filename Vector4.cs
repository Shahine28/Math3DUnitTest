namespace Maths_Matrices.Tests;

public class Vector4
{
    public float x;
    public float y;
    public float z;
    public float w;
    
    public Vector4()
    {
        x = 0f;
        y = 0f;
        z = 0f;
        w = 0f;
    }
    public Vector4(float X, float Y, float Z, float W)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }
    
    public float this[int i]
    {
        get
        {
            switch (i)
            {
                case 0: return x;
                case 1: return y;
                case 2: return z;
                case 3: return w;
                default:
                    throw new IndexOutOfRangeException("Vector4 index must be 0, 1, 2 or 3.");
            }
        }
        set
        {
            switch (i)
            {
                case 0: x = value; break;
                case 1: y = value; break;
                case 2: z = value; break;
                case 3: w = value; break;
                default:
                    throw new IndexOutOfRangeException("Vector4 index must be 0, 1, 2 or 3.");
            }
        }
    }

    public static Vector4 operator *(MatrixFloat m, Vector4 v)
    {
        Vector4 result = new Vector4(0f, 0f, 0f, 0f);
        for (int row = 0; row < 4; row++)
        {
            float sum = 0f;
            for (int col = 0; col < 4; col++)
            {
                sum += m[row, col] * v[col];
            }
            result[row] = sum;
        }
        return result;
    }
}