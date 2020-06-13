out.txt: main.exe
	mono $< > $@ && cat out.txt
main.exe: main.cs vector3d.dll
	mcs -target:exe -out:$@ -reference:$(word 2,$^) $<
vector3d.dll: vector3d.cs
	mcs -target:library -out:$@ $<

.PHONY:clean
clean: 
	rm -f *.svg *.exe out.* *.dll
