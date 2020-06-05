all: out.B.txt PlotB.svg

PlotB.svg: plotB.gpi out.func.txt
	gnuplot $<

out.B.txt out.func.txt: main.exe
	mono $< 1> out.B.txt 2> out.err.txt && cat out.B.txt

out.func.txt: out.B.txt

main.exe: main.cs math.dll
	csc -out:$@ -reference:math.dll $<

LIBDIR = ../../../matlib
LOCALLIB = ../lib
SRC := $(LIBDIR)/matrix/matrix.cs
SRC += $(LIBDIR)/matrix/vector.cs
SRC += $(LOCALLIB)/rootf.cs
SRC += ../../2-lineq/lib/linalg.cs
SRC += ../../5-ode/lib/ode.cs

math.dll: $(SRC)
	csc -target:library -out:$@ $^


.PHONY:clean
clean:
	rm -f *.svg *.exe out.*.txt *.dll



