using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix {

    public float[,] matrix;
    public int row;
    public int column;

    public Matrix(int initRow, int initColumn)
    {
        row = initRow;
        column = initColumn;
        matrix = new float[row, column];
    }

    float sigmoid(float x)
    {
        float sigm = 1 / (1 + Mathf.Exp(-x));

        return sigm;
    }

    // gradient error = derivative of the activation function (sigmoid)
    float dsigmoid(float y)
    {   
        return sigmoid(y) * ( 1 - sigmoid(y) );
    }  

    public void SetMatrixElement(float number)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                matrix[i, j] = number;
            }
        }
    }
    
    public float[] toArray()
    {
        List<float> result = new List<float>();
        
        for (int i = 0; i < row; i++)
        {
            result.Add(matrix[i, 0]);
        }
        return result.ToArray();
    }

    
    public void Randomize()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                matrix[i, j] = Random.Range(0f, 2f);
            }
        }
    }

    public void Print()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Debug.Log("Row " + i + " column " + j + " = " + matrix[i, j]);
            }
        }
    }

    public void PrintRowColumnInfo()
    {
        Debug.Log("Row " + row + " column = " + column);
    }

    public void PrintActivation()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Debug.Log("Activation / Output ke - " + i + " = " + matrix[i, j]);
            }
        }
    }

    public void PrintOnlyRow()
    {
        for (int i = 0; i < row; i++)
        {   
            Debug.Log(matrix[i,0]);
        }
    }

    public static Matrix Add(Matrix a, Matrix b)
    {
        for (int i = 0; i < a.row; i++)
        {
            for (int j = 0; j < a.column; j++)
            {
                a.matrix[i, j] += b.matrix[i, j];
            }
        }
        return a;
    }

    public static Matrix Subtract(Matrix a, Matrix b)
    {
        Matrix result = new Matrix(a.row, a.column);

        if (a.row != b.row || a.column != b.column)
        {
            Debug.Log("ERROR:: Columns and Rows of A must match Columns and Rows of B.");
            return null;
        }

        for (int i = 0; i < result.row; i++)
        {
            for (int j = 0; j < result.column; j++)
            {
                result.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j];
            }
        }

        return result;
    }

    /**
     * static method multiply = dot product
     * 
     * 2 args = Matrix 
     * return Matrix
     * 
     * */
    public static Matrix Multiply(Matrix a, Matrix b)
    {
        if (a.column != b.row)
        {
            Debug.Log("ERPRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
            return null;
        }

        Matrix result = new Matrix(a.row, b.column);

        for (int i = 0; i < result.row; i++)
        {
            for (int j = 0; j < result.column; j++)
            {
                float s = 0;
                for (int k = 0; k < a.column; k++)
                {
                    s += a.matrix[i, k] * b.matrix[k, j];
                }

                result.matrix[i, j] = s;
            }
        }

        return result;
    }
    
    public static Matrix Transpose(Matrix a)
    {   
        Matrix result = new Matrix(a.column, a.row);

        for (var i = 0; i < result.column; i++)
        {
            for (var j = 0; j < result.row; j++)
            {
                result.matrix[j, i] = a.matrix[i, j];
            }
        }
        return result;
        
    }

    public static Matrix fromArray(float[] arr)
    {
        Matrix forArr = new Matrix(arr.Length, 1);

        for (int i = 0; i < arr.Length; i++)
        {
            forArr.matrix[i, 0] = arr[i];
            
        }
        return forArr;
    }

    public static Matrix Derivative(Matrix a)
    {
        Matrix result = new Matrix(a.row, a.column);
        
        for (int i = 0; i < result.row; i++)
        {
            for (int j = 0; j < result.column; j++)
            {
                result.matrix[i, j] = a.dsigmoid(a.matrix[i, j]);
            }
        }

        return result;
    }

    /**
     * activation function (map value beetwen 0 - 1)
     * @return Matrix;
     * 
     * */
    public Matrix Activation()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                matrix[i, j] = sigmoid(matrix[i, j]);
            }
        }
        return this;
    }

    public Matrix Add(Matrix b)
    {
        if (row != b.row || column != b.column)
        {
            Debug.Log("ERROR ADD MATRIX: row and column should be same size");
            return null;
        }

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                matrix[i, j] += b.matrix[i, j];
            }
        }

        return this;
    }

    /**
     * 
     * instance method multiply self element / element wise
     * 
     * @return result matrix
     * 
     * */
    public Matrix Multiply<T>(T type)
    {
        if(typeof(T) == typeof(Matrix))
        {
            Matrix b = (Matrix)(object)type;
            if (row != b.row || column != b.column)
            {
                Debug.Log("ERROR:: Columns and Rows of A must match Columns and Rows of B.");
                return null;
            }
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] *= b.matrix[i, j];
                }
            }
        }
        else if(typeof(T) == typeof(float))
        {
            float x = (float)(object)type;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] *= x;
                }
            }
        }
        else
        {
            Debug.Log("ERROR:: Multiply value must be Matrix or float");
        }

        return this;
        
    }
    
    public Matrix Transpose()
    {
        Matrix result = new Matrix(column, row);
        
        for (var i = 0; i < result.column; i++)
        {
            for (var j = 0; j < result.row; j++)
            {
                result.matrix[j, i] = matrix[i, j];
            }
        }
        return result;
    }
    
}
