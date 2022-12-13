#! /usr/bin/env bash

doppler secrets download --name-transformer dotnet --no-file > doppler.appsettings.json
exec sh -c 'sleep 3 && rm -f ./doppler.appsettings.json ./bin/Debug/net6.0/doppler.appsettings.json' &
exec "$@"
