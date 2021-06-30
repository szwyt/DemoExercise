sed -i 's/\r$//' publish.sh
docker rmi $(docker images | grep "none" | awk '{print $3}')
docker rm -f mongodb_demo
echo "Container deletion successful"
docker rmi -f mongodb
echo "Image deletion successful"
docker build  -t  mongodb -f mongodb ../
echo "image create successful"
docker run -p 5001:5001 --name mongodb_demo --restart=always -d  mongodb
echo "publish successful"