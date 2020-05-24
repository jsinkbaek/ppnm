all: out.A.txt out.B.txt

out.A.txt: A-gramschmidt/out.A.txt
	cp A-gramschmidt/out.A.txt ./

A-gramschmidt/out.A.txt: A-gramschmidt/Makefile
	cd A-gramschmidt/ && make

out.B.txt: B-qr_gs_inverse/out.B.txt
	cp B-qr_gs_inverse/out.B.txt ./

B-qr_gs_inverse/out.B.txt: B-qr_gs_inverse/Makefile
	cd B-qr_gs_inverse/ && make


.PHONY:clean
clean:
	cd A-gramschmidt/ && make clean
	cd B-qr_gs_inverse/ && make clean
	rm -f out.A.txt out.B.txt
