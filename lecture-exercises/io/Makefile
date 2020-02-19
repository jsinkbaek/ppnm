all: out.txt out-stdin.txt

out.txt: cmdline.exe input.txt Makefile
	mono $< 1 2 3 4 > $@
	mono $< $$(cat input.txt) 1>> $@
	# 1> is standard output redirect, 2> is standard error output, >> is used if you want to append to the file instead of overwriting

out-stdin.txt: stdin.exe input.txt Makefile
	mono $< < input.txt  > $@
	cat input.txt | mono $< >> $@

cmdline.exe: cmdline.cs
	mcs $<

stdin.exe: stdin.cs
	mcs $<

clean:
	$(RM) *.exe out*

input.txt: Makefile
	/bin/echo -e '0.5\n2.5\n3.5' > $@
