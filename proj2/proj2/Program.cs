using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace proj2
{
    class Program
    {
        static void Main(string[] args)
        {
            Subject Sub = new Subject();
            Average Avg = new Average();
            Percentage Per = new Percentage();
            Companies Com = new Companies();

            Sub.Parse();
            Sub.AddOB(Avg);
            Sub.AddOB(Per);
            Sub.AddOB(Com);
            Sub.Update();
            Console.ReadKey();
        }
    }
/// <summary>
/// SUB CLASS
/// </summary>
    public abstract class SUBJECT
    {
        public abstract void AddOB(Observer o);
        public abstract void Update();
        public abstract void List();
    }

    public class Subject : SUBJECT
    {
        const int R = 900;
        const int C = 9;
        int Count = 0;
        string Line;
        List<Observer> OL = new List<Observer>();
        string[] Ticker = new string[R];
        string[,] Parsed = new string[R, C];
        char[] Nums = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        Regex Reg = new Regex(@"[a-zA-Z]*[\s]+$");
        int Index = 0;

        public Subject()
        {
            StreamReader file = new StreamReader(@"Ticker.dat");
            while ((Line = file.ReadLine()) != null)
            {
                Ticker[Count] = Line;
                Count++;
            }
            file.Close();
        }
/// <summary>
/// Parse 
/// </summary>
        public void Parse()
        {
            for (int i = 0; i < Ticker.Length; i++)
            {
                string tp = Ticker[i];
                Index = 0;

                if (tp == null || tp == "")
                {
                    continue;
                }
                else if (tp.Contains("Last updated"))
                {
                    Parsed[i, 0] = tp;
                }
                else
                {
                    string[] Temp = tp.Split(Nums, 2);
                    Parsed[i, 0] = Temp[0];
                    string[] numbers = Regex.Split(tp, @"^[a-zA-Z][a-zA-Z\s.&',-]+");
                    string Temp1 = numbers[1];
                    string TickerSB = "";
                    TickerSB = Reg.Match(Temp[0]).ToString();
                    Parsed[i, 1] = TickerSB;
                    string[] Temp2 = Regex.Split(Temp1, @"[\s]+");
                    for (int j = 2; j < C; j++)
                    {
                        Temp2[Index].Trim();
                        Parsed[i, j] = Temp2[Index];
                        Index++;
                    }
                }
            } 
        }

        public override void AddOB(Observer o)
        {
            OL.Add(o);
        }
        public override void List()
        {
            for (int i = 0; i < OL.Count; i++)
            {
                Console.WriteLine(OL[i]);
            }
        }
        public override void Update()
        {
            for (int i = 0; i < OL.Count; i++)
            {
                OL[i].update(Parsed);
                Console.ReadKey();
            }
        }
    }

 /// <summary>
 /// AVERAGE OB CLASS 
 /// </summary>
    public abstract class Observer
    {
        public abstract void update(string[,] Pdata);
    }

    public class Average : Observer
    {
        const int R = 900;
        const int C = 9;
        string[,] data = new string[R, C];

        public override void update(string[,] Pdata)
        {
            data = Pdata;
            average();
        }
        public void average()
        {
            double avg = 0;
            string date = "";
            int Count = 0;
            string[] Line = new string[100];
            int Index = 0;
            for (int i = 0; i < R; i++)
            {
                if (data[i, 0] == null)
                {
                    continue;

                }
                else if (data[i, 0].Contains("Last updated"))
                {
                    if (avg != 0)
                    {
                        avg = avg / Count;
                        Console.WriteLine("{0}, Average Price : {1}", date, avg);
                        Line[Index] = date + " Average Price : " + avg;
                        Index++;
                    }
                    date = data[i, 0].Remove(0, 13);
                    avg = 0;
                    Count = 0;
                }
                else
                {
                    avg += Convert.ToDouble(data[i, 2]);
                    Count++;
                }

            }
            avg = avg / Count;
            Console.WriteLine("{0}, Average Price : {1}", date, avg);
            Line[Index] = date + " Average price : " + avg;
            File.WriteAllLines(@"average.dat", Line);
        }
    }

/// <summary>
/// PERCENTAGE OB CLASS
/// </summary>
    public class Percentage : Observer
    {
        const int R = 900;
        string Date = "";
        public override void update(string[,] Pdata)
        {
            PerChange(Pdata);
        }
        public void PerChange(string[,] Pdata)
        {
            string[] Line = new string[100];
            int Index = 0;

            for (int i = 0; i < R; i++)
            {
                if (Pdata[i, 0] == null)
                {
                    continue;
                }
                else if (Pdata[i, 0].Contains("Last updated"))
                {
                    Date = Pdata[i, 0].Remove(0, 13);
                    Console.WriteLine();
                    Console.WriteLine("{0}", Date);
                    Line[Index] = Date;
                }
                else if (Math.Abs(Convert.ToDouble(Pdata[i, 4])) > 10)
                {
                    string tick = Pdata[i, 1];
                    string price = Pdata[i, 2];
                    string change = Pdata[i, 4];

                    Console.WriteLine("{0}, {1}, {2}", tick, price, change);
                    Line[Index] = tick + " " + price + " " + change;
                    Index++;
                }
            }
            File.WriteAllLines(@"change10.dat", Line);
        }
    }

/// <summary>
/// COMPANIESE OB CLASS
/// </summary>
    public class Companies : Observer
    {
        public override void update(string[,] Pdata)
        {
            companies(Pdata);
        }
        public void companies(string[,] Pdata)
        {
            string[] companies = { "ALL ", "BA ", "BC ", "GBEL ", "KRT ", "MCD ", "TR ", "WAG " };
            const int r = 800;
            string[] Line = new string[100];
            int Index = 0;

            for (int i = 0; i < r; i++)
            {
                if (Pdata[i, 0] == null)
                {
                    continue;
                }
                else if (Pdata[i, 0].Contains("Last Updated"))
                {
                    string time = Pdata[i, 0];
                    Console.WriteLine(time);
                    Line[Index] = time;
                }
                else
                {
                    for (int j = 0; j < companies.Length; j++)
                    {
                        if (companies[j] == Pdata[i, 1])
                        {
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}", Pdata[i, 0], Pdata[i, 2], Pdata[i, 3], Pdata[i, 4], Pdata[i, 5], Pdata[i, 6], Pdata[i, 7], Pdata[i, 8]);
                            Line[Index] = Pdata[i, 0] + " " + Pdata[i, 2] + " " + Pdata[i, 3] + " " + Pdata[i, 4] + " " + Pdata[i, 5] + " " + Pdata[i, 6] + " " + Pdata[i, 7] + " " + Pdata[i, 8];
                            Index++;
                        }
                    }
                }
            }
            File.WriteAllLines(@"selections.dat", Line);
        }
    }
}
