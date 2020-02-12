CSFLAGS = -optimize+ -platform:x64
.PHONEY:all
all: out.txt
	cat out.txt
out.txt : main.exe
	mono main.exe > out.txt
main.exe : main.cs
	mcs $(CSFLAGS) -out:main.exe main.cs

# If you have file clean in project uncomment
#.PHONEY:clean
clean:
	rm --force out.txt main.exe
