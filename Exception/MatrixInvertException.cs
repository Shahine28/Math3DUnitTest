using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.Execution;

namespace Maths_Matrices.Tests;

public class MatrixInvertException : Exception
{
    public MatrixInvertException(string message) : base(message) { }
}