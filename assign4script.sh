#Katelyn Jaing
#Email: kjaing18@csu.fullerton.edu
#Assignment 4: Traveling Ball
#CPSC 223N
#assign4script.sh

mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:assign4travel.dll assign4travel.cs
mcs -r:System.Windows.Forms.dll -r:assign4travel.dll -out:assign4.exe assign4main.cs

./assign4.exe





