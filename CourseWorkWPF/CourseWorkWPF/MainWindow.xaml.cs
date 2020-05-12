using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
//using Math = System.Math;

namespace CourseWorkWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Переменная для обычной генерации, не по ключу генерации
        private static Random gen = new Random();

        // Список для генерации по ключу генерации
        private List<Random> genList = new List<Random>();

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

        // Корни уравнений
        private double xx1, x2, x3, x4;

        // Тип задачи третьего уровня
        private int type;

        // Сид для генерации по ключу генерации
        private int seed;

        // Счетчик списка рандомов, увеличивается каждый раз при генерации по ключу генерации
        private int rCount = -1;

        private void Keygen_Click(object sender, RoutedEventArgs e)
        {
            string message = "Ключ генерации должен состоять из 4 цифр";

            // KeyBox - поле ввода для ключа генерации
            if (KeyBox.Text == "Введите ключ генерации...") MessageBox.Show("Вы не ввели ключ генерации!");
            if (KeyBox.Text.Length != 4)
            {
                MessageBox.Show(message);
                return;
            }
            foreach (var p in KeyBox.Text)
            {
                if (p < '0' || p > '9')
                {
                    MessageBox.Show(message);
                    return;
                }
            }

            butgen.Visibility = Visibility.Hidden;
            cbox.Visibility = Visibility.Hidden;
            cbox2.Visibility = Visibility.Hidden;
            bracket.Visibility = Visibility.Visible;
            formula.Visibility = Visibility.Visible;
            formula2.Visibility = Visibility.Visible;
            bnext.Visibility = Visibility.Visible;
            ansbut.Visibility = Visibility.Visible;
            keygen.Visibility = Visibility.Hidden;
            KeyBox.Visibility = Visibility.Hidden;

            rCount++;
            int.TryParse(KeyBox.Text, out seed);
            genList.Add(new Random(seed));
            cbox.Text = "";

            // Генерируем сложность и количество задач с заданным сидом
            cbox.Text += genList[rCount].Next(1, 4).ToString();
            taskCount = genList[rCount].Next(1, 11);
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

        // Обработка потери фокуса с поля ввода ключа генерации
        private void KeyBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (KeyBox.Text == "") KeyBox.Text = "Введите ключ генерации...";
        }

        private void KeyBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (KeyBox.Text == "Введите ключ генерации...") KeyBox.Text = "";
        }

        private void KeyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (KeyBox.Text.Length > 30)
            {
                KeyBox.Text = "";
                MessageBox.Show("Ключ генерации должен состоять из 4 цифр");
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Приложение для генерации задач на тему 'системы неравенств'.\n Для начала работы выберите сложность и количество задач " +
                "либо введите ключ генерации и нажмите кнопку 'сгенерировать задачи' или 'сгенерировать задачи с помощью ключа генерации' соответственно" +
                ". Ключ генерации должен состоять из 4 цифр. Чтобы посмотреть ответ на задачу, нажмите на кнопку 'показать ответ'. После решения всех задач, нажмите 'начать заново'" +
                " для возврата в главное меню либо 'выйти' для выхода из программы.", "Справка");
        }

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
            KeyBox.Text = "Введите ключ генерации...";
        }

        /// <summary>
        /// Красивый вывод для задач первого уровня
        /// </summary>
        private void BeatOut1(ref string a, ref string d)
        {
            if (a == "1") a = "";
            if (d == "1") d = "";
        }

        /// <summary>
        /// Красивый вывод для задач второго уровня
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
            // Если был введен корректный ключ генерации и нажата соответствующая кнопка для генерации по ключу, то идем по ветке false
            if (KeyBox.Text == "Введите ключ генерации...")
            {
                a = gen.Next(1, 30).ToString();
                b = gen.Next(1, 30).ToString();
                c = gen.Next(1, 30).ToString();
                d = gen.Next(1, 30).ToString();
                e = gen.Next(1, 30).ToString();
                f = gen.Next(1, 30).ToString();
                sgn1 = eqsigns[gen.Next(4)];
            }
            else
            {
                a = genList[rCount].Next(1, 30).ToString();
                b = genList[rCount].Next(1, 30).ToString();
                c = genList[rCount].Next(1, 30).ToString();
                d = genList[rCount].Next(1, 30).ToString();
                e = genList[rCount].Next(1, 30).ToString();
                f = genList[rCount].Next(1, 30).ToString();
                sgn1 = eqsigns[genList[rCount].Next(4)];
            }
            BeatOut1(ref a, ref d);
            formula.Formula = $"{a}x+{b} {sgn1} {c}";
            formula2.Formula = $"{d}x+{e} {sgn1} {f}";
        }

        /// <summary>
        /// Проверяет, имеет ли на данном наборе система неравенств решение
        /// </summary>
        /// <returns>true,если удовлетворяет</returns>
        private bool CheckQadr(string sgn1, string sgn2, double x1, double x2, double x3, double x4)
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
            if (KeyBox.Text == "Введите ключ генерации...")
            {
                sgn1 = eqsigns[gen.Next(4)];
                sgn2 = eqsigns[gen.Next(4)];
            }
            else
            {
                sgn1 = eqsigns[genList[rCount].Next(4)];
                sgn2 = eqsigns[genList[rCount].Next(4)];
            }

            // Генерируем коэффициенты квадратного уравнения, пока дискрминант не будет больше нуля
            do
            {
                if (KeyBox.Text == "Введите ключ генерации...")
                {
                    ai = 1;
                    bi = gen.Next(-10, 10);
                    ci = gen.Next(-10, 10);
                    di = gen.Next(-10, 10);
                    ei = 1;
                    fi = gen.Next(-10, 10);
                    gi = gen.Next(-10, 10);
                    hi = gen.Next(-10, 10);
                }
                else
                {
                    ai = 1;
                    bi = genList[rCount].Next(-10, 10);
                    ci = genList[rCount].Next(-10, 10);
                    di = genList[rCount].Next(-10, 10);
                    ei = 1;
                    fi = genList[rCount].Next(-10, 10); ;
                    gi = genList[rCount].Next(-10, 10);
                    hi = genList[rCount].Next(-10, 10);
                }

                dis1 = bi * bi - 4 * ai * (ci - di);
                dis2 = fi * fi - 4 * (gi - hi) * ei;
                xx1 = (-bi - Math.Pow(dis1, 0.5)) / 2;
                x2 = (-bi + Math.Pow(dis1, 0.5)) / 2;
                x3 = (-fi - Math.Pow(dis2, 0.5)) / 2;
                x4 = (-fi + Math.Pow(dis2, 0.5)) / 2;
            } while (bi * bi - 4 * ai * (ci - di) <= 0 || fi * fi - 4 * ei * (gi - hi) <= 0 || ci == 0 || gi == 0 || bi == 0 || fi == 0 || !CheckQadr(sgn1, sgn2, xx1, x2, x3, x4));

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
        /// Проверяет, имеет ли система логарифмических неравенств на данном наборе коэффициентов решения
        /// </summary>
        /// <param name="sgn1">Знак перового неравенства</param>
        /// <param name="sgn2">Знак второго неравенства</param>
        /// <param name="x2">максимальный  корень из уравнения одз</param>
        /// <param name="x3">корень из первого неравенства</param>
        /// <param name="x4">корень из второго неравенства</param>
        /// <returns>true,если удовлетворяет</returns>
        private bool CheckLog(string sgn1, string sgn2, double x2, double x3, double x4, double ai, double di)
        {
            if (ai < 1)
            {
                if (sgn1 == eqsigns[0]) sgn1 = eqsigns[1];
                if (sgn1 == eqsigns[1]) sgn1 = eqsigns[0];
                if (sgn1 == eqsigns[2]) sgn1 = eqsigns[3];
                if (sgn1 == eqsigns[3]) sgn1 = eqsigns[2];
            }

            if (di < 1)
            {
                if (sgn2 == eqsigns[0]) sgn2 = eqsigns[1];
                if (sgn2 == eqsigns[1]) sgn2 = eqsigns[0];
                if (sgn2 == eqsigns[2]) sgn2 = eqsigns[3];
                if (sgn2 == eqsigns[3]) sgn2 = eqsigns[2];
            }

            if (x2 >= Math.Min(x3, x4)) return false;

            if (sgn1 == @"\leq")
            {
                if (sgn2 == @"\leq")
                {
                    return true;
                }

                if (sgn2 == @"\lt")
                {
                    return true;
                }

                if (sgn2 == @"\geq")
                {
                    if (x4 <= x3) return true;
                }

                if (sgn2 == @"\gt")
                {
                    if (x4 < x3) return true;
                }
            }

            if (sgn1 == @"\lt")
            {
                if (sgn2 == @"\leq")
                {
                    return true;
                }

                if (sgn2 == @"\lt")
                {
                    return true;
                }

                if (sgn2 == @"\geq")
                {
                    if (x4 < x3) return true;
                }

                if (sgn2 == @"\gt")
                {
                    if (x4 < x3) return true;
                }
            }

            if (sgn1 == @"\geq")
            {
                if (sgn2 == @"\leq")
                {
                    if (x3 <= x4) return true;
                }

                if (sgn2 == @"\lt")
                {
                    if (x3 < x4) return true;
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
                    if (x3 < x4) return true;
                }

                if (sgn2 == @"\lt")
                {
                    if (x3 < x4) return true;
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
        /// Проверяет, имеет ли система показательных неравенств на данном наборе коэффициентов решения
        /// </summary>
        /// <returns>true,если удовлетворяет</returns>
        private bool CheckLog2(string sgn1, string sgn2, double x3, double x4)
        {
            if (sgn1 == @"\leq")
            {
                if (sgn2 == @"\leq")
                {
                    return true;
                }

                if (sgn2 == @"\lt")
                {
                    return true;
                }

                if (sgn2 == @"\geq")
                {
                    if (x4 <= x3) return true;
                }

                if (sgn2 == @"\gt")
                {
                    if (x4 < x3) return true;
                }
            }

            if (sgn1 == @"\lt")
            {
                if (sgn2 == @"\leq")
                {
                    return true;
                }

                if (sgn2 == @"\lt")
                {
                    return true;
                }

                if (sgn2 == @"\geq")
                {
                    if (x4 < x3) return true;
                }

                if (sgn2 == @"\gt")
                {
                    if (x4 < x3) return true;
                }
            }

            if (sgn1 == @"\geq")
            {
                if (sgn2 == @"\leq")
                {
                    if (x3 <= x4) return true;
                }

                if (sgn2 == @"\lt")
                {
                    if (x3 < x4) return true;
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
                    if (x3 < x4) return true;
                }

                if (sgn2 == @"\lt")
                {
                    if (x3 < x4) return true;
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
        /// Генератор задач третьего уровня сложности
        /// </summary>
        private void GenTask3()
        {
            // Если был введен корректный ключ генерации и нажата соответствующая кнопка для генерации по ключу, то идем по ветке false
            if (KeyBox.Text == "Введите ключ генерации...")
            {
                // Определяем тип неравенств - с показательными функциями или с логарифмом
                if ((type = gen.Next(2)) == 1)
                {
                    do
                    {
                        ai = gen.Next(1, 100) * 0.1;
                        bi = gen.Next(2, 10);
                        ci = gen.Next(2, 10);
                        di = gen.Next(1, 100) * 0.1;
                        ei = gen.Next(2, 10);
                        fi = gen.Next(2, 10);
                        sgn1 = eqsigns[gen.Next(4)];
                        sgn2 = eqsigns[gen.Next(4)];
                        xx1 = -ci / bi;
                        x2 = -fi / ei;
                        x3 = -(ci - 1) / bi;
                        x4 = -(fi - 1) / ei;
                    } while (ai == 1 || di == 1 || !CheckLog(sgn1, sgn2, Math.Max(xx1, x2), x3, x4, ai, di) ||
                             x3 == x4);

                    formula.Formula = $@"log_{{{ai}}}({bi}x+{ci}) {sgn1} 0";
                    formula2.Formula = $@"log_{{{di}}}({ei}x+{fi}) {sgn2} 0";
                }
                else
                {
                    do
                    {
                        ai = gen.Next(2, 30);
                        bi = gen.Next(2, 30);
                        ci = gen.Next(2, 30);
                        di = gen.Next(2, 30);
                        sgn1 = eqsigns[gen.Next(4)];
                        sgn2 = eqsigns[gen.Next(4)];
                        x3 = Math.Log(bi, ai);
                        x4 = Math.Log(di, ci);
                    } while (x3 == x4 || !CheckLog2(sgn1, sgn2, x3, x4));

                    formula.Formula = $@"{ai}^x {sgn1} {bi}";
                    formula2.Formula = $@"{ci}^x {sgn2} {di}";
                }
            }
            else
            {
                if ((type = genList[rCount].Next(2)) == 1)
                {
                    do
                    {
                        ai = genList[rCount].Next(1, 100) * 0.1;
                        bi = genList[rCount].Next(2, 10);
                        ci = genList[rCount].Next(2, 10);
                        di = genList[rCount].Next(1, 100) * 0.1;
                        ei = genList[rCount].Next(2, 10);
                        fi = genList[rCount].Next(2, 10);
                        sgn1 = eqsigns[genList[rCount].Next(4)];
                        sgn2 = eqsigns[genList[rCount].Next(4)];
                        xx1 = -ci / bi;
                        x2 = -fi / ei;
                        x3 = -(ci - 1) / bi;
                        x4 = -(fi - 1) / ei;
                    } while (ai == 1 || di == 1 || !CheckLog(sgn1, sgn2, Math.Max(xx1, x2), x3, x4, ai, di) ||
                             x3 == x4);

                    formula.Formula = $@"log_{{{ai}}}({bi}x+{ci}) {sgn1} 0";
                    formula2.Formula = $@"log_{{{di}}}({ei}x+{fi}) {sgn2} 0";
                }
                else
                {
                    do
                    {
                        ai = genList[rCount].Next(2, 30);
                        bi = genList[rCount].Next(2, 30);
                        ci = genList[rCount].Next(2, 30);
                        di = genList[rCount].Next(2, 30);
                        sgn1 = eqsigns[genList[rCount].Next(4)];
                        sgn2 = eqsigns[genList[rCount].Next(4)];
                        x3 = Math.Log(bi, ai);
                        x4 = Math.Log(di, ci);
                    } while (x3 == x4 || !CheckLog2(sgn1, sgn2, x3, x4));

                    formula.Formula = $@"{ai}^x {sgn1} {bi}";
                    formula2.Formula = $@"{ci}^x {sgn2} {di}";
                }
            }
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

        // Обработчик клика кнопки ">>"
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

        // Обаботчик кнопки "Выйти"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        // Обработчик кнопки "Начать заново"
        private void Rest_Click(object sender, RoutedEventArgs e)
        {
            rest.Visibility = Visibility.Hidden;
            exit.Visibility = Visibility.Hidden;
            butgen.Visibility = Visibility.Visible;
            cbox.Visibility = Visibility.Visible;
            cbox2.Visibility = Visibility.Visible;
            keygen.Visibility = Visibility.Visible;
            KeyBox.Visibility = Visibility.Visible;
            cbox.SelectedIndex = 0;
            cbox2.SelectedIndex = 0;
        }

        // Обработчик кнопки "Сгенерировать задачи"
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
            butgen.Visibility = Visibility.Hidden;
            cbox.Visibility = Visibility.Hidden;
            cbox2.Visibility = Visibility.Hidden;
            keygen.Visibility = Visibility.Hidden;
            KeyBox.Visibility = Visibility.Hidden;
            bracket.Visibility = Visibility.Visible;
            formula.Visibility = Visibility.Visible;
            formula2.Visibility = Visibility.Visible;
            bnext.Visibility = Visibility.Visible;
            ansbut.Visibility = Visibility.Visible;
            KeyBox.Text = "Введите ключ генерации...";
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

        // Обработчик кнопки "Показать ответ"
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

                // Корни
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
                double negbi = -bi, negfi = -fi;
                string xxx1 = $@"\frac{{{negbi}-\sqrt{{{dis1}}}}}{{{2}}}",
                    xx2 = $@"\frac{{{negbi}+\sqrt{{{dis1}}}}}{{{2}}}",
                    xx3 = $@"\frac{{{negfi}-\sqrt{{{dis2}}}}}{{{2}}}",
                    xx4 = $@"\frac{{{negfi}+\sqrt{{{dis2}}}}}{{{2}}}";
                // Рассматриваем все возможные варианты взаимного расположения корней на прямой
                if (sgn1 == @"\leq")
                {
                    if (sgn2 == @"\leq")
                    {
                        if (x2 == x3)
                        {
                            var.Formula = "x = " + xx2;
                        }

                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xx3},{xx2}]";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 == x3)
                        {
                            var.Formula = $@"x\in [{xx3},{xx2}]";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 > x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xx3},{xx2}]";
                        }

                        if (x2 == x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }

                        if (x2 == x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xx3},{xx4}]";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in [{xx3},{xx4}]";
                        }

                        if (x2 > x4 && xx1 < x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx4}]";
                        }

                        if (xx1 == x4)
                        {
                            var.Formula = $@"x = {xx4}";
                        }
                    }

                    if (sgn2 == @"\lt")
                    {
                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx2}]";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx2}]";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 > x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2}]";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx2})";
                        }

                        if (x2 == x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 == x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx2})";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }

                        if (x2 > x4 && xx1 < x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in [{xxx1},{xx4})";
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

                        if (x2 < x4 && x2 > x3 && xx1 < x3)
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

                        if (x2 > x4 && xx1 > x3 && xx1 < x4)
                        {
                            var.Formula = $@"x\in [{xx4},{xx2}]";
                        }

                        if (x2 > x4 && xx1 == x4)
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

                        if (x2 > x4 && xx1 > x3 && xx1 < x4)
                        {
                            var.Formula = $@"x\in ({xx4},{xx2}]";
                        }

                        if (x2 > x4 && xx1 == x4)
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
                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xx3},{xx2})";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx2})";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 > x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xx3},{xx2})";
                        }

                        if (x2 == x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 == x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in [{xx3},{xx4}]";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4}]";
                        }

                        if (x2 > x4 && xx1 < x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx4}]";
                        }
                    }

                    if (sgn2 == @"\lt")
                    {
                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx2})";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx2})";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 > x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx2})";
                        }

                        if (x2 == x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 == x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }

                        if (x2 > x4 && xx1 < x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx4})";
                        }
                    }

                    if (sgn2 == @"\geq")
                    {
                        if (x2 < x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 == x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx3}]";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx3}]";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx3}]\cup[{xx4},{xx2})";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in [{xx4},{xx2})";
                        }

                        if (x2 > x4 && xx1 > x3 && xx1 < x4)
                        {
                            var.Formula = $@"x\in [{xx4},{xx2})";
                        }

                        if (x2 > x4 && xx1 == x4)
                        {
                            var.Formula = $@"x\in ({xx4},{xx2})";
                        }

                        if (xx1 > x4)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }
                    }

                    if (sgn2 == @"\gt")
                    {
                        if (x2 < x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 == x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx3})";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx3})";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx3})\cup({xx4},{xx2})";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx4},{xx2})";
                        }

                        if (x2 > x4 && xx1 > x3 && xx1 < x4)
                        {
                            var.Formula = $@"x\in ({xx4},{xx2})";
                        }

                        if (x2 > x4 && xx1 == x4)
                        {
                            var.Formula = $@"x\in ({xx4},{xx2})";
                        }

                        if (xx1 > x4)
                        {
                            var.Formula = $@"x\in ({xxx1},{xx2})";
                        }
                    }
                }

                if (sgn1 == @"\geq")
                {
                    if (sgn2 == @"\leq")
                    {
                        if (x4 < xx1)
                        {
                            var.Formula = $@"x\in [{xx3},{xx4}]";
                        }

                        if (x4 == xx1)
                        {
                            var.Formula = $@"x\in [{xx3},{xx4}]";
                        }

                        if (x4 < x2 && x4 > xx1 && x3 < xx1)
                        {
                            var.Formula = $@"x\in [{xx3},{xxx1}]";
                        }

                        if (x4 < x2 && xx1 == x3)
                        {
                            var.Formula = $@"x = {xxx1}";
                        }

                        if (x2 == x4 && x3 < xx1)
                        {
                            var.Formula = $@"x\in [{xx3},{xxx1}]\cup{{{xx4}}}";
                        }

                        if (x2 == x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in {{{xx2},{xx3}}}";
                        }

                        if (x2 == x4 && x3 > xx1)
                        {
                            var.Formula = $@"x = {xx2}";
                        }

                        if (x4 > x2 && x3 < xx1)
                        {
                            var.Formula = $@"x\in [{xx3},{xxx1}]\cup[{xx2},{xx4}]";
                        }

                        if (x4 > x2 && xx1 == x3)
                        {
                            var.Formula = $@"x\in [{xx2},{xx4}]\cup{{{xxx1}}}";
                        }

                        if (x4 > x2 && x3 > xx1 && x3 < x2)
                        {
                            var.Formula = $@"x\in [{xx2},{xx4}]";
                        }

                        if (x4 > x2 && x3 == x2)
                        {
                            var.Formula = $@"x\in [{xx2},{xx4}]";
                        }

                        if (x3 > x2)
                        {
                            var.Formula = $@"x\in [{xx3},{xx4}]";
                        }
                    }

                    if (sgn2 == @"\lt")
                    {
                        if (x4 < xx1)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }

                        if (x4 == xx1)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }

                        if (x4 < x2 && x4 > xx1 && x3 < xx1)
                        {
                            var.Formula = $@"x\in ({xx3},{xxx1}]";
                        }

                        if (x2 == x4 && x3 < xx1)
                        {
                            var.Formula = $@"x\in ({xx3},{xxx1}]";
                        }

                        if (x4 > x2 && x3 < xx1)
                        {
                            var.Formula = $@"x\in ({xx3},{xxx1}]\cup[{xx2},{xx4})";
                        }

                        if (x4 > x2 && xx1 == x3)
                        {
                            var.Formula = $@"x\in [{xx2},{xx4})";
                        }

                        if (x4 > x2 && x3 > xx1 && x3 < x2)
                        {
                            var.Formula = $@"x\in [{xx2},{xx4})";
                        }

                        if (x4 > x2 && x3 == x2)
                        {
                            var.Formula = $@"x\in ({xx2},{xx4})";
                        }

                        if (x3 > x2)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }
                    }

                    if (sgn2 == @"\geq")
                    {
                        if (x2 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup[{xx2},{xx3}]\cup[{xx4},\infty)";
                        }

                        if (x2 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup{{{xx2}}}\cup[{xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup[{xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup[{xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 > x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup[{xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup[{xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup[{xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup[{xx4},\infty)";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup[{xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup[{xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 > x3 && xx1 < x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup[{xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 == x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup[{xx2},\infty)";
                        }

                        if (xx1 > x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup[{xx4},{xxx1}]\cup[{xx2},\infty)";
                        }
                    }

                    if (sgn2 == @"\gt")
                    {
                        if (x2 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup[{xx2},{xx3})\cup({xx4},\infty)";
                        }

                        if (x2 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup({xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup({xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 > x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup({xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx4},\infty)";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1}]\cup[{xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup[{xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 > x3 && xx1 < x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup[{xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 == x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup[{xx2},\infty)";
                        }

                        if (xx1 > x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx4},{xxx1}]\cup[{xx2},\infty)";
                        }
                    }
                }

                if (sgn1 == @"\gt")
                {
                    if (sgn2 == @"\leq")
                    {
                        if (x4 < xx1)
                        {
                            var.Formula = $@"x\in [{xx3},{xx4}]";
                        }

                        if (x4 == xx1)
                        {
                            var.Formula = $@"x\in [{xx3},{xx4})";
                        }

                        if (x4 < x2 && x4 > xx1 && x3 < xx1)
                        {
                            var.Formula = $@"x\in [{xx3},{xxx1})";
                        }

                        if (x2 == x4 && x3 < xx1)
                        {
                            var.Formula = $@"x\in [{xx3},{xxx1})";
                        }

                        if (x4 > x2 && x3 < xx1)
                        {
                            var.Formula = $@"x\in [{xx3},{xxx1})\cup({xx2},{xx4}]";
                        }

                        if (x4 > x2 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx2},{xx4}]";
                        }

                        if (x4 > x2 && x3 > xx1 && x3 < x2)
                        {
                            var.Formula = $@"x\in ({xx2},{xx4}]";
                        }

                        if (x4 > x2 && x3 == x2)
                        {
                            var.Formula = $@"x\in ({xx2},{xx4}]";
                        }

                        if (x3 > x2)
                        {
                            var.Formula = $@"x\in [{xx3},{xx4}]";
                        }
                    }

                    if (sgn2 == @"\lt")
                    {
                        if (x4 < xx1)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }

                        if (x4 == xx1)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }

                        if (x4 < x2 && x4 > xx1 && x3 < xx1)
                        {
                            var.Formula = $@"x\in ({xx3},{xxx1})";
                        }

                        if (x2 == x4 && x3 < xx1)
                        {
                            var.Formula = $@"x\in ({xx3},{xxx1})";
                        }

                        if (x4 > x2 && x3 < xx1)
                        {
                            var.Formula = $@"x\in ({xx3},{xxx1})\cup({xx2},{xx4})";
                        }

                        if (x4 > x2 && xx1 == x3)
                        {
                            var.Formula = $@"x\in ({xx2},{xx4})";
                        }

                        if (x4 > x2 && x3 > xx1 && x3 < x2)
                        {
                            var.Formula = $@"x\in ({xx2},{xx4})";
                        }

                        if (x4 > x2 && x3 == x2)
                        {
                            var.Formula = $@"x\in ({xx2},{xx4})";
                        }

                        if (x3 > x2)
                        {
                            var.Formula = $@"x\in ({xx3},{xx4})";
                        }
                    }

                    if (sgn2 == @"\geq")
                    {
                        if (x2 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx2},{xx3}]\cup[{xx4},\infty)";
                        }

                        if (x2 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup[{xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup[{xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup[{xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 > x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup[{xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup({xx4},\infty)";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 > x3 && xx1 < x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup({xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 == x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup({xx2},\infty)";
                        }

                        if (xx1 > x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3}]\cup[{xx4},{xxx1})\cup({xx2},\infty)";
                        }
                    }

                    if (sgn2 == @"\gt")
                    {
                        if (x2 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx2},{xx3})\cup({xx4},\infty)";
                        }

                        if (x2 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx4},\infty)";
                        }

                        if (x2 < x4 && x2 > x3 && xx1 > x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx4},\infty)";
                        }

                        if (x2 == x4 && xx1 > x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx4},\infty)";
                        }

                        if (x2 > x4 && xx1 < x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 == x3)
                        {
                            var.Formula = $@"x\in (-\infty,{xxx1})\cup({xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 > x3 && xx1 < x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx2},\infty)";
                        }

                        if (x2 > x4 && xx1 == x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx2},\infty)";
                        }

                        if (xx1 > x4)
                        {
                            var.Formula = $@"x\in (-\infty,{xx3})\cup({xx4},{xxx1})\cup({xx2},\infty)";
                        }
                    }
                }
            }

            if (cbox.Text == "3" && type == 1)
            {
                string xx2, xx3, xx4;
                if (-ci / bi > -fi / ei) xx2 = $@"\frac{{{-ci}}}{{{bi}}}";
                else xx2 = $@"\frac{{{-fi}}}{{{ci}}}";
                xx3 = $@"\frac{{{-(ci - 1)}}}{{{bi}}}";
                xx4 = $@"\frac{{{-(fi - 1)}}}{{{ei}}}";
                if (ai < 1)
                {
                    if (sgn1 == eqsigns[0]) sgn1 = eqsigns[1];
                    if (sgn1 == eqsigns[1]) sgn1 = eqsigns[0];
                    if (sgn1 == eqsigns[2]) sgn1 = eqsigns[3];
                    if (sgn1 == eqsigns[3]) sgn1 = eqsigns[2];
                }

                if (di < 1)
                {
                    if (sgn2 == eqsigns[0]) sgn2 = eqsigns[1];
                    if (sgn2 == eqsigns[1]) sgn2 = eqsigns[0];
                    if (sgn2 == eqsigns[2]) sgn2 = eqsigns[3];
                    if (sgn2 == eqsigns[3]) sgn2 = eqsigns[2];
                }
                if (sgn1 == @"\leq")
                {
                    if (sgn2 == @"\leq")
                    {
                        if (x3 < x4) var.Formula = $@"x\in ({xx2},{xx3}]";
                        else var.Formula = $@"x\in ({xx2},{xx4}]";
                    }

                    if (sgn2 == @"\lt")
                    {
                        if (x3 < x4) var.Formula = $@"x\in ({xx2},{xx3}]";
                        else var.Formula = $@"x\in ({xx2},{xx4})";
                    }

                    if (sgn2 == @"\geq")
                    {
                        var.Formula = $@"x\in [{xx4},{xx3}]";
                    }

                    if (sgn2 == @"\gt")
                    {
                        var.Formula = $@"x\in ({xx4},{xx3}]";
                    }
                }

                if (sgn1 == @"\lt")
                {
                    if (sgn2 == @"\leq")
                    {
                        if (x3 < x4) var.Formula = $@"x\in ({xx2},{xx3})";
                        else var.Formula = $@"x\in ({xx2},{xx4}]";
                    }

                    if (sgn2 == @"\lt")
                    {
                        if (x3 < x4) var.Formula = $@"x\in ({xx2},{xx3})";
                        else var.Formula = $@"x\in ({xx2},{xx4})";
                    }

                    if (sgn2 == @"\geq")
                    {
                        var.Formula = $@"x\in [{xx4},{xx3})";
                    }

                    if (sgn2 == @"\gt")
                    {
                        var.Formula = $@"x\in ({xx4},{xx3})";
                    }
                }

                if (sgn1 == @"\geq")
                {
                    if (sgn2 == @"\leq")
                    {
                        var.Formula = $@"x\in [{xx3},{xx4}]";
                    }

                    if (sgn2 == @"\lt")
                    {
                        var.Formula = $@"x\in [{xx3},{xx4})";
                    }

                    if (sgn2 == @"\geq")
                    {
                        if (x3 > x4) var.Formula = $@"x\in [{xx3},\infty)";
                        else var.Formula = $@"x\in [{xx4},\infty)";
                    }

                    if (sgn2 == @"\gt")
                    {
                        if (x3 > x4) var.Formula = $@"x\in [{xx3},\infty)";
                        else var.Formula = $@"x\in ({xx4},\infty)";
                    }
                }

                if (sgn1 == @"\gt")
                {
                    if (sgn2 == @"\leq")
                    {
                        var.Formula = $@"x\in ({xx3},{xx4}]";
                    }

                    if (sgn2 == @"\lt")
                    {
                        var.Formula = $@"x\in ({xx3},{xx4})";
                    }

                    if (sgn2 == @"\geq")
                    {
                        if (x3 > x4) var.Formula = $@"x\in ({xx3},\infty)";
                        else var.Formula = $@"x\in [{xx4},\infty)";
                    }

                    if (sgn2 == @"\gt")
                    {
                        if (x3 > x4) var.Formula = $@"x\in ({xx3},\infty)";
                        else var.Formula = $@"x\in ({xx4},\infty)";
                    }
                }
            }

            if (cbox.Text == "3" && type == 0)
            {
                string xx3, xx4;
                if (bi == 1) xx3 = "0";
                else
                {
                    if (bi == ai) xx3 = "1";
                    else xx3 = $@"log_{{{ai}}}{bi}";
                }
                if (di == 1) xx4 = "0";
                else
                {
                    if (di == ci) xx4 = "1";
                    else xx4 = $@"log_{{{ci}}}{di}";
                }

                if (sgn1 == @"\leq")
                {
                    if (sgn2 == @"\leq")
                    {
                        if (x3 < x4) var.Formula = $@"x\in (-\infty,{xx3}]";
                        else var.Formula = $@"x\in (-\infty,{xx4}]";
                    }

                    if (sgn2 == @"\lt")
                    {
                        if (x3 < x4) var.Formula = $@"x\in (-\infty,{xx3}]";
                        else var.Formula = $@"x\in (-\infty,{xx4})";
                    }

                    if (sgn2 == @"\geq")
                    {
                        var.Formula = $@"x\in [{xx4},{xx3}]";
                    }

                    if (sgn2 == @"\gt")
                    {
                        var.Formula = $@"x\in ({xx4},{xx3}]";
                    }
                }

                if (sgn1 == @"\lt")
                {
                    if (sgn2 == @"\leq")
                    {
                        if (x3 < x4) var.Formula = $@"x\in (-\infty,{xx3})";
                        else var.Formula = $@"x\in (-\infty,{xx4}]";
                    }

                    if (sgn2 == @"\lt")
                    {
                        if (x3 < x4) var.Formula = $@"x\in (-\infty,{xx3})";
                        else var.Formula = $@"x\in (-\infty,{xx4})";
                    }

                    if (sgn2 == @"\geq")
                    {
                        var.Formula = $@"x\in [{xx4},{xx3})";
                    }

                    if (sgn2 == @"\gt")
                    {
                        var.Formula = $@"x\in ({xx4},{xx3})";
                    }
                }

                if (sgn1 == @"\geq")
                {
                    if (sgn2 == @"\leq")
                    {
                        var.Formula = $@"x\in [{xx3},{xx4}]";
                    }

                    if (sgn2 == @"\lt")
                    {
                        var.Formula = $@"x\in [{xx3},{xx4})";
                    }

                    if (sgn2 == @"\geq")
                    {
                        if (x3 > x4) var.Formula = $@"x\in [{xx3},\infty)";
                        else var.Formula = $@"x\in [{xx4},\infty)";
                    }

                    if (sgn2 == @"\gt")
                    {
                        if (x3 > x4) var.Formula = $@"x\in [{xx3},\infty)";
                        else var.Formula = $@"x\in ({xx4},\infty)";
                    }
                }

                if (sgn1 == @"\gt")
                {
                    if (sgn2 == @"\leq")
                    {
                        var.Formula = $@"x\in ({xx3},{xx4}]";
                    }

                    if (sgn2 == @"\lt")
                    {
                        var.Formula = $@"x\in ({xx3},{xx4})";
                    }

                    if (sgn2 == @"\geq")
                    {
                        if (x3 > x4) var.Formula = $@"x\in ({xx3},\infty)";
                        else var.Formula = $@"x\in [{xx4},\infty)";
                    }

                    if (sgn2 == @"\gt")
                    {
                        if (x3 > x4) var.Formula = $@"x\in ({xx3},\infty)";
                        else var.Formula = $@"x\in ({xx4},\infty)";
                    }
                }
            }
        }

        // Обработчики разворачивания и сворачивания combobox с выбором количества и сложности задач
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