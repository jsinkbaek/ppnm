all:out.txt
	cat $<
out.txt: main.exe
	mono $< > $@ 2>log

main.exe:main.cs math.dll
	mcs $< -reference:math.dll

math.dll: quado4.cs quadc5.cs quadc7.cs quado8.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.dll out* log*
