using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace LFIAgregation
{
    class LFI
    {
        public List<double> ListOfExtremePoints = new List<double>();
        //{ 0, Math.Round(Math.PI / 2, 6), Math.Round(Math.PI, 6), Math.Round(3 * Math.PI / 2, 6), Math.Round(2 * Math.PI, 6) };
        //{ -0.5};
        public int N { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        //---------------------------------------------------------------------------------------------------------------------------//
        public List<double> X = new List<double>();
        public List<double> KUp = new List<double>();
        public List<double> KDown = new List<double>();
        public List<double> MUp = new List<double>();
        public List<double> MDown = new List<double>();
        public List<double> FDown = new List<double>();
        public List<double> FUp = new List<double>();
        //-----------------------------------------------------------------------------------------------------------------------------//
        public delegate double ElementaryFunction(double x);
        public delegate bool ElementaryCheck(double x);
        public delegate double DerivativeElementaryFunction(double x);
        public ElementaryFunction ElemFunction;
        public ElementaryCheck ElemCheck;
        public DerivativeElementaryFunction DElemFunction;
        //-------------------------------------------------------------------------------------------------------------------------------//
        public LFI(int N1, double A1, double B1, List<double> ListOfExtremePoints1, ElementaryFunction ElemFunction1, DerivativeElementaryFunction DElemFunction1)
        {
            //if (ElemCheck1(A) && ElemCheck1(B))
            {
                N = N1;
                A = A1;
                B = B1;
                ListOfExtremePoints = ListOfExtremePoints1;
                ElemFunction = ElemFunction1;
                DElemFunction = DElemFunction1;
                //ElemCheck = ElemCheck1;
            }
            //else
            {
                //    MessageBox.Show("Межі , які ви вказали є несумісні з потрібною функцією. Будь ласка вкажіть правильні межі " + ElemCheck1.ToString());
            }

        }
        //-------------------------------------------------------------------------------------------------------------------------------//
        private double TangentLine(double x, double x0)
        {
            return ElemFunction(x0) + DElemFunction(x0) * (x - x0);
        }
        private double LineThroughTwoPoints(double x, Interval A)
        {
            return ((x - A.Begin) * (ElemFunction(A.End) - ElemFunction(A.Begin))) / (A.End - A.Begin) + ElemFunction(A.Begin);
        }
        private double CrossLinePoint(double PreviousPoint, double NextPoint)
        {
            return (ElemFunction(NextPoint) - DElemFunction(NextPoint) * NextPoint - ElemFunction(PreviousPoint) + DElemFunction(PreviousPoint) * PreviousPoint) / (DElemFunction(PreviousPoint) - DElemFunction(NextPoint));
        }
        private double LineFunction(double x, double K, double M)
        {
            return K * x + M;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------//
        public void PaintLFI(Chart chart)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Series[2].Points.Clear();
            for (int i = 0; i < X.Count - 1; i++)
            {
                for (double k = X[i]; k <= X[i + 1]; k += 0.05)
                {
                    chart.Series[0].Points.AddXY(k, ElemFunction(k));
                    chart.Series[1].Points.AddXY(k, LineFunction(k, KUp[i], MUp[i]));
                    chart.Series[2].Points.AddXY(k, LineFunction(k, KDown[i], MDown[i]));
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------//
        private List<Interval> CreateList(List<double> ListOfPoint)
        {
            List<Interval> C = new List<Interval>();
            double X = A;
            foreach (var P in ListOfPoint)
            {
                if (X < P && P < B)
                {
                    C.Add(new Interval(X, P));
                    X = P;
                }
            }
            C.Add(new Interval(X, B));
            return C;
        }
        //--------------------------------------------------------------------------------------------------------------------------//
        private double Step(List<Interval> List)
        {
            double min = List[0].Width();
            for (int i = 1; i < List.Count; i++)
            {
                if (min > List[i].Width())
                    min = List[i].Width();
            }
            return min / N;
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        private double CreateKUp(double x0)
        {
            return DElemFunction(x0);
        }
        private double CreateMUp(double x0)
        {
            return ElemFunction(x0) - DElemFunction(x0) * x0;
        }
        //----------------------------------------------------------------------------------------------------------------------------//
        private double CreateKDown(Interval A)
        {
            return (ElemFunction(A.End) - ElemFunction(A.Begin)) / (A.End - A.Begin);
        }
        private double CreateMDown(Interval A)
        {
            return (((ElemFunction(A.End) - ElemFunction(A.Begin)) / (A.End - A.Begin)) * (-A.Begin)) + ElemFunction(A.Begin);
        }
        //----------------------------------------------------------------------------------------------------------------------------//
        public void BuildLFI()
        {
            List<double> TangentPoints = new List<double>();
            List<Interval> ListOfIntervals = new List<Interval>();
            List<Interval> ListOfIntervalsIncreaseDecrease = CreateList(ListOfExtremePoints);
            //double h = (X.End - X.Begin) / N;
            double step = Step(ListOfIntervalsIncreaseDecrease);
            int Count;
            double h;
            foreach (var I in ListOfIntervalsIncreaseDecrease)
            {
                Count = (int)((I.End - I.Begin) / step) + 1;
                h = (I.End - I.Begin) / Count;
                TangentPoints.Clear();
                ListOfIntervals.Clear();
                double s = I.Begin;
                for (int i = 0; i <= Count; i++)
                {
                    TangentPoints.Add(s);
                    s += h;
                }
                s = I.Begin;
                double s1 = 0;
                for (int i = 0; i < TangentPoints.Count - 1; i++)
                {
                    s1 = CrossLinePoint(TangentPoints[i], TangentPoints[i + 1]);
                    ListOfIntervals.Add(new Interval(s, s1));
                    s = s1;
                }
                ListOfIntervals.Add(new Interval(s1, I.End));
                for (int i = 0; i < ListOfIntervals.Count; i++)
                {
                    X.Add(ListOfIntervals[i].Begin);
                    if (TangentLine((ListOfIntervals[i].End + ListOfIntervals[i].Begin) / 2, TangentPoints[i]) >
                        LineThroughTwoPoints((ListOfIntervals[i].End + ListOfIntervals[i].Begin) / 2, ListOfIntervals[i]))
                    {
                        KUp.Add(CreateKUp(TangentPoints[i]));
                        MUp.Add(CreateMUp(TangentPoints[i]));
                        KDown.Add(CreateKDown(ListOfIntervals[i]));
                        MDown.Add(CreateMDown(ListOfIntervals[i]));
                    }
                    else
                    {
                        KUp.Add(CreateKDown(ListOfIntervals[i]));
                        MUp.Add(CreateMDown(ListOfIntervals[i]));
                        KDown.Add(CreateKUp(TangentPoints[i]));
                        MDown.Add(CreateMUp(TangentPoints[i]));
                    }
                }
                //PaintGraph(chart, 0, I);
                //for (int i = 0; i < ListOfIntervals.Count; i++)
                //{
                //    PaintBoundary(chart, 1, TangentPoints[i], ListOfIntervals[i]);
                //    PaintBoundary(chart, 2, ListOfIntervals[i]);
                //}
            }
            X.Add(ListOfIntervals[ListOfIntervals.Count - 1].End);
            for (int i = 0; i < X.Count - 1; i++)
            {
                FDown.Add(LineFunction(X[i], KDown[i], MDown[i]));
                FUp.Add(LineFunction(X[i], KUp[i], MUp[i]));
            }
            FDown.Add(LineFunction(X[X.Count - 1], KDown[KDown.Count - 1], MDown[MDown.Count - 1]));
            FUp.Add(LineFunction(X[X.Count - 1], KUp[KUp.Count - 1], MUp[MUp.Count - 1]));
        }
        //------------------------------------------------------------------------------------------------------------------------//
        public void ListsToDataGridView(DataGridView dt)
        {
            //dt.Rows.Clear();
            for (int i = 0; i < KUp.Count; i++)
            {
                dt.Rows.Add(KUp[i].ToString(), MUp[i].ToString(), KDown[i].ToString(), MDown[i].ToString());
            }
        }
        public void ListToDataGridView(DataGridView dt)
        {
            //dt.Rows.Clear();
            for (int i = 0; i < X.Count; i++)
            {
                dt.Rows.Add(X[i].ToString());
            }
        }
        //public List<double> OutputList(string str)
        //{
        //    switch(str)
        //    {
        //        case "X":
        //            return X;
        //        case "KUp":
        //            return KUp;
        //        case "KDown":
        //            return KDown;
        //        case "MUp":
        //            return MUp;
        //        case "MDown":
        //            return MDown;
        //        default:
        //            return null;
        //    }
        //}
        //------------------------------------------------------------------------------------------------------------------------------//
    }
}
