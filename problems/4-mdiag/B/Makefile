all: out.B.txt PlotB1.svg PlotB3t.svg PlotB3r.svg PlotB4r.svg PlotB4t.svg
	cat out.B.txt

PlotB4t.svg: plotB4t.gpi out.b4time.txt
	gnuplot $<

PlotB4r.svg: plotB4r.gpi out.b4rot.txt
	gnuplot $<

PlotB3t.svg: plotB3t.gpi out.b3time.txt
	gnuplot $<

PlotB3r.svg: plotB3r.gpi out.b3rot.txt
	gnuplot $<

PlotB1.svg: plotB1.gpi out.time.txt
	gnuplot $<

out.B.txt out.time.txt out.b3time.txt out.b3rot.txt out.b4time.txt out.b4rot.txt: main.exe
	mono $< 1> out.B.txt

#out.time.txt: out.B.txt

#out.b3time.txt: out.B.txt

#out.b3rot.txt: out.B.txt

#out.b4time.txt: out.B.txt

#out.b4rot.txt: out.B.txt

main.exe: main.cs math.dll
	csc -out:$@ -reference:math.dll $<

LIBDIR = ../../../matlib
LOCALLIB = ../lib
SRC := $(LIBDIR)/matrix/matrix.cs
SRC += $(LIBDIR)/matrix/vector.cs
SRC += $(LOCALLIB)/mathelp.cs
SRC += $(LOCALLIB)/linalg.cs
SRC += ../../3-lsfit/lib/lsfit.cs

math.dll: $(SRC)
	csc -target:library -out:$@ $^


.PHONY:clean
clean:
	rm -f *.exe out.*.txt *.dll *.svg



