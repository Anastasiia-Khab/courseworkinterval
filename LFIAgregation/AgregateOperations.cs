using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace LFIAgregation
{
    class MyFunction
    {
        public double K;
        public double M;
        private LFI.ElementaryFunction ElemFunction;
        private LFI.DerivativeElementaryFunction DElemFunction;
        public MyFunction(double k, double m, LFI.ElementaryFunction elemFunction, LFI.DerivativeElementaryFunction dElemFunction)
        {
            K = k;
            M = m;
            ElemFunction = elemFunction;
            DElemFunction = dElemFunction;
        }
        public double Value(double x)
        {
            return ElemFunction(K * x + M);
        }
        public double DValue(double x)
        {
            return DElemFunction(K * x + M) * K;
        }
    }
    class AgregateOperation
    {
        //public delegate double ElementaryFunction(double x);
        private LFI.ElementaryFunction ElemFunction1;
        private LFI.ElementaryFunction ElemFunction2;
        private LFI.DerivativeElementaryFunction DElemFunction2;
        public LFI.ElementaryFunction Function;
        //---------------------------------------------------------------------------------------------------------//
        public List<double> X = new List<double>();
        public List<double> XRUp = new List<double>();
        public List<double> XRDown = new List<double>();
        public List<double> KUp = new List<double>();
        public List<double> KDown = new List<double>();
        public List<double> MUp = new List<double>();
        public List<double> MDown = new List<double>();
        public List<double> FDown = new List<double>();
        public List<double> FUp = new List<double>();
        //---------------------------------------------------------------------------------------------------------//
        private List<double> X1 = new List<double>();
        private List<double> KUp1 = new List<double>();
        private List<double> KDown1 = new List<double>();
        private List<double> MUp1 = new List<double>();
        private List<double> MDown1 = new List<double>();
        //---------------------------------------------------------------------------------------------------------//
        private List<double> X2 = new List<double>();
        private List<double> X1_2 = new List<double>();
        //---------------------------------------------------------------------------------------------------------//
        private List<double> KUp11 = new List<double>();
        private List<double> KDown11 = new List<double>();
        private List<double> MUp11 = new List<double>();
        private List<double> MDown11 = new List<double>();
        //---------------------------------------------------------------------------------------------------------//
        List<MyFunction> ListFunctionsUp = new List<MyFunction>();
        List<MyFunction> ListFunctionsDown = new List<MyFunction>();
        private List<LFI> LFIUp = new List<LFI>();
        private List<LFI> LFIDown = new List<LFI>();
        //---------------------------------------------------------------------------------------------------------//
        public AgregateOperation(List<double> X11, List<double> KUp11, List<double> KDown11, List<double> MUp11, List<double> MDown11,
            List<double> X22, LFI.ElementaryFunction Func1, LFI.ElementaryFunction Func2, LFI.DerivativeElementaryFunction DFunc2)
        {
            X1 = X11;
            KUp1 = KUp11;
            KDown1 = KDown11;
            MUp1 = MUp11;
            MDown1 = MDown11;
            X2 = X22;
            ElemFunction1 = Func1;
            ElemFunction2 = Func2;
            DElemFunction2 = DFunc2;
        }
        //---------------------------------------------------------------------------------------------------------//
        private void ListUnion()
        {
            X1_2.Clear();
            for (int i = 0; i < X2.Count - 1; i++)
            {
                for (int j = 0; j < X1.Count - 1; j++)
                {
                    if ((KUp1[j] * X1[j] + MUp1[j] < X2[i]) && (KUp1[j] * X1[j + 1] + MUp1[j] > X2[i]))
                    {
                        X1_2.Add((X2[i] - MUp1[j]) / KUp1[j]);
                        //MessageBox.Show("yes");
                    }
                    if ((KDown1[j] * X1[j] + MDown1[j] < X2[i]) && (KDown1[j] * X1[j + 1] + MDown1[j] > X2[i]))
                    {
                        X1_2.Add((X2[i] - MDown1[j]) / KDown1[j]);
                        //MessageBox.Show("yes");
                    }
                    if ((KUp1[j] * X1[j] + MUp1[j] > X2[i]) && (KUp1[j] * X1[j + 1] + MUp1[j] < X2[i]))
                    {
                        X1_2.Add((X2[i] - MUp1[j]) / KUp1[j]);
                        //MessageBox.Show("yes");
                    }
                    if ((KDown1[j] * X1[j] + MDown1[j] > X2[i]) && (KDown1[j] * X1[j + 1] + MDown1[j] < X2[i]))
                    {
                        X1_2.Add((X2[i] - MDown1[j]) / KDown1[j]);
                        // MessageBox.Show("yes");
                    }
                }

            }
            X = X1.Union(X1_2).ToList();
            X.Sort();
        }
        private void CreateFunctions()
        {
            ListFunctionsUp.Clear();
            ListFunctionsDown.Clear();
            for (int i = 0; i < X.Count - 1; i++)
            {
                ListFunctionsUp.Add(new MyFunction(KUp11[i], MUp11[i], ElemFunction2, DElemFunction2));
                ListFunctionsDown.Add(new MyFunction(KDown11[i], MDown11[i], ElemFunction2, DElemFunction2));
            }

        }
        //---------------------------------------------------------------------------------------------------------//
        public double AgFunction(double x)
        {
            return ElemFunction2(ElemFunction1(x));
        }
        //---------------------------------------------------------------------------------------------------------//
        private void TransformationOfLists()
        {
            for (int i = 0; i < X.Count - 1; i++)
            {
                for (int j1 = 0; j1 < X1.Count - 1; j1++)
                {
                    if (X[i] >= X1[j1] && X[i] < X1[j1 + 1])
                    {
                        KUp11.Add(KUp1[j1]);
                        KDown11.Add(KDown1[j1]);
                        MUp11.Add(MUp1[j1]);
                        MDown11.Add(MDown1[j1]);
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------//
        private double LineFunction(double x, double K, double M)
        {
            return K * x + M;
        }
        private void CreatedFLists()
        {
            FUp.Clear();
            FDown.Clear();
            for (int i = 0; i < XRUp.Count - 1; i++)
            {
                FUp.Add(LineFunction(XRUp[i], KUp[i], MUp[i]));
            }
            FUp.Add(LineFunction(XRUp[XRUp.Count - 1], KUp[KUp.Count - 1], MUp[MUp.Count - 1]));
            for (int i = 0; i < XRDown.Count - 1; i++)
            {
                FDown.Add(LineFunction(XRDown[i], KDown[i], MDown[i]));
            }
            FDown.Add(LineFunction(XRDown[XRDown.Count - 1], KDown[KDown.Count - 1], MDown[MDown.Count - 1]));
        }
        //---------------------------------------------------------------------------------------------------------//
        public void Agregate()
        {
            MDown.Clear();
            KUp.Clear();
            KDown.Clear();
            MUp.Clear();
            XRUp.Clear();
            XRDown.Clear();
            ListUnion();
            TransformationOfLists();
            CreateFunctions();
            for (int i = 0; i < X.Count - 1; i++)
            {
                if (DElemFunction2(ElemFunction1(X[i])) >= 0)
                {
                    LFIUp.Add(new LFI(2, X[i], X[i + 1], new List<double>() { }, ListFunctionsUp[i].Value, ListFunctionsUp[i].DValue));
                    LFIDown.Add(new LFI(2, X[i], X[i + 1], new List<double>() { }, ListFunctionsDown[i].Value, ListFunctionsDown[i].DValue));
                    LFIUp[i].BuildLFI();
                    LFIDown[i].BuildLFI();
                    for (int j = 0; j < LFIUp[i].X.Count - 1; j++)
                    {
                        XRUp.Add(LFIUp[i].X[j]);
                        KUp.Add(LFIUp[i].KUp[j]);
                        MUp.Add(LFIUp[i].MUp[j]);
                    }
                    for (int j = 0; j < LFIDown[i].X.Count - 1; j++)
                    {
                        XRDown.Add(LFIDown[i].X[j]);
                        KDown.Add(LFIDown[i].KDown[j]);
                        MDown.Add(LFIDown[i].MDown[j]);
                    }
                }
                else
                {
                    LFIUp.Add(new LFI(2, X[i], X[i + 1], new List<double>() { }, ListFunctionsDown[i].Value, ListFunctionsDown[i].DValue));
                    LFIDown.Add(new LFI(2, X[i], X[i + 1], new List<double>() { }, ListFunctionsUp[i].Value, ListFunctionsUp[i].DValue));
                    LFIUp[i].BuildLFI();
                    LFIDown[i].BuildLFI();
                    for (int j = 0; j < LFIUp[i].X.Count - 1; j++)
                    {
                        XRUp.Add(LFIUp[i].X[j]);
                        KUp.Add(LFIUp[i].KUp[j]);
                        MUp.Add(LFIUp[i].MUp[j]);
                    }
                    for (int j = 0; j < LFIDown[i].X.Count - 1; j++)
                    {
                        XRDown.Add(LFIDown[i].X[j]);
                        KDown.Add(LFIDown[i].KDown[j]);
                        MDown.Add(LFIDown[i].MDown[j]);
                    }
                }

            }

            XRUp.Add(X.Last());
            XRDown.Add(X.Last());
            //MessageBox.Show(X.Last() + "x   \nxr" + XRUp.Last() + "   \nxrd" + XRDown.Last());
            CreatedFLists();
        }
        public void PaintOperation(Chart chart)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Series[2].Points.Clear();
            for (int i = 0; i < XRUp.Count - 1; i++)
            {
                for (double j = XRUp[i]; j <= XRUp[i + 1]; j += 0.05)
                {
                    chart.Series[1].Points.AddXY(j, LineFunction(j, KUp[i], MUp[i]));
                }
            }
            for (double j = XRUp.Last(); j <= X.Last(); j += 0.05)
            {
                chart.Series[1].Points.AddXY(j, LineFunction(j, KUp.Last(), MUp.Last()));
            }
            for (int i = 0; i < XRDown.Count - 1; i++)
            {
                for (double j = XRDown[i]; j <= XRDown[i + 1]; j += 0.05)
                {
                    chart.Series[2].Points.AddXY(j, LineFunction(j, KDown[i], MDown[i]));
                }
            }
            for (double j = XRDown.Last(); j <= X.Last(); j += 0.05)
            {
                chart.Series[2].Points.AddXY(j, LineFunction(j, KDown.Last(), MDown.Last()));
            }
            for (double i = X[0]; i < X[X.Count - 1]; i += 0.05)
            {
                chart.Series[0].Points.AddXY(i, ElemFunction2(ElemFunction1(i)));
            }
        }

    }
}
