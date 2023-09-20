#!/bin/bash

set -e

aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 092376285579.dkr.ecr.us-east-1.amazonaws.com

docker build -f ./Dockerfile-DataLoader ./CloudWeather.DataLoader -t cloudweather-dataloader:latest

docker tag cloudweather-dataloader:latest 092376285579.dkr.ecr.us-east-1.amazonaws.com/cloud-weather-dataloader 

docker push 092376285579.dkr.ecr.us-east-1.amazonaws.com/cloud-weather-dataloader