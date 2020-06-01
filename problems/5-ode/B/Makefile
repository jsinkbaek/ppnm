all: PlotB.svg PlotB0.svg PlotB1.svg PlotB2.svg PlotB3.svg out.B.txt

PlotB.svg: plotB.gpi out.B0.txt out.B1.txt out.B2.txt out.B3.txt
	gnuplot $<

PlotB0.svg: plotB0.gpi out.B0.txt
	gnuplot $<

PlotB1.svg: plotB1.gpi out.B1.txt
	gnuplot $<

PlotB2.svg: plotB2.gpi out.B2.txt
	gnuplot $<

PlotB3.svg: plotB3.gpi out.B3.txt
	gnuplot $<

out.B.txt out.B0.txt out.B1.txt out.B2.txt: main.exe
	mono $< 1> out.B.txt 2> out.err.txt && cat out.B.txt

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



