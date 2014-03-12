using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**********************************************************
 * 
 * Name: ExampleClass
 * 
 * ========================================================
 * 
 * Description: This class is here to serve as an example
 *              for coding standards. The programmers will
 *              adhere to the commenting standards that are
 *              evident in this example.
 *                            
 * ========================================================         
 * 
 * Modification History
 * --------------------
 * 3/8/2014     THH     Original version.
 * 3/9/2014     AAH     Added awesomeness.
 *  
 **********************************************************/

namespace Processing
{
    
    class ExampleClass
    {

        /* Constants. */
        public const string EXAMPLE_PUBLIC_CONSTANT = "This is a public constant.";
        protected const string EXAMPLE_PROTECTED_CONSTANT = "This is a protected constant.";
        private const string EXAMPLE_PRIVATE_CONSTANT = "This is a private constant.";
        
        /* Public members. */
        public bool examplePublicBoolean;
        public int examplePublicInteger;
                
        /* Protected members. */
        protected int exampleProtectedInteger;

        /* Private members. */
        private int examplePrivateInteger;


        /* Public methods. */

        /**********************************************************
         * 
         * Name:        Main
         * 
         * Author(s):   First Last
         *              First Last
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: The description of the method goes here.
         *              It also continues here.
         *              (include any non-returned output, i.e. 
         *              printed to screen, pass-by-reference 
         *              changes, etc.)
         *  
         **********************************************************/
        public static void Main()
        {
            /* Variable declarations. */
            int input;
            
            /* Single line comments are formatted like this. */
            Console.WriteLine("Hello World!");

            /* 
             * Block comments are formatted like so.
             * All programmers should follow this format.
             * Last comment line. 
             */
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Console.Write("Enter an integer: ");
            input = Convert.ToInt32(Console.ReadLine());

            ExampleClass temp = new ExampleClass();

            temp.ExamplePrivateMethod(4);

            temp.Print(input);

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            bool hello = temp.ExamplePrivateMethod(5);

            Base16Practice();
        }

        /**********************************************************
         * 
         * Name:        Base16Practice
         * 
         * Author(s):   First Last
         *              First Last
         *              
         * Input:       N/A
         * Return:      N/A
         * 
         * Description: This method works just to practice with
         *              converting integers to base 16 and back.
         *              The examples are printed out to the console.
         *  
         **********************************************************/
        private static void Base16Practice()
        {
            int val = Convert.ToInt32("00000026", 16);
            Console.WriteLine("00 00 00 26  converted to integer: " + val);
            
            string stringVal = val.ToString("X");
            Console.WriteLine("Val converted back to hex (before): " + stringVal);
            stringVal = stringVal.PadLeft(8, '0');
            Console.WriteLine("Val converted back to hex (after): " + stringVal);
            Console.ReadKey();
        }


        /* Protected methods. */

        /**********************************************************
         * 
         * Name:        Print
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       Number to be returned. (include slight 
         *              description of input)
         * Return:      True/False
         * 
         * Description: This method prints the number that was 
         *              passed to it to the console.
         *              (include any non-returned output, i.e. 
         *              printed to screen, pass-by-reference 
         *              changes, etc.).
         *  
         **********************************************************/
        protected bool Print(int numberOne)
        {
            /* Loop from 0 to numberOne, printing each increment */
            for (int i = 0; i < numberOne; i++)
                Console.WriteLine(i + " ");

            Console.WriteLine();

            /* Brief description of what will trigger the if and what will occur */
            if (numberOne < 10)
                Console.WriteLine();
           
            /* Brief description of what will trigger the else and what will occur */
            else
            {
                numberOne = 10;
                Console.WriteLine(numberOne);
            }
           
            return true;
        }


        /* Private methods. */

        /**********************************************************
         * 
         * Name:        ExamplePrivateMethod
         * 
         * Author(s):   First Last
         *              First Last
         *              
         * Input:       Number to be returned. (include slight 
         *              description of input)
         * Return:      True/False
         * 
         * Description: This method prints the number that was 
         *              passed to it to the console.
         *  
         **********************************************************/
        private bool ExamplePrivateMethod(int numberOne)
        {
            Console.WriteLine(numberOne);

            return true;
        }

    }

}
