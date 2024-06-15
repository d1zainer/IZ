using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Windows;
using System.Windows.Media;

namespace IZ
{
    public class Graph
    {
        private readonly CartesianChart CartesianChart;
        public Graph(CartesianChart cartesianChart)
        {
            this.CartesianChart = cartesianChart;
        }


        public Point FindIntersection(double y1, double y2, double y3, double y4, Player player)
        {
            double m1, b1, m2, b2;

            if (player == Player.B)
            {
                 m1 = y2 - y1;
                 b1 = y1;

                 m2 = y4 - y3;
                 b2 = y3;
                // Находим x-координату точки пересечения
                double xIntersection = Math.Round((b2 - b1) / (m1 - m2), 2);

                // Подставляем найденную x-координату в уравнение первой линии для нахождения y-координаты
                double yIntersection = Math.Round((m1 * xIntersection + b1), 2);
                // Точка пересечения
                Point intersectionPoint = new Point(xIntersection, yIntersection);
                return intersectionPoint;
            }
            else 
            {
                 m1 = y3 - y1;
                 b1 = y1;

                 m2 = y4 - y2;
                 b2 = y2;
                // Находим x-координату точки пересечения
                double xIntersection = Math.Round((b2 - b1) / (m1 - m2), 2);

                // Подставляем найденную x-координату в уравнение первой линии для нахождения y-координаты
                double yIntersection = Math.Round((m1 * xIntersection + b1), 2);
                // Точка пересечения
                Point intersectionPoint = new Point(xIntersection, yIntersection);
                return intersectionPoint;
            }
            
           
        }
        public void CreateLines(double y1, double y2, double y3, double y4, Player player)
        {
            InitializeGraph(y1, y2, y3, y4);


            InitializeGraph(y1, y2, y3, y4);

            LineSeries lineY1 = new LineSeries();
            LineSeries lineY2 = new LineSeries();

            switch (player)
            {
                case Player.A:
                    lineY1 = new LineSeries
                    {
                        Title = "B1B1",
                        Values = new ChartValues<ObservablePoint>
                        {
                            new ObservablePoint(0, y1),
                            new ObservablePoint(1, y3)
                        },
                        Fill = Brushes.Transparent
                    };

                    lineY2 = new LineSeries
                    {
                        Title = "B2B2",
                        Values = new ChartValues<ObservablePoint>
                        {
                            new ObservablePoint(0, y2),
                            new ObservablePoint(1, y4)
                        },
                        Fill = Brushes.Transparent
                    };
                    break;
                case Player.B:
                    lineY1 = new LineSeries
                    {
                        Title = "A1A1",
                        Values = new ChartValues<ObservablePoint>
                        {
                            new ObservablePoint(0, y1),
                            new ObservablePoint(1, y2)
                        },
                        Fill = Brushes.Transparent
                    };

                    lineY2 = new LineSeries
                    {
                        Title = "A2A2",
                        Values = new ChartValues<ObservablePoint>
                        {
                            new ObservablePoint(0, y3),
                            new ObservablePoint(1, y4)
                        },
                        Fill = Brushes.Transparent
                    };
                    break;
                default:
                    break;
            }

            // Добавление обеих линий на график
            CartesianChart.Series.Add(lineY1);
            CartesianChart.Series.Add(lineY2);
            // Находим точку пересечения
            Point intersection = FindIntersection(y1, y2, y3, y4, player);

            // Добавляем маркер точки пересечения
            ScatterSeries scatterSeries = new ScatterSeries
            {
                Title = "Точка пересечения",
                Values = new ChartValues<ObservablePoint> { new ObservablePoint(intersection.X, intersection.Y) },
                PointGeometry = DefaultGeometries.Circle,
                Fill = Brushes.Green
            };

            LineSeries lineIntersection = new LineSeries
            {
                Title = "Перпендикуляр",
                Values = new ChartValues<ObservablePoint>
                {
                    // Добавление точек для второй линии
                    new ObservablePoint(intersection.X, intersection.Y),
                    new ObservablePoint(intersection.X, 0)
                },
                Fill = Brushes.Transparent // Установка прозрачного цвета для заполнения области под графиком
            };


            CartesianChart.Series.Add(scatterSeries);
            CartesianChart.Series.Add(lineIntersection);  




        }


        private void InitializeGraph(double y1, double y2, double y3, double y4)
        {
            double maxY = Math.Max(Math.Max(y1, y2), Math.Max(y3, y4));
            double minY = Math.Min(Math.Min(Math.Min(y1, y2), Math.Min(y3, y4)), 0);
            // Устанавливаем границы по оси Y
            CartesianChart.AxisY.Clear();
            CartesianChart.AxisY.Add(new Axis
            {
                MinValue = minY,
                MaxValue = maxY + 1 // Добавляем некоторый отступ для лучшей визуализации
            });
        }


    }
}
