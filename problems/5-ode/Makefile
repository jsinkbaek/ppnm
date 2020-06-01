all: PlotA.svg PlotAdevi.svg out.A.txt out.B.txt PlotB0.svg PlotB1.svg PlotB2.svg PlotB3.svg PlotB.svg

PlotA.svg PlotAdevi.svg out.A.txt: A/PlotA.svg
	cp A/PlotA.svg ./
	cp A/PlotAdevi.svg ./
	cp A/out.A.txt ./

PlotAdevi.svg: PlotA.svg

out.A.txt: PlotA.svg

A/PlotA.svg: A/Makefile
	cd A/ && make

out.B.txt PlotB0.svg PlotB1.svg PlotB2.svg PlotB3.svg PlotB.svg: B/out.B.txt
	cp B/out.B.txt ./
	cp B/PlotB0.svg ./
	cp B/PlotB1.svg ./
	cp B/PlotB2.svg ./
	cp B/PlotB3.svg ./
	cp B/PlotB.svg ./

PlotB0.svg: out.B.txt

PlotB1.svg: out.B.txt

PlotB2.svg: out.B.txt

PlotB3.svg: out.B.txt

PlotB.svg: out.B.txt

B/out.B.txt: B/Makefile
	cd B/ && make


.PHONY:clean
clean:
	cd A/ && make clean
	cd B/ && make clean
	rm -f *.svg out.*.txt
