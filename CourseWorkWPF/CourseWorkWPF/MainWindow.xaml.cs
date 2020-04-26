using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.InteropServices;
using WpfMath;
using WpfMath.Controls;
using Math = System.Math;

namespace CourseWorkWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Random gen = new Random();
        // Счетчик задач
        private static int taskCount; 
        // Коэффициенты неравенств
        private static string a, b, c, d, e, f, g, h;
        // Целочисленные аналоги коэффициентов неравенств
        private static double ai, bi, ci, di, ei, fi, gi, hi;
        // Массив знаков неравенств
        private static string[] eqsigns =  {@"\leq", @"\geq", @"\gt", @"\lt"};
        // Знаки перового и второго неравенства
        private static string sgn1,sgn2;
        public MainWindow()
        {
            InitializeComponent();
            cbox.Items.Insert(0, "выберите сложность задач");
            cbox.SelectedIndex = 0;
            cbox2.Items.Insert(0, "выберите количество задач");
            cbox2.SelectedIndex = 0;
            bracket.Visibility = Visibility.Hidden;
            formula.Visibility = Visibility.Hidden;
            formula2.Visibility = Visibility.Hidden;
            bnext.Visibility = Visibility.Hidden;
            rest.Visibility = Visibility.Hidden;
            exit.Visibility = Visibility.Hidden;
            ansbut.Visibility = Visibility.Hidden;
            anstext.Visibility = Visibility.Hidden;
            var.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Красивый вывод
        /// </summary>
        void BeatOut1(ref string a, ref string b, ref string c, ref string d, ref string e, ref string f)
        {
            if (a == "1") a = "";
            if (d == "1") d = "";
        }

        /// <summary>
        /// Красивый вывод
        /// </summary>
        void BeatOut2(ref string a, ref string b, ref string c, ref string e, ref string f, ref string g)
        {

        }

        /// <summary>
        /// Генератор задач первого уровня сложности
        /// </summary>
        private void GenTask1()
        {
            a = gen.Next(1, 30).ToString();
            b = gen.Next(1, 30).ToString();
            c = gen.Next(1, 30).ToString();
            d = gen.Next(1, 30).ToString();
            e = gen.Next(1, 30).ToString();
            f = gen.Next(1, 30).ToString();
            BeatOut1(ref a, ref b, ref c, ref d, ref e, ref f);
            sgn1 = eqsigns[gen.Next(4)]; 
            formula.Formula = $"{a}x+{b} {sgn1} {c}";
            formula2.Formula = $"{d}x+{e} {sgn1} {f}";
        }
        /// <summary>
        /// Генератор задач второго уровня сложности
        /// </summary>
        private void GenTask2()
        {
            // Генерируем коэффициенты квадратного уравнения, пока дискрминант не будет больше или равен нулю
            do
            {
                ai = gen.Next(-29, 30);
                bi = gen.Next(-29, 30);
                ci = gen.Next(-29, 30);
                di = gen.Next(-29, 30);
                ei = gen.Next(-29, 30);
                fi = gen.Next(-29, 30);
                gi = gen.Next(-29, 30);
                hi = gen.Next(-29, 30);
            } while (bi * bi - 4 * ai * (ci - di) < 0 || fi * fi - 4 * ei * (gi - hi) < 0 || ci==0 || gi==0);

            a = ai.ToString();
            b = bi.ToString();
            c = ci.ToString();
            e = ei.ToString();
            f = fi.ToString();
            g = gi.ToString();
            BeatOut2(ref a, ref b, ref c, ref e, ref f, ref g);
            formula.Formula = $"{ai}x^2+{bi}x+{ci} = {di}";
            formula2.Formula = $"{ei}x^2+{fi}x+{gi} = {hi}";
        }
        /// <summary>
        /// Генератор задач третьего уровня сложности
        /// </summary>
        private void GenTask3()
        {

        }

        /// <summary>
        /// Завершение программы
        /// </summary>
        void EndPr()
        {
            bnext.Visibility = Visibility.Hidden;
            formula2.Visibility = Visibility.Hidden;
            formula.Visibility = Visibility.Hidden;
            bracket.Visibility = Visibility.Hidden;
            ansbut.Visibility = Visibility.Hidden;
            exit.Visibility = Visibility.Visible;
            rest.Visibility = Visibility.Visible;
            
        }

        /// <summary>
        /// Повтор решения
        /// </summary>
        void Restart()
        {
            
        }
        private void Bnext_Click(object sender, RoutedEventArgs e)
        {
            if (cbox.Text == "1") GenTask1();
            if (cbox.Text == "2") GenTask2();
            if (cbox.Text == "3") GenTask3();
            taskCount--;
            anstext.Visibility = Visibility.Hidden;
            var.Visibility = Visibility.Hidden;
            if (taskCount==0)
            {
                EndPr();
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Rest_Click(object sender, RoutedEventArgs e)
        {
            rest.Visibility = Visibility.Hidden;
            exit.Visibility = Visibility.Hidden;
            butgen.Visibility = Visibility.Visible;
            cbox.Visibility = Visibility.Visible;
            cbox2.Visibility = Visibility.Visible;
            cbox.SelectedIndex = 0;
            cbox2.SelectedIndex = 0;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cbox.Text == "выберите сложность задач")
            {
                MessageBox.Show("Выберите сложность задач!");
                return;
            }

            if (cbox2.Text == "выберите количество задач")
            {
                MessageBox.Show("Выберите количество задач!");
                return;
            }
            butgen.Visibility = Visibility.Collapsed;
            cbox.Visibility = Visibility.Collapsed;
            cbox2.Visibility = Visibility.Collapsed;
            bracket.Visibility = Visibility.Visible;
            formula.Visibility = Visibility.Visible;
            formula2.Visibility = Visibility.Visible;
            bnext.Visibility = Visibility.Visible;
            ansbut.Visibility = Visibility.Visible;
            int.TryParse(cbox2.Text, out taskCount);
            if (cbox.Text == "1")
            {
                GenTask1();
            }

            if (cbox.Text == "2")
            {
                GenTask2();
            }

            if (cbox.Text == "3")
            {
                GenTask3();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs m)
        {
            //anstext.Visibility = Visibility.Visible;
            var.Visibility = Visibility.Visible;
            double.TryParse(a, out ai);
            double.TryParse(b, out bi);
            double.TryParse(c, out ci);
            double.TryParse(d, out di);
            double.TryParse(e, out ei);
            double.TryParse(f, out fi);
            if (cbox.Text == "1")
            {
                double first = (ci - bi) / ai, second = (fi - ei) / di;
                if (sgn1 == @"\leq" || sgn1 == @"\lt")
                {
                    //anstext.Text = @"(-inf," + $"{Math.Min(1,2)}]";
                    if (first <= second)
                    {
                        int temp = (int)(ci - bi);
                        if (sgn1== @"\leq") var.Formula = $@"x\in (-\infty,\frac{{{temp}}}{{{ai}}}]";
                        else var.Formula = $@"x\in(-\infty,\frac{{{temp}}}{{{ai}}})";
                    }
                    else
                    {
                        int temp = (int)(fi-ei);
                        if (sgn1 == @"\leq") var.Formula = $@"(-\infty,\frac{{{temp}}}{{{di}}}]";
                        else var.Formula = $@"x\in(-\infty,\frac{{{temp}}}{{{di}}})";
                    }
                }
                else
                {
                    if (first >= second)
                    {
                        int temp = (int)(ci - bi);
                        if (sgn1 == @"\geq") var.Formula = $@"x\in [\frac{{{temp}}}{{{ai}}},\infty)";
                        else var.Formula = $@"x\in (\frac{{{temp}}}{{{ai}}},\infty)";
                    }
                    else
                    {
                        int temp = (int)(fi-ei);
                        if (sgn1 == @"\geq") var.Formula = $@"x\in [\frac{{{temp}}}{{{di}}},\infty)";
                        else var.Formula = $@"x\in (\frac{{{temp}}}{{{di}}},\infty)";
                    }
                }
            }

            if (cbox.Text == "2")
            {

            }

        }

        private static int p;
        private void Cbox_DropDownOpened(object sender, EventArgs e)
        {
            
            cbox.Items.RemoveAt(0);
        }

        private void Cbox_DropDownClosed(object sender, EventArgs e)
        {
            cbox.Items.Insert(0, "выберите сложность задач");
            if (cbox.SelectedIndex == -1)
            {
                cbox.SelectedIndex = 0;
            }
        }

        private void Cbox2_DropDownOpened(object sender, EventArgs e)
        {
            cbox2.Items.RemoveAt(0);
        }

        private void Cbox2_DropDownClosed(object sender, EventArgs e)
        {
            cbox2.Items.Insert(0, "выберите количество задач");
            if (cbox2.SelectedIndex == -1)
            {
                cbox2.SelectedIndex = 0;
            }
        }
    }
}
