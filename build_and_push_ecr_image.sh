#!/bin/bash

set -e

aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 092376285579.dkr.ecr.us-east-1.amazonaws.com

docker build -f ./Dockerfile-Temperature ./CloudWeather.Temperature -t cloudweather-temperature:latest

docker tag cloudweather-temperature:latest 092376285579.dkr.ecr.us-east-1.amazonaws.com/cloud-weather-temperature

docker push 092376285579.dkr.ecr.us-east-1.amazonaws.com/cloud-weather-temperature