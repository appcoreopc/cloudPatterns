resourceGroupName="sbCompetingConsumerGroup"
Location=australiasoutheast

az group create --name $resourceGroupName --location $Location

namespaceName=sbService550

az servicebus namespace create --resource-group $resourceGroupName --name $namespaceName --location $Location

az servicebus queue create --resource-group $resourceGroupName --namespace-name $namespaceName --name TradeQueue

az servicebus topic create --resource-group $resourceGroupName --namespace-name sbService550 --name tradetopic

az servicebus topic subscription create --resource-group $resourceGroupName --namespace-name $namespaceName --topic-name tradetopic --name tradesubscription

Endpoint=sb://sbservice550.servicebus.windows.net/;SharedAccessKeyName=TradeTopicPolicy;SharedAccessKey=S7jZdIxUrjNuOb9lVQ6WvRLAE7EK0CutiWgn9QEdPy0=;

dotnet new console -n TopicConsumer

dotnet new console -n TopicProducer

dotnet add package Microsoft.Azure.ServiceBus --version 3.4.0

az group delete --resource-group $resourceGroupName


Event Hub

az eventhubs namespace create --name tradeNs --resource-group $resourceGroupName -l $Location

az eventhubs eventhub create --name tradehub --resource-group $resourceGroupName  --namespace-name tradeNs


Service Fabric 

ClusterName="apptradetestcluster" 
Subject="apptradetestcluster.australiasoutheast.cloudapp.azure.com" 
VaultName="containertestvault" 
VmPassword="Mypa$$word!321"
VmUserName="sfadminuser"

VmPassword="Mypassword#321"

az group create --name $resourceGroupName --location $Location 

az sf cluster create --resource-group $resourceGroupName --location $Location --certificate-output-folder . --certificate-password $Password --certificate-subject-name $Subject --cluster-name $ClusterName --cluster-size 3 --os UbuntuServer1604 --vault-name $VaultName --vault-resource-group $resourceGroupName --vm-password $VmPassword --vm-user-name $VmUserName

sfctl cluster select --endpoint https://apptradetestcluster.australiasoutheast.cloudapp.azure.com:19080 --pem sbCompetingConsumerGroup201904042153.pem --no-verify

az aks create \
    --resource-group $resourceGroupName \
    --name aksDev \
    --node-count 1 \
    --enable-addons monitoring \
    --generate-ssh-keys

az aks install-cli
az aks get-credentials --resource-group $resourceGroupName --name myAKSCluster

kubectl get nodes



az group create --name myResourceGroup --location westeurope
az storage account create --name <storage_name> --location westeurope --resource-group myResourceGroup --sku Standard_LRS
az functionapp create --resource-group myResourceGroup --consumption-plan-location westeurope \
--name <app_name> --storage-account  <storage_name> --runtime <language> 
func azure functionapp publish <FunctionAppName>





az functionapp deployment source config --name $functionapp --resource-group $resourceGroupName --branch master --manual-integration --repo-url https://github.com/Azure-Samples/function-image-upload-resize



