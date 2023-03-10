//Задание 1: Поиск самой длинной общей подпоследовательности между двумя строками с помощью многопоточности.

//В этом задании нужно реализовать алгоритм поиска длинной общей подпоследовательности между двумя строками с использованием многопоточности.
//Для выполнения задания нужно выполнить следующие шаги:
//Написать функцию, которая будет принимать на вход две строки и возвращать длину наибольшей общей подпоследовательности между ними. В качестве возвращаемого значения может быть целое число.
//Реализовать многопоточную версию функции. Для этого можно разбить обе строки на несколько частей и запустить для каждой пары частей по отдельному потоку. После завершения всех потоков необходимо собрать результаты и выбрать максимальную длину общей подпоследовательности.
//Общая подпоследовательность (англ. Longest Common Subsequence, LCS) - это последовательность элементов, которые встречаются в исходных последовательностях, но не обязательно на одной и той же позиции.
//Например, для двух строк "abcde" и "ace" общей подпоследовательностью будет "ace", так как она содержит элементы, которые есть и в первой, и во второй строке (a и e в первой строке, tableLCS во второй строке), и при этом не нарушается порядок элементов в общей последовательности.
//Поиск общей подпоследовательности является важной задачей в биоинформатике, например, при сравнении геномов. Одним из классических алгоритмов для нахождения длины наибольшей общей подпоследовательности является динамическое программирование.

int lengthLCS=0;

Console.Write("Enter first string: ");
string str1 = Console.ReadLine();

Console.Write("Enter second string: ");
string str2 = Console.ReadLine();

Console.Write("Enter count of Tasks - ");
int countTasks = int.Parse(Console.ReadLine());

string[] arr1 = SplitString(str1, countTasks).ToArray();
string[] arr2 = SplitString(str2, countTasks).ToArray();

LengthLCSAsync();

Console.WriteLine("Length No tasks - " + LongestCommonSubsequenceAsync(str1, str2).Result);

Console.WriteLine("Length with tasks - " + lengthLCS);

async void LengthLCSAsync()
{
    for (int i = 0; i < countTasks; i++)
    {
        lengthLCS += await LongestCommonSubsequenceAsync(arr1[i], arr2[i]);
    }
}

async Task<int> LongestCommonSubsequenceAsync(string s1, string s2)
{
    return await Task.Run(() =>
    {
        int n = s1.Length;
        int m = s2.Length;
        int[,] tableLCS = new int[(n + 1), (m + 1)];


        for (int i = 1; i <= n; ++i)
        {
            for (int j = 1; j <= m; ++j)
            {
                if (s1[i - 1] == s2[j - 1])
                    tableLCS[i, j] = tableLCS[i - 1, j - 1] + 1;
                else
                    tableLCS[i, j] = MAX(tableLCS[i - 1, j], tableLCS[i, j - 1]);
            }
        }

        return tableLCS[n, m];
    });
}

int MAX(int a, int b)
{
    return a > b ? a : b;
}

IEnumerable<string> SplitString(string str, int countTasks)
{
    int maxSubstringSize = str.Length / countTasks;

    for (int i = 0; i < str.Length; i += maxSubstringSize)
        yield return str.Substring(i, Math.Min(maxSubstringSize, str.Length - i));
}
