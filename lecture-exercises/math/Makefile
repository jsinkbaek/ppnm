CSFLAGS = -optimize+ -platform:x64
.PHONEY:all
all: out.txt
	cat out.txt
out.txt : main.exe
	mono main.exe > out.txt
main.exe : main.cs cmath.dll
	mcs $(CSFLAGS) -out:main.exe -reference:cmath.dll main.cs

cmath.dll: ../lib/complex/complex.cs ../lib/complex/cmath.cs
	mcs $^ -target:library -out:$@
	cp ../lib/complex/cmath.dll .

# If you have file clean in project uncomment
#.PHONEY:clean
clean:
	rm --force out.txt main.exe
