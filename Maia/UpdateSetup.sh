#!/bin/bash

kill -9 $2
echo "Updating..."
cp $1/*.* .
echo "Cleaning up..."
rm -f $1/*
rmdir $1
echo "Update complete!"
echo "Application will now restart!"
sleep 5
exec Run.sh