all: PlotA.svg PlotB.svg

PlotA.svg: A-linear/PlotA.svg
	cp A-linear/PlotA.svg ./

A-linear/PlotA.svg: A-linear/Makefile
	cd A-linear/ && make

PlotB.svg: B-quadratic/PlotB.svg
	cp B-quadratic/PlotB.svg ./

B-quadratic/PlotB.svg: B-quadratic/Makefile
	cd B-quadratic/ && make


.PHONY:clean
clean:
	cd A-linear/ && make clean
	cd B-quadratic/ && make clean
	rm -f PlotA.svg PlotB.svg
