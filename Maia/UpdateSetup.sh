#!/bin/bash

dir=$1
proc=$2

kill -9 $proc
echo "Updating..."
cp $dir/*.* .
echo "Cleaning up..."
rm -f $dir/*
rmdir $dir
echo "Update complete!"
echo "Application will now restart!"
sleep '5'
./Run.sh