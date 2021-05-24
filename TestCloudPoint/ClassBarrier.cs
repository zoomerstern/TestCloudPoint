using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCloudPoint
{
    class Barrier
    {
        public myPoint[] mass { get; set; }
        public Barrier(myPoint[] mass)
        {
            this.mass = mass;
        }
    }
    class Route
    {
        List<myPoint> mass = new List<myPoint>();
        public Route()
        {

        }
        public Route(myPoint[] mass)
        {
            this.mass = mass.ToList();
        }
        public void AddPoint(myPoint point)
        {
            mass.Add(point);
        }
        public myPoint[] MassReturn() 
        {
            return mass.ToArray();
        }
    }
    
}
