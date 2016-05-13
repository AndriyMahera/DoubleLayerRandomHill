using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleLayerRandomHill
{
    public static class MathOperations
    {
        //Транспонування матриці
        public static T[,] TransponeMatrix<T>(T[,] input)
        {
            T[,] output = new T[input.GetLength(1), input.GetLength(0)];
            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int j = 0; j < output.GetLength(1); j++)
                {
                    output[j, i] = input[i, j];
                }
            }
            return output;
        }
        //Створити меншу матрицю
        public static T[,] CreateSmallerMatrix<T>(T[,] input, int i, int j)
        {
            int order = (int)Math.Sqrt(input.Length);
            T[,] output = new T[order - 1, order - 1];
            int x = 0, y = 0;
            for (int m = 0; m < order; m++, x++)
            {
                if (m != i)
                {
                    y = 0;
                    for (int n = 0; n < order; n++)
                    {
                        if (n != j)
                        {
                            output[x, y] = input[m, n];
                            y++;
                        }
                    }
                }
                else
                {
                    x--;
                }
            }
            return output;
        }
        //Знак мінора
        public static int SighOfElement(int i, int j)
        {
            if ((i + j) % 2 == 0)
            {
                return 1;
            }
            else
                return -1;
        }
        //Детермінант
        public static double Determinant(double[,] input)
        {
            int order = (int)Math.Sqrt(input.Length);
            if (order > 2)
            {
                double value = 0;
                for (int j = 0; j < order; j++)
                {
                    double[,] Temp = CreateSmallerMatrix(input, 0, j);
                    value += input[0, j] * (SighOfElement(0, j) * Determinant(Temp));
                }
                return value;
            }
            else if (order == 2)
            {
                return ((input[0, 0] * input[1, 1]) - (input[1, 0] * input[0, 1]));
            }
            else
            {
                return input[0, 0];
            }
        }
        //Матриця мінорів
        public static double[,] MinoreMatrix(double[,] input)
        {
            int order = (int)Math.Sqrt(input.Length);
            double[,] output = new double[order, order];
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    double[,] Temp = CreateSmallerMatrix(input, i, j);
                    output[i, j] = Determinant(Temp) * SighOfElement(i, j);
                }
            }
            return output;
        }
        //Формування оберненої матриці
        public static double[,] FormInverseMatrix(double[,] input, int coef, int alphabet)
        {
            int order = (int)Math.Sqrt(input.Length);
            double[,] output = new double[order, order];
            output = MinoreMatrix(input);
            output = TransponeMatrix(output);
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    output[i, j] = output[i, j] % alphabet;
                    if (output[i, j] < 0)
                        output[i, j] += alphabet;
                }
            }
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    output[i, j] *= coef;
                    output[i, j] = output[i, j] % alphabet;
                }
            }
            return output;
        }
        //Знайти коеф
        public static int FindCoef(int a, int m)
        {
            int x, y;
            int g = GCD(a, m, out x, out y);
            if (g != 1)
                throw new ArgumentException();
            return (x % m + m) % m;
        }
        //Розширений алгоритм Евкліда
        public static int GCD(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            int x1, y1;
            int d = GCD(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }
        //Помножити вектор на матрицю
        public static double[] MultiplyMatrices(double[] vector, double[,] matrix, int alphabet)
        {
            int order = (int)Math.Sqrt(matrix.Length);
            double[] newVector = new double[vector.Length];
            for (int j = 0; j < order; j++)
            {
                for (int i = 0; i < order; i++)
                {
                    newVector[j] += matrix[j, i] * vector[i];
                }
                newVector[j] = newVector[j] % alphabet;
            }
            return newVector;
        }
    }
}
