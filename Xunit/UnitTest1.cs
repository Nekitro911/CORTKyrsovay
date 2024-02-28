using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CORTKyrsovay;
using System.Linq;
using System.Diagnostics;
namespace Xunit
{
    [TestClass]
    public class BubbleSortTests
    {
        [TestMethod]
        public void BubbleSort_EmptyArray_ReturnsEmptyArray()
        {
            int[] arr = new int[] { };
            (int[], int, long) result = Program.BubbleSort(arr);

            CollectionAssert.AreEqual(new int[] { }, result.Item1);
            Assert.AreEqual(0, result.Item2);
            Assert.AreEqual(0, result.Item3);
        }

        [TestMethod]
        public void BubbleSort_SingleElementArray_ReturnsSingleElementArray()
        {
            int[] arr = new int[] { 1 };
            (int[], int, long) result = Program.BubbleSort(arr);

            CollectionAssert.AreEqual(new int[] { 1 }, result.Item1);
            Assert.AreEqual(0, result.Item2);
            Assert.AreEqual(0, result.Item3);
        }

        [TestMethod]
        public void BubbleSort_SortedArray_ReturnsSortedArray()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            (int[], int, long) result = Program.BubbleSort(arr);

            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, result.Item1);
            Assert.AreEqual(0, result.Item2);
            Assert.AreEqual(0, result.Item3);
        }

        [TestMethod]
        public void BubbleSort_UnsortedArray_ReturnsSortedArray()
        {
            int[] arr = new int[] { 5, 3, 1, 2, 4 };
            (int[], int, long) result = Program.BubbleSort(arr);

            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, result.Item1);
            Assert.AreEqual(6, result.Item2);
            Assert.AreNotEqual(6, result.Item3);
        }

        [TestMethod]
        public void BubbleSort_LargeArray_ReturnsSortedArray()
        {
            int[] arr = new int[10000];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            Shuffle(arr);

            (int[], int, long) result = Program.BubbleSort(arr);

            CollectionAssert.AreEqual(Enumerable.Range(0, 10000).ToArray(), result.Item1);
            Assert.AreNotEqual(0, result.Item2);
            Assert.AreNotEqual(0, result.Item3);
        }

        private void Shuffle(int[] arr)
        {
            Random rng = new Random();

            for (int i = 0; i < arr.Length; i++)
            {
                int j = rng.Next(i, arr.Length);

                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
    }
    [TestClass]
    public class SelectionSortTests
    {
        [TestMethod]
        public void SelectionSort_EmptyArray_ReturnsEmptyArray()
        {
            int[] arr = new int[] { };
            (int[], int, long) result = SelectionSort(arr);

            Assert.AreEqual(arr, result.Item1);
            Assert.AreEqual(0, result.Item2);
            Assert.AreEqual(0, result.Item3);
        }

        [TestMethod]
        public void SelectionSort_SortedArray_ReturnsSortedArray()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            (int[], int, long) result = SelectionSort(arr);

            Assert.AreEqual(arr, result.Item1);
            Assert.AreEqual(0, result.Item2);
            Assert.AreEqual(0, result.Item3);
        }      
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
            }

            stopwatch.Stop();

            return (arr, numSwaps, stopwatch.ElapsedMilliseconds);
        }
    }
    [TestClass]
    public class QuickSort
    {        
        [TestMethod]
        public void QuickSort_WithEmptyArray_ReturnsEmptyArray()
        {
            // Arrange
            int[] arr = new int[] { };

            // Act
            var result = Program.QuickSort(arr);
            int[] sortedArray = result.Item1;
            int numSwaps = result.Item2;
            long elapsedMilliseconds = result.Item3;

            // Assert

            Assert.AreEqual(0, sortedArray.Length);
            Assert.AreEqual(0, numSwaps);
            Assert.AreEqual(0, elapsedMilliseconds);
        }

        private static int[] GenerateRandomArray(int size)
        {
            int[] arr = new int[size];

            for (int i = 0; i < size; i++)
            {
                arr[i] = new Random().Next(1, 1000);
            }

            return arr;
        }
    }
}