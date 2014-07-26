using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utils.Helpers.Generators
{
    public class RandomGroups
    {
        readonly double[,] numbers = new double[,]
        {
            {2,3,4,},
            {3,4,5},
        };
        /// <summary>
        /// генерит функцию, дающую по индексу double числа от -1 до 1 с точностью до 3 знаков
        /// </summary>
        public static void Generate(string file,  int countOfGroups,int countInGroup)
        {
            Random rand = new Random();
            
            string res = "public static double[] GetRandomGroup(int index)\r\n" +
            "{\r\n" + "return NUMBERS[index];\r\n}"

            + "\r\nstatic readonly double[][] NUMBERS = new double[][]\r\n{\r\n";
            for(int i = 0; i < countOfGroups; i++){
                res += "new double[]{";
                for(int j = 0; j < countInGroup; j++)
                    res+= (rand.NextDouble()*2-1).ToString("N3")+",";
                res += "},";
            }

            res += "};";

            File.Create(file).Close();
            File.AppendAllText(file,res);
            
        }
    }
}
