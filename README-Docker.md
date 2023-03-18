============================ Docker Compose =================================================
1- Go into compose file folder (ex: d:kafka)
	- Add docker-compose.yml file to folder

2- Add into docker-compose.yml file
---
version: '3.4'
services:
  zookeeper:
    image: confluentinc/cp-zookeeper:latest
    hostname: zookeeper
    container_name: zookeeper
    ports:
      - "2181:2181"
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181
      ZOOKEEPER_TICK_TIME: 2000
  broker:
    image: confluentinc/cp-server
    hostname: broker
    container_name: broker
    depends_on:
      - zookeeper
    ports:
      - "9092:9092"
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: 'zookeeper:2181'
      KAFKA_LISTENER_SECURITY_PROTOCOL_MAP: PLAINTEXT:PLAINTEXT,PLAINTEXT_HOST:PLAINTEXT
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://broker:29092,PLAINTEXT_HOST://localhost:9092
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1
      KAFKA_GROUP_INITIAL_REBALANCE_DELAY_MS: 0
      KAFKA_CONFLUENT_LICENSE_TOPIC_REPLICATION_FACTOR: 1
      CONFLUENT_SUPPORT_CUSTOMER_ID: 'anonymous'

3- in terminal(cmd, powershell) type :
	- docker-compose up
	- wait for download images and run containers

4- get list of run container type :
	- docker ps 

5- type this command to go inside server kafka container
	- docker exec -it {cp-server container id} bash 

6- LIST ALL KAKFA TOPICS
	- kafka-topics --list --bootstrap-server localhost:9092

7-  CREATE NEW KAKFA TOPIC
 	- kafka-console-producer --bootstrap-server  localhost:9092 --topic {topic_name}

8-  READ VALUE FROM KAFKA TOPIC
	- kafka-console-consumer --bootstrap-server localhost:9092 --topic {topic_name} --from-beginning

