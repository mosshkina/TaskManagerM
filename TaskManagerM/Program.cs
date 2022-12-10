using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerM;

namespace TaskManagerM
{
    class Program
    {
        static void Main(string[] args)
        {
            Employer employer = new Employer("Евгений", "Карчевский");
            int count_workers = 10;
            List<Worker> workers = new List<Worker>(count_workers);
            workers.Add(new Worker("Мария", "Мошкина"));
            workers.Add(new Worker("Денис", "Сабаев"));
            workers.Add(new Worker("Юлия", "Лешина"));
            workers.Add(new Worker("Настя", "Сучок"));
            workers.Add(new Worker("Катя", "Мирская"));
            workers.Add(new Worker("Маша", "Головина"));
            workers.Add(new Worker("Аделя", "Гильфанова"));
            workers.Add(new Worker("Леша", "Носков"));
            workers.Add(new Worker("Денис", "Шумахер"));
            workers.Add(new Worker("Ильдар", "Ахметов"));
            Customer customer = new Customer("Фермерсий магазин Волков ");
            List<Task> tasks = new List<Task>(count_workers);
            tasks.Add(new Task("Арендовать помещения", employer));
            tasks.Add(new Task("Создать вывеску", employer));
            tasks.Add(new Task("Купить рекламу", employer));
            tasks.Add(new Task("Закупить продукты", employer));
            tasks.Add(new Task("Подсчитать расходы", employer));
            tasks.Add(new Task("Провести опрос среди клиентов", employer));
            tasks.Add(new Task("Закупить новые холодильники", employer));
            tasks.Add(new Task("Сделать пост в инстаграмме про акции и скидки", employer));
            tasks.Add(new Task("Улучшить качество сервиса", employer));
            tasks.Add(new Task("Обновить каталог на сайте", employer));
            DateTime date = DateTime.Now.AddDays(365);
            Project project = new Project("Открытие нового магазина", date, employer, customer);
            project.AddTasksInProject(tasks);
            Worker.GiveTask(workers, tasks);
            Console.ReadKey();
            Console.Clear();
            while (workers.Count > 0)
            {
                Console.WriteLine("Чтобы сдать отчет, выберите сотрудника");
                Console.WriteLine("Сотрудники");
                for (int i = 0; i < workers.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {workers[i].Print()}");
                }
                int index;
                while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > workers.Count)
                {
                    Console.WriteLine("Неверный ввод");
                }
                Report report = Report.CreateReport(workers[index - 1]);
                Console.WriteLine($"Утверждает ли инициатор отчёт сотрудника {workers[index - 1].Print()}");
                Console.WriteLine("Отчёт:");
                report.Print();
                string answer = Console.ReadLine().ToLower();
                while (!answer.Equals("да") && !answer.Equals("нет"))
                {
                    Console.WriteLine("Повторите ввод");
                    answer = Console.ReadLine();
                }
                if (answer.Equals("да"))
                {
                    Task.CloseTask(workers[index - 1]);
                    workers.Remove(workers[index - 1]);
                    report = null;
                }
                else
                {
                    Console.WriteLine("Отчет не завершен");
                    report = null;
                }
            }
            project.CloseProject();
        }
    }
}