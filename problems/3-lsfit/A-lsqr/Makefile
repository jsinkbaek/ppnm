all: out.A.txt PlotA.svg

PlotA.svg: plotA.gpi out.data.txt out.fit.txt
	gnuplot $<

out.A.txt out.data.txt out.fit.txt: main.exe
	mono $< 1> $@ && cat $@

out.data.txt: out.A.txt

out.fit.txt: out.A.txt

main.exe: main.cs math.dll
	csc -out:$@ -reference:math.dll $<

LIBDIR = ../../../matlib
LOCALLIB = ../lib
SRC := $(LIBDIR)/matrix/matrix.cs
SRC += $(LIBDIR)/matrix/vector.cs
SRC += $(LOCALLIB)/lsfit.cs
SRC += ../../2-lineq/lib/linalg.cs

math.dll: $(SRC)
	csc -target:library -out:$@ $^


.PHONY:clean
clean:
	rm -f *.exe out.*.txt *.dll *.svg



