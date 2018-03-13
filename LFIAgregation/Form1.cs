using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFIAgregation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       // private delegate double ElementaryFunction(double x);
        private LFI.ElementaryFunction ElemFunction1;
        private LFI.ElementaryFunction ElemFunction2;
        private LFI.DerivativeElementaryFunction DerivateFunction1;
        private LFI.DerivativeElementaryFunction DerivateFunction2;
        private List<double> X1 = new List<double>();
        private List<double> X2 = new List<double>();

            List<double> ListOfExtremePointsSin = new List<double>() { (1 - 2 * Math.PI) / 2, (2 - 3 * Math.PI) / 4, (1 - Math.PI) / 2, (2 - Math.PI) / 4, 0.5, (2 + Math.PI) / 4, (1 + Math.PI) / 2, (2 + 3 * Math.PI) / 4, (1 + 2 * Math.PI) / 2 };

            List<double> ListOfExtremePointsCos = new List<double>() { -2.5 * Math.PI ,- 2 * Math.PI, -1.5 * Math.PI, -Math.PI, -Math.PI / 2.0, 0, Math.PI / 2.0, Math.PI, 1.5 * Math.PI, 2 * Math.PI, 2.5 * Math.PI, 3 * Math.PI, 3.5 * Math.PI, 4 * Math.PI };
            //{ (-6*Math.PI-2)/10, (-5*Math.PI-2)/10, (-4*Math.PI-2)/10,(-3*Math.PI-2)/10,(-2*Math.PI-2)/10,(-Math.PI-2)/10, -2/10,
            //(-2+Math.PI)/10, (-2+2*Math.PI)/10,(-2+3*Math.PI)/10, (-2+4*Math.PI)/10,(-2+5*Math.PI)/10,(-2+6*Math.PI)/10, (-2+7*Math.PI)/10,(-2+8*Math.PI)/10,(-2+9*Math.PI)/10,(-2+10*Math.PI)/10,(-2+11*Math.PI)/10,
            //(-2+12*Math.PI)/10,(-2+13*Math.PI)/10,(-2+14*Math.PI)/10,(-2+15*Math.PI)/10,(-2+16*Math.PI)/10,(-2+17*Math.PI)/10,(-2+18*Math.PI)/10,(-2+19*Math.PI)/10,(-2+20*Math.PI)/10};

            List<double> ListOfExtremePointsPow = new List<double>() { };

            List<double> ListOfExtremePointsPowMy = new List<double>() { };

            List<double> ListOfExtremePointsQuad = new List<double>() { 0.0, (4 - 2 * Math.Sqrt(8)) / 2, 2.0, (4 + 2 * Math.Sqrt(8)) / 2 };

            List<double> ListOfExtremePointsQuadMy = new List<double>() { -1.0, 0.0, 0.5, 2.0 };

            List<double> ListOfExtremePointsLn = new List<double>() { -2.25 };

        //-------------------------------------------------------------------------------------------------------------------------//
        private double QuadraticFunction(double x)
        {
            //return x*x + x + 1;
            return x * x - 4 * x + 2;
        }
        private double QuadraticFunctionMy(double x)
        {
            //return x*x + x + 1;
            return x * x - x - 2;
        }
        private double Sin(double x)
        {
            //return Math.Sin(x);
            return Math.Sin(2 * x - 1);
        }
        private double Cos(double x)
        {
            return Math.Cos(x);
            //return Math.Cos(5 * x + 1);
        }
        private double Pow(double x)
        {
            return Math.Pow(0.5, x);
        }
        private double PowMy(double x)
        {
            return Math.Pow(2, x);
        }

        private double Ln(double x)
        {
            return Math.Log(2 * x + 4.5) + 1;
        }
        private double Agreg(Func<double, double> func, double k, double m, double x)
        {
            return func(k * x + m);
        }
        //-------------------------------------------------------------------------------------------------------------------------//
        private double DerivativeSin(double x)
        {
            //return Math.Cos(x);
            return 2 * Math.Cos(1 - 2 * x);
        }
        private double DerivativeLn(double x)
        {
            return 1 / (x + 2.25);
        }
        private double DerivativeCos(double x)
        {
            return -Math.Sin(x);
            //return -5 * Math.Sin(5 * x + 1);
        }
        private double DerivateQuadFunc(double x)
        {
            //return 2 * x + 1;
            return 2 * x - 4;
        }
        private double DerivateQuadFuncMy(double x)
        {
            //return 2 * x + 1;
            return 2 * x - 1;
        }
        private double DerivativePow(double x)
        {
            return Math.Pow(0.5, x) * Math.Log(0.5);
        }
        private double DerivativePowMy(double x)
        {
            return Math.Pow(2, x) * Math.Log(2);
        }
        
        //-------------------------------------------------------------------------------------------------------------------------//
        private bool CheckLn(double x)
        {
            return x > -0.5;
        }
        private bool CheckQuad(double x)
        {
            return true;
        }
        //-------------------------------------------------------------------------------------------------------------------------//

        private void CreateLFIbutton_Click(object sender, EventArgs e)
        {
            int N = Convert.ToInt32(NtextBox.Text);
            double A = Convert.ToDouble(AtextBox.Text);
            double B = Convert.ToDouble(BtextBox.Text);

            //LFI LinFuncIntQuad = new LFI(N, A, B, ListOfExtremePointsQuad, QuadraticFunction, DerivateQuadFunc);
            //LFI LinFuncIntQuadMy = new LFI(N, A, B, ListOfExtremePointsQuadMy, QuadraticFunctionMy, DerivateQuadFuncMy);
            //LFI LinFuncIntSin = new LFI(N, A, B, ListOfExtremePointsSin, Sin, DerivativeSin);
            //LFI LinFuncIntCos = new LFI(N, A, B, ListOfExtremePointsCos, Cos, DerivativeCos);
            //LFI LinFuncIntPow = new LFI(N, A, B, ListOfExtremePointsPow, Pow, DerivativePow);
            //LFI LinFuncIntLn = new LFI(N, A, B, ListOfExtremePointsLn, Ln, DerivativeLn);
            LFI LFI1 = new LFI(N, A, B, X1, ElemFunction1, DerivateFunction1);
            LFI LFI2 = new LFI(N, A, B, X2, ElemFunction2, DerivateFunction2);
            LFI1.BuildLFI();
            LFI1.PaintLFI(chart1);

            LFI2.BuildLFI();
            LFI2.PaintLFI(chart2);

            ArithmeticOperation Sum = new ArithmeticOperation(LFI1.X, LFI1.KUp, LFI1.KDown, LFI1.MUp, LFI1.MDown,
             LFI2.X, LFI2.KUp, LFI2.KDown, LFI2.MUp, LFI2.MDown, LFI1.ElemFunction, LFI2.ElemFunction);
            Sum.Sum();
            Sum.PaintOperation(chart3);
            Sum.Norma();
            Sum.Width();
            //Sum.PaintLists(dgvSuma);
            //Sum.FunctionalNorm(dgvSumNorm);
            //Sum.FunctionalWidth(dgvSumWidth);

            ArithmeticOperation Sub = new ArithmeticOperation(LFI1.X, LFI1.KUp, LFI1.KDown, LFI1.MUp, LFI1.MDown,
             LFI2.X, LFI2.KUp, LFI2.KDown, LFI2.MUp, LFI2.MDown, LFI1.ElemFunction, LFI2.ElemFunction);
            Sub.Sub();
            Sub.PaintOperation(chart4);
            Sub.Norma();
            Sub.Width();
            //Sub.PaintLists(dgvSub);
            //Sub.FunctionalNorm(dgvSubNorm);
            //Sub.FunctionalWidth(dgvSubWidth);

            //ArithmeticOperation Mul = new ArithmeticOperation(LinFuncIntQuad.X, LinFuncIntQuad.KUp, LinFuncIntQuad.KDown, LinFuncIntQuad.MUp, LinFuncIntQuad.MDown,
            // LinFuncIntLn.X, LinFuncIntLn.KUp, LinFuncIntLn.KDown, LinFuncIntLn.MUp, LinFuncIntLn.MDown, LinFuncIntQuad.ElemFunction, LinFuncIntLn.ElemFunction);
            //Mul.Mul();
            //Mul.PaintOperation(chart5);
            //Mul.Norma();
            //Mul.Width();
            //Mul.FunctionalNorm(dgvMulNorm);
            //Mul.FunctionalWidth(dgvMulWidth);
            //Mul.PaintLists(dgvMul);

            AgregateOperation Ag = new AgregateOperation(LFI1.X, LFI1.KUp, LFI1.KDown, LFI1.MUp, LFI1.MDown,
LFI2.X, LFI1.ElemFunction, LFI2.ElemFunction, DerivateFunction2);

 // ++          AgregateOperation Ag = new AgregateOperation(LinFuncIntQuad.X, LinFuncIntQuad.KUp, LinFuncIntQuad.KDown, LinFuncIntQuad.MUp, LinFuncIntQuad.MDown,
 //LinFuncIntPow.X, QuadraticFunction, PowMy, DerivativePowMy);

            //             AgregateOperation Ag = new AgregateOperation(LinFuncIntQuad.X, LinFuncIntQuad.KUp, LinFuncIntQuad.KDown, LinFuncIntQuad.MUp, LinFuncIntQuad.MDown,
            //ListOfExtremePointsCos, QuadraticFunction, Cos, DerivativeCos);
        

            Ag.Agregate();
            Ag.PaintOperation(chart5);
            //Sub.Norma();
            //Sub.Width();
            //Sub.PaintLists(dgvSub);
            //Sub.FunctionalNorm(dgvSubNorm);
            //Sub.FunctionalWidth(dgvSubWidth);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NtextBox.Text = "1";
            AtextBox.Text = "-2";
            BtextBox.Text = "6";
        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }


        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedIndex == 0)
            {
                ElemFunction1 = QuadraticFunctionMy;
                DerivateFunction1 = DerivateQuadFuncMy;
                X1 = ListOfExtremePointsQuadMy;
            }
            else if (comboBox.SelectedIndex == 1)
            {
                ElemFunction1 = PowMy;
                DerivateFunction1 = DerivativePowMy;
                X1 = ListOfExtremePointsPowMy;
            }
            else if (comboBox.SelectedIndex == 2)
            {
                ElemFunction1 = Pow;
                DerivateFunction1 = DerivativePow;
                X1 = ListOfExtremePointsPow;
            }
            else if (comboBox.SelectedIndex == 3)
            {
                ElemFunction1 = Cos;
                DerivateFunction1 = DerivativeCos;
                X1 = ListOfExtremePointsCos;
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedIndex == 0)
            {
                ElemFunction2 = QuadraticFunctionMy;
                DerivateFunction2 = DerivateQuadFuncMy;
                X2 = ListOfExtremePointsQuadMy;
            }
            else if (comboBox.SelectedIndex == 1)
            {
                ElemFunction2 = PowMy;
                DerivateFunction2 = DerivativePowMy;
                X2 = ListOfExtremePointsPowMy;
            }
            else if (comboBox.SelectedIndex == 2)
            {
                ElemFunction2 = Pow;
                DerivateFunction2 = DerivativePow;
                X2 = ListOfExtremePointsPow;
            }
        }
    }
}
