using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestCloudPoint
{
    public class Auto
    {//
        public Rectangle Figure;
        public Auto(int myWidth, int myHeight)
        {
            Figure = new Rectangle() { Width = myWidth, Height = myHeight, Fill = new SolidColorBrush(Colors.Aquamarine), RadiusY = 15, RadiusX = 15 };
        }
        public Rectangle AutoFigure()
        {
            return Figure;
        }
         
    }
}
