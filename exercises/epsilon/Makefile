all: out.txt
	cat out.txt
out.txt:main.exe
	mono $< > $@
main.exe:main.cs
	mcs -out:$@ $<
