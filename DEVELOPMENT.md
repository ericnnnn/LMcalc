# Development Guide

## Run Backend
```bash
cd backend-dotnet
dotnet run
```

## Run Frontend
```bash
cd ui-angular
npm install
ng serve --configuration=local
```

## Proxy API Calls
UI → http://localhost:4200/api/* → Backend at http://localhost:5000/api/*
