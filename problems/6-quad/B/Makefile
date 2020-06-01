all: out.B.txt

out.B.txt: main.exe
	mono $< 1> out.B.txt 2> out.err.txt && cat out.B.txt

main.exe: main.cs math.dll quado8.dll
	csc -out:$@ -reference:math.dll -reference:quadmlib=quado8.dll $<

LIBDIR = ../../../matlib
LOCALLIB = ../lib
SRC := $(LIBDIR)/matrix/matrix.cs
SRC += $(LIBDIR)/matrix/vector.cs
SRC += $(LOCALLIB)/quad.cs
SRC2 := $(LIBDIR)/integration/quado8.cs

math.dll: $(SRC)
	csc -target:library -out:$@ $^

quado8.dll: $(SRC2)
	csc -target:library -out:$@ $^


.PHONY:clean
clean:
	rm -f *.svg *.exe out.*.txt *.dll



