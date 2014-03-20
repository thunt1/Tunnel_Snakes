using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assist_UNA.Processing.MachineOpTable
{

/**************************************************************************************************
* 
* Name: MachineOpTable
* 
* ================================================================================================
* 
* Description: Instructions on modifications.
*              
*                       
* ================================================================================================        
* 
* Modification History
* --------------------
* 03/17/2014    AAH  Created class, added data members and set up functions and headers. 
*                    Wrote the GetOperand1Type, GetOperand2Type, GetOperand3Type, and GetOpType
*                       methods.
*                      
*************************************************************************************************/

    class MachineOpTable
    {
        /* Constants. */
        private const int NUM_INSTRUCTIONS = 47;
        private const int NUM_OP_TABLE_COLS = 5;
        private const int OPCODE_COL = 0;
        private const int OPTYPE_COL = 1;
        private const int OPERAND1_COL = 2;
        private const int OPERAND2_COL = 3;
        private const int OPERAND3_COL = 4;

        /* Private members. */
        private string[ , ] opTable = new string[NUM_INSTRUCTIONS, NUM_OP_TABLE_COLS];


        /* Public methods. */

        /******************************************************************************************
         * 
         * Name:        MachineOpTable       
         * 
         * Author(s):   Andrew Hamilton
         *              
         *              
         * Input:        N/A 
         * Return: 
         * Description: This method will return the first operand of a given instruction in the 
         *              machine op table.
         *              
         *****************************************************************************************/
        public MachineOpTable()
        { 
            
        }

        /******************************************************************************************
         * 
         * Name:        GetOperand1Type        
         * 
         * Author(s):   Andrew Hamilton
         *              
         *              
         * Input:       The index of the instruction.   
         * Return:      The instruction's first operand.
         * Description: This method will return the first operand of a given instruction in the 
         *              machine op table.
         *              
         *****************************************************************************************/
        public string GetOperand1Type(int index)
        {
            return opTable[index, OPERAND1_COL];
        }

        /******************************************************************************************
         * 
         * Name:        GetOperand2Type        
         * 
         * Author(s):   Andrew Hamilton
         *              
         *              
         * Input:       The index of the instruction.   
         * Return:      The instruction's second operand.
         * Description: This method will return the second operand of a given instruction in the 
         *              machine op table.
         *              
         *****************************************************************************************/
        public string GetOperand2Type(int index)
        {
            return opTable[index, OPERAND2_COL];
        }

        /******************************************************************************************
         * 
         * Name:        GetOperand3Type        
         * 
         * Author(s):   Andrew Hamilton
         *              
         *              
         * Input:       The index of the instruction.   
         * Return:      The instruction's third operand.
         * Description: This method will return the third operand of a given instruction in the 
         *              machine op table. If the given instruction does not have a third operand,
         *              then NULL will be returned.
         * 
         *****************************************************************************************/
        public string GetOperand3Type(int index)
        {
            return opTable[index, OPERAND3_COL];
        }

        /******************************************************************************************
         * 
         * Name:        GetOpType       
         * 
         * Author(s):   Andrew Hamilton  
         *              
         *              
         * Input:       The index of the instruction.      
         * Return:      The format of the given instruction (RR, RX, RS, SS, SI, X).
         * Description: This method will return the format of the given instruction.
         *              
         *****************************************************************************************/
        public string GetOpType(int index)
        {
            return opTable[index, OPTYPE_COL];
        }

        /******************************************************************************************
         * 
         * Name:        isOpcode   
         * 
         * Author(s):   Andrew Hamilton  
         *              
         *              
         * Input:       A candidate string to be determined whether or not it is a valid opcode.      
         * Return:      The method will return the index of the given opcode if it is found in the 
         *              machine op table, or -1 if it is a invalid opcode.
         * Description: This method will determine whether or not a given string is a valid 
         *              instruction.
         *              
         *              
         *****************************************************************************************/
        public int IsOpcode(string opcode)
        {
            return 0;
        }


        /* Private methods. */

        /******************************************************************************************
         * 
         * Name:        Hash      
         * 
         * Author(s):   Andrew Hamilton  
         *              
         *              
         * Input:       
         * Return:      
         * Description: 
         *              
         *              
         *****************************************************************************************/
        private int Hash(string opcode)
        {
            return 0;
        }
        
    }

}
