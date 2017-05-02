# Binary-Decimal-Manipulation
This is a Binary Adder/Subtractor that creates takes Decimal input/output. This effectively works by taking Decimal input, converting it into binary, then, using a virtualized Adder/Subtractor circuit, adding both Binary inputs, then taking both Binary inputs and displaying it on a single line on the console, then converting the Binary output and converting it into Decimal again.

The program is capable of adding/subtracting numbers less than 2^(bitWidth) - 1. At the time of writing, I've set the bitWidth to 32 bits, so... the largest number to add/subtract is 4,294,967,295.

<h1>Usage</h1>
<p>You're greeted with a prompt of whether or not you'd like to add or subtract</p>
<p><i>Would you like to add or subtract? If you're subtracting, please put (y)es, if not, please put (n)o</i></p>
<p>The program will proceed with adding if you put 'no', 'No', 'n', or 'N'</p>
<p>The program will begin to start subtracting if you put 'yes', 'Yes', 'y', or 'Y'</p>
<p><i>Thank you. Please input values:</i></p>
<p>For this, it depends if you're adding or subtracting. If you're adding, then the only thing that you have to worry about is that it is less than <b>2^(biitWidth) - 1</b>, or <b>4,294,967,295.</b> I've added a 'feature' that prevents you from inputting anything higher, to prevent a crash.</p>
<p>If you're subtracting, then the second value that you will input will be the same as <i>-1 * (your input)</i>. Also, in this case, make sure that the second value is less than the first, as negative outputs <b>do not work well</b>, as the program uses unsigned binary. Thus, if you do this, then you will receive a well-deserved <b>faulty output.</b></p>
<p><i>Thank you. Press any key to continue</i></p>
<p>Now comes the fun stuff. I wrote this program to illustrate what Binary is like. I made some of the program 'stepping,' so that you can see the converted binary values of the decimal input. So, in general, the majority of the garble that comes out of the program is the result of what's going on throughout the program. I'll outline that further on in the README.</p>
<p><i>...Done. Would you like to add/subtract again? (y/n)</i></p>
<p>If you input 'yes', 'Yes', 'y', or 'Y', then the program will start again, all of the variables will be cleared, and you can add/subtract again. If you input anything other than that, then you will see this:</p>
<p><i>Press any key to exit the program</i></p>
<p>In which case, you will just press a key to exit the program.</p>

<h1>Explanation</h1>
<p>If you don't understand what my boatload of comments mean, but want to know how this works, then <em>this</em> is the section for you!</p>
<p>In summary, <i>Main()</i> takes in inputs A and B, then places it into the function <i>decBin()</i> to convert the Decimal input to Binary. Then, if the user decides to subtract, a small function inside of <i>Main()</i> will invert the bits of the (converted Binary) Input B. Then, the program will add the two (now arrays) into a for() loop with the <i>add()</i> function inside. If the user chose to subtract, then 1 will be put into the carryIn of the <i>add()</i> function by law of two's complement. After the output of <i>add()</i> is stored in binOut[], the values stored within are taken and combined into a single number for easiy display on the console by running it through the function <i>combinBin()</i>. After this, the values of binOut[] are converted into a decimal output by running it through the function <i>binDec()</i> Finally, both the Binary and the Decimal output are displayed on the console via <i>Main()</i> In the end, you, the user, are given a choice between adding/subtracting again or exiting the program. There is a label inside of <i>Main()</i> called 'Reset:' to which the program will 'goto' if the user decides so.</p>
<!--<h3>How <i>decBin()</i> Works</h3>
<p></p>-->
