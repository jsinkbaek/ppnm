all: out.B.txt #Plot.calls.svg Plot.time.svg Plot.deviation.svg

Plot.deviation.svg: plot.deviation.gpi out.z.txt out.xy.txt
	gnuplot $<

Plot.time.svg: plot.time.gpi out.z.txt out.xy.txt
	gnuplot $<

Plot.calls.svg: plot.calls.gpi out.z.txt out.xy.txt
	gnuplot $<

out.B.txt out.zcalls.txt out.xycalls.txt: main.exe
	mono $< 1> out.B.txt 2> out.err.txt && cat out.B.txt

main.exe: main.cs math.dll
	csc -out:$@ -reference:math.dll $<

LIBDIR = ../matlib
LOCALLIB = ../lib
SRC := $(LIBDIR)/matrix/matrix.cs
SRC += $(LIBDIR)/matrix/vector.cs
SRC += $(LIBDIR)/complex/cmath.cs
SRC += $(LIBDIR)/complex/complex.cs
SRC += $(LOCALLIB)/rootf.cs
SRC += $(LIBDIR)/linalg/linalg.cs

math.dll: $(SRC)
	csc -target:library -out:$@ $^


.PHONY:clean
clean:
	rm -f *.svg *.exe out.* *.dll



