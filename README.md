# Sistema Bancario

Progetto di esempio per la gestione di conti bancari con operazioni di deposito, prelievo, trasferimento, visualizzazione saldo e lista transazioni.

Il repository è pensato per **sviluppo e test locale**, non per utilizzo pubblico.

## Stack tecnologico

### Backend
- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT Authentication

### Frontend
- React
- TypeScript
- Vite

## Prerequisiti

Assicurarsi di avere installato:

- .NET SDK
- Node.js
- npm
- SQL Server

## Setup

### 1. Configurazione backend

Nel file `appsettings.json` aggiungere questa configurazione(o modificata):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaBancario;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "AuthPropertiesConfig": {
    "Issuer": "SistemaBancarioAPI",
    "Audience": "SistemaBancarioClient",
    "SecretKey": "A9F3K2L8M4N7Q1R6S0T5U2V9W3X7Y8Z1A2B3C4D5E6F7G8H9",
    "ExpiryMinutes": "60"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
2. Creazione del database

Dalla cartella del backend eseguire:

dotnet ef database update

Questo comando crea il database e applica le migrazioni.

3. Avvio backend
dotnet run
4. Avvio frontend

Dalla cartella del frontend eseguire:

npm install
npm run dev

Il frontend sarà disponibile su:

http://localhost:5173
Test dell'applicazione

Per testare la dashboard usare un URL nel formato:

http://localhost:5173/home/{accountId}

Esempio:

http://localhost:5173/home/9691BB7E-6BFA-4073-AB56-24280E0430C9

Per il test è necessario inserire nell'URL l'ID di un conto bancario esistente.

Endpoint API

Per vedere e testare gli endpoint del backend usare Swagger dopo aver avviato il progetto backend.

Aprire nel browser l'indirizzo dello Swagger esposto dall'applicazione, ad esempio:

http://localhost:<porta>/swagger

Da lì è possibile vedere tutti gli endpoint disponibili e provarli direttamente.