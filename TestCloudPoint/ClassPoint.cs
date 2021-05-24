using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TestCloudPoint
{
    public struct myPoint
    {
        public int x;
        public int y;
        public myPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public string GetPoint()
        {
            return "("+x+", "+y+") ";
        }
    }
    public static class Cloud
    {
        public static myPoint[] InputCloud(myPoint myXY, myPoint maxXY)
        {//Сформировать облако точек в виде прямоугольной области
            int Sp = 4; //площадь точки
            int volume = (maxXY.x - myXY.x) * (maxXY.y - myXY.y) / Sp;//Площадь области делим на площадь точки - получаем колличество точек
            int x, y;
            myPoint [] mass =new myPoint[volume];
            for (int i = 0; i < volume; i += 1)
            {
                x = RandValue(myXY.x, maxXY.x);
                y = RandValue(myXY.y, maxXY.y);
                mass[i] = new myPoint(x,y);
            }
            return mass;
        }
        public static Ellipse InputPoint(SolidColorBrush color)
        {//рисуем точку
            return new Ellipse() { Fill = color == null ? new SolidColorBrush(Colors.Blue) : color, Height = 2, Width = 2 };
        }
        //==    
        public static int RandValue(int min, int max)
        {//Случайное значение от до
            Random rand = new Random();
            return rand.Next(min, max);
        }
        //==Чтение/запись
        public static void WriteCloud(string path, myPoint[] mass)
        {//Запись облака точек
            for (int i = 0; i < mass.Length; i++)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                    {
                        sw.Write(mass[i].GetPoint());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static List<myPoint> LoadCloud(string path)
        {//Загрузка облака точек из файла
            List<myPoint> mass = new List<myPoint>();
            Regex regex = new Regex(@"\d+, \d+");
            MatchCollection matches;
            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        regex = new Regex(@"\d+, \d+");
                        matches = regex.Matches(line);
                        regex = new Regex(@"\d+");
                        foreach (string match in matches.OfType<Match>().Select(m => m.Value).ToArray())
                        {
                            string[] result = regex.Matches(match).Cast<Match>().Select(m => m.Value).ToArray();
                            int x = int.Parse(result[0]);
                            int y = int.Parse(result[1]);
                            mass.Add(new myPoint(x, y));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return mass;
        }
    }
   
}
