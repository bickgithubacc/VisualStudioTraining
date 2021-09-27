using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using ConsoleApp1.Classes;
using StringLanguageExtensions;
namespace ConsoleApp1
{
    class Program
    {
        private static readonly string _fileName =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "PersonBirthdays.json");
        static void Main(string[] args)
        {
            Debugging1();
        }
        private static void Debugging1()
        {
            if (File.Exists(_fileName))
            {
                string fileContent = File.ReadAllText(_fileName);
                List<Person1> people = fileContent.JSonToList<Person1>();

                foreach (var person1 in people)
               
                {
                }
            }
            else
            {
                FileNotFound();
            }
        }

        /// <summary>
        /// Two different solutions to find values greater than 250
        /// </summary>
        private static void AggregateOver250()
        {
            List<int> list = new List<int>() { 100, 200, 300, 400, 500 };

            int result1 = list.Aggregate(0, (inValue, outValue) => outValue > 250 ? inValue + 1 : inValue);
            int result2 = list.Count(item => item > 250);

        }

        private static void FindByFirstAndLastName()
        {
            if (File.Exists(_fileName))
            {
                string fileContent = File.ReadAllText(_fileName);
                List<Person1> people = fileContent.JSonToList<Person1>();
                var personResult = people.FirstOrDefault(person => person.FirstName == "Nicolas" && person.LastName == "Salinas");

                Debug.WriteLine(personResult is not null ?
                    $"Id: {personResult.Id} Birthday is {personResult.BirthDate:d}"
                    : "Not found");

            }
            else
            {
                FileNotFound();
            }
        }
        private static void AverageBirthMonthFromJson()
        {

            if (File.Exists(_fileName))
            {
                string fileContent = File.ReadAllText(_fileName);
                List<Person1> people = fileContent.JSonToList<Person1>();

                var averageMonth = people.Average(person => person.BirthDate.Month);
                Debug.WriteLine($"Average month: {Math.Round(averageMonth, MidpointRounding.ToEven)}");

            }
            else
            {
                Console.ReadLine();
            }
        }
        

        private static void GroupByBirthYearWithCount()
        {

            if (File.Exists(_fileName))
            {
                string fileContent = File.ReadAllText(_fileName);
                List<Person1> people = fileContent.JSonToList<Person1>();

                var groupedByBirthYear = people
                    .OrderBy(person => person.LastName)
                    .GroupBy(person => person.BirthDate.Year)
                    .Select(@group => new
                    {
                        @group.Key,
                        Count = @group.Count(),
                        IGroupingValues = @group
                    })
                    .ToList();

                foreach (var anonymousItem in groupedByBirthYear)
                {
                    Debug.WriteLine($"Count {anonymousItem.Count:D2} for {anonymousItem.Key}");
                    foreach (var person1 in anonymousItem.IGroupingValues)
                    {
                       // Debug.WriteLine($"\t{person1.FullName,20} {person1.BirthDate:MM-dd-yyyy}");
                    }

                    Debug.WriteLine("");

                }

                Console.WriteLine();
            }
            else
            {
                FileNotFound();
            }
        }
        /// <summary>
        /// Demonstrates a conditional breakpoint
        /// </summary>


        /// <summary>
        /// Example using 'let'
        ///
        /// https://tinyurl.com/yp54j32c
        /// 
        /// </summary>
        /// <returns></returns>
        private static List<AllDoneCode> ReadAllDoneFile()
        {

            return (
                    from line in File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TextFiles", "All_Done_Codes.txt"))
                    where line.Length > 0

                    let Items = line.Split(',')

                    select new AllDoneCode()
                    {
                        Code = Items[0],
                        Description = Items[1]
                    })
                .OrderBy(item => item.Code)
                .ToList();

        }

        private static void FileNotFound()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to find");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{_fileName}");
            Console.ResetColor();
            Console.ReadLine();
        }

#if UseModuleInitializer
        [ModuleInitializer]
        public static void FileCheck()
        {

            if (!File.Exists(_fileName))
            {
                Debug.WriteLine($"Missing: {_fileName}");
                System.Environment.Exit(-1);
            }
        }
#endif

    }

}