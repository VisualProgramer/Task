//Задание 2: Обработка большого объема данных с помощью многопоточности.
//В этом задании нужно обработать большой объем данных с помощью многопоточности.
//Для выполнения задания нужно выполнить следующие шаги:
//Написать функцию, которая будет обрабатывать данные. Это может быть, например, функция, которая будет сортировать большой массив данных.
//Сгенерировать большой объем данных, который будет обрабатываться. Для этого можно использовать функцию Enumerable.Range для генерации большого списка чисел.
//Разбить список данных на несколько частей и запустить для каждой части отдельный поток.
//После завершения всех потоков собрать результаты и объединить их в общий список.

int count = 1000;
Random random = new Random();
IEnumerable<int> Numbers = Enumerable.Range(1, count).Select(x => random.Next(1, 1000));
var arr = Numbers.ToArray();

Console.ForegroundColor = ConsoleColor.Blue;
Console.Write("Array: ");
foreach (int number in arr)
{
    Console.Write(number + " ");
}

arr = Task<int[]>.Run(() => { return Devide(arr); }).Result;

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("\n\nArray after sorting: ");
foreach (int number in arr)
{
    Console.Write(number + " ");
}
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine();

int[] Devide(int[] array)
{
    if (array.Length == 1)
    {
        return array;
    }

    int middle = array.Length / 2;
    return MergeAndSort(Devide(array.Take(middle).ToArray()), Devide(array.Skip(middle).ToArray()));
}

int[] MergeAndSort(int[] arr1, int[] arr2)
{
    int ptr1 = 0, ptr2 = 0;
    int[] merged = new int[arr1.Length + arr2.Length];

    for (int i = 0; i < merged.Length; ++i)
    {
        if (ptr1 < arr1.Length && ptr2 < arr2.Length)
        {
            merged[i] = arr1[ptr1] > arr2[ptr2] ? arr2[ptr2++] : arr1[ptr1++];
        }
        else
        {
            merged[i] = ptr2 < arr2.Length ? arr2[ptr2++] : arr1[ptr1++];
        }
    }
    return merged;
}