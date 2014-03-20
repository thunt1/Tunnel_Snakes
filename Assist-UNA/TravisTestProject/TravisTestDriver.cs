using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TravisTestProject
{
    class TravisTestDriver
    {

        public static void Main()
        {
            /* Allow for testing on other's machines. */
            Console.Write("Use default on Trav's computer? (y/n): ");
            string choice = Console.ReadLine();
            while (choice != "y" && choice != "n")
                choice = Console.ReadLine();

            FileStream source;
            FileStream intermediate;
            if (choice == "y") 
            { 
                source = new FileStream("C:\\Users\\Travis\\Documents\\Courses\\Spring 2014" + 
                                        "\\Software Engineering\\Team Project\\Test Files" +
                                        "\\Translator\\TranslatorTest.txt", 
                                        FileMode.Open,FileAccess.Read);
                
                intermediate = new FileStream("C:\\Users\\Travis\\Documents\\Courses\\Spring 2014" +
                                              "\\Software Engineering\\Team Project\\Test Files" + 
                                              "\\Translator\\TestIntermediateFile.txt", 
                                              FileMode.Create, FileAccess.Write);
            }
            else
            {
                Console.WriteLine("Enter path of source code (only one chance!): ");
                source = new FileStream(Console.ReadLine(), FileMode.Open, FileAccess.Read);
                Console.WriteLine("Enter path for intermediate file (only one chance!): ");
                intermediate = new FileStream(Console.ReadLine(), FileMode.Create, FileAccess.Write);
            }
            
            TranslatorTest translator = new TranslatorTest(source,intermediate);
            
            Console.WriteLine("Reading file...");

            /* Actual testing of translator pass 1. */
            translator.Pass1();
            
            Console.WriteLine("Reading file complete.");

            translator.PrintErrorStream();

            Console.WriteLine("Pass 1 complete.");
            Console.ReadKey();
        }
    }
}
