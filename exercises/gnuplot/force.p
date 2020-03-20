set autoscale          # Scales axes automatically 
unset log              # Remove any log-scaling
unset label            # Remove any previous labels
set xtic auto          
set ytic auto
set title "Force Deflection Data for a Beam and a Column"
set xlabel "Deflection (meters)"
set ylabel "Force (kN)"
set key at 0.01,100       # move legend/key 
set label "Yield Point" at 0.003,260
set arrow from 0.0028,250 to 0.003,280
set xrange [0.0:0.022]
set yrange [0:325]
plot "force.dat" using 1:2 title 'Column' with linespoints, \
        "force.dat" using 1:3 title 'Beam' with points
