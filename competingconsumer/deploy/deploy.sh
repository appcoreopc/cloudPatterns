resourceGroupName="sbCompetingConsumerGroup"

az group create --name $resourceGroupName --location australiasoutheast

namespaceName=sbService550

az servicebus namespace create --resource-group $resourceGroupName --name $namespaceName --location australiasoutheast

az servicebus queue create --resource-group $resourceGroupName --namespace-name $namespaceName --name TradeQueue

az servicebus topic create --resource-group $resourceGroupName --namespace-name sbService550 --name tradetopic

az servicebus topic subscription create --resource-group $resourceGroupName --namespace-name $namespaceName --topic-name tradetopic --name tradesubscription

Endpoint=sb://sbservice550.servicebus.windows.net/;SharedAccessKeyName=TradeTopicPolicy;SharedAccessKey=S7jZdIxUrjNuOb9lVQ6WvRLAE7EK0CutiWgn9QEdPy0=;

dotnet new console -n TopicConsumer

dotnet new console -n TopicProducer

dotnet add package Microsoft.Azure.ServiceBus --version 3.4.0

az group delete --resource-group $resourceGroupName


Event Hub

az eventhubs namespace create --name tradeNs --resource-group $resourceGroupName -l australiasoutheast

az eventhubs eventhub create --name tradehub --resource-group $resourceGroupName  --namespace-name tradeNs
