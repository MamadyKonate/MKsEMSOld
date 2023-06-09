# Docs for the Azure Web Apps Deploy action: https://github.com/Azure/webapps-deploy
# More GitHub Actions for Azure: https://github.com/Azure/actions

name: Build and deploy ASP.Net Core app to Azure Web App - emsproj

on:
  push:
    branches:
      - main
  workflow_dispatch:
env:
  AZURE_WEBAPP_NAME: mksemsold
  AZURE_WEBAPP_PACKAGE_PATH: "./publish"
jobs:
  build:
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v3

      - name: Set up .NET Core
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '7.0.x'
      - name: restore
        
        run: dotnet restore ./MKsEMS.csproj      
      
      - name: Build with dotnet
        run: dotnet build ./MKsEMS.csproj --configuration Release --no-restore
      
      
      
 #     - name: Set working directory
  #      run: cd ..
 #       working-directory: ./MKsEMSTestProject
        
 #     - name: Test 
#        run: dotnet test ./MKsEMSTestProject.csproj --no-build --verbosity normal
        
      - name: dotnet publish
        run: dotnet publish ./MKsEMS.csproj --configuration Release --no-restore --output ' ${{ env.AZURE_WEBAPP_PACKAGE_PATH}}'
      
      - name: deploy dotnet
        uses: azure/webapps-deploy@v2
        with:
          app-name: ${{ env.AZURE_WEBAPP_NAME}}
          publish-profile: '${{ env.AZURE_PUBLISH_PROFILE }}'
          package: "${{ env.AZURE_WEBAPP_PACKAGE_PATH }}"
#      - name: Upload artifact for deployment job
#        uses: actions/upload-artifact@v2
 #       with:
 #           name: .net-app
  #          path: ${{env.DOTNET_ROOT}}/myapp

  deploy:
    runs-on: ubuntu-latest
    needs: build
    environment:
      name: 'Production'
      url: ${{ steps.deploy-to-webapp.outputs.webapp-url }}

    steps:
      - name: Download artifact from build job
        uses: actions/download-artifact@v2
        with:
          name: .net-app          
      
      
      
      
#      - name: Deploy to Azure Web App
#        id: deploy-to-webapp
#        uses: azure/webapps-deploy@v2
 #       with:
 #         app-name: 'emsproj'
#          slot-name: 'Production'
 #         publish-profile: ${{ secrets.AZUREAPPSERVICE_PUBLISHPROFILE_4C8BAA93F5B74E19A0BD86D966CB1B95 }}
 #         package: .
   
