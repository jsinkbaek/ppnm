CSFLAGS = -optimize+ -platform:x64
.PHONEY:all
all: out.txt
	cat out.txt
out.txt : main.exe
	mono main.exe > out.txt
main.exe : main.cs world.dll
	mcs $(CSFLAGS) -out:main.exe -reference:world.dll  main.cs
world.dll : world.cs
	mcs $(CSFLAGS) -out:world.dll -target:library world.cs
# If you have file clean in project uncomment (never do *.)
#.PHONEY:clean
clean:
	rm --force out* *.exe *.dll
