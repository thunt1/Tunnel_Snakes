using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/**************************************************************************************************
 * 
 * Name: TranslatorTest
 * 
 * ================================================================================================
 * 
 * Description: This class is to serve as the working test model of the final Translator Class for
 *              the ASSIST/UNA project.
 *                            
 * ================================================================================================        
 * 
 * Modification History
 * --------------------
 * 03/18/2014   THH     Original version.
 *  
 *************************************************************************************************/

namespace TravisTestProject
{
    class TranslatorTest
    {
        /* Private members. */
        private FileStream intermediateFile;
        private int numErrors;
        private int locationCounter;
        private string[,] errorStream;
        private string label;
        private string line;
        private string instruction;
        private string parameters;
        private FileStream sourceFile;
        private string validChars;
        //private SymbolTable symTable;


        /* Public methods. */

        /******************************************************************************************
         * 
         * Name:        TranslatorTest (Constructor)     
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       FileStreams for both the source file and intermediate file. 
         * Return:      N/A 
         * Description: The constructor for TranslatorTest.              
         *              
         *****************************************************************************************/
        public TranslatorTest(FileStream source, FileStream intermediate)
        {
            /* Initialize all the data members to default values. */
            numErrors = 0;
            locationCounter = 0;

            /* The size of this will need to figured out later... */
            errorStream = new string[100,3];
            validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
           
            label = "";
            line = "";
            instruction = "";
            parameters = "";
            
            sourceFile = source;
            intermediateFile = intermediate;
            
            //symTable = new SymbolTable();
        }

        /******************************************************************************************
        * 
        * Name:        GetIntermediateFileStream 
        * 
        * Author(s):   Travis Hunt
        *              
        * Input:       N/A     
        * Return:      The intermediate file location as a FileStream.
        * Description: This method is the getter function for the intermediate file stream.              
        *              
        *****************************************************************************************/
        public FileStream GetIntermediateFileStream()
        {
            return intermediateFile;
        }

        /******************************************************************************************
         * 
         * Name:        PrintErrorStream     
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       N/A        
         * Return:      N/A
         * Description: This method prints the items in the errorStream array to the console. 
         *              For testing purposes only.
         *              
         *****************************************************************************************/
        public void PrintErrorStream()
        {
            if (numErrors > 0)
            {
                Console.WriteLine("\n****ERRORS****");
                Console.WriteLine("---------------");
                for (int i = 0; i < numErrors; i++)
                {
                    Console.WriteLine("Line: " + errorStream[i, 0] + "\nError: " + errorStream[i, 1] +
                                      "\nSource:  " + errorStream[i, 2] + "\n");
                }
                Console.WriteLine("**************\n");
            }

            else
                Console.WriteLine("No errors detected.");
        }

        /******************************************************************************************
         * 
         * Name:        Pass1     
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       N/A
         * Return:      N/A
         * Description: This method drives the translating pass1 process. It will read through each
         *              line of the source code file and send it to the ProcessLine method.
         *              
         *****************************************************************************************/
        public void Pass1()
        {
            /* Open up streams to read from the source and write to the intermediate file. */
            StreamReader inStream = new StreamReader(sourceFile);
            StreamWriter outStream = new StreamWriter(intermediateFile);

            /* Skip the options portion of the project file. */
            string line = inStream.ReadLine();
            while (line == "" || line[0] == '#')
                line = inStream.ReadLine();

            /* Process each line of the source file. */
            while (!inStream.EndOfStream)
                ProcessLine(inStream.ReadLine(), outStream);

            /* Close all the streams. (intermediate will need to be moved to pass 2 later...) */
            inStream.Close();
            outStream.Close();
            sourceFile.Close();
            intermediateFile.Close();
        }


        /* Private methods. */

        /******************************************************************************************
         * 
         * Name:        ProcessLine     
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       Line from source code as string.    
         * Return:      N/A 
         * Description: This method processes the source code line that is passed to it. The method
         *              pulls out the label, operation and parameters.
         *              
         *****************************************************************************************/
        private void ProcessLine(string inputLine, StreamWriter intermediateOutStream)
        {
            line = inputLine;

            /* If the first column contains a *, the row is commented out so ignore line. */
            if (line[0] != '*' && line[0] != '#')
            {
                /* Store the label portion of the line and validate. */
                label = line.Substring(0, 8);
                ValidateLabel();

                /* If column 9 is not a space, send the error. */
                if (line[8] != ' ')
                {
                    errorStream[numErrors, 0] = (locationCounter + 1).ToString();
                    errorStream[numErrors, 1] = "Invalid label format, column 9 must be" +
                                                " blank";
                    errorStream[numErrors, 2] = line;
                    numErrors++;
                }

                /* Store the instruction portion of the line and validate. */
                instruction = line.Substring(9, 5);
                ValidateInstruction();

                /* If column 15 is not a space, send the error. */
                if (line[14] != ' ')
                {
                    errorStream[numErrors, 0] = (locationCounter + 1).ToString();
                    errorStream[numErrors, 1] = "Invalid instruction format, column 15 " +
                                                "must be blank";
                    errorStream[numErrors, 2] = line;
                    numErrors++;
                }

                /* 
                 * Store the parameters portion of the line and validate.
                 * Only the parameters are stored (up to the first space character).
                 * This is so no comments are stored as a parameter.
                 */
                int parameterLastIndex = line.IndexOf(' ', 15);
                parameterLastIndex -= 14;

                if (parameterLastIndex < 0)
                    parameters = line.Substring(15);
                else
                    parameters = line.Substring(15, parameterLastIndex);

                ValidateParameters();
                //ValidateInput();

                intermediateOutStream.WriteLine("Line: " + (locationCounter + 1) + "\t" + label + 
                                                " " + instruction + " " + parameters);

            }

            locationCounter++;
        }

        /******************************************************************************************
         * 
         * Name:        ValidateLabel     
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       N/A    
         * Return:      True if the label is valid, false if otherwise.  
         * Description: This methods checks to see if the label is in the valid format.
         *              Used for Pass 1
         *              
         *****************************************************************************************/
        private bool ValidateLabel()
        {
            /* 
             * Find the first space in the label, then make sure the rest of the string is spaces 
             * as well. 
             */
            int firstSpaceIndex = label.IndexOf(' ');
            if (firstSpaceIndex >= 0)
            {
                for (int i = firstSpaceIndex; i < label.Length; i++)
                {
                    if (label[i] != ' ')
                    {
                        errorStream[numErrors, 0] = (locationCounter + 1).ToString();
                        errorStream[numErrors, 1] = "Label is invalid, cannot contain spaces " +
                                                    "inbetween characters";
                        errorStream[numErrors, 2] = line;
                        numErrors++;
                        return false;
                    }
                }
            }
            
            /* Search the label for invalid characters and store the error if one is found. */
            for (int i = 0; i < firstSpaceIndex; i++)
            {
                if (!validChars.Contains(label[i]))
                {
                    errorStream[numErrors, 0] = (locationCounter + 1).ToString();
                    errorStream[numErrors, 1] = "Error: Label is invalid, contains invalid " + 
                                                "character(s)";
                    errorStream[numErrors, 2] = line;
                    numErrors++;
                    return false;
                }
            }

            return true;
        }

        /******************************************************************************************
         * 
         * Name:        ValidateInstruction     
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       N/A    
         * Return:      True if instruction is formatted correctly, false if not.   
         * Description: This methods checks to see if the instruction is in the valid format.
         *              Used for Pass 1             
         *              
         *****************************************************************************************/
        private bool ValidateInstruction()
        {
            return true;
        }

        /******************************************************************************************
         * 
         * Name:        ValidateParameters     
         * 
         * Author(s):   Travis Hunt
         *              
         * Input:       N/A    
         * Return:      True if parameters are formatted correctly, false if not.  
         * Description: This methods checks to see if the parameters are in the valid format.
         *              Used for Pass 1              
         *              
         *****************************************************************************************/
        private bool ValidateParameters()
        {
            return true;
        }
    }
}
