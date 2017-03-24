using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Ch24Ex06
{
    internal class Program
    {
        // Метод исполняемый как задача.
        static void MyTask()
        {
            WriteLine("MyTask() запущен.");

            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                WriteLine($"В методе MyTask() подсчёт равен {count}");
            }

            WriteLine("MyTask завершён");
        }

        // Метод, исполняемый как продолжение задачи.
        static void ContTask(Task t)
        {
            WriteLine("Продолжение запущено.");

            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                WriteLine($"В продолжении подсчёт равен {count}");
            }

            WriteLine("Продолжение завершено.");
        }

        static void Main()
        {
            WriteLine("Основной поток запущен");

            // Сконструировать объект первой задачи.
            Task task = new Task(MyTask);

            // А теперь создать продолжение задачи.
            Task taskCont = task.ContinueWith(ContTask);

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
