# This workflow demonstrates how you can use various environments (development, staging, production) in github.

name: CI/CD pipeline

# Define when the workflow should run. On a push to main, a PR to main or manually with worfkow_dispatch
on: 
  push:
    branches: [ main ] 
  pull_request:
    branches: [ main ]
  workflow_dispatch:

# Define some jobs that should run

jobs:
  # Define a build job
  Build:
    runs-on: ubuntu-latest
    strategy:
      matrix:
        dotnet-version: [ '6.0.x', '7.0.x']
    
    steps:
    - uses: actions/checkout@v3
    - name: Setup .NET version ${{ matrix.dotnet-version }}
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version:  ${{ matrix.dotnet-version }}       
    - name: Restore project dependencies
    
    
    # Build and test have completed successfully

  # Define a deploy to development environment job
  DeployDev:
    name: Deploy to Development
    # Job runs only if the event is a pull request
    if: github.event_name == 'pull_request'
    # The job needs the build job to pass first
    needs: [ Build ]
    runs-on: ubuntu-latest
    # Define some environment variables
    environment: 
      # The environment name must match the envrionment name in the repository
      name: Development
      url: 'http//dev/myapp.com'
    steps:
      - name: Deploy
        run: echo Your code is being deployed to http//dev/myapp.com
  
  # Our deployment to staging environment
  DeployStaging:
    name: Deploy to Staging
    # deploy to staging only if we are committing to main
    if: github.event.ref == 'refs/heads/main'
    needs: [ Build ]
    runs-on: ubuntu-latest
    environment:
      name: Staging
      url: 'http://test.myapp.com'
    steps:
    - name: Deploy
      run: echo I am deploying to staging http://test.myapp.com
  
  # Deploy to production environment
  DeployProduction:
    name: Deploy to Production
    needs: [DeployStaging]
    runs-on: ubuntu-latest
    environment:
      name: Production
      url: 'http://www.myapp.com'
    steps:
      - name: Deploy
        run: echo I am deploying to production