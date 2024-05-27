
kcat-produce:
	kcat -b localhost:19092 -t opensearch1 -P msgs/msg1.json

kcat-produce2:
	kcat -b localhost:19092 -t students1 -P msgs/student2.json

kcat-ls:
	kcat -L -b localhost:19092

connector-create:
	curl -i -X POST -H "Accept:application/json" -H "Content-Type:application/json" localhost:8083/connectors/ -d @ops2.json

connector-delete:
	curl -i -X DELETE localhost:8083/connectors/opensearch-students-sink

connector-restart:
	curl -i -X POST localhost:8083/connectors/opensearch-students-sink/restart

connector-ls:
	curl -i -H "Accept:application/json" -H "Content-Type:application/json" localhost:8083/connectors/

connector-config:
	curl -H "Accept:application/json" -H "Content-Type:application/json" localhost:8083/connectors/opensearch-students-sink/config | jq

connector-status:
	curl -H "Accept:application/json" -H "Content-Type:application/json" localhost:8083/connectors/opensearch-connector/status | jq
