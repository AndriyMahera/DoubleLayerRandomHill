using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleLayerRandomHill
{
    public static class Code
    {
        public static List<int> HillPlusRandomEncrypt(List<int> list, double[,] matrix, int alphabet,List<int> random)
        {
            random.Clear();
            List<int> outList = new List<int>();
            List<int> reserve = new List<int>();
            reserve.AddRange(list);
            Random rnd = new Random();
            int order = (int)Math.Sqrt(matrix.Length);

            double[] Temp = new double[order];
            while (reserve.Count != 0)
            {
                if (reserve.Count < order) 
                {
                    int resC =reserve.Count;
                    for (int i = 0; i < order-resC; i++)
                        reserve.Add(26);
                }
                Temp = MathOperations.MultiplyMatrices(reserve.Take(order).Select(x => (double)x).ToArray(), matrix, alphabet);
                outList.AddRange(Temp.Select(x=>(int)x));
                reserve.RemoveRange(0,order);
                if (reserve.Count != 0)
                {
                    int ran = rnd.Next(0, 5);
                    ran = ran > reserve.Count ? reserve.Count : ran;
                    outList.AddRange(reserve.Take(ran));
                    random.Add(ran);
                    reserve.RemoveRange(0, ran);
                }
            }
            return outList;  
        }
        public static List<int> HillPlusRandomDecrypt(List<int> list, double[,] matrix, int alphabet, List<int> random)
        {          
            List<int> outList = new List<int>();
            List<int> reserve = new List<int>();
            List<int> resRand = new List<int>();
            resRand.AddRange(random);
            reserve.AddRange(list);
            int order = (int)Math.Sqrt(matrix.Length);

            double[] Temp = new double[order];
            while (reserve.Count != 0)
            {
                if (reserve.Count < order)
                    break;
                Temp = MathOperations.MultiplyMatrices(reserve.Take(order).Select(x => (double)x).ToArray(), matrix, alphabet);
                outList.AddRange(Temp.Select(x => (int)x));
                reserve.RemoveRange(0, order);
                if (resRand.Count != 0)
                {
                    outList.AddRange(reserve.Take(resRand.First()));
                    reserve.RemoveRange(0, resRand.First());
                    resRand.RemoveAt(0);
                }               
            }
            return outList;  
        }
    }
}
