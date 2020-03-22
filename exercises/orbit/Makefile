out.txt: main.exe
	mono $< > $@
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
	rm -f *.exe out* log* *.dll
