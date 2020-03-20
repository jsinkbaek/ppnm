all: ms.pdf

ms.pdf: ms.tex plot-exp.tex
	pdflatex $<
	pdflatex $<

main.exe: main.cs exp.cs matlib.dll
	mcs $(word 1, $^) $(word 2, $^) -reference:$(lastword $^)

plot-exp.tex: plot-exp.gpi out.exp.txt
	gnuplot $<

out.exp.txt: main.exe
	mono $< > $@

LIBDIR = ../../matlib
SRC := $(LIBDIR)/matrix/vector.cs
SRC += $(LIBDIR)/ode/ode.cs

matlib.dll: $(SRC)
	mcs $^ -target:library -out:$@

clean:
	$(RM) *.log *.out *.aux *.pdf *.dvi
