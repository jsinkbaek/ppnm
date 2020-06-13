all: out.txt Plot.erf.txt Plot.gam.txt

Plot.erf.txt: plot.erf.gpi out.erf.txt erf.tab.txt
	gnuplot $<

Plot.gam.txt: plot.gam.gpi out.gam.txt gam.tab.txt
	gnuplot $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs
	csc $<
