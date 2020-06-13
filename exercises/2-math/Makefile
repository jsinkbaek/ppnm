all: out.txt
	cat out.txt
out.txt:main.exe
	mono $< > $@
main.exe:main.cs cmath.dll
	mcs -out:$@ -reference:cmath.dll $<
cmath.dll:cmath.cs complex.cs
	mcs $^ -target:library -out:$@

.PHONEY:clean
clean:
	rm -f *.svg *.exe *.txt *.dll
