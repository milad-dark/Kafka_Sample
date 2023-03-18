1- Go to kafka folder> bin > windows
	1- go to config folder edit server.properties
		- listeners=PLAINTEXT://localhost:9092
		- log.dirs= logs/kafka-logs

	2- go to config folder edit zookeeper.properties
		- dataDir= logs/zookeeper
		
	3- cd kafka/bin/windows
	
2- Create zoopeeter server
	CMD\kafka\bin\windows> zookeeper-server-start.bat .\config\zookeeper.properties

3- Create kafka server
	CMD\kafka\bin\windows> kafka-server-start.bat .\config\server.properties

4- Create kafka topics
	CMD\kafka\bin\windows> kafka-topics.bat --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic {topic_name}  -note: if error zookeeper is not a recognized option run second comment
	CMD\kafka\bin> kafka-topics.sh --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic {topic_name}
4-1- Get list of topics
	CMD\kafka\bin\windows> kafka-topics.bat --list --bootstrap-server localhost:9092

5- Create Producer for send message
	CMD\kafka\bin\windows> kafka-console-producer.bat --broker-list localhost:9092 --topic {topic_name}
	- type message and enter

5- Create Consumer for get messages
	CMD\kafka\bin\windows> kafka-console-consumer.bat --bootstrap-server localhost:9092 --from-beginning --topic {topic_name}
	- get all message from {topic_name}

Notes: All run commands in {file}.sh in directory bin not bin/windows


6- Shut Down Zookeeper and Kafka
	-CMD\kafka\bin\windows> zookeeper-server-stop.bat
	-CMD\kafka\bin\windows> kafka-server-stop.bat
