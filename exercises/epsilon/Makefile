all: out.txt
	cat out.txt
out.txt:main.exe
	mono $< > $@
main.exe:main.cs approx.dll
	mcs -reference:approx.dll -out:$@ $<
approx.dll:approx.cs
	mcs -target:library -out:$@ $<
