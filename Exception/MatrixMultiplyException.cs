namespace Maths_Matrices.Tests;

public class MatrixMultiplyException : Exception
{
    public MatrixMultiplyException(string message) : base(message) { }
    
    public MatrixMultiplyException() : base("NbColumns of Matrix 1 need to be equal to NbLines of Matrix 2 for multiplication"){}
}