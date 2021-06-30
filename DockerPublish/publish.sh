sed -i 's/\r$//' publish.sh
docker ps -a | grep -w mongodb_demo | awk '{print $1 }'|xargs docker rm -f
echo "Container delete successful"
docker rmi -f mongodb
echo "Image delete successful"
docker build  -t  mongodb -f mongodb ../
echo "image create successful"
docker run -p 5001:5001 --name mongodb_demo --restart=always -d  mongodb
echo "publish successful"
docker rmi $(docker images | grep "none" | awk '{print $3}')
echo "Image is none delete successful"