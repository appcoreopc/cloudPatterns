
resourceGroupName="sbCompetingConsumerGroup"

az group create --name $resourceGroupName --location australiasoutheast

namespaceName=sbService$RANDOM

az servicebus namespace create --resource-group $resourceGroupName --name $namespaceName --location australiasoutheast

az servicebus queue create --resource-group $resourceGroupName --namespace-name $namespaceName --name TradeQueue





