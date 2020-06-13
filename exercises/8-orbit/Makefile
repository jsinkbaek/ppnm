all: out.txt PlotA.txt

PlotA.txt: plotA.gpi aout
	gnuplot $<

out.txt aout b1out b2out b3out: main.exe
	mono $< 1> $@ 2> out.err.txt
	cat $@

main.exe: main.cs a.cs b.cs matlib.dll
	mcs $(word 1, $^) $(word 2, $^) $(word 3, $^) -reference:$(REFS)

REFS = matlib.dll
LIBDIR = ../../matlib
SRC = $(LIBDIR)/ode/ode.cs
SRC += $(LIBDIR)/matrix/vector.cs

matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe out* log* *.dll *.svg aout b*out
