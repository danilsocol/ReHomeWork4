using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace ReHomeWork4
{
    class Program
    {
        static int numColumnSort { get; set; }
        static bool TypeSortUp { get; set; }

        static int countRow { get; set; }
        static bool IsDigit { get; set; }
        static void Main(string[] args)
        {
            DrawTable();
            ReadData();
            SaveData();
            SplitThroughOne();
            SortOne();
            SplitThroughTwo();
            SortTwo();

            Console.ReadLine();
        }

        static void DrawTable()
        {
            countRow = File.ReadAllLines("testTable.txt").Length;

            for (int i = 0; i < countRow; i++)
            {
                string[] text = File.ReadAllLines("testTable.txt")[i].Split(";");

                for (int j = 0; j < text.Length; j++)
                {
                    Console.Write($"{text[j]} ");
                }
                Console.WriteLine();
            }
        }

        static void ReadData()
        {
            Console.WriteLine("\n\nВведите название столбца по которому будете сортировать");
            // string nameColumnSort = Console.ReadLine();
            string nameColumnSort = "name";

            string[] text = File.ReadAllLines("testTable.txt")[0].Split(";");
            for (int i = 0; i < text.Length; i++)
            {
                if (nameColumnSort == text[i])
                {
                    numColumnSort = i;
                }
            }

            Console.WriteLine("Если хотите сортировать по возрастанию напишите UP, по убыванию DOWN ");
            // string TypeSort = Console.ReadLine();
            string TypeSort = "UP";
            if (TypeSort == "UP")
            {
                TypeSortUp = true;
            }
            else if (TypeSort == "DOWN")
            {
                TypeSortUp = false;
            }


        }

        static void SaveData()
        {
            ClearFile("A.txt");

            using (StreamWriter sw = new StreamWriter("A.txt", true, System.Text.Encoding.Default))
            {
                Console.WriteLine("Записываем выбранный столбец в файл");
                for (int i = 1; i < countRow; i++)
                {
                    string[] text = File.ReadAllLines("testTable.txt")[i].Split(";");
                    Console.Write($"{text[numColumnSort]} ");
                    sw.WriteLine($"{text[numColumnSort]};{i}");
                }
                string[] type = File.ReadAllLines("testTable.txt")[1].Split(";");
                IsDigit = type[numColumnSort].Length == type[numColumnSort].Where(c => char.IsDigit(c)).Count();

                Console.WriteLine();
            }
        }

        static void SplitThroughOne()
        {
            ClearFile("B.txt");
            ClearFile("C.txt");
            Console.WriteLine("Раскидываем по файлам через 1");

            for (int i = 0; i < countRow - 1; i++)
            {
                string text = File.ReadAllLines("A.txt")[i];
                if (i % 2 == 1)
                {
                    using (StreamWriter sw = new StreamWriter("B.txt", true, System.Text.Encoding.Default))
                    {
                        Console.WriteLine($"{text.Split(";")[0]} в B");
                        sw.WriteLine($"{text}");
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter("C.txt", true, System.Text.Encoding.Default))
                    {
                        Console.WriteLine($"{text.Split(";")[0]} в C");
                        sw.WriteLine($"{text}");
                    }
                }
            }
        }

        static void SplitThroughTwo()
        {
            ClearFile("B.txt");
            ClearFile("C.txt");
            Console.WriteLine("Раскидываем по файлам через 2");

            int j = 0;

            for (int i = 0; i < (countRow - 1) / 2; i++)
            {

                if (i % 2 == 1)
                {
                    for (int k = 0; k < 2; k++)
                    {

                        string text = File.ReadAllLines("A.txt")[j];

                        using (StreamWriter sw = new StreamWriter("B.txt", true, System.Text.Encoding.Default))
                        {
                            Console.WriteLine($"{text.Split(";")[0]} в B");
                            sw.WriteLine($"{text}");
                        }
                        j++;
                    }
                }
                else
                {
                    for (int k = 0; k < 2; k++)
                    {
                        string text = File.ReadAllLines("A.txt")[j];

                        using (StreamWriter sw = new StreamWriter("C.txt", true, System.Text.Encoding.Default))
                        {
                            Console.WriteLine($"{text.Split(";")[0]} в C");
                            sw.WriteLine($"{text}");
                        }
                        j++;
                    }
                }
            }
        }

        static void ClearFile(string file)
        {
            File.Delete(file);
            var myFile = File.Create(file);
            myFile.Close();
        }



        static void SortOne()
        {
            ClearFile("A.txt");

            int i = 0;
            int j = 0;
            string elB = "";
            string elC = "";

            int countLinesInB = File.ReadAllLines("B.txt").Length;
            int countLinesInC = File.ReadAllLines("C.txt").Length;

            while (i + j < countRow - 1)
            {
                //if (i >= countLinesInB || j >= countLinesInC)
                //{

                //    if (i >= countLinesInB)
                //    {
                //        elC = File.ReadAllLines("C.txt")[j];
                //        WriteSort(elB, elC);
                //        j++;
                //    }
                //    if (j >= countLinesInC)
                //    {
                //        elB = File.ReadAllLines("B.txt")[i];
                //        WriteSort(elC, elB);
                //        i++;
                //    }
                //}
                //else 
                //{
                //    if (i < countLinesInB)
                //    {
                //        elB = File.ReadAllLines("B.txt")[i];
                //    }

                //    if (j < countLinesInC)
                //    {
                //        elC = File.ReadAllLines("C.txt")[j];
                //    }

                //    SelectSort(elB, elC, ref i, ref j);
                //}

                if (i < countLinesInB)
                {
                    elB = File.ReadAllLines("B.txt")[i];
                }

                if (j < countLinesInC)
                {
                    elC = File.ReadAllLines("C.txt")[j];
                }

                Console.WriteLine($"Берем элемент {elB.Split(";")[0]} из B и элемент {elC.Split(";")[0]} из С, сортируем их");

                SelectSortOne(elB, elC, ref i, ref j);

            }
        }
        static void SortTwo()
        {
            ClearFile("A.txt");

            int i = 0;
            int j = 0;
            string elB = "";
            string elC = "";

            int countLinesInB = File.ReadAllLines("B.txt").Length;
            int countLinesInC = File.ReadAllLines("C.txt").Length;

            //if (i < countLinesInB)
            //{
            //    elB = File.ReadAllLines("B.txt")[i];
            //}

            //if (j < countLinesInC)
            //{
            //    elC = File.ReadAllLines("C.txt")[j];
            //}
            //Console.WriteLine($"Берем элемент {elB.Split(";")[0]} из B и элемент {elC.Split(";")[0]} из С, сортируем их");

            //SelectSortTwo(elB, elC, ref i, ref j);

            for (int k = 0; k < (countRow-1)/4; k++)
            {
                elB = "";
                elC = "";

                for (int n = 0; n < 4; n++)
                {
                    if (i <= 2 * (k+1)-1 && j <= 2 * (k + 1) - 1)
                    {
                        if (i < countLinesInB)
                        {
                            elB = File.ReadAllLines("B.txt")[i];
                        }
                        if (j < countLinesInC)
                        {
                            elC = File.ReadAllLines("C.txt")[j];
                        }
                        SelectSortTwo(elB, elC, ref i, ref j);
                    }
                    else
                    {
                        if(i <= 2 * (k + 1) - 1)
                        {
                            elB = File.ReadAllLines("B.txt")[i];
                            WriteSort(elB);
                            i++;
                        }
                        else if (j <= 2 * (k + 1) - 1)
                        {
                            elC = File.ReadAllLines("C.txt")[j];
                            WriteSort(elC);
                            j++;
                        }
                    }
                    
                }

                
            }

            //while (i + j < countRow - 1)
            //{
            //    if (i >= countLinesInB || j >= countLinesInC)
            //    {

            //        if (j >= countLinesInB)
            //        {
            //            elB = File.ReadAllLines("B.txt")[i];
            //            WriteSort(elB);
            //            i++;
            //        }
            //        else if (i >= countLinesInC)
            //        {
            //            elC = File.ReadAllLines("C.txt")[j];
            //            WriteSort(elC);
            //            j++;
            //        }
            //    }
            //    else
            //    {
            //        if (i < countLinesInB)
            //        {
            //            elB = File.ReadAllLines("B.txt")[i];
            //        }

            //        if (j < countLinesInC)
            //        {
            //            elC = File.ReadAllLines("C.txt")[j];
            //        }
            //        Console.WriteLine($"Берем элемент {elB.Split(";")[0]} из B и элемент {elC.Split(";")[0]} из С, сортируем их");

            //        SelectSortTwo(elB, elC, ref i, ref j);
            //    }
            //}
        }
            static void SelectSortTwo(string elB, string elC, ref int i, ref int j)
            {
                if (IsDigit && TypeSortUp)
                {
                    if (Convert.ToInt32(elB.Split(";")[0]) < Convert.ToInt32(elC.Split(";")[0]))
                    {
                        WriteSort(elB);
                        i++;
                    }
                    else
                    {
                        WriteSort(elC);
                        j++;
                    }
                }
                else if (IsDigit && !TypeSortUp)
                {
                    if (Convert.ToInt32(elB.Split(";")[0]) > Convert.ToInt32(elC.Split(";")[0]))
                    {
                        WriteSort(elB);
                        i++;
                    }
                    else
                    {
                        WriteSort(elC);
                        j++;
                    }
                }
                else if (!IsDigit && TypeSortUp)
                {
                    if ((int)elB.Split(";")[0][0] < (int)elC.Split(";")[0][0])
                    {
                        WriteSort(elB);
                        i++;
                    }
                    else
                    {
                        WriteSort(elC);
                        j++;
                    }
                }
                else if (!IsDigit && !TypeSortUp)
                {
                    if ((int)elB.Split(";")[0][0] > (int)elC.Split(";")[0][0])
                    {
                        WriteSort(elB);
                        i++;
                    }
                    else
                    {
                        WriteSort(elC);
                        j++;
                    }
                }
            }

        static void SelectSortOne(string elB, string elC, ref int i, ref int j)
        {
            if (IsDigit && TypeSortUp)
            {
                if (Convert.ToInt32(elB.Split(";")[0]) < Convert.ToInt32(elC.Split(";")[0]))
                {
                    WriteSort(elB, elC);
                }
                else
                {
                    WriteSort(elC, elB);
                }
            }
            else if (IsDigit && !TypeSortUp)
            {
                if (Convert.ToInt32(elB.Split(";")[0]) > Convert.ToInt32(elC.Split(";")[0]))
                {
                    WriteSort(elB, elC);
                }
                else
                {
                    WriteSort(elC, elB);
                }
            }
            else if (!IsDigit && TypeSortUp)
            {
                if ((int)elB.Split(";")[0][0] < (int)elC.Split(";")[0][0])
                {
                    WriteSort(elB, elC);
                }
                else
                {
                    WriteSort(elC, elB);
                }
            }
            else if (!IsDigit && !TypeSortUp)
            {
                if ((int)elB.Split(";")[0][0] > (int)elC.Split(";")[0][0])
                {
                    WriteSort(elB, elC);
                }
                else
                {
                    WriteSort(elC, elB);
                }
            }
            i++;
            j++;
        }
        static void WriteSort(string FirstEl, string SecondEl)
            {
                using (StreamWriter sw = new StreamWriter("A.txt", true, System.Text.Encoding.Default))
                {
                    Console.WriteLine($"Записываем {FirstEl.Split(";")[0]} и {SecondEl.Split(";")[0]}  в A");
                    sw.WriteLine($"{FirstEl}");
                    sw.WriteLine($"{SecondEl}");
                }
            }
        static void WriteSort(string FirstEl)
        {
            using (StreamWriter sw = new StreamWriter("A.txt", true, System.Text.Encoding.Default))
            {
                Console.WriteLine($"Записываем {FirstEl.Split(";")[0]}  в A");
                sw.WriteLine($"{FirstEl}");
            }
        }
    }
    
}
