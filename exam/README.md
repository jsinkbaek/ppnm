This folder contains my exam response to the Practical Programming and Numerical methods exam.
My student number is 201604681. So my exam problem is 81 % 22 = 15.
My C sharp compiler info, and mono version, can be found in the README of the course folder.

---------------------------------------------------
Problem 15 - Rootfinding: 1D complex vs. 2D real.
---------------------------------------------------

Part A:
I test a Newton's method for rootfinding of 1D complex analytic functions of complex variables, 
using the analytic derivative of the functions.
I compare this method with the method for rootfinding of multi-variable real functions, by converting the 
1D complex equation f(z) = 0 into two real equations Re(f(x+iy))=0, Im(f(x+iy))=0. This multi-variable method
utilizes a quasi-Newton method, where the Jacobi matrix is calculated using finite differences.
They should all produce the same results, and the 1D complex method should use slightly fewer function calls, given
its specialization.

Part B:
I implement a 1D complex quasi Newton's method for rootfinding of complex functions. It uses a simple
derivative approximation using finite differences. I then test that it gives the same result as the 1D complex
Newton's method on an analytic function.
Then I test it on a non-analytic function f = conj(z), and compare with the 2D case.

Part C:
Here I plot function calls and execution time for calculation of exponential (potens) functions of increasing
exponent. It gives a basic illustration of the extra overhead needed when using the more general routine and
treating the problem as a 2D real one, instead of the more streamlined 1D complex treatment.
Both methods use finite difference approximations for the derivative.


---------------------------------------------------

lib/rootf.cs:
I have included 2 rootfinding methods, both contained in the lib/rootf.cs file.
One deals with 1D complex functions of complex variables. 
The other is an overload using multi-variable functions, taken from my solution to the assignment problem on 
rootfinding.

Dependencies:
The 1D complex method depends on Dmitris implementation of complex numbers and complex math.
The multi-variable method depends on Dmitris implementation of vectors and matrices, and my linear equations solver.
However, some additional features have been implemented in the vector.cs file by me (notably a traditional euclidean norm calculation).
As such, all dependencies have been included directly in the exam/matlib/, to be sure that it is included.
