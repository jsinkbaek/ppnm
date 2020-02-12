# Set warning mode to highest level for debug (4), and turn off optimization
CSFLAGS = mcs -warn:2 -optimize-

out.txt : main.exe
	mono $< > $@
main.exe : main.cs
	mcs $<

clean:
	rm -f out.txt,main.exe
