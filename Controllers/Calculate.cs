using System;
using System.Windows.Controls;

public enum Player
{
    A,
    B
}
public class Calculate
{
    // Поля для хранения ссылок на текстовые блоки для игроков A и B
    private readonly TextBlock playerACoordsTextBlock;
    private readonly TextBlock playerAStrategiesTextBlock;
    private readonly TextBlock playerAAverageTextBlock;
    private readonly TextBlock playerBCoordsTextBlock;
    private readonly TextBlock playerBStrategiesTextBlock;
    private readonly TextBlock playerBAverageTextBlock;

    // Конструктор, принимающий ссылки на текстовые блоки
    public Calculate(TextBlock playerACoordsTextBlock, TextBlock playerAStrategiesTextBlock, TextBlock playerAAverageTextBlock,
                     TextBlock playerBCoordsTextBlock, TextBlock playerBStrategiesTextBlock, TextBlock playerBAverageTextBlock)
    {
        this.playerACoordsTextBlock = playerACoordsTextBlock;
        this.playerAStrategiesTextBlock = playerAStrategiesTextBlock;
        this.playerAAverageTextBlock = playerAAverageTextBlock;
        this.playerBCoordsTextBlock = playerBCoordsTextBlock;
        this.playerBStrategiesTextBlock = playerBStrategiesTextBlock;
        this.playerBAverageTextBlock = playerBAverageTextBlock;
    }
    public (double, double, double, double) Function(double x1, double x2, double x3, double x4, Player player)
    {
        double m1, m2, b1, b2;

        if (player == Player.B)
        {
            if (x1 == x2)
            {
                // Обработка деления на ноль для m1
                // Если x1 == x2, то m1 будет бесконечность или NaN
                m1 = double.NaN; // или double.NaN
            }
            else
            {
                m1 = (x1 - x2) / (1 - 0);
            }

            b1 = x2 - m1 * 0;

            if (x3 == x4)
            {
                // Обработка деления на ноль для m2
                // Если x3 == x4, то m2 будет бесконечность или NaN
                m2 = double.NaN; ; // или double.NaN
            }
            else
            {
                m2 = (x3 - x4) / (1 - 0);
            }

            b2 = x4 - m2 * 0;
        }
        else // Для игрока A
        {
            if (x1 == x3)
            {
                // Обработка деления на ноль для m1
                // Если x1 == x3, то m1 будет бесконечность или NaN
                m1 = double.NaN; // или double.NaN
            }
            else
            {
                m1 = (x3 - x1) / (1 - 0);
            }

            b1 = x1 - m1 * 0;

            if (x2 == x4)
            {
                // Обработка деления на ноль для m2
                // Если x2 == x4, то m2 будет бесконечность или NaN
                m2 = double.NaN; // или double.NaN
            }
            else
            {
                m2 = (x4 - x2) / (1 - 0);
            }

            b2 = x2 - m2 * 0;
        }


        double x, y, p1, p2;

        if (double.IsInfinity(m1) || double.IsInfinity(m2))
        {
            // Если m1 или m2 равны бесконечности, то x и y также будут равны бесконечности
            x = double.PositiveInfinity;
            y = double.PositiveInfinity;
            p1 = double.NaN;
            p2 = double.NaN;
        }
        else
        {
            x = (b2 - b1) / (m1 - m2);
            y = Math.Round((m1 * x + b1), 2);
            p1 = Math.Round((1 - x), 3);
            p2 = Math.Round(x, 3);
            x = Math.Round(x, 3);
        }

        return (x, y, p1, p2);
    }

    public void PrintTextResult(string namePlayer, (double, double, double, double) result)
    {
        var (x, y, p1, p2) = result;

        if (namePlayer == "A")
        {
            // Записываем результаты игрока A в соответствующие TextBlock элементы
            playerACoordsTextBlock.Text = $"Координаты точки М для игрока A: ({x}, {y})";
            playerAStrategiesTextBlock.Text = $"Смешанные стратегии игрока A: p1 = {p1}, p2 = {p2}";
            playerAAverageTextBlock.Text = $"Средний выигрыш игрока A: v = {y}";
        }
        else if (namePlayer == "B")
        {
            // Записываем результаты игрока B в соответствующие TextBlock элементы
            playerBCoordsTextBlock.Text = $"Координаты точки М для игрока B: ({x}, {y})";
            playerBStrategiesTextBlock.Text = $"Смешанные стратегии игрока B: q1 = {p2}, q2 = {p1}";
            playerBAverageTextBlock.Text = $"Средний выигрыш игрока B: v = {y}";
        }
    }
}