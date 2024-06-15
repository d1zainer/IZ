using LiveCharts.Wpf;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IZ
{

    public partial class MainWindow : Window
    {
        private TextBox[,] textFields;

        private double x1;
        private double x2;
        private double x3;
        private double x4;

        public MainWindow()
        {
         
            InitializeComponent();
            InitializeTextBoxArray();
            this.ResizeMode = ResizeMode.NoResize;
            PlayerA.IsEnabled = false;
            PlayerB.IsEnabled = false;
        }

        private void InitializeTextBoxArray()
        {
            // Инициализируем массив текстовых полей
            textFields = new TextBox[2, 2]
            {
                { textBox11, textBox12, },
                { textBox21, textBox22 },
            };  
        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validation.ValidateInput(textBox11.Text) ||
                    !Validation.ValidateInput(textBox12.Text) ||
                    !Validation.ValidateInput(textBox21.Text) ||
                    !Validation.ValidateInput(textBox22.Text))
                {
                    MessageBox.Show("Вы ввели неправильные значения");
                    return;
                }

                x1 = Convert.ToDouble(textBox11.Text);
                x2 = Convert.ToDouble(textBox12.Text);
                x3 = Convert.ToDouble(textBox21.Text);
                x4 = Convert.ToDouble(textBox22.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Вы ввели неправильные значения");
                return;
            }


            Calculate calculate = new Calculate(PlayerACoordsTextBlock, PlayerAStrategiesTextBlock, PlayerAAverageTextBlock, PlayerBCoordsTextBlock, PlayerBStrategiesTextBlock, PlayerBAverageTextBlock);

            bool flagError = false;

            var taskA = Task.Run(() =>
            {
                try
                {
                    var resultA = calculate.Function(x1, x2, x3, x4, Player.A);
                    if (double.IsNaN(resultA.Item1) || double.IsNaN(resultA.Item2) || double.IsNaN(resultA.Item3) || double.IsNaN(resultA.Item4) || double.IsInfinity(resultA.Item1) || double.IsInfinity(resultA.Item2) || double.IsInfinity(resultA.Item3) || double.IsInfinity(resultA.Item4))
                    {
                        throw new Exception("Результаты вычислений для игрока A содержат недопустимые значения (NaN). Проверьте входные данные и попробуйте еще раз.");
                        
                    }
                    return resultA;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return (0.0, 0.0, 0.0, 0.0); // Возвращаем значение по умолчанию
                    
                }
               
            });

            var taskB = Task.Run(() =>
            {
                try
                {
                    var resultB = calculate.Function(x1, x2, x3, x4, Player.B);
                    if (double.IsNaN(resultB.Item1) || double.IsNaN(resultB.Item2) || double.IsNaN(resultB.Item3) || double.IsNaN(resultB.Item4) || double.IsInfinity(resultB.Item1) || double.IsInfinity(resultB.Item2) || double.IsInfinity(resultB.Item3) || double.IsInfinity(resultB.Item4))
                    {
                        throw new Exception("Результаты вычислений для игрока A содержат недопустимые значения (NaN). Проверьте входные данные и попробуйте еще раз.");
                       
                    }
                    return resultB;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return (0.0, 0.0, 0.0, 0.0); // Возвращаем значение по умолчанию
                }
            });

            try
            {
                await Task.WhenAll(taskA, taskB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                flagError = true;
            }

            if (!flagError)
            {
                calculate.PrintTextResult("A", taskA.Result);
                calculate.PrintTextResult("B", taskB.Result);

                PlayerA.IsEnabled = true;
                PlayerB.IsEnabled = true;

                PlayerB.IsChecked = true;
                Player_Checked(sender, e);
            }




        }

        private void Player_Checked(object sender, RoutedEventArgs e)
        {
         
            myChart.AxisY.Clear();
            myChart.AxisX.Clear();
            myChart.Series.Clear();

            if (PlayerA.IsChecked == true)
            {
                Graph graph = new Graph(myChart);
                graph.CreateLines(x1, x2, x3, x4, Player.A);
            }
            else if (PlayerB.IsChecked == true)
            {
                Graph graph = new Graph(myChart);
                graph.CreateLines(x1, x2, x3, x4, Player.B);
            }
        }




    }
}

