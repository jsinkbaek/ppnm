out.txt: main.exe
	cat in.txt | mono $< > $@
	cat $@
main.exe: main.cs
	 mcs $<

.PHONY:clean
clean:
	rm -f out* *.exe *.dll
