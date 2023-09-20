#!/bin/bash

set -e

aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 092376285579.dkr.ecr.us-east-1.amazonaws.com 

docker build -f ./Dockerfile-WeatherReport ./CloudWeather.Report -t cloudweather-report:latest

docker tag cloudweather-report:latest 092376285579.dkr.ecr.us-east-1.amazonaws.com/cloud-weather-report

docker push 092376285579.dkr.ecr.us-east-1.amazonaws.com/cloud-weather-report