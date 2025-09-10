namespace Maths_Matrices.Tests;

public class MatrixSumException : Exception
{
    public MatrixSumException(string message) : base(message) { }
    public MatrixSumException() : base("Matrix dimensions must match for addition.") { }
}