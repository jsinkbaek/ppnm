all: out.A.txt out.B.txt out.C.txt Plot.time.svg Plot.calls.svg

out.A.txt: A/out.A.txt
	cp A/out.A.txt ./


A/out.A.txt: A/Makefile
	cd A/ && make

out.B.txt : B/out.B.txt
	cp B/out.B.txt ./

B/out.B.txt: B/Makefile
	cd B/ && make

out.C.txt Plot.time.svg Plot.calls.svg: C/out.C.txt
	cp C/out.C.txt ./
	cp C/Plot.time.svg ./
	cp C/Plot.calls.svg ./

C/out.C.txt: C/Makefile
	cd C/ && make


.PHONY:clean
clean:
	cd A/ && make clean
	cd B/ && make clean
	cd C/ && make clean
	rm -f *.svg out.*.txt
