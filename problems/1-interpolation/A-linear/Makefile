all: plotA.svg

plotA.svg: plotA.gpi out.A.txt testdata.txt
	gnuplot $<

out.A.txt: main.exe
	mono $< 1> $@

main.exe: main.cs math.dll testdata.txt
	csc -out:$@ -reference:math.dll $<

testdata.txt: datamaker.exe
	mono $< 1> $@

datamaker.exe: ../lib/dataMaker.cs math.dll
	csc -out:$@ -reference:math.dll $<

LIBDIR = ../../../matlib
LOCALLIB = ../lib
SRC := $(LIBDIR)/matrix/matrix.cs
SRC += $(LIBDIR)/matrix/vector.cs
SRC += $(LOCALLIB)/dataMaker.cs
SRC += $(LOCALLIB)/interp.cs

math.dll: $(SRC)
	csc -target:library -out:$@ $^


.PHONY:clean
clean:
	rm -f *.exe out.*.txt testdata.txt *.svg *.dll



