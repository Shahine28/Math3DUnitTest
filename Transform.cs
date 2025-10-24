namespace Maths_Matrices.Tests;

public class Transform
{
    
    public Vector3 LocalPosition { get; set; }
    public Vector3 LocalRotation { get; set; }
    public Vector3 LocalScale { get; set; }
    public Transform Parent { get; set; }
    
    public Vector3 WorldPosition
    {
        get
        {
            MatrixFloat worldMatrix;
            if (Parent != null)
            {
                MatrixFloat parentWorldMatrix = Parent.LocalToWorldMatrix;
                MatrixFloat localMatrix = LocalToWorldMatrix;
                worldMatrix = parentWorldMatrix * localMatrix;
            }
            else
            {
                worldMatrix = LocalToWorldMatrix;
            }
            return new Vector3(worldMatrix[0, 3], worldMatrix[1, 3], worldMatrix[2, 3]);
        }
    }
    
    public Transform()
    {
        LocalPosition = new Vector3(0f, 0f, 0f);
        LocalRotation = new Vector3(0f, 0f, 0f);
        LocalScale = new Vector3(1f, 1f, 1f);
    }
    
    public void SetParent(Transform parent)
    {
        Parent = parent;
    }
    public MatrixFloat LocalTranslationMatrix
    {
        get
        {
            return new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, LocalPosition.x },
                { 0f, 1f, 0f, LocalPosition.y },
                { 0f, 0f, 1f, LocalPosition.z },
                { 0f, 0f, 0f, 1f },
            });
        }
    }
    
    public MatrixFloat LocalRotationXMatrix
    {
        get
        {
            float radians = LocalRotation.x * (float)(Math.PI / 180.0);
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            return new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, cos, -sin, 0f },
                { 0f, sin, cos, 0f },
                { 0f, 0f, 0f, 1f },
            });
        }
    }
    
    public MatrixFloat LocalRotationYMatrix
    {
        get
        {
            float radians = LocalRotation.y * (float)(Math.PI / 180.0);
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            return new MatrixFloat(new[,]
            {
                { cos, 0f, sin, 0f },
                { 0f, 1f, 0f, 0f },
                { -sin, 0f, cos, 0f },
                { 0f, 0f, 0f, 1f },
            });
        }
    }
    
    public MatrixFloat LocalRotationZMatrix
    {
        get
        {
            float radians = LocalRotation.z * (float)(Math.PI / 180.0);
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            return new MatrixFloat(new[,]
            {
                { cos, -sin, 0f, 0f },
                { sin, cos, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            });
        }
    }
    
    public MatrixFloat LocalRotationMatrix
    {
        get
        {
            return LocalRotationYMatrix * LocalRotationXMatrix * LocalRotationZMatrix;
        }
    }
    
    public MatrixFloat LocalScaleMatrix
    {
        get
        {
            return new MatrixFloat(new[,]
            {
                { LocalScale.x, 0f, 0f, 0f },
                { 0f, LocalScale.y, 0f, 0f },
                { 0f, 0f, LocalScale.z, 0f },
                { 0f, 0f, 0f, 1f },
            });
        }
    }
    
    public MatrixFloat LocalToWorldMatrix
    {
        get
        {
            MatrixFloat localMatrix = LocalTranslationMatrix * LocalRotationMatrix * LocalScaleMatrix;

            if (Parent != null)
            {
                MatrixFloat parentWorld = Parent.LocalToWorldMatrix;
                return parentWorld * localMatrix; // hiérarchie : Parent * Local
            }

            return localMatrix;
        }
    }

    
    public MatrixFloat WorldToLocalMatrix
    {
        get
        {
            return LocalToWorldMatrix.InvertByRowReduction();
        }
    }
}