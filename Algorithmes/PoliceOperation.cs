using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmes
{
    //https://www.hackerrank.com/challenges/police-operation/problem

    public static class PoliceOperation
    {
        static int[] arr;
        static double h;
        static int count;
        static List<Task> tasks = new List<Task>();

        static double CalcMinimalChargeold(int pos, double charged)
        {
            if (pos == count)
                return charged;
            else if (pos + 1 == count)
                return charged + h;


            double tempCharged = 0;

            int startPos = pos;
            int i = pos + 1;
            while (i < count)
            {


                if (i + 1 < count)
                {
                    if (tempCharged == 0 && arr[i] - arr[i - 1] > arr[i + 1] - arr[i])
                        tempCharged = CalcMinimalCharge(i, charged + h + Math.Pow(arr[i - 1] - arr[startPos], 2));
                }

                if (h < Math.Pow(arr[i] - arr[startPos], 2))
                {
                    charged += h + Math.Pow(arr[i - 1] - arr[startPos], 2);
                    startPos = i;
                }

                if (i + 1 == count)
                {
                    charged += h + Math.Pow(arr[i] - arr[startPos], 2);
                    break;
                }

                i++;
            }

            if (charged < tempCharged || tempCharged == 0)
                return charged;
            else
                return tempCharged;
        }


        static double CalcMinimalCharge(int pos, double charged)
        {
            if (pos == count)
                return charged;
            else if (pos + 1 == count)
                return charged + h;


            double tempCharged = Task.Run<double>(() => CalcMinimalCharge(pos + 1, charged + h)).Result;

            int startPos = pos;
            int i = pos + 1;
            while (i < count)
            {
                if (h < Math.Pow(arr[i] - arr[startPos], 2))
                {
                    charged += h + Math.Pow(arr[i - 1] - arr[startPos], 2);
                    startPos = i;
                }

                if (i + 1 == count)
                {
                    charged += h + Math.Pow(arr[i] - arr[startPos], 2);
                    break;
                }

                i++;
            }


            if (charged < tempCharged)
                return charged;
            else
                return tempCharged;
        }


        static void Main(string[] args)
        {
            /* string inputParams1 = Console.ReadLine();
             string inputParams2 = Console.ReadLine();*/
            string[] inputs = System.IO.File.ReadAllLines("input1.txt"); //2462218192998
            string inputParams1 = inputs[0];
            string inputParams2 = inputs[1];

            /* string inputParams1 = "5 10";
             string inputParams2 = "1 4 5 6 9";*/

            //  List<vector> vectors = new List<vector>();
            string[] firstInput = inputParams1.Split(' ');
            arr = inputParams2.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
            count = Convert.ToInt32(firstInput[0]);
            h = Convert.ToInt32(firstInput[1]);

            if (h == 1)
                Console.WriteLine(count * 1);
            else
            {
                //double total = CalcMinimalCharge(0, 0);
                double total = Task.Run<double>(() => CalcMinimalCharge(0, 0)).Result;

                Console.WriteLine(total);
            }

        }
    }
}
