using CreateBD20.DBStructure;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace CreateBD20
{
    class Finder
    {
        /// <summary>
        /// This function is made for linear search of elements using. It measures search time.
        /// </summary>
        /// <returns>
        /// Sorted List of Infos
        /// </returns>
        /// <param name="input">
        /// List of Infos where we try to find field-element
        /// </param>
        /// <param name="field">
        /// key field-element
        /// </param>

        public static List<Info> LinearSearch(List<Info> input, string field)
        {
            List<Info> result = new List<Info>();

            DateTime start = DateTime.Now;


            for (int i = 0; i < input.Count; ++i)
                if (input[i].Port == field) result.Add(input[i]);

            DateTime end = DateTime.Now;

            Console.WriteLine($"Linear search, {input.Count} elements, {(end - start).TotalMilliseconds} ms");

            return result;
        }

        /// <summary>
        /// This function is made as an interface for binary search of elements using. It measures search time.
        /// </summary>
        /// <returns>
        /// Sorted List of Infos
        /// </returns>
        /// <param name="input">
        /// List of Infos where we try to find field-element
        /// </param>
        /// <param name="field">
        /// key field-element
        /// </param>
        /// 
        public static List<Info> BinarySearch(List<Info> input, string field)
        {
            List<Info> result = new List<Info>();

            DateTime startWithSort = DateTime.Now;

            List<Info> sorted = QuickSortInterface(input);

            DateTime startWithoutSort = DateTime.Now;

            BinarySearching(input, 0, input.Count - 1, field, result);

            DateTime end = DateTime.Now;

            Console.WriteLine($"Binary search with sorting, {input.Count} elements, {(end - startWithSort).TotalMilliseconds} ms");
            Console.WriteLine($"Binary search without sorting, {input.Count} elements, {(end - startWithoutSort).TotalMilliseconds} ms");

            return result;
        }
        /// <summary>
        /// This recursive function is made for binary search.
        /// </summary>
        /// <returns>
        /// Sorted List of Infos
        /// </returns>
        /// <param name="input">
        /// List of Infos where we try to find field-element
        /// </param>
        /// <param name="start">
        /// start index
        /// </param>
        /// <param name="end">
        /// end index
        /// </param>
        /// <param name="field">
        /// key field-element
        /// </param>
        /// <param name="result">
        /// List of Infos where we put the finded elements
        /// </param>
        
        static void BinarySearching(List<Info> input, int start, int end, string field, List<Info> result)
        {
            if (start > end) return;
            if (start == end)
            {
                if (string.Compare(input[start].Port, field) == 0)
                    SearchInLine(input, start, field, result);
                return;
            }

            if (string.Compare(input[(start + end) / 2].Port, field) != 0)
            {
                if (string.Compare(input[(start + end) / 2].Port, field) > 0)
                    BinarySearching(input, start, (start + end) / 2 - 1, field, result);
                else
                    BinarySearching(input, (start + end) / 2 + 1, end, field, result);

                return;
            }

            SearchInLine(input, (start + end) / 2, field, result);
        }

        /// <summary>
        /// This function is made for the case when input[(start + end) / 2] == key-field.
        /// </summary>
        /// <param name="input">
        /// List of Infos where we try to find field-element
        /// </param>
        /// <param name="start">
        /// start index
        /// </param>
        /// <param name="field">
        /// key field-element
        /// </param>
        /// <param name="result">
        /// List of Infos where we put the finded elements
        /// </param>

        static void SearchInLine(List<Info> input, int start, string field, List<Info> result)
        {
            int i = start;
            while (i >= 0 && string.Compare(input[i].Port, field) == 0)
                --i;
            ++i;
            while (i < input.Count && string.Compare(input[i].Port, field) == 0)
                result.Add(input[i]);
        }


        /// <summary>
        /// This function is used as interface for quick sort
        /// </summary>
        /// <returns>
        /// Sorted List of Infos
        /// </returns>
        /// <param name="input">
        /// List of Infos that we want to sort
        /// </param>

        public static List<Info> QuickSortInterface(List<Info> input)
        {
            Info[] output = new Info[input.Count];
            input.CopyTo(output, 0);
            List<Info> outl = output.ToList();

            DateTime start = DateTime.Now;

            QuickSort(outl, 0, output.Length - 1);

            DateTime end = DateTime.Now;

            Console.WriteLine($"Quick sort, {input.Count} elements, {(end - start).TotalMilliseconds} ms");

            return outl;
        }

        /// <summary>
        /// This recursive function is made for sorting list of elements using quick sort. It measures sorting time. 
        /// </summary>
        /// <param name="input">
        /// List of Info
        /// </param>
        /// /// <param name="start">
        /// Position of sort_start
        /// </param>
        /// /// <param name="end">
        /// Position of sort_end
        /// </param>
        public static void QuickSort(List<Info> input, int start, int end)
        {
            if (start == end) return;

            if (end - start == 1)
            {
                if (input[end] < input[start])
                {
                    Info x = input[start];
                    input[start] = input[end];
                    input[end] = x;
                }
                return;
            }

            int med = (start + end) / 2;

            int i = start, j = end;

            while (i < j)
            {
                while (input[i] < input[med]) ++i;
                while (input[j] > input[med]) --j;

                if (i <= j)
                {
                    Info x = input[i];
                    input[i] = input[j];
                    input[j] = x;
                    ++i;
                    --j;
                }
            }

            if (j > start) QuickSort(input, start, j);
            if (i < end) QuickSort(input, i, end);

            return;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            using (DataContext100 db = new DataContext100())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string, Info> infos_look = (Lookup<string, Info>)infos.ToLookup(x => x.Port, x => x);
                string searchField = infos[random.Next(infos.Count)].Port;
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };

            using (var db = new DataContext1000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string, Info> infos_look = (Lookup<string, Info>)infos.ToLookup(x => x.Port, x => x);
                string searchField = infos[random.Next(infos.Count)].Port;
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };

            using (var db = new DataContext10000())
            { 
                List<Info> infos = db.Info.ToList();
            Lookup<string, Info> infos_look = (Lookup<string, Info>)infos.ToLookup(x => x.Port, x => x);
            string searchField = infos[random.Next(infos.Count)].Port;
            Finder.LinearSearch(infos, searchField);
            Finder.BinarySearch(infos, searchField);
            DateTime start = DateTime.Now;
            infos_look.Where(x => x.Key == searchField).Select(x => x);
            DateTime end = DateTime.Now;
            Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");

        };

            using (var db = new DataContext20000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string, Info> infos_look = (Lookup<string, Info>)infos.ToLookup(x => x.Port, x => x);
                string searchField = infos[random.Next(infos.Count)].Port;
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };

            using (var db = new DataContext40000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string, Info> infos_look = (Lookup<string, Info>)infos.ToLookup(x => x.Port, x => x);
                string searchField = infos[random.Next(infos.Count)].Port;
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };

            using (var db = new DataContext60000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string, Info> infos_look = (Lookup<string, Info>)infos.ToLookup(x => x.Port, x => x);
                string searchField = infos[random.Next(infos.Count)].Port;
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };

            using (var db = new DataContext80000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string, Info> infos_look = (Lookup<string, Info>)infos.ToLookup(x => x.Port, x => x);
                string searchField = infos[random.Next(infos.Count)].Port;
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };

            using (var db = new DataContext100000())
            {
                List<Info> infos = db.Info.ToList();
                Lookup<string, Info> infos_look = (Lookup<string, Info>)infos.ToLookup(x => x.Port, x => x);
                string searchField = infos[random.Next(infos.Count)].Port;
                Finder.LinearSearch(infos, searchField);
                Finder.BinarySearch(infos, searchField);
                DateTime start = DateTime.Now;
                infos_look.Where(x => x.Key == searchField).Select(x => x);
                DateTime end = DateTime.Now;
                Console.WriteLine($"Lookup search, {infos.Count} elements, {(end - start).TotalMilliseconds} ms");
            };
        }
    }
}