using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Ch24Ex06
{
    internal static class Program
    {
        // Метод исполняемый как задача.
        private static void MyTask()
        {
            WriteLine("MyTask() запущен.");

            for (var count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                WriteLine($"В методе MyTask() подсчёт равен {count}");
            }

            WriteLine("MyTask завершён");
        }

        // Метод, исполняемый как продолжение задачи.
        private static void ContTask(Task t)
        {
            WriteLine("Продолжение запущено.");

            for (var count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                WriteLine($"В продолжении подсчёт равен {count}");
            }

            WriteLine("Продолжение завершено.");
        }

        private static void Main()
        {
            WriteLine("Основной поток запущен");

            // Сконструировать объект первой задачи.
            var task = new Task(MyTask);

            // А теперь создать продолжение задачи.
            var taskCont = task.ContinueWith(ContTask);

            // Начать выполнение последовательности задач.
            task.Start();

            // Ожидать завершения задачи продолжения.
            taskCont.Wait();

            task.Dispose();
            taskCont.Dispose();

            WriteLine("Основной поток завершён.");
        }
    }
}
