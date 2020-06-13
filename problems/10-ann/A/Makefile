all: out.A.txt PlotA.svg

PlotA.svg: plotA.gpi out.train.txt out.test.txt
	gnuplot $<

out.A.txt out.train.txt out.test.txt: main.exe
	mono $< 1> out.A.txt 2> out.err.txt && cat out.A.txt

main.exe: main.cs math.dll
	csc -out:$@ -reference:math.dll $<

LIBDIR = ../../../matlib
LOCALLIB = ../lib
SRC := $(LIBDIR)/matrix/matrix.cs
SRC += $(LIBDIR)/matrix/vector.cs
SRC += $(LOCALLIB)/ann.cs
SRC += ../../8-min/lib/minimization.cs

math.dll: $(SRC)
	csc -target:library -out:$@ $^


.PHONY:clean
clean:
	rm -f *.svg *.exe out.*.txt *.dll



