namespace App.Lab4;

public static class Logger
{
    public static string LogGeometry(IGeometry geometry)
    {
        return geometry switch
        {
            Square => "Квадрат",
            Rectangle => "Прямоугольник",
            Triangle => "Треугольник",
            _ => "Неизвестная геометрия"
        };
    }
}