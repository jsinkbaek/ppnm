all: out.B.txt PlotC.svg

PlotC.svg: plotC.gpi out.data.txt out.fit.txt
	gnuplot $<

out.B.txt out.data.txt out.fit.txt out.fitu.txt out.fitl.txt: main.exe
	mono $< 1> $@ && cat $@

out.data.txt: out.B.txt

out.fit.txt: out.B.txt

out.fitu.txt: out.B.txt

out.fitl.txt: out.B.txt

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



