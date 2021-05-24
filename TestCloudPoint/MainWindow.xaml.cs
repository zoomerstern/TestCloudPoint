using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TestCloudPoint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Route myRoute = new Route();//Наш путь
        private List<Barrier> BarrierList = new List<Barrier>();//Список объектов-ограничений
        public MainWindow()
        {
            InitializeComponent();

            BarrierList.Add(new Barrier(Cloud.LoadCloud(@".\text.txt").ToArray()));
            ShowPoint(BarrierList[0].mass, new SolidColorBrush(Colors.Blue));

            //BarrierList.Add(new Barrier(Cloud.LoadCloud(@".\b0.txt").ToArray()));
            //ShowPoint(BarrierList[0].mass, new SolidColorBrush(Colors.Blue));
            //BarrierList.Add(new Barrier(Cloud.LoadCloud(@".\b1.txt").ToArray()));
            //ShowPoint(BarrierList[0].mass, new SolidColorBrush(Colors.Blue));
            //BarrierList.Add(new Barrier(Cloud.LoadCloud(@".\b2.txt").ToArray()));
            //ShowPoint(BarrierList[0].mass, new SolidColorBrush(Colors.Blue));
            //BarrierList.Add(new Barrier(Cloud.LoadCloud(@".\b3.txt").ToArray()));
            //ShowPoint(BarrierList[0].mass, new SolidColorBrush(Colors.Blue));
            //BarrierList.Add(new Barrier(Cloud.LoadCloud(@".\b4.txt").ToArray()));
            //ShowPoint(BarrierList[0].mass, new SolidColorBrush(Colors.Blue));

            LoadAuto();

            //myRoute=new Route(Cloud.LoadPoint(@".\route.txt").ToArray());
            //ShowPoint(myRoute.MassReturn());
            //PutBarrier();
            //SaveBarrier();

        }
        //==Для отметки пути мышкой==
        private Point prev;//Предыдущая точка
        private SolidColorBrush color = new SolidColorBrush(Colors.Black);//Цвет
        private bool isPaint = false;
        private const int SIZE = 5;//Рамер точки
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {//соытие при нажатии мышки
            if (isPaint) return;
            isPaint = true;
            prev = Mouse.GetPosition(MyCanvas);
            var dot = new Ellipse { Width = SIZE, Height = SIZE, Fill = color };
            Canvas.SetLeft(dot, (int)prev.X);
            Canvas.SetTop(dot, (int)prev.Y);
            MyCanvas.Children.Add(dot);
            myRoute.AddPoint(new myPoint((int)prev.X, (int)prev.Y));//Добавляем току в массив
        }
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {//событие при отжатии 
            isPaint = false;
        }
        private void OnMouseMove(object sender, MouseEventArgs e)
        {//событи при перемещении ажатой мышки
            if (!isPaint) return;
            var point = Mouse.GetPosition(MyCanvas);
            var line = new Line
            {
                Stroke = color,
                StrokeThickness = SIZE,
                X1 = (int)prev.X,
                Y1 = (int)prev.Y,
                X2 = (int)point.X,
                Y2 = (int)point.Y,
                StrokeStartLineCap = PenLineCap.Round, //Начало оркужности закругляем
                StrokeEndLineCap = PenLineCap.Round  //Конец окружности закругляем
            };
            myRoute.AddPoint(new myPoint((int)prev.X, (int)prev.Y));//Добавляем току в массив
            prev = point;
            MyCanvas.Children.Add(line);
        }
        //======================
        public void LoadAuto()
        {//Вывод авто на экран
            Auto myAuto = new Auto(60, 100);
            ShowElement(myAuto.Figure, new myPoint(40, 550));
        }
        private void ShowPoint(myPoint [] mass, SolidColorBrush color)
        {//вывод точек на экран
            for (int i = 0; i < mass.Length; i++)
                ShowElement(Cloud.InputPoint(color),mass[i]);
        }
        private void ShowElement(Shape el, myPoint XY)
        {//Вывод точи
            Canvas.SetLeft(el, XY.x);
            Canvas.SetTop(el, XY.y);
            MyCanvas.Children.Add(el);
        }
        public void SaveRoute(object sender, RoutedEventArgs e)
        {//записать очерченый путь
            Cloud.WriteCloud(@".\road.txt", myRoute.MassReturn());
        }
        //======для создания своих баръеров==========================
        public void PutBarrier()
        {//Очертим путь из прямоугольников 
        //    //Крание точки-границы прмяоуголной обасти
        //    //границы дорги 1
            BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(5, 500), new myPoint(905, 502))));
            BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(5, 798), new myPoint(1175, 800))));

        //    ////===препятствия на дороге 1
            BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(135, 500), new myPoint(200, 650))));
        //    BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(255, 735), new myPoint(370, 800))));
        //    BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(425, 775), new myPoint(455, 800))));
        //    BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(625, 740), new myPoint(625, 800))));
        //    BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(425, 500), new myPoint(805, 650))));

        //    ////====препятствия на дороге 2
        //    BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(905, 355), new myPoint(1040, 425))));
        //    BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(1005, 655), new myPoint(1100, 800))));

        //    ////границы дорги 2
            BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(905, 100), new myPoint(907, 500))));
            BarrierList.Add(new Barrier(Cloud.InputCloud(new myPoint(1175, 100), new myPoint(1177, 800))));
        }
        private void SaveBarrier()
        {
            for (int i = 0; i < BarrierList.Count; i++)
                Cloud.WriteCloud(@".\barier" + i + ".txt", BarrierList[i].mass);
        }
    }
}
