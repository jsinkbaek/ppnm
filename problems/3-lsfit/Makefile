all: PlotA.svg PlotC.svg out.A.txt out.B.txt

PlotA.svg out.A.txt: A-lsqr/PlotA.svg
	cp A-lsqr/PlotA.svg ./
	cp A-lsqr/out.A.txt ./

out.A.txt: PlotA.svg

A-lsqr/PlotA.svg: A-lsqr/Makefile
	cd A-lsqr/ && make

PlotC.svg out.B.txt: BC/PlotC.svg
	cp BC/PlotC.svg ./
	cp BC/out.B.txt ./

out.B.txt: PlotC.svg

BC/PlotC.svg: BC/Makefile
	cd BC/ && make


.PHONY:clean
clean:
	cd A-lsqr/ && make clean
	cd BC/ && make clean
	rm -f PlotA.svg PlotB.svg
