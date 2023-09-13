#!/bin/bash

if [ $1 ]
then
    dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
    if [ $2 ]
    then
        reportgenerator -reports:"./Tests/coverage.cobertura.xml" -targetdir:"./Tests/Coveragereport" -reporttypes:Html
        if [ $3 ]
        then
            google-chrome ./Tests/Coveragereport/index.html
        fi
    fi
else
    dotnet test
fi

