using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CORTKyrsovay
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Создаем массив случайных чисел
            int[] arr = GenerateRandomArray(20);
            Console.WriteLine("Массив:");
            PrintArray(arr);
            Console.WriteLine("-----------------------------");
            // Сортируем массив с помощью разных алгоритмов
            var sortingAlgorithms = new Dictionary<string, Func<int[], (int[], int, long)>>
            {
                { "Bubble Sort", BubbleSort },
                { "Selection Sort", SelectionSort },
                { "Insertion Sort", InsertionSort },
                { "Merge Sort", MergeSort },
                { "Quick Sort", QuickSort }
            };
            var sortedArrays = new Dictionary<string, int[]>();
            // Выводим результаты сортировки
            foreach (var algorithm in sortingAlgorithms)
            {
                // Сортируем массив
                var result = algorithm.Value(arr);
                int[] sortedArray = result.Item1;
                int numSwaps = result.Item2;

                // Выводим название алгоритма, количество перестановок и время выполнения
                sortedArrays[algorithm.Key] = sortedArray;
                Console.WriteLine($"{algorithm.Key}:");
                Console.WriteLine($"Отсортированный массив: {string.Join(", ", sortedArray)}");
                Console.WriteLine($"Кол-во перестановок: {numSwaps}");
                Console.WriteLine($"Время: {result.Item2} ms");
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        static void PrintArray(int[] arr)
        {
            Console.WriteLine(string.Join(", ", arr));
        }
        public static int[] GenerateRandomArray(int size)
        {
            Random random = new Random();
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = random.Next(1, 1000);
            }
            return arr;
        }
        // Пузырьковая сортировка
        public static (int[], int, long) BubbleSort(int[] arr)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int n = arr.Length;
            int numSwaps = 0;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        numSwaps++;
                    }
                }
            }

            stopwatch.Stop();
           
            return (arr, numSwaps, stopwatch.ElapsedMilliseconds);
        }
        // Сортировка выбором
        public static (int[], int, long) SelectionSort(int[] arr)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = arr.Length;
            int numSwaps = 0;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    int temp = arr[i];
                    arr[i] = arr[minIndex];
                    arr[minIndex] = temp;
                    numSwaps++;
                }
                numSwaps++;
            }

            stopwatch.Stop();

            return (arr, numSwaps, stopwatch.ElapsedMilliseconds);
        }

        // Сортировка вставками
        public static (int[], int, long) InsertionSort(int[] arr)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = arr.Length;
            int numSwaps = 0;

            for (int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                    
                }
                numSwaps++;
                arr[j + 1] = key;
            }

            stopwatch.Stop();
           
            return (arr, numSwaps, stopwatch.ElapsedMilliseconds);
        }      
        public static (int[], int, long) MergeSort(int[] arr)
        {
            //int numComparisons = 0;
            int n = arr.Length;
            int numSwaps = 0;
            // Проверка на пустой массив
            if (arr == null || arr.Length == 0)
            {
                return (new int[0], 0, 0);
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
           
            //numComparisons += leftComparisons + rightComparisons;
            MergeSortRecursive(arr, 0, n - 1, ref numSwaps);

            stopwatch.Stop();
          
            return (arr, numSwaps, stopwatch.ElapsedMilliseconds);
        }

        private static void MergeSortRecursive(int[] arr, int left, int right, ref int numSwaps)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;

                MergeSortRecursive(arr, left, mid, ref numSwaps);
                MergeSortRecursive(arr, mid + 1, right, ref numSwaps);

                Merge(arr, left, mid, right, ref numSwaps);
            }
        }

        private static void Merge(int[] arr, int left, int mid, int right, ref int numSwaps)
        {
            int[] temp = new int[right - left + 1];

            int i = left;
            int j = mid + 1;
            int k = 0;

            while (i <= mid && j <= right)
            {
                if (arr[i] <= arr[j])
                {
                    temp[k] = arr[i];
                    i++;
                    numSwaps++;
                    //numSwaps += mid - i + 1;
                }
                else
                {
                    temp[k] = arr[j];
                    j++;
                    
                }

                k++;
            }

            while (i <= mid)
            {
                temp[k] = arr[i];
                i++;
                k++;
            }

            while (j <= right)
            {
                temp[k] = arr[j];
                j++;
                k++;
            }

            for (int index = left; index <= right; index++)
            {
                arr[index] = temp[index - left];
            }
        }
        // Быстрая сортировка
        public static (int[], int, long) QuickSort(int[] arr)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = arr.Length;
            int numSwaps = 0;

            QuickSortRecursive(arr, 0, n - 1, ref numSwaps);

            stopwatch.Stop();
           
            return (arr, numSwaps, stopwatch.ElapsedMilliseconds);
        }

        private static void QuickSortRecursive(int[] arr, int left, int right, ref int numSwaps)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right, ref numSwaps);

                QuickSortRecursive(arr, left, pivot - 1, ref numSwaps);
                QuickSortRecursive(arr, pivot + 1, right, ref numSwaps);
            }
        }

        private static int Partition(int[] arr, int left, int right, ref int numSwaps)
        {
            int pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;

                    int temp1 = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp1;
                    numSwaps++;
                }
            }

            int temp = arr[i + 1];
            arr[i + 1] = arr[right];
            arr[right] = temp;
          

            return i + 1;
        }
    }
}

