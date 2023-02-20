//Напишите программу, которая создает 10 задач на выполнение некоторой работы, используя класс Task.
//Каждая задача должна вычислять квадрат числа от 1 до 10 и выводить его на консоль.
//Каждая задача должна быть выполнена в отдельном потоке.
//После запуска всех задач, программа должна ждать их завершения и выводить сообщение о завершении работы каждой задачи.

//=====================================================
//Task(Action<object?> action, object? state);

int count = 10;
Random random = new Random();
var tasks = new Task[count];

for (int i = 0; i < count; i++)
{
    tasks[i] = new Task(() => { MyTask(random.Next(1,10)); });
    tasks[i].Start();
}

for (int i = 0; i < count; i++)
{
    tasks[i].Wait();
    Console.WriteLine($"MyTask #{i} - ended work ");
}


void MyTask(int number)
{
    Console.WriteLine(number * number);
}
