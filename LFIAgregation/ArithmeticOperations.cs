using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace LFIAgregation
{

    class ArithmeticOperation
    {
        //public delegate double ElementaryFunction(double x);
        private LFI.ElementaryFunction ElemFunction1;
        private LFI.ElementaryFunction ElemFunction2;
        public LFI.ElementaryFunction Function;
        //---------------------------------------------------------------------------------------------------------//
        public List<double> X = new List<double>();
        public List<double> KUp = new List<double>();
        public List<double> KDown = new List<double>();
        public List<double> MUp = new List<double>();
        public List<double> MDown = new List<double>();
        public List<double> FDown = new List<double>();
        public List<double> FUp = new List<double>();
        //public List<List<int>> MinList = new List<List<int>>();
        //public List<List<int>> MaxList = new List<List<int>>();
        //public List<Interval> MaxIntervalList = new List<Interval>();
        //public List<Interval> MinIntervalList = new List<Interval>();
        //-------------------------------------------------------------------------------------------------------//
        private List<double> X1 = new List<double>();
        private List<double> KUp1 = new List<double>();
        private List<double> KDown1 = new List<double>();
        private List<double> MUp1 = new List<double>();
        private List<double> MDown1 = new List<double>();
        //---------------------------------------------------------------------------------------------------------//
        private List<double> X2 = new List<double>();
        private List<double> KUp2 = new List<double>();
        private List<double> KDown2 = new List<double>();
        private List<double> MUp2 = new List<double>();
        private List<double> MDown2 = new List<double>();
        //-----------------------------------------------------------------------------------------------------------//
        private List<double> KUp11 = new List<double>();
        private List<double> KDown11 = new List<double>();
        private List<double> MUp11 = new List<double>();
        private List<double> MDown11 = new List<double>();
        private List<double> KUp22 = new List<double>();
        private List<double> KDown22 = new List<double>();
        private List<double> MUp22 = new List<double>();
        private List<double> MDown22 = new List<double>();
        //------------------------------------------------------------------------------------------------------------//
        private List<List<double>> KoefLFIUp = new List<List<double>>();
        private List<List<double>> KoefLFIDown = new List<List<double>>();
        private double K1;
        private double K2;
        private double M1;
        private double M2;
        // private double K11;
        // private double K22;
        // private double M11;
        // private double M22;
        //------------------------------------------------------------------------------------------------------------//
        public ArithmeticOperation(List<double> X11, List<double> KUp11, List<double> KDown11, List<double> MUp11, List<double> MDown11,
            List<double> X22, List<double> KUp22, List<double> KDown22, List<double> MUp22, List<double> MDown22, LFI.ElementaryFunction Func1, LFI.ElementaryFunction Func2)
        {
            X1 = X11;
            KUp1 = KUp11;
            KDown1 = KDown11;
            MUp1 = MUp11;
            MDown1 = MDown11;
            X2 = X22;
            KUp2 = KUp22;
            KDown2 = KDown22;
            MUp2 = MUp22;
            MDown2 = MDown22;
            ElemFunction1 = Func1;
            ElemFunction2 = Func2;
        }


        //-----------------------------------------------------------------------------------------------------------------//
        private void ListUnion()
        {
            X = X1.Union(X2).ToList();
            X.Sort();
        }
        //--------------------------------------------------------------------------------------------------------------------//
        public double SumFunction(double x)
        {
            return ElemFunction1(x) + ElemFunction2(x);
        }
        public double SubFunction(double x)
        {
            return ElemFunction1(x) - ElemFunction2(x);
        }
        public double MulFunction(double x)
        {
            return ElemFunction1(x) * ElemFunction2(x);
        }
        public double DivFunction(double x)
        {
            return ElemFunction1(x) / ElemFunction2(x);
        }
        //---------------------------------------------------------------------------------------------------------------------//
        private void TransformationOfLists()
        {
            for (int i = 0; i < X.Count - 1; i++)
            {
                for (int j1 = 0; j1 < X1.Count - 1; j1++)
                {
                    if (X[i] >= X1[j1] && X[i + 1] <= X1[j1 + 1])
                    {
                        KUp11.Add(KUp1[j1]);
                        KDown11.Add(KDown1[j1]);
                        MUp11.Add(MUp1[j1]);
                        MDown11.Add(MDown1[j1]);
                    }
                }
                for (int j2 = 0; j2 < X2.Count - 1; j2++)
                {
                    if (X[i] >= X2[j2] && X[i + 1] <= X2[j2 + 1])
                    {
                        KUp22.Add(KUp2[j2]);
                        KDown22.Add(KDown2[j2]);
                        MUp22.Add(MUp2[j2]);
                        MDown22.Add(MDown2[j2]);
                    }
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//
        public void CreatedListsForMultiple()
        {
            for (int i = 0; i < X.Count - 1; i++)
            {
                if ((Math.Round(LineFunction(X[i], KDown11[i], MDown11[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KDown11[i], MDown11[i]), 6) >= 0)
                    && (Math.Round(LineFunction(X[i], KDown22[i], MDown22[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KDown22[i], MDown22[i]), 6) >= 0))
                {
                    KoefLFIDown.Add(new List<double>() { KDown11[i], KDown22[i], MDown11[i], MDown22[i] });
                    KoefLFIUp.Add(new List<double>() { KUp11[i], KUp22[i], MUp11[i], MUp22[i] });
                }
                if ((Math.Round(LineFunction(X[i], KDown11[i], MDown11[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KDown11[i], MDown11[i]), 6) >= 0)
                    && (Math.Round(LineFunction(X[i], KUp22[i], MUp22[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KUp22[i], MUp22[i]), 6) <= 0))
                {
                    KoefLFIDown.Add(new List<double>() { KUp11[i], KDown22[i], MUp11[i], MDown22[i] });
                    KoefLFIUp.Add(new List<double>() { KDown11[i], KUp22[i], MDown11[i], MUp22[i] });
                }
                if ((Math.Round(LineFunction(X[i], KDown11[i], MDown11[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KDown11[i], MDown11[i]), 6) >= 0)
                    && ((Math.Round(LineFunction(X[i], KDown22[i], MDown22[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KDown22[i], MDown22[i]), 6) <= 0) && (Math.Round(LineFunction(X[i], KUp22[i], MUp22[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KUp22[i], MUp22[i]), 6) >= 0)))
                {
                    KoefLFIDown.Add(new List<double>() { KUp11[i], KDown22[i], MUp11[i], MDown22[i] });
                    KoefLFIUp.Add(new List<double>() { KUp11[i], KUp22[i], MUp11[i], MUp22[i] });
                }
                //--------------------------------------------------------------------------------------------------------------//
                if ((Math.Round(LineFunction(X[i], KUp11[i], MUp11[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KUp11[i], MUp11[i]), 6) <= 0)
                    && (Math.Round(LineFunction(X[i], KDown22[i], MDown22[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KDown22[i], MDown22[i]), 6) >= 0))
                {
                    KoefLFIDown.Add(new List<double>() { KDown11[i], KUp22[i], MDown11[i], MUp22[i] });
                    KoefLFIUp.Add(new List<double>() { KUp11[i], KDown22[i], MUp11[i], MDown22[i] });
                }
                if ((Math.Round(LineFunction(X[i], KUp11[i], MUp11[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KUp11[i], MUp11[i]), 6) <= 0)
                    && (Math.Round(LineFunction(X[i], KUp22[i], MUp22[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KUp22[i], MUp22[i]), 6) <= 0))
                {
                    KoefLFIDown.Add(new List<double>() { KUp11[i], KUp22[i], MUp11[i], MUp22[i] });
                    KoefLFIUp.Add(new List<double>() { KDown11[i], KDown22[i], MDown11[i], MDown22[i] });
                }
                if ((Math.Round(LineFunction(X[i], KUp11[i], MUp11[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KUp11[i], MUp11[i]), 6) <= 0)
                    && ((Math.Round(LineFunction(X[i], KDown22[i], MDown22[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KDown22[i], MDown22[i]), 6) <= 0) && (Math.Round(LineFunction(X[i], KUp22[i], MUp22[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KUp22[i], MUp22[i]), 6) >= 0)))
                {
                    KoefLFIDown.Add(new List<double>() { KDown11[i], KUp22[i], MDown11[i], MUp22[i] });
                    KoefLFIUp.Add(new List<double>() { KDown11[i], KDown22[i], MDown11[i], MDown22[i] });
                }
                //-------------------------------------------------------------------------------------------------------------//
                if (((Math.Round(LineFunction(X[i], KDown11[i], MDown11[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KDown11[i], MDown11[i]), 6) <= 0) && (Math.Round(LineFunction(X[i], KUp11[i], MUp11[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KUp11[i], MUp11[i]), 6) >= 0))
                    && (Math.Round(LineFunction(X[i], KDown22[i], MDown22[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KDown22[i], MDown22[i]), 6) >= 0))
                {
                    KoefLFIDown.Add(new List<double>() { KDown11[i], KUp22[i], MDown11[i], MUp22[i] });
                    KoefLFIUp.Add(new List<double>() { KUp11[i], KUp22[i], MUp11[i], MUp22[i] });
                }
                if (((Math.Round(LineFunction(X[i], KDown11[i], MDown11[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KDown11[i], MDown11[i]), 6) <= 0) && (Math.Round(LineFunction(X[i], KUp11[i], MUp11[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KUp11[i], MUp11[i]), 6) >= 0))
                   && (Math.Round(LineFunction(X[i], KUp22[i], MUp22[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KUp22[i], MUp22[i]), 6) <= 0))
                {
                    KoefLFIDown.Add(new List<double>() { KUp11[i], KDown22[i], MUp11[i], MDown22[i] });
                    KoefLFIUp.Add(new List<double>() { KDown11[i], KDown22[i], MDown11[i], MDown22[i] });
                }
                if (((Math.Round(LineFunction(X[i], KDown11[i], MDown11[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KDown11[i], MDown11[i]), 6) <= 0) && (Math.Round(LineFunction(X[i], KUp11[i], MUp11[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KUp11[i], MUp11[i]), 6) >= 0))
                    && ((Math.Round(LineFunction(X[i], KDown22[i], MDown22[i]), 6) <= 0 && Math.Round(LineFunction(X[i + 1], KDown22[i], MDown22[i]), 6) <= 0) && (Math.Round(LineFunction(X[i], KUp22[i], MUp22[i]), 6) >= 0 && Math.Round(LineFunction(X[i + 1], KUp22[i], MUp22[i]), 6) >= 0)))
                {
                    KoefLFIDown.Add(new List<double>() { KDown11[i], KUp22[i], MDown11[i], MUp22[i], KUp11[i], KDown22[i], MUp11[i], MDown22[i] });
                    KoefLFIUp.Add(new List<double>() { KDown11[i], KDown22[i], MDown11[i], MDown22[i], KUp11[i], KUp22[i], MUp[i], MUp22[i] });
                }
            }
        }

        private void CreatedFLists()
        {
            for (int i = 0; i < X.Count - 1; i++)
            {
                FDown.Add(LineFunction(X[i], KDown[i], MDown[i]));
                FUp.Add(LineFunction(X[i], KUp[i], MUp[i]));
            }
            FDown.Add(LineFunction(X[X.Count - 1], KDown[KDown.Count - 1], MDown[MDown.Count - 1]));
            FUp.Add(LineFunction(X[X.Count - 1], KUp[KUp.Count - 1], MUp[MUp.Count - 1]));
        }

        private void CreateIntervalList(ref List<Interval> ExtInt)
        {
            List<Interval> List = new List<Interval>();
            ExtInt = ExtInt.Distinct().ToList();
            int count;
            for (int i = 0; i < ExtInt.Count; i++)
            {
                count = 0;
                for (int j = 0; j < ExtInt.Count; j++)
                {
                    if (ExtInt[i].Include(ExtInt[j]) == true)
                    {
                        count += 1;
                    }
                }
                if (count > 1)
                {
                    List.Add(ExtInt[i]);
                }

            }
            //Ind = Ind.Distinct().ToList();
            for (int k = 0; k < List.Count; k++)
            {
                ExtInt.Remove(List[k]);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------//
        public void Sum()
        {
            Function = SumFunction;
            ListUnion();
            TransformationOfLists();
            for (int i = 0; i < X.Count - 1; i++)
            {
                KUp.Add(KUp11[i] + KUp22[i]);
                KDown.Add(KDown11[i] + KDown22[i]);
                MUp.Add(MUp11[i] + MUp22[i]);
                MDown.Add(MDown11[i] + MDown22[i]);
            }
            CreatedFLists();
        }
        public void Sub()
        {
            Function = SubFunction;
            ListUnion();
            TransformationOfLists();
            for (int i = 0; i < X.Count - 1; i++)
            {
                KUp.Add(KUp11[i] - KDown22[i]);
                KDown.Add(KDown11[i] - KUp22[i]);
                MUp.Add(MUp11[i] - MDown22[i]);
                MDown.Add(MDown11[i] - MUp22[i]);
            }
            CreatedFLists();
        }
        public void Mul()//DataGridView dt, DataGridView dt1, DataGridView dt2, DataGridView dt3,DataGridView dt4)
        {
            Function = MulFunction;
            ListUnion();
            TransformationOfLists();
            CreatedListsForMultiple();
            List<double> X1 = new List<double>();
            List<double> X2 = new List<double>();
            for (int k = 0; k < X.Count; k++)
            {
                X1.Add(X[k]);
                X2.Add(X[k]);
            }

            List<double> KD = new List<double>();
            List<double> KU = new List<double>();
            List<double> MD = new List<double>();
            List<double> MU = new List<double>();
            //foreach (var V in X)
            //{
            //    dt4.Rows.Add(V.ToString());
            //}
            for (int i = 0; i < X.Count - 1; i++)
            {

                if (Math.Round(KoefLFIDown[i][0], 6) != 0 && Math.Round(KoefLFIDown[i][1], 6) != 0)
                {
                    K1 = KoefLFIDown[i][0]; K2 = KoefLFIDown[i][1]; M1 = KoefLFIDown[i][2]; M2 = KoefLFIDown[i][3];
                    List<double> ListD = new List<double>() { ExtremePointForParabola() };
                    LFI LinFuncIntD = new LFI(1, X[i], X[i + 1], ListD, ParabolaFunction, DerivativeParabolaFunction);
                    LinFuncIntD.BuildLFI();
                    //LinFuncIntD.PaintLFI(chart);
                    for (int j = 1; j < LinFuncIntD.X.Count - 1; j++)
                    {
                        X1.Add(Math.Round(LinFuncIntD.X[j], 10));
                    }
                    for (int j = 0; j < LinFuncIntD.KDown.Count; j++)
                    {
                        KD.Add(LinFuncIntD.KDown[j]);
                        MD.Add(LinFuncIntD.MDown[j]);
                    }
                }
                else
                {
                    K1 = KoefLFIDown[i][0]; K2 = KoefLFIDown[i][1]; M1 = KoefLFIDown[i][2]; M2 = KoefLFIDown[i][3];
                    KD.Add(CreateMulK());
                    MD.Add(CreateMulM());
                }
                //---------------------------------------------------------------------------------------------------------//
                if (Math.Round(KoefLFIUp[i][0], 6) != 0 && Math.Round(KoefLFIUp[i][1], 6) != 0)
                {
                    K1 = KoefLFIUp[i][0]; K2 = KoefLFIUp[i][1]; M1 = KoefLFIUp[i][2]; M2 = KoefLFIUp[i][3];
                    List<double> ListU = new List<double>() { ExtremePointForParabola() };
                    LFI LinFuncIntU = new LFI(1, X[i], X[i + 1], ListU, ParabolaFunction, DerivativeParabolaFunction);
                    LinFuncIntU.BuildLFI();
                    for (int j = 1; j < LinFuncIntU.X.Count - 1; j++)
                    {
                        X2.Add(Math.Round(LinFuncIntU.X[j], 10));
                    }
                    for (int j = 0; j < LinFuncIntU.KUp.Count; j++)
                    {
                        KU.Add(LinFuncIntU.KUp[j]);
                        MU.Add(LinFuncIntU.MUp[j]);
                    }
                }
                else
                {
                    K1 = KoefLFIUp[i][0]; K2 = KoefLFIUp[i][1]; M1 = KoefLFIUp[i][2]; M2 = KoefLFIUp[i][3];
                    KU.Add(CreateMulK());
                    MU.Add(CreateMulM());
                }
            }

            X1.Sort();
            X2.Sort();


            X.Clear();
            //List<double> NewX = new List<double>();
            X = X1.Union(X2).ToList();
            //X = X.Distinct().ToList();
            X.Sort();
            //for (int k = 0; k < X1.Count; k++)
            //{
            //    dt.Rows.Add(X1[k].ToString());
            //}

            //for (int k = 0; k < X2.Count; k++)
            //{
            //    dt1.Rows.Add(X2[k].ToString());
            //}
            //for (int k = 0; k < X.Count; k++)
            //{
            //    dt2.Rows.Add(X[k].ToString());
            //}

            for (int i = 0; i < X.Count - 1; i++)
            {
                for (int j1 = 0; j1 < X1.Count - 1; j1++)
                {
                    if (X[i] >= X1[j1] && X[i + 1] <= X1[j1 + 1])
                    {
                        KDown.Add(KD[j1]);
                        MDown.Add(MD[j1]);
                    }
                }
                for (int j2 = 0; j2 < X2.Count - 1; j2++)
                {
                    if (X[i] >= X2[j2] && X[i + 1] <= X2[j2 + 1])
                    {
                        KUp.Add(KU[j2]);
                        MUp.Add(MU[j2]);
                    }
                }
            }
            CreatedFLists();
            //for (int k = 0; k < KU.Count; k++)
            //{
            //    dt3.Rows.Add(KU[k].ToString(), MU[k].ToString());//,KDown[k].ToString(), MDown[k].ToString());
            //}
        }
        public void PaintTestOperation(Chart chart)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Series[2].Points.Clear();
            for (int i = 0; i < X.Count - 1; i++)
            {
                for (double j = X[i]; j <= X[i + 1]; j += 0.05)
                {
                    chart.Series[0].Points.AddXY(j, Function(j));
                    chart.Series[1].Points.AddXY(j, ParabolaFunction(j, KoefLFIUp[i][0], KoefLFIUp[i][1], KoefLFIUp[i][2], KoefLFIUp[i][3]));
                    chart.Series[2].Points.AddXY(j, ParabolaFunction(j, KoefLFIDown[i][0], KoefLFIDown[i][1], KoefLFIDown[i][2], KoefLFIDown[i][3]));
                }
            }
        }
        public void PaintOperation(Chart chart)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Series[2].Points.Clear();
            for (int i = 0; i < X.Count - 1; i++)
            {
                for (double j = X[i]; j <= X[i + 1]; j += 0.05)
                {
                    chart.Series[0].Points.AddXY(j, Function(j));
                    chart.Series[1].Points.AddXY(j, LineFunction(j, KUp[i], MUp[i]));
                    chart.Series[2].Points.AddXY(j, LineFunction(j, KDown[i], MDown[i]));
                }
            }
        }
        public void PaintLists(DataGridView dt1)
        {
            dt1.Rows.Clear();
            for (int i = 0; i < X.Count - 1; i++)
            {
                dt1.Rows.Add(Math.Round(X[i], 5), Math.Round(KDown[i], 5), Math.Round(KUp[i], 5), Math.Round(MDown[i], 5), Math.Round(MUp[i], 5), Math.Round(FDown[i], 5), Math.Round(FUp[i], 5));
            }
            dt1.Rows.Add(Math.Round(X[X.Count - 1], 5), null, null, null, null, Math.Round(FDown[X.Count - 1], 5), Math.Round(FUp[X.Count - 1], 5));
        }
        //-----------------------------------------------------------------------------------------------------------------------------//
        private double LineFunction(double x, double K, double M)
        {
            return K * x + M;
        }
        private double ParabolaFunction(double x)
        {
            //return (K1 * x + M1) * (K2 * x + M2);
            return K1 * K2 * x * x + (K1 * M2 + K2 * M1) * x + M1 * M2;
        }
        private double ParabolaFunction(double x, double K1, double K2, double M1, double M2)
        {
            //return (K1 * x + M1) * (K2 * x + M2);
            return K1 * K2 * x * x + (K1 * M2 + K2 * M1) * x + M1 * M2;
        }
        private double HyperboleFunction(double x)
        {
            return (K1 * x + M1) / (K2 * x + M2);
        }
        private double HyperboleFunction(double x, double K1, double K2, double M1, double M2)
        {
            return (K1 * x + M1) / (K2 * x + M2);
        }
        private double DerivativeParabolaFunction(double x)
        {
            return 2 * K1 * K2 * x + (K1 * M2 + K2 * M1);
        }
        private double DerivativeParabolaFunction(double x, double K1, double K2, double M1, double M2)
        {
            return 2 * K1 * K2 * x + (K1 * M2 + K2 * M1);
        }
        private double DerivativeHyperboleFunction(double x)
        {
            return (K1 * (K2 * x + M2) - (K1 * x + M1) * K2) / ((K2 * x + M2) * (K2 * x + M2));
        }
        private double CreateMulK()
        {
            return K1 * M2 + K2 * M1;
        }
        private double CreateMulM()
        {
            return M1 * M2;
        }
        private double CreateDivK()
        {
            return K1 / M2;
        }
        private double CreateDivM()
        {
            return M1 / M2;
        }
        private double ExtremePointForParabola()
        {
            return -((K1 * M2 + K2 * M1) / (2 * K1 * K2));
        }
        //-------------------------------------------------------------------------------------------------------------------------------//
        public double Norma()
        {
            double MaxUp = Math.Abs(FUp[0]), MaxDown = Math.Abs(FDown[0]);
            for (int i = 1; i < FUp.Count; i++)
            {
                if (MaxUp < Math.Abs(FUp[i]))
                {
                    MaxUp = Math.Abs(FUp[i]);
                }
                if (MaxDown < Math.Abs(FDown[i]))
                {
                    MaxDown = Math.Abs(FDown[i]);
                }
            }

            return Math.Max(MaxDown, MaxUp);
        }

        public double Width()
        {
            double max = Math.Abs(Math.Abs(FUp[0]) - Math.Abs(FDown[0]));

            for (int i = 0; i < FUp.Count; i++)
            {
                if (Math.Abs(Math.Abs(FUp[i]) - Math.Abs(FDown[i])) > max)
                {
                    max = Math.Abs(Math.Abs(FUp[i]) - Math.Abs(FDown[i]));
                }
            }
            return (max);
        }

        public void FunctionalNorm(DataGridView dgv)
        {
            for (int i = 0; i < X.Count; i++)
            {
                dgv.Rows.Add(Math.Round(X[i], 5), Math.Round(Math.Abs(Math.Abs(FDown[i]) - Math.Abs(FUp[i])), 5));
            }
        }

        public void FunctionalWidth(DataGridView dgv)
        {
            for (int i = 0; i < X.Count; i++)
            {
                dgv.Rows.Add(Math.Round(X[i], 5), Math.Round(Math.Max(Math.Abs(FDown[i]), Math.Abs(FUp[i])), 5));
            }
        }
    }
}
