all: PlotA.svg out.A.txt PlotAdevi.svg

PlotA.svg: plotA.gpi out.rk12.txt out.rk45.txt
	gnuplot $<

PlotAdevi.svg: plotAdevi.gpi out.deviation.txt
	gnuplot $<

out.A.txt out.rk12.txt out.rk45.txt out.deviation.txt: main.exe
	mono $< 1> out.A.txt 2> out.err.txt && cat out.A.txt

main.exe: main.cs math.dll
	csc -out:$@ -reference:math.dll $<

LIBDIR = ../../../matlib
LOCALLIB = ../lib
SRC := $(LIBDIR)/matrix/matrix.cs
SRC += $(LIBDIR)/matrix/vector.cs
SRC += $(LOCALLIB)/ode.cs

math.dll: $(SRC)
	csc -target:library -out:$@ $^


.PHONY:clean
clean:
	rm -f *.svg *.exe out.*.txt *.dll



