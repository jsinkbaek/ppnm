This folder contains my exam response to the Practical Programming and Numerical methods exam.
My student number is 201604681. So my exam problem is 81 % 22 = 15.

Problem 15 - Rootfinding: 1D complex vs. 2D real.

lib/rootf.cs:
I have included 2 rootfinding methods, both contained in the lib/rootf.cs file.
One deals with 1D complex functions of complex variables. 
The other is an overload using multi-variable functions, taken from my solution to the course problem on rootfinding.

Dependencies:
The 1D complex method depends on Dmitris implementation of complex numbers and complex math.
The multi-variable method depends on Dmitris implementation of vectors and matrices, and my linear equations solver.
However, some additional features have been implemented in the vector.cs file by me (notably a traditional euclidean norm calculation).
As such, all dependencies have been included directly in the exam/matlib/, to be sure that it is included.

main.cs:
In main.cs, I test the 1D complex method on a complex function, and do the same for a 2D real version on the multi-variable method.
After that, I compare the two methods by rootfinding of z^p+5 for increasing p.
Here I note that the 1D complex rootfinder is both faster in execution time, and completes using fewer function calls.
Those results can be found in Plot.calls.svg and Plot.time.svg.
Plot.deviation.svg shows deviation from the closest (and therefore expected) root, and is useful to show that the two methods produce equivalent results.
