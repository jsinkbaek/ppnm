all: out.A.txt out.B.txt

out.A.txt: A/out.A.txt
	cp A/out.A.txt ./


A/out.A.txt: A/Makefile
	cd A/ && make

out.B.txt: B/out.B.txt
	cp B/out.B.txt ./

B/out.B.txt: B/Makefile
	cd B/ && make


.PHONY:clean
clean:
	cd A/ && make clean
	cd B/ && make clean
	rm -f *.svg out.*.txt
