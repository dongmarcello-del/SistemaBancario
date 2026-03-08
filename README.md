# 🏦 Sistema Bancario

Progetto di esempio per la gestione di conti bancari, con supporto a depositi, prelievi, trasferimenti, visualizzazione del saldo e cronologia delle transazioni.

---

## Stack tecnologico

| Layer | Tecnologie |
|---|---|
| **Backend** | ASP.NET Core, Entity Framework Core, SQL Server, JWT Authentication |
| **Frontend** | React, TypeScript, Vite |

---

## Prerequisiti

Assicurarsi di avere installato:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) e npm
- [SQL Server](https://www.microsoft.com/sql-server)

---

## Setup

### 1. Configurazione backend

Nel file `appsettings.json`, aggiungere o aggiornare la seguente configurazione:

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
```

### 2. Creazione del database

Dalla cartella del backend, eseguire:

```bash
dotnet ef database update
```

Questo comando crea il database e applica tutte le migrazioni.

### 3. Avvio del backend

```bash
dotnet run
```

### 4. Avvio del frontend

Dalla cartella del frontend:

```bash
npm install
npm run dev
```

Il frontend sarà disponibile su: **http://localhost:5173**

---

## Test dell'applicazione

Per testare la dashboard, aprire un URL nel seguente formato:

```
http://localhost:5173/home/{accountId}
```

**Esempio:**

```
http://localhost:5173/home/9691BB7E-6BFA-4073-AB56-24280E0430C9
```

> L'`accountId` deve corrispondere a un conto bancario esistente nel database.

---

## API — Documentazione Swagger

Dopo aver avviato il backend, è possibile esplorare e testare tutti gli endpoint tramite Swagger:

```
http://localhost:<porta>/swagger
```

L'interfaccia Swagger permette di visualizzare tutti gli endpoint disponibili e di eseguire richieste direttamente dal browser.