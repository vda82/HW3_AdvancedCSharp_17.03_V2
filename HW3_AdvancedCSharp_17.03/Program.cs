using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HW3_AdvancedCSharp_17._03.FileSystemVisitor;

namespace HW3_AdvancedCSharp_17._03
{
    class Program
    {
        static bool MyFilter (string str)
        {
            return str.Contains("txt");
        }
        static void Main(string[] args)
        {
            string path = @"C:\Users\Vitaliy_Dryha\Desktop\C# Anton";
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(MyFilter);

            fileSystemVisitor.SearchStarted += OnSearchStarted;
            fileSystemVisitor.SearchFinished += OnSearchFinished;

            fileSystemVisitor.EntryFound += OnEntryFound;
            fileSystemVisitor.FilteredEntryFound += OnFilteredEntryFound;

            
            foreach (var dirOrFile in fileSystemVisitor.GetAllDirectoriesAndFiles(path))
            {
                Console.WriteLine(dirOrFile);
            }
        }


        public static void OnSearchFinished()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-----Search Finished-----", Console.ForegroundColor);
            Console.ResetColor();
        }

        public static void OnSearchStarted()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-----Search Started-----");
            Console.ResetColor();
        }

        public static void OnEntryFound (object sourse, string str)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Not filtered result is " + str);
            Console.ResetColor();
        }

        public static void OnFilteredEntryFound(object sourse, string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Filtered result is     " + str);
            Console.ResetColor();
        }


    }


}



