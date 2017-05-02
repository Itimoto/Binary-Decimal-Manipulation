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
