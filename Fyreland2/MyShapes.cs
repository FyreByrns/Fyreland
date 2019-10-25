using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fyreland2 {
    public class Triangle : Shape {
        protected override Geometry DefiningGeometry => GetGeometry();

        public Triangle() {
            Stretch = Stretch.Fill;
        }

        Geometry GetGeometry() => Geometry.Parse("M 0 1 l 1 0 v -2 Z");
    }

    public class PoleVert : Shape {
        protected override Geometry DefiningGeometry => GetGeometry();

        public PoleVert() { Stretch = Stretch.Uniform; }

        Geometry GetGeometry() => Geometry.Parse("M .3 0 v -1 h .3 v 1 Z");
    }

    public class PoleHoriz : Shape {
        protected override Geometry DefiningGeometry => GetGeometry();

        public PoleHoriz() { Stretch = Stretch.Uniform; }

        Geometry GetGeometry() => Geometry.Parse("M 0 .3 v -.3 h 1 v .3 Z");
    }

    public class PoleVertSlope : Shape {
        protected override Geometry DefiningGeometry => GetGeometry();

        public PoleVertSlope() { Stretch = Stretch.Fill; }

        Geometry GetGeometry() {
            Geometry triangle = Geometry.Parse("M 0 1 l 1 0 v -2 Z");
            Geometry rectangle = Geometry.Parse("M .3 0 v -1 h .3 v 1 Z");

            return Geometry.Combine(triangle, rectangle, GeometryCombineMode.Union, null);
        }
    }

    public class PoleHorizSlope : Shape {
        protected override Geometry DefiningGeometry => GetGeometry();

        public PoleHorizSlope() { Stretch = Stretch.Fill; }

        Geometry GetGeometry() {
            Geometry triangle = Geometry.Parse("M 0 1 l 1 0 v -2 Z");
            Geometry rectangle = Geometry.Parse("M 0 .3 v -.6 h 1 v .3 Z");

            return Geometry.Combine(triangle, rectangle, GeometryCombineMode.Union, null);
        }
    }

    public class Soft : Shape {
        protected override Geometry DefiningGeometry => GetGeometry();

        public Soft() { Stretch = Stretch.Uniform; }

        Geometry GetGeometry() => Geometry.Parse("M 0 0 v -0.5 h 1 v .5 Z");
    }
}
