all: PlotA.svg PlotB1.svg PlotB3t.svg PlotB3r.svg PlotB4t.svg PlotB4r.svg out.A.txt out.B.txt

PlotA.svg out.A.txt: A/PlotA.svg
	cp A/PlotA.svg ./
	cp A/out.A.txt ./

out.A.txt: PlotA.svg

A/PlotA.svg: A/Makefile
	cd A/ && make

out.B.txt PlotB1.svg PlotB3t.svg PlotB3r.svg PlotB4t.svg PlotB4r.svg: B/out.B.txt
	cp B/out.B.txt ./
	cp B/PlotB1.svg ./
	cp B/PlotB3t.svg ./
	cp B/PlotB3r.svg ./
	cp B/PlotB4t.svg ./
	cp B/PlotB4r.svg ./

PlotB1.svg: out.B.txt

PlotB3t.svg: out.B.txt

PlotB3r.svg: out.B.txt

PlotB4t.svg: out.B.txt

PlotB4r.svg: out.B.txt

B/out.B.txt: B/Makefile
	cd B/ && make


.PHONY:clean
clean:
	cd A/ && make clean
	cd B/ && make clean
	rm -f *.svg out.*.txt
