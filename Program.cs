using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Decimal_Manipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dae is Bae

            //RipAdd Decimal, Version 1.6
            //RipAdd is based on the way computers add numbers on the logic gate level
            //This program was originally written in TI-BASIC, for the TI-84
            //Modifications to code will be written here:
            /*  
             *  4.30.17: Took out special case (2^0 or 1) in function decBin
            */
            try
            {
         Reset:
                int valA = 0; //This is going to be the first value
                int valB = 0; //This is going to be the second value; if subtracting, it will be the subtractend
                int bitWidth = 4; //This is going to be the bit width of the adder/subtractor

                int[] binA = new int[bitWidth]; //This array will store the binary value of A, with the length of bit width
                int[] binB = new int[bitWidth]; //This array will store the binary value of B, with the lenght of bit width
                int[] binOutput = new int[bitWidth + 2]; //This array will store the binary output of the selected operation, with the length of the bit width + 2

                int finBinOut = 0; //This will store the Finalized Binary Output of the adder, to be shown later
                int decOutput = 0; //This will store the value of the Decimal Output, shown at the very end of the program
                
                Console.WriteLine("Welcome!"); //Welcome Code
                Console.WriteLine("Would you like to Add or Subtract?");
                Console.WriteLine("If Subtracting, Please Put 'y'");
                Console.WriteLine("Otherwise, put 'n'");
                
         addSub: //Label for human-error prevention
                string subtractTrue = Console.ReadLine(); //Assign input to variable 'subtractTrue'
                int subtract = 0; //Subtract needs to be an integer, as it will be used as a numerical input in the Adding stage of the program.

                if (subtractTrue == "y" || subtractTrue == "Y") //If the user wants to subtract the two values, then:
                {

                    subtract = 1; //The value that will be used in the subtracting stage is assigned

                }
                else if (subtractTrue == "n" || subtractTrue == "N") //If the user wants to add the two values, the:
                {

                    subtract = 0; //The value that will be used in the subtracting stage will be disabled, effectively disabling the rest of the subtracting process.

                }
                else
                {

                    Console.WriteLine("You did not type a viable input"); //Human Error; if user doesn't input a valid response, this is thrown.
                    Console.WriteLine("Would you like to subtract?");
                    Console.WriteLine("Please specify (y)es or (n)o");
                    Console.WriteLine();
                    goto addSub;

                }

                Console.WriteLine(); //Aesthetically pleasing space.
                Console.WriteLine("Now, please input your two values"); //Inform user of choices
                Console.WriteLine("These two values must be integers"); //Specify the limits of the program
                Console.WriteLine("Please input the first value, which should be less than 2^32, or [INFOMISSING]"); //Declare the obvious

                valA = Int32.Parse(Console.ReadLine()); //Convert the input into a workable integer instead of a string, because you can't add strings.
                Console.WriteLine("Thank You"); //Aesthetically pleasing 'thank you'

                Console.WriteLine(); //Aesthetically pleasing space.

                Console.WriteLine("Now, please input the second value, which should be less than 2^32, or [INFOMISSING]"); //Declare the obvious, part two.
                if (subtract == 1) //If the user specified wanted to subtract, then show:
                {

                    Console.WriteLine(); //Aesthetically pleasing space
                    Console.WriteLine("Since you have selected the subtraction method,"); //The obvious
                    Console.WriteLine("You should treat this value is if it were negative"); //How the user should plan on subtracting said numbers
                    Console.WriteLine("Also, please don't input a value that will result in a negative value"); //Inform user of limitations
                    Console.WriteLine("This program uses unsigned binary, so the output will always be positive"); //Inform user of why there are limitations
                    Console.WriteLine("As such, the output will be wrong if the the desired result is negative"); //Warn user of exceeding limiations

                }

                valB = Int32.Parse(Console.ReadLine()); //Convert input into a workable integer again, but for value B
                Console.WriteLine(); //Aesthetically pleasing space.
                Console.WriteLine("Thank You"); //Aesthetically pleasing 'thank you'

                decBin(valA, binA, bitWidth); //Convert (decimal) value A (valA) into binary value A, then store in Array 'binA'
                decBin(valB, binB, bitWidth); //Convert (decimal) value B (valB) into binary value A, then store in Array 'binB'
                Console.WriteLine(); //Aesthetically pleasing space

                //Preparation for Subtraction

                if (subtract == 1) //If the user wants to subtract, then:
                {

                    /* The way that one subtracts two binary numbers is through something known as 'two's complement'
                     *  Simply put, you invert all of the bits in the number that will be the subtractend (here, it's valB)
                     *      So, if the input is 1101, then it'll become 0010.
                     *  Then, you add one.
                     *      So, 0010 becomes 0011.
                     *  It's that simple. The inversion occurs directly afterwards.
                     *  Adding one will occur during the adding stage. As in, the carryIn of the Adder will be equal to the subtract int.
                     */

                    Console.WriteLine("Inverting bits..."); //Notify user of bit inversion
                    for (int i = 0; i <= bitWidth; i++) //Begin loop that inverts bits in array 'binB'
                    {

                        binB[i] = binB[i] ^ subtract; /*XOR is used for inverting values;
                                                      * For example, if the input is 1, and subtract is 1,
                                                      *     then the output is 0
                                                      * If the input is 0, and subtract is 0, then the 
                                                      *    output is 1
                                                      */
                        Console.WriteLine(binB[i]); //Show the user what's going on.

                    }
                    Console.WriteLine("...Done"); //Notify the user of the end of the function

                }

                //Adding Section:

                Console.WriteLine("About to start adding..."); //Notify user of what's going on.

                /*Yes, I know it's bad practice to declare variables at the end, but if you want to make heads or tails of what's going on, 
                    then this'll be a lot more helpful. */
                int carryIn = subtract; //The carryIn value will store the value of subtract, for reasons outlined in the second major Subtraction block desc.
                int carryOut = 0; //carryOut will store the value of carryOut in the function 'add();' (Wow! So creative!)
                bool overflow = false; //Whether or not there is an overflow dictates the number of cycles in combineBin();

                for (int i = 0; i < bitWidth; i++) //Repeat the adding process 
                {

                    add(binA[i], binB[i], ref carryIn, ref carryOut, ref binOutput[i]); //Add corresponding binary places, with the variable carryIn
                    carryIn = carryOut; //carryOut becomes carryIn, as with any Ripple Adder (hence the original name)

                }

                binOutput[bitWidth + 1] = carryOut; //Possible overflow bit is stored in last place
                if (binOutput[bitWidth + 1] == 1) //If there is overflow
                {

                    Console.WriteLine("OVERFLOW"); //Display the fact that there is overflow
                    overflow = true; //Set overflow boolean for use with combinBin();

                }

                //Taking array information in binOutput and combining it into finBinOut.
                combineBin(ref binOutput, ref finBinOut, bitWidth, overflow, subtract); //Combine the values in binOutput and place the output into finBinOut
                binDec(ref binOutput, ref decOutput, bitWidth, overflow); //Convert the binary output to decimal

                Console.WriteLine(); //Aesthetically pleasing space
                Console.WriteLine("Finished All Functions...");
                Console.WirteLine("Binary Output is " + finBinOut); //Show final binary output
                Console.WriteLine("Decimal Output is " + decOutput); //Show final decimal output
                Console.WriteLine(); //Aesthetically pleasing space
                
                Console.WriteLine("Would you like to Add/Subtract Again? (Y/n)"); //Propose a 'reset' switch
                
                if (Console.ReadKey() == "y") //User types wants to reset...
                {
                    
                    goto Reset; //Reset the program
                    
                } else //User either doesn't care or types in 'n'
                {
                 
                    Console.WriteLine("Press any key to close the program..."); //Inform the user that they can kill the program
                    
                }
                
                Console.ReadKey();

            }


            catch (Exception ex) //If a bug occurs, then:
            {

                Console.WriteLine("Oops! Something went wrong."); //Inform user that they should do something
                Console.WriteLine("Press any key to close the program."); //Oh, wait; this is something.
                Console.ReadKey();

            }


        }

        //
        //AESTHETICALLY PLEASING EMPTY SPACE (TM)
        //

        public static void decBin(int inputVal, int[] binArray, int bitWidth)
        {
            /* This function converts a decimal input ,'inputVal', into binary and deposits each
             *  individual bit into it's respective place in the array 'binArray'
             * 
             * The way that it works is by taking the input and finding the largest power of two 
             *  that fits inside of it. Then, it subtracts that value, and stores '1' in the appropriate
             *  power. Then, it keeps repeating the process until the original value reaches 1 or 0, after
             *  which the appropriate value would be stored. 
             *  
             *  For example:
             *  Input = 13
             *      13 != 1; 13 != 1; 13 > 1;
             *      13 != 1; 13 != 2; 13 > 2;
             *      13 != 1; 13 != 4; 13 > 4;
             *      13 != 1; 13 != 8; 13 > 8;
             *      13 != 1; 13 != 16; 13 < 16; 13 - 8 (or 2^(4-1)); 2^(4-1)'s place = 1; Input = 5; Reset powerOfTwo;
             *      5 != 1; 5 != 1; 5 > 1;
             *      5 != 1; 5 != 2; 5 > 2;
             *      5 != 1; 5 != 4; 5 > 4;
             *      5 != 1; 5 != 8; 5 < 8; 5 - 4 (or 2^(3-1)); 2^(3-1)'s place = 1; Input = 1; Reset powerOfTwo;
             *      1 == 1; 1's place or 2^0's place = 1; End.
             *  Output: 1101
             * 
             */

            Console.WriteLine(); //Aesthetically pleasing space
            Console.WriteLine("Converting Decimal into Binary..."); //Inform user of what's going on...
            Console.WriteLine("Decimal Value = " + inputVal); //Inform user of number being converted

            int powerOfTwo = 0; //powerOfTwo will represent the running powerOfTwo in question, along with the place value that '1' would be placed into

            while (inputVal > 0) //Repeat this while the inputted value still has a decimal value
            {

                if ((Math.Pow(2, powerOfTwo)) == inputVal) //If the inputted value is already equal to a power of two, then:
                {

                    binArray[powerOfTwo] = 1; //'1' is placed in the appropriate place value
                    Console.WriteLine("Power of 2 Found"); //Notify user of the great news
                    Console.WriteLine("Input in place value " + (powerOfTwo)); //Notify user of input (just for Aesthetic)
                    goto Finish; //Skip the rest of the code and go to the end of the function
                }

                if ((Math.Pow(2, powerOfTwo)) > inputVal) //If the inputted value is less than the current powerOfTwo, then:
                {

                    inputVal -= ((int)Math.Pow(2, (powerOfTwo - 1))); //The current powerOfTwo - 1 is subtracted from the input value, because the current powerOfTwo is 
                                                                      //greater than the input value, the one before it is less, leaving us with workable (non-negative)
                                                                      //information. For example: 8 (2^3) is greater than the input 5. So, 2^2 (or 2^(3-1)) is 4, which
                                                                      //IS less than 5. So, we'll subtract 4 from 5, and be left with 1 to continue sifting through.
                    binArray[powerOfTwo-1] = 1; //Input '1' into the necessary place
                    Console.WriteLine("Input in place value " + (powerOfTwo-1)); //Notify user of input (just for Aesthetic)
                    powerOfTwo = 0; //Reset the powerOfTwo variable to continue the cycle.

                } else //If the inputted value is still greater than the current powerOfTwo, then:
                {

                    powerOfTwo++; //Increment powerOfTwo in order to test it against the inputted value again

                }

            }

        Finish: //When a program is done before the 'While' loop is over, then:
            Console.WriteLine("...Completed Binary Conversion"); //Notify the user of what's happened
            Console.WriteLine("Binary Output:"); //Inform the user of what they're seeing
            for (int i = 0; i < bitWidth; i++) //Aesthetically pleasing looping array values (TM)
            {

                Console.WriteLine(binArray[i]); //Show the value of a corresponding place value dictated by the i

            }

            Console.WriteLine("Please press any key to continue..."); //So that the user can stop and see the outputted value
            Console.ReadKey(); //Ditto ^^


        }

        //
        //AESTHETICALLY PLEASING EMPTY SPACE (TM)
        //

        public static void add(int inValA, int inValB, ref int inCarryIn, ref int carryOut, ref int output)
        {

            /* This is the C# equivalent of a real-world Full Adder; The circuit is as follows:
             * Since the code is hard to follow, please use this map (with appropriate variable
             *  names) to understand the relationships between the variables
             *            ______                 ______
             * valA}--|-\ \      \   |---------\ \     \
             *        |   | | XOR }-(AxorB)     | | XOR }-- output
             * valB}--U-/ /______/   |       |-/ /_____/
             *  |  |              |--|    |--|
             * carryIn}-----------U-------|
             *  |  |              |       |     
             *  |  |              |       |      _____
             *  |  |              |       |-----|     \
             *  |  |              |             | AND  }-(IandAB)____
             *  |  |              |-------------|_____/   |----\     \
             *  |  |                                            | OR  }-- CarryOut
             *  |  |                             _____    |----/_____/
             *  |  |----------------------------|     \   |
             *  |                               | AND  }-(AandB)
             *  |-------------------------------|_____/
             * 
             * Truth Table:
             * ____________________________
             * |   |   |     ||     |      |
             * | A | B | Cin || Out | Cout |
             * |___|___|_____||_____|______|
             * | 0 | 0 |  0  ||  0  |  0   |
             * | 1 | 0 |  0  ||  1  |  0   |
             * | 0 | 1 |  0  ||  1  |  0   |
             * | 1 | 1 |  0  ||  0  |  1   |
             * | 1 | 0 |  1  ||  0  |  1   |
             * | 0 | 1 |  1  ||  0  |  1   |
             * | 1 | 1 |  1  ||  1  |  1   |
             * | 0 | 0 |  1  ||  1  |  0   |
             * 
             * This took WAY too long to make.
             * 
             * Nonetheless, the code replicates this by converting the integer input
             *  into boolean to be worked with just like a logic gate, then outputs
             *  an integer to be worked with at a later time
            */

            bool valA = Convert.ToBoolean(inValA); //Converting input into boolean for easy manipulation
            bool valB = Convert.ToBoolean(inValB);
            bool carryIn = Convert.ToBoolean(inCarryIn);

            bool AxorB = false; //Stores the value of input A XOR input B
            bool IandAB = false; //Stores the value of carryIn AND (A XOR B)
            bool AandB = false; //Stores the value of A AND B

            output = 0; //reset output (because it remains constant throughout the program)
            carryOut = 0; //reset carryOut (because it remains constant throughout the program)
          
            Console.WriteLine("Adding...");

            if (valA ^ valB) //Is there a carry bit w/ only A and B?
            {

                AxorB = true; //Sets the value of the output of A XOR B to true

            }

            if (carryIn ^ AxorB) //If everything's 1, then the output is 1.
            {

                output = 1; //Sets the output to 1

            }

            if (carryIn && AxorB) //Will there be a carry bit w/ carryIn and OG?
            {

                IandAB = true; //Sets the value of the output of carryIn AND (valA XOR valB) to true

            }

            if (valA && valB) //Will there be a carry bit w/ A and B?
            {

                AandB = true; //Sets the output of A AND B to true

            }

            if (IandAB || AandB) //If either one of them has a carry bit, then there will be a carryOut
            {

                carryOut = 1; // Sets the carry bit to 1

            }

            Console.WriteLine("...Done"); //Aesthetically pleasing notification
            Console.WriteLine("Out = " + output); //Show user what the output is
            Console.WriteLine("Carry Out = " + carryOut); //Show user what the carry output is
            Console.WriteLine(); //Aesthetically pleasing space

        }

        public static void combineBin(ref int[] binInput, ref int binOutput, int bitWidth, bool overflow, int subtract)
        {

            //This function will combine the binary values inside of the binInput array and output
            // it inside of binOutput. The process is described more in detail further on.

            Console.WriteLine("Combining Binary Values into single Variable...");

            int powerOfTwo = 0; //This variable will store the 'place value' where the given value will be deposited

            if (overflow == false) //If there is no overflow, then:
            {

                powerOfTwo = bitWidth - 1; //The maximum place value will be the size of the bit width

            } else //If there is an overflow, then:
            {

                powerOfTwo = bitWidth; //The maximum place value will be the size of the bit width + 1, to accomodate the overflow

            }

            while (powerOfTwo >= 0) //While all of the possible place value values haven't been placed, then:
            {

                binOutput += ((binInput[powerOfTwo]) * ((int)Math.Pow(10, (powerOfTwo))));
                /*  Simplified version: binOutput = binOutput+(binInput[powerOfTwo]) * (10^(H-1))
                 *  Walk through it: for example, it's taking [1,1,0,1] and wants to make it 1,011
                 *  If the first 1 on the far left is the 0th place (or 2^0 place) then, it will take
                 *      that and multiply it by 1 (or 10^0), and depositing it into binOutput (1+0)
                 *  Now, take the second 1 on the far left, which is the 1st place (or 2^1 place), and 
                 *      take that and multiply it by 10 (or 10^1), and depositing it into binOutput (10+1) 
                 *  Now, take the 0, which is the 3rd place, which is the 2nd place (or 2^2 place), and 
                 *      take that and multiply it by 100 (or 10^2), and depositing it into binOutput (0+10+1)
                 *  Now, take the 1, which is in the 4th place, which is the 2^3 place, then take it and
                 *      multiply it by 1000 (or 10^3), and deposit it into binOutput (10000+0+10+1)
                 *  Now, print it. We'll get 1,011, exactly what we're expecting.
                 */
                powerOfTwo--; //decrement the value

            }

            if (subtract == 1 && overflow == true) //If the user chose to subtract:
            {

                binOutput = -1*((int)Math.Pow(10, bitWidth) - binOutput); //I still have no idea what this does.
                binInput[bitWidth + 1] = 0; //Set overflow to 0

            }

            Console.WriteLine("...Done");

        }
        
        public static void binDec(ref int[] binInput, ref int decOutput, int bitWidth, bool overflow)
        {
            
            //This function will convert a binary input into a decimal ouutput
            Console.WriteLine("Converting binary values into a decimal value...");
            
            int powerOfTwo = 0; //powerOfTwo will store the running power that the bit will be converted to
            
            if (overflow == false) //If there's no overflow, then:
            {
             
                powerOfTwo = bitWidth - 1;   //There will be no cycle for an overflow bit
                
            } else //Otherwise,
            {
                
                powerOfTwo = bitWidth; //There will be a cycle for an overflow bit
                
            }
            
            while (powerOfTwo > 0) //While there are still remaining cycles,
            {
                
                decOutput = decOutput + (binInput[powerOfTwo]*((int)Math.Pow(2,powerOfTwo))) //The decimal output
                
            }
            
            Console.WriteLine("...Done");
            
            
        }

    }
}
