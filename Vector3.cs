namespace Maths_Matrices.Tests;

public class Vector3
{
    public float x;
    public float y;
    public float z;
    
    public Vector3()
    {
        x = 0f;
        y = 0f;
        z = 0f;
    }
    public Vector3(float X, float Y, float Z)
    {
        x = X;
        y = Y;
        z = Z;
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
                default:
                    throw new IndexOutOfRangeException("Vector4 index must be 0, 1, or 2.");
            }
        }
        set
        {
            switch (i)
            {
                case 0: x = value; break;
                case 1: y = value; break;
                case 2: z = value; break;
                default:
                    throw new IndexOutOfRangeException("Vector3 index must be 0, 1, or 2.");
            }
        }
    }
}