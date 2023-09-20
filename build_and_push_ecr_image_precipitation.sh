#!/bin/bash

set -e

aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 092376285579.dkr.ecr.us-east-1.amazonaws.com

docker build -f ./Dockerfile-Precipitation ./CloudWeather.Precipitation -t cloudweather-precipitation:latest

docker tag cloudweather-precipitation:latest 092376285579.dkr.ecr.us-east-1.amazonaws.com/cloud-weather-precipitation

docker push 092376285579.dkr.ecr.us-east-1.amazonaws.com/cloud-weather-precipitation