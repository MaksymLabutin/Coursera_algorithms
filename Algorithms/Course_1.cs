using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms
{
     [TestClass]
    public class Course_1
    {
        //todo
        // Нужно умножать 4 последних числа input2 поочередно с 4мя каждыми числами из input1 ( то есть последние 4 числа в итоге умножиться на весь input1 и тд.) 
        [TestMethod]
        //public void TestMethod1()
        //{
        //    //var input1 = new BigInteger(3141592653589793238462643383279502884197169399375105820974944592);
        //    //var input2 = 2718281828459045235360287471352662497757247093699959574966967627;

        //    //var sum = GetMultpFourNumbers(input1, input2);

        //    var rez ="8539734222673567065463550869546574495034888535765114961879601127067743044893204848617875072216249073013374895871952806582723184";

        //    var aaa = rez.Equals(sum);
        //}

        private List<string> GetMultpFourNumbers(string input1, string input2, int iteration = 1)
        {
            var rez = new List<string>();
            var y = input2.Substring(input2.Length - 4, 4);
            input2 = input2.Remove(input2.Length - 4, 4);
            if (input2.Length > 4)
            {
                rez = GetMultpFourNumbers(input1, input2, iteration++);
            }


            return rez;
        }

        //private List<string> GetMultpFourNumbers(string input1, string input2, int iteration = 1)
        //{
        //    var y = input2.Substring(input2.Length - 4, 4);

        //    if (input1.Length > 4)
        //    {

        //    }
        //        var x = input1.Substring(input1.Length - 4, 4);
        //        input1 = input1.Remove(input1.Length - 4, 4);
        //        var x_1_1 = x.Substring(0, 2);
        //        var x_1_2 = x.Substring(2, 2);

        //        var y_1_1 = y.Substring(0, 2);
        //        var y_1_2 = y.Substring(2, 2);

        //        var a = Convert.ToInt32(x_1_1);
        //        var b = Convert.ToInt32(x_1_2);
        //        var c = Convert.ToInt32(y_1_1);
        //        var d = Convert.ToInt32(y_1_2);

        //        var step1 = a * c;
        //        var step2 = b * d;
        //        var step3 = (a + b) * (c + d);
        //        var step4 = step3 - step2 - step1;
        //        var rez = (step1 * 10000 + step2 + step4 * 100).ToString();


        //    return new List<string>();
        //}


        [TestMethod]
        public void SortingArray()
        {
            var list = new List<int>(100100);
            var line = "";

            System.IO.StreamReader file = new System.IO.StreamReader(@"d:\array.txt");
            while ((line = file.ReadLine()) != null)
            {
                list.Add(Convert.ToInt32(line));
            }

            file.Close();

            var sortedList = CoutInversion(list);

        }

        private Tuple<long, List<int>> CoutInversion(List<int> list)
        {
            if (list.Count < 2)
            {
                return new Tuple<long, List<int>>(0, list);
            }

            // divide

            int middle = list.Count / 2;
            var leftList = list.GetRange(0, middle);
            var rightList = list.GetRange(middle, list.Count - leftList.Count);


            // process recursively
            var left = CoutInversion(leftList);
            var right = CoutInversion(rightList);

            // merge
            var mergetList = MargeAndCount(left.Item2, right.Item2);

            return new Tuple<long, List<int>>(left.Item1 + right.Item1 + mergetList.Item1, mergetList.Item2);

        }

        private Tuple<long, List<int>> MargeAndCount(List<int> left, List<int> right)
        {
            var inversions = 0;
            var i = 0;
            var j = 0;

            var newList = new List<int>();

            while (left.Count > i && right.Count > j)
            {
                if (left[i] < right[j])
                {
                    newList.Add(left[i]);
                    i++;
                }
                else
                {
                    newList.Add(right[j]);
                    j++;
                    inversions += left.Count - i;
                }
            }

            if (left.Count > i)
            {
                newList.AddRange(left.GetRange(i, left.Count - i));

            }
            else if (right.Count > j)
            {
                newList.AddRange(right.GetRange(j, right.Count - j));
            }

            return new Tuple<long, List<int>>(inversions, newList);
        }


        //Week 3
        [TestMethod]
        public void QuickSort()
        {
            var list = new List<int>(10000);
            var line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(@"d:\arr2.txt");
            while ((line = file.ReadLine()) != null)
            {
                list.Add(Convert.ToInt32(line));
            }


            
            //var list = new List<int>
            //{
            //    2, 20, 1, 15, 3, 11, 13, 6, 16, 10, 19, 5, 4, 9, 8, 14, 18, 17, 7, 12
            //};

            //pivot = 0
            //var rez = QuickSortArrayPivot0(list);

            //sotring by last element
            //var rezLastPivot = QuickSortArrayPivotLast(list);

            //find list pivot and change with first arr el
            //var pivotWithExchange = QuickSortArrayExchangePivot(list);

            //find and use mediane pivot
            var pivotMedian = QuickSortArrayMedianPivot(list);
        }

        private static Tuple<int, List<int>> QuickSortArrayPivot0(List<int> list, int pivot = 0)
        {
            if (list.Count < 2)
            {
                return new Tuple<int, List<int>>(0, list);
            }
            var tmp = 0;


            var i = pivot + 1;
            var comparisons = list.Count - i;

            var lenght = list.Count;

            for (var j = i; j < lenght; j++)
            {
                if (list[j] < list[pivot])
                {
                    tmp = list[j];
                    list[j] = list[i];
                    list[i] = tmp;
                    i += 1;

                }
            }

            tmp = list[pivot];
            list[pivot] = list[i - 1];
            list[i - 1] = tmp;

            var biger = QuickSortArrayPivot0(list.GetRange(i, list.Count - i));
            list.RemoveRange(i, biger.Item2.Count);
            list.InsertRange(i, biger.Item2);

            var less = QuickSortArrayPivot0(list.GetRange(0, i - 1));
            list.RemoveRange(0, less.Item2.Count);
            list.InsertRange(0, less.Item2);

            return new Tuple<int, List<int>>(comparisons + biger.Item1 + less.Item1, list);

        }

        private static Tuple<int, List<int>> QuickSortArrayExchangePivot(List<int> list, int pivot = 0)
        {
            if (list.Count < 2)
            {
                return new Tuple<int, List<int>>(0, list);
            }
            var tmp = 0;

            var i = pivot + 1;

            tmp = list[0];
            list[0] = list[list.Count - 1];
            list[list.Count - 1] = tmp;

            var comparisons = list.Count - 1;

            var lenght = list.Count;

            for (var j = i; j < lenght; j++)
            {
                if (list[j] < list[pivot])
                {
                    tmp = list[j];
                    list[j] = list[i];
                    list[i] = tmp;
                    i += 1;

                }
            }

            tmp = list[pivot];
            list[pivot] = list[i - 1];
            list[i - 1] = tmp;

            var biger = QuickSortArrayExchangePivot(list.GetRange(i, list.Count - i));
            list.RemoveRange(i, biger.Item2.Count);
            list.InsertRange(i, biger.Item2);

            var less = QuickSortArrayExchangePivot(list.GetRange(0, i - 1));
            list.RemoveRange(0, less.Item2.Count);
            list.InsertRange(0, less.Item2);

            return new Tuple<int, List<int>>(comparisons + biger.Item1 + less.Item1, list);

        }

        private static Tuple<int, List<int>> QuickSortArrayPivotLast(List<int> list)
        {
            if (list.Count < 2)
            {
                return new Tuple<int, List<int>>(0, list);
            }

            var lenght = list.Count;
            var count = lenght - 1;
            var pivot = lenght - 1;
            var tmp = 0;
            var i = pivot - 1;

            for (var j = i; j >= 0; j--)
            {
                if (list[j] > list[pivot])
                {
                    tmp = list[j];
                    list[j] = list[i];
                    list[i] = tmp;
                    i -= 1;

                }
            }

            tmp = list[pivot];
            list[pivot] = list[i + 1];
            list[i + 1] = tmp;

            var biger = QuickSortArrayPivotLast(list.GetRange(i + 1, lenght - (i + 1)));
            list.RemoveRange(i + 1, biger.Item2.Count);
            list.InsertRange(i + 1, biger.Item2);


            var less = QuickSortArrayPivotLast(list.GetRange(0, i + 1));
            list.RemoveRange(0, less.Item2.Count);
            list.InsertRange(0, less.Item2);

            return new Tuple<int, List<int>>(count + biger.Item1 + less.Item1, list);

        }

        private static Tuple<int, List<int>> QuickSortArrayMedianPivot(List<int> list, int pivot = 0)
        {
            if (list.Count < 2)
            {
                return new Tuple<int, List<int>>(0, list);
            }
            var tmp = 0;

            var i = pivot + 1;

            var median = 0;
            var medianaIndex = 0;
            var left = list[0];
            var right = list.Last();

            if (list.Count % 2 == 0 )
            {
                if(list.Count == 2)
                {
                    medianaIndex = 0;
                }else
                {
                    var list2 = new List<int>() { list[list.Count / 2 - 1], left, right };
                    list2.Sort();
                    median = list2[1];

                    if (left == median)
                    {
                        medianaIndex = 0;
                    }
                    else if(right == median)
                    {
                        medianaIndex = list.Count - 1;
                    }
                    else
                    {
                        medianaIndex = list.Count / 2 - 1;
                    }
                }
                
            }
            else
            {
                var list2 = new List<int>() { list[list.Count / 2], left, right };
                list2.Sort();
                median = list2[1];

                if (left == median)
                {
                    medianaIndex = 0;
                }
                else if(right == median)
                {
                    medianaIndex = list.Count - 1;
                }
                else
                {
                    medianaIndex = list.Count / 2;
                }
            }
            
            tmp = list[0];
            list[0] = list[medianaIndex];
            list[medianaIndex] = tmp;

            var comparisons = list.Count - 1;

            var lenght = list.Count;

            for (var j = i; j < lenght; j++)
            {
                if (list[j] < list[pivot])
                {
                    tmp = list[j];
                    list[j] = list[i];
                    list[i] = tmp;
                    i += 1;
                }
            }

            tmp = list[pivot];
            list[pivot] = list[i - 1];
            list[i - 1] = tmp;

            var biger = QuickSortArrayMedianPivot(list.GetRange(i, list.Count - i));
            list.RemoveRange(i, biger.Item2.Count);
            list.InsertRange(i, biger.Item2);

            var less = QuickSortArrayMedianPivot(list.GetRange(0, i - 1));
            list.RemoveRange(0, less.Item2.Count);
            list.InsertRange(0, less.Item2);

            return new Tuple<int, List<int>>(comparisons + biger.Item1 + less.Item1, list);

        }
    }
}
