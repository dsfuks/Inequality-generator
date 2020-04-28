using System;
using System.Windows;
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
        private static string[] eqsigns = { @"\leq", @"\geq", @"\gt", @"\lt" };

        // Знаки перового и второго неравенства
        private static string sgn1, sgn2;

        // Дискриминанты
        private double dis1, dis2;

        // Корни квадратных уравнений
        double xx1, x2, x3, x4;

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
            var.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Красивый вывод
        /// </summary>
        private void BeatOut1(ref string a, ref string b, ref string c, ref string d, ref string e, ref string f)
        {
            if (a == "1") a = "";
            if (d == "1") d = "";
        }

        /// <summary>
        /// Красивый вывод
        /// </summary>
        private void BeatOut2(ref string a, ref string b, ref string c, ref string e, ref string f, ref string g)
        {
            int bi, ci, fi, gi;
            int.TryParse(b, out bi);
            int.TryParse(c, out ci);
            int.TryParse(f, out fi);
            int.TryParse(g, out gi);
            if (a == "1") a = "";
            if (b == "1") b = "+";
            if (e == "1") e = "";
            if (f == "1") f = "+";
            if (a == "-1") a = "-";
            if (b == "-1") b = "-";
            if (e == "-1") e = "-";
            if (f == "-1") f = "-";
            if (bi > 1) b = $"+{bi.ToString()}";
            if (fi > 1) f = $"+{fi.ToString()}";
            if (ci >= 1) c = $"+{ci.ToString()}";
            if (gi >= 1) g = $"+{gi.ToString()}";
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
        /// Проверяет, имеет ли на данном наборе система неравенств решение
        /// </summary>
        /// <returns>true,если удовлетворяет</returns>
        bool CheckQadr(string sgn1, string sgn2, double x1, double x2, double x3, double x4)
        {
            if (sgn1 == @"\leq")
            {
                if (sgn2 == @"\leq")
                {
                    if (x2 <= x4 && x2 >= x3) return true;
                    if (x4 <= x2 && x4 >= x1) return true;
                }

                if (sgn2 == @"\lt")
                {
                    if (x2 < x4 && x2 > x3) return true;
                    if (x4 < x2 && x4 > x1) return true;
                }

                if (sgn2 == @"\geq")
                {
                    if (x2 >= x4 || x1 >= x4 || x2 <= x3 || x1 <= x3) return true;
                }

                if (sgn2 == @"\gt")
                {
                    if (x2 > x4 || x1 > x4 || x2 < x3 || x1 < x3) return true;
                }
            }

            if (sgn1 == @"\lt")
            {
                if (sgn2 == @"\leq")
                {
                    if (x2 < x4 && x2 > x3) return true;
                    if (x4 < x2 && x4 > x1) return true;
                }

                if (sgn2 == @"\lt")
                {
                    if (x2 < x4 && x2 > x3) return true;
                    if (x4 < x2 && x4 > x1) return true;
                }

                if (sgn2 == @"\geq")
                {
                    if (x2 > x4 || x1 > x4 || x2 < x3 || x1 < x3) return true;
                }

                if (sgn2 == @"\gt")
                {
                    if (x2 > x4 || x1 > x4 || x2 < x3 || x1 < x3) return true;
                }
            }

            if (sgn1 == @"\geq")
            {
                if (sgn2 == @"\leq")
                {
                    if (x4 >= x2 || x3 >= x2 || x4 <= x1 || x3 <= x1) return true;
                }

                if (sgn2 == @"\lt")
                {
                    if (x4 > x2 || x3 > x2 || x4 < x1 || x3 < x1) return true;
                }

                if (sgn2 == @"\geq")
                {
                    return true;
                }

                if (sgn2 == @"\gt")
                {
                    return true;
                }
            }

            if (sgn1 == @"\gt")
            {
                if (sgn2 == @"\leq")
                {
                    if (x4 > x2 || x3 > x2 || x4 < x1 || x3 < x1) return true;
                }

                if (sgn2 == @"\lt")
                {
                    if (x4 > x2 || x3 > x2 || x4 < x1 || x3 < x1) return true;
                }

                if (sgn2 == @"\geq")
                {
                    return true;
                }

                if (sgn2 == @"\gt")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Генератор задач второго уровня сложности
        /// </summary>
        private void GenTask2()
        {
            sgn1 = eqsigns[gen.Next(3)];
            sgn2 = eqsigns[gen.Next(3)];
            // Генерируем коэффициенты квадратного уравнения, пока дискрминант не будет больше нуля
            do
            {
                ai = 1;
                bi = gen.Next(-10, 10);
                ci = gen.Next(-10, 10);
                di = gen.Next(-10, 10);
                ei = 1;
                fi = gen.Next(-10, 10);
                gi = gen.Next(-10, 10);
                hi = gen.Next(-10, 10);
                dis1 = bi * bi - 4 * ai * (ci-di);
                dis2 = fi * fi - 4 * (gi-hi)* ei;
                xx1 = (-bi - Math.Pow(dis1, 0.5)) / 2;
                x2 = (-bi + Math.Pow(dis1, 0.5)) / 2;
                x3 = (-fi - Math.Pow(dis2, 0.5)) / 2;
                x4 = (-fi + Math.Pow(dis2, 0.5)) / 2;
            } while (bi * bi - 4 * ai * (ci - di) <= 0 || fi * fi - 4 * ei * (gi - hi) <= 0 || ci == 0 || gi == 0 || bi == 0 || fi == 0 || !CheckQadr(sgn1,sgn2,xx1,x2,x3,x4));

            a = ai.ToString();
            b = bi.ToString();
            c = ci.ToString();
            e = ei.ToString();
            f = fi.ToString();
            g = gi.ToString();
            BeatOut2(ref a, ref b, ref c, ref e, ref f, ref g);
            formula.Formula = $"{a}x^2{b}x{c} {sgn1} {di}";
            formula2.Formula = $"{e}x^2{f}x{g} {sgn2} {hi}";
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
        private void EndPr()
        {
            bnext.Visibility = Visibility.Hidden;
            formula2.Visibility = Visibility.Hidden;
            formula.Visibility = Visibility.Hidden;
            bracket.Visibility = Visibility.Hidden;
            ansbut.Visibility = Visibility.Hidden;
            exit.Visibility = Visibility.Visible;
            rest.Visibility = Visibility.Visible;
        }
        
        private void Bnext_Click(object sender, RoutedEventArgs e)
        {
            if (cbox.Text == "1") GenTask1();
            if (cbox.Text == "2") GenTask2();
            if (cbox.Text == "3") GenTask3();
            taskCount--;
            var.Visibility = Visibility.Hidden;
            if (taskCount == 0)
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
            var.Visibility = Visibility.Visible;
            if (cbox.Text == "1")
            {
                double.TryParse(a, out ai);
                double.TryParse(b, out bi);
                double.TryParse(c, out ci);
                double.TryParse(d, out di);
                double.TryParse(e, out ei);
                double.TryParse(f, out fi);
                double first = (ci - bi) / ai, second = (fi - ei) / di;
                if (sgn1 == @"\leq" || sgn1 == @"\lt")
                {
                    if (first <= second)
                    {
                        int temp = (int)(ci - bi);
                        if (sgn1 == @"\leq") var.Formula = $@"x\in (-\infty,\frac{{{temp}}}{{{ai}}}]";
                        else var.Formula = $@"x\in(-\infty,\frac{{{temp}}}{{{ai}}})";
                    }
                    else
                    {
                        int temp = (int)(fi - ei);
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
                        int temp = (int)(fi - ei);
                        if (sgn1 == @"\geq") var.Formula = $@"x\in [\frac{{{temp}}}{{{di}}},\infty)";
                        else var.Formula = $@"x\in (\frac{{{temp}}}{{{di}}},\infty)";
                    }
                }
            }

            if (cbox.Text == "2")
            {
                // Для красивого вывода
                double negbi=-bi, negfi=-fi;
                string xxx1 = $@"\frac{{{negbi}-\sqrt{{{dis1}}}}}{{{2}}}",
                    xx2 = $@"\frac{{{negbi}+\sqrt{{{dis1}}}}}{{{2}}}",
                    xx3 = $@"\frac{{{negfi}-\sqrt{{{dis2}}}}}{{{2}}}",
                    xx4 = $@"\frac{{{negfi}+\sqrt{{{dis2}}}}}{{{2}}}";
                if (sgn1 == @"\leq")
                {
                    if (sgn2 == @"\leq")
                    {
                        if (x2 == x3)
                        {
                            var.Formula = "x = " + xx2;
                        }

                        if (xx1 == x4)
                        {
                            var.Formula = $@"x = {xx4}";
                        }

                        if (x2 <= x4 && x2>x3 && xx1 <= x3)
                        {
                            var.Formula = $@"x\in [{xx3},{xx2}]";
                        }

                        if (x2 <= x4 && x2>x3 && xx1 > x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }

                        if (x2 > x4 && xx1 < x4 && xx1>x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx4}]";
                        }

                        if (x3 > xx1 && x4 <= x2)
                        {
                            var.Formula = $@"x\in [{xx3},{xx4}]";
                        }
                    }

                    if (sgn2 == @"\lt")
                    {
                        if (x2 < x4 && x2>x3 && xx1 <= x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx2}]";
                        }

                        if (x2 == x4 && xx1 <= x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }

                        if (x2 < x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }

                        if (x2 == x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx4})";
                        }

                        if (x2 > x4 && xx1 < x4 && xx1>x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx4})";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }

                        if (x3 > xx1 && x4 < x2)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }
                    }

                    if (sgn2 == @"\geq")
                    {
                        if (x2 < x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }

                        if (x2 == x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }

                        if (x2 < x4 && x2>x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx3}]";
                        }

                        if (x2 < x4 && x3 == xx1)
                        {
                            var.Formula = $@"x = {xx3}";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx3}]\cup{{{xx2}}}";
                        }

                        if (x2 == x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in {{{xx4},{xxx1}}}";
                        }

                        if (x2 == x4 && xx1 > x3)
                        {
                            var.Formula = $@"x = {xx4}";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx3}]\cup[{xx4},{xx2}]";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in [{xx4},{xx2}]\cup{{{xx3}}}";
                        }

                        if (x2 > x4 && xx1 > x3 && xx1 <= x4)
                        {
                            var.Formula = $@"x\in [{xx4},{xx2}]";
                        }

                        if (xx1 > x4)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }
                    }

                    if (sgn2 == @"\gt")
                    {
                        if (x2 < x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }

                        if (x2 == x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2})";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx3})";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx3})";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx3})\cup({xx4},{xx2}]";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx4},{xx2}]";
                        }

                        if (x2 > x4 && xx1 > x3 && xx1 <= x4)
                        {
                            var.Formula = $@"x\in ({xx4},{xx2}]";
                        }

                        if (xx1 > x4)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }
                    }
                }

                if (sgn1 == @"\lt")
                {
                    if (sgn2 == @"\leq")
                    {
                        
                    }

                    if (sgn2 == @"\lt")
                    {
                        
                    }

                    if (sgn2 == @"\geq")
                    {
                       
                    }

                    if (sgn2 == @"\gt")
                    {
                        
                    }
                }

                if (sgn1 == @"\geq")
                {
                    if (sgn2 == @"\leq")
                    {
                        
                    }

                    if (sgn2 == @"\lt")
                    {
                        
                    }

                    if (sgn2 == @"\geq")
                    {
                        
                    }

                    if (sgn2 == @"\gt")
                    {
                       
                    }
                }

                if (sgn1 == @"\gt")
                {
                    if (sgn2 == @"\leq")
                    {
                       
                    }

                    if (sgn2 == @"\lt")
                    {
                        
                    }

                    if (sgn2 == @"\geq")
                    {
                       
                    }

                    if (sgn2 == @"\gt")
                    {
                        
                    }
                }
            }
        }

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