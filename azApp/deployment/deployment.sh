

resourceGroupName=ApoCoreDevResourceGroup
storageName=appcoredevstorage
location=australiasoutheast
appGroup=appstatic-content-app

az group create -n $appGroup -l $location

az storage account create -n $storageName -g $appGroup --kind StorageV2 -l $location --https-only true --sku Standard_LRS

az storage blob upload-batch -s . -d \$web --account-name $storageName

az storage container create -n images --account-name $storageName --public-access blob

az functionapp create -n safeUpload -g $appGroup -s $storageName -c $location

az functionapp config appsettings set --name safeUpload -g $appGroup --settings FUNCTIONS_EXTENSION_VERSION=~1

export STORAGE_CONNECTION_STRING=$(az storage account show-connection-string -n appcoredevstorage  -g appstatic-content-app --query "connectionString" --output tsv)

az functionapp config appsettings set -n safeUpload -g appstatic-content-app --settings AZURE_STORAGE_CONNECTION_STRING=$STORAGE_CONNECTION_STRING -o table

export FUNCTION_APP_URL="https://"$(az functionapp show -n safeUpload -g appstatic-content-app --query "defaultHostName" --output tsv)


export BLOB_BASE_URL=$(az storage account show -n appcoredevstorage -g appstatic-content-app --query primaryEndpoints.blob -o tsv | sed 's/\/$//')

az storage blob upload -c \$web --account-name appcoredevstorage -f settings.js -n settings.js


az storage blob list --account-name appcoredevstorage -c images -o table

az storage blob delete-batch -s images --account-name appcoredevstorage


az storage container create -n thumbnails --account-name appcoredevstorage --public-access blob


az storage blob list --account-name appcoredevstorage -c images -o table


az storage blob list --account-name appcoredevstorage -c thumbnails -o table


Create a function app with source files deployed from the specified GitHub repo.
az functionapp create \
  --name $functionAppName \
  --storage-account $storageName \
  --consumption-plan-location westeurope \
  --resource-group myResourceGroup \
  --deployment-source-url $gitrepo \
  --deployment-source-branch master



  az cognitiveservices account create -g first-serverless-app -n <computer vision account name> --kind ComputerVision --sku F0 -l westcentralus

export COMP_VISION_KEY=$(az cognitiveservices account keys list -g first-serverless-app -n <computer vision account name> --query key1 --output tsv)


export COMP_VISION_URL=$(az cognitiveservices account show -g first-serverless-app -n <computer vision account name> --query endpoint --output tsv)


func init AzFunctionCore

func extensions install


