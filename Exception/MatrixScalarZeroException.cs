namespace Maths_Matrices.Tests;

public class MatrixScalarZeroException : Exception
{
    public MatrixScalarZeroException() : base("Matrix Scalar Zero Exception") {}
    
    public MatrixScalarZeroException(string message) : base(message) {}
}