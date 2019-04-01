resourceGroupName="sbCompetingConsumerGroup"

az group create --name $resourceGroupName --location australiasoutheast

namespaceName=sbService$RANDOM

az servicebus namespace create --resource-group $resourceGroupName --name $namespaceName --location australiasoutheast

az servicebus queue create --resource-group $resourceGroupName --namespace-name $namespaceName --name TradeQueue

az storage account create --name functestappstore --location australiasoutheast --resource-group $resourceGroupName --sku Standard_LRS

az functionapp create --resource-group $resourceGroupName --consumption-plan-location australiasoutheast \
--name azFunHttp --storage-account functestappstore --runtime dotnet

az group delete --resource-group myResourceGroup



func azure functionapp publish azFunHttp

az functionapp deployment source config-zip  -g FuncGroup -n \
azFunHttp --src <zip_file_path>







