all: out.A.txt out.B.txt PlotA.svg PlotB.svg

out.A.txt PlotA.svg: A/out.A.txt
	cp A/out.A.txt ./
	cp A/PlotA.svg ./


A/out.A.txt: A/Makefile
	cd A/ && make

out.B.txt PlotB.svg: B/out.B.txt
	cp B/out.B.txt ./
	cp B/PlotB.svg ./

B/out.B.txt: B/Makefile
	cd B/ && make


.PHONY:clean
clean:
	cd A/ && make clean
	cd B/ && make clean
	rm -f *.svg out.*.txt
