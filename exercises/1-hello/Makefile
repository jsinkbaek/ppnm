all: out.txt

out.txt: hello.exe
	mono $< $@

hello.exe: hello.cs
	csc $<

.PHONY:clean
clean:
	rm -f *.svg *.exe out.txt *.dll
