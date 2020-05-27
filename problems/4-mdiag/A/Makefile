all: out.A.txt PlotA.svg

PlotA.svg: plotA.gpi out.fun.txt
	gnuplot $<

out.A.txt out.fun.txt: main.exe
	mono $< 1> $@ && cat $@

out.fun.txt: out.A.txt

main.exe: main.cs math.dll
	csc -out:$@ -reference:math.dll $<

LIBDIR = ../../../matlib
LOCALLIB = ../lib
SRC := $(LIBDIR)/matrix/matrix.cs
SRC += $(LIBDIR)/matrix/vector.cs
SRC += $(LOCALLIB)/mathelp.cs
SRC += $(LOCALLIB)/linalg.cs

math.dll: $(SRC)
	csc -target:library -out:$@ $^


.PHONY:clean
clean:
	rm -f *.exe out.*.txt *.dll



