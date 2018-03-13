using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFIAgregation
{
    class Interval
    {
        protected double a;
        protected double b;

        public Interval()
            : this(0, 0)
        {
        }
        public Interval(double a1, double b1)
        {
            a = a1;
            b = b1;
        }

        public double Begin
        {
            get { return a; }
            set { a = value; }
        }
        public double End
        {
            get { return b; }
            set { b = value; }
        }
        public override bool Equals(Object obj)
        {
            Interval c = obj as Interval;
            if (a == c.Begin && b == c.End)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public override int GetHashCode()
        {
            return (int)(a + b);
        }
        public static Interval operator +(Interval A1, Interval A2)
        {
            return new Interval(A1.a + A2.a, A1.b + A2.b);
        }
        public static Interval operator +(double x, Interval A1)
        {
            return new Interval(x + A1.a, x + A1.b);
        }
        public static Interval operator +(Interval A1, double x)
        {
            return new Interval(A1.a + x, A1.b + x);
        }
        public static Interval operator -(Interval A1, Interval A2)
        {
            return new Interval(A1.a - A2.b, A1.b - A2.a);
        }
        public static Interval operator -(Interval A1, double c)
        {
            if (A1.a - c < A1.b - c)
            {
                return new Interval(A1.a - c, A1.b - c);
            }
            else
            {
                return new Interval(A1.b - c, A1.a - c);
            }
        }
        public static Interval operator -(double c, Interval A1)
        {
            if (c - A1.a < c - A1.b)
            {
                return new Interval(c - A1.a, c - A1.b);
            }
            else
            {
                return new Interval(c - A1.b, c - A1.a);
            }
        }
        public static Interval operator *(Interval A1, Interval A2)
        {
            double min, max;
            if (Math.Min(A1.a * A2.a, A1.a * A2.b) < Math.Min(A1.b * A2.a, A1.b * A2.b))
            {
                min = Math.Min(A1.a * A2.a, A1.a * A2.b);
            }
            else
            {
                min = Math.Min(A1.b * A2.a, A1.b * A2.b);
            }
            if (Math.Max(A1.a * A2.a, A1.a * A2.b) > Math.Max(A1.b * A2.a, A1.b * A2.b))
            {
                max = Math.Max(A1.a * A2.a, A1.a * A2.b);
            }
            else
            {
                max = Math.Max(A1.b * A2.a, A1.b * A2.b);
            }
            return new Interval(min, max);
        }
        public static Interval operator *(double c, Interval A1)
        {
            if (A1.a * c < A1.b * c)
            {
                return new Interval(A1.a * c, A1.b * c);
            }
            else
            {
                return new Interval(A1.b * c, A1.a * c);
            }
        }
        public static Interval operator *(Interval A1, double c)
        {
            if (A1.a * c < A1.b * c)
            {
                return new Interval(A1.a * c, A1.b * c);
            }
            else
            {
                return new Interval(A1.b * c, A1.a * c);
            }
        }
        public static Interval operator /(Interval A1, Interval A2)
        {
            double min, max;
            if (Math.Min(A1.a / A2.a, A1.a / A2.b) < Math.Min(A1.b / A2.a, A1.b / A2.b))
            {
                min = Math.Min(A1.a / A2.a, A1.a / A2.b);
            }
            else
            {
                min = Math.Min(A1.b / A2.a, A1.b / A2.b);
            }
            if (Math.Max(A1.a / A2.a, A1.a / A2.b) > Math.Max(A1.b / A2.a, A1.b / A2.b))
            {
                max = Math.Max(A1.a / A2.a, A1.a / A2.b);
            }
            else
            {
                max = Math.Max(A1.b / A2.a, A1.b / A2.b);
            }
            return new Interval(min, max);
        }
        public static Interval operator /(Interval A1, double x)
        {
            return new Interval(A1.a / x, A1.b / x);
        }
        public static Interval operator /(double x, Interval A1)
        {
            if (x / A1.a < x / A1.b)
            {
                return new Interval(x / A1.a, x / A1.b);
            }
            else
            {
                return new Interval(x / A1.b, x / A1.a);
            }
        }

        public Interval IntersectionInterval(Interval A1, Interval A2, ref bool IsIntersection)
        {
            if ((A1.a <= A2.a) && (A2.b <= A1.b))
                return new Interval(A2.a, A2.b);
            if ((A2.a <= A1.a) && (A1.b <= A2.b))
                return new Interval(A1.a, A1.b);
            if ((A1.a <= A2.a) && (A2.a <= A1.b) && (A1.b <= A2.b))
                return new Interval(A2.a, A1.b);
            if ((A2.a <= A1.a) && (A1.a <= A2.b) && (A2.b <= A1.b))
                return new Interval(A1.a, A2.b);
            IsIntersection = false;

            return new Interval();
        }
        public bool Include(Interval A1)
        {
            if (a <= A1.Begin && A1.End <= b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Interval[] DivInf(double a)
        {
            Interval[] twoInterval = new Interval[2] { new Interval(), new Interval() };

            if (a > 0)
            {
                twoInterval[0].a = -100000000;
                twoInterval[0].b = a / this.a;
                twoInterval[1].a = a / this.b;
                twoInterval[1].b = 100000000;
            }
            else
            {
                twoInterval[0].a = -100000000;
                twoInterval[0].b = a / this.b;
                twoInterval[1].a = a / this.a;
                twoInterval[1].b = 100000000;
            }
            return twoInterval;
        }
        public bool IsZero()
        {
            if ((this.a <= 0) & (0 <= this.b)) return true;
            else return false;
        }
        public double Width()
        {
            return b - a;
        }
    }
}
