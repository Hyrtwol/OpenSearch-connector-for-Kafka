# OpenSearch connector for Kafka

POC using OpenSearch connector for Kafka.

## How to run locally

1. Run docker-compose up
2. Add the connector by posting the setup to the connector. See [setup.http](KafkaProducerExample\setup.http)
3. Add 1000 persons by running KafkaProducerExample
4. In [OpenSearch Dashboard](http://localhost:5601/) create a new Index for `students1-*`
5. In [OpenSearch Dashboard](http://localhost:5601/) you can then discover the student data.

## Endpoints

* [UI for Apache Kafka](http://localhost:8080/)
* [OpenSearch Dashboard](http://localhost:5601/)
* [Connector](http://localhost:8083/)
* [Neo4j](http://localhost:7474/) (not connected to kafka yet)

## Research

* <https://www.baeldung.com/ops/kafka-docker-setup>
* <https://kafkatool.com/download.html>
* <https://github.com/edenhill/librdkafka/blob/master/CONFIGURATION.md>
* <https://randomtools.io/dev-tools/data-generator/>
