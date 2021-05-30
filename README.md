# Secure instant messaging

This repository contains the Secure Instant Messaging work of SECG4 labo.

## What the program can do
En terme de fonctionnalités:
    Le client peut s'enregistrer et se connecter. 
    Une fois connecté l'utilisateur peut consulter sa liste d'amis.
    Dans cette liste ce trouve tout les amis ainsi qu'une option pour ajouter un amis.
    Pour ajouter un amis il suffit de tapper son nom.

    Si vous cliquer sur un amis vous aurez 2 options, Message et Supprimer l'ami de votre liste.

En terme de sécurité:
    Au moment de s'enregistrer, le client crée une clé privé qu'il va stocker dans sa propre base de donnée.
    Le nom d'utilisateur, le mot de passe hashé + salté et la clé publique est stocké sur la base de donnée du serveur.
    Un token est généré avec une date d'expiration.

    A chaque connection le token de l'utilisateur est mis a jour.

    A partir de la tout les appel API vérifient d'abord si le token est toujours valable pour empécher le vol de token.
    Si celui-ci est expiré une nouvel connection est créee et mets a jour le token.
    Pour chaque appel API nous ajoutons une authentification avec ce token (bearer). Le token est vérifié sur le serveur pour
    voir si les informations n'ont pas été modifiés.

    Il y a un système de heartbeat
    //TODO EXPLIQUER HEARTBEAT

    Quand vous envoyer/recever un message, pour garder un historique ceux-ci sont encrypté et stocké sur une base de donnée
    locale, ces messages peuvent seulement être décrypté par votre clé privé.

    Lors de l'envoi nous encryptons le message avec la clé publique du récepteur, les messages encrypté sont envoyés.

    //TODO Expliquer les sécu serveur

## Dependencies
This application work with .NET 5 framework and Entity Framework core tools.

### To install .NET 5 SDK with SNAP

```
sudo snap install dotnet-sdk --classic --channel=5.0
```

### To install EF core tools

```
dotnet tool install --global dotnet-ef --version 5.0.6
```

### Nuget package (installed with make command)
InstantMessagingServer:

- [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/6.1.4?_src=template)
- [Microsoft.VisualStudio.Web.CodeGeneration.Design](https://www.nuget.org/packages/Microsoft.VisualStudio.Web.CodeGeneration.Design/5.0.2?_src=template)
- [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/5.0.6?_src=template)
- [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/5.0.6?_src=template)
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/5.0.6?_src=template)

InstantMessagingClient:

- [SimpleTCP.Core](https://www.nuget.org/packages/SimpleTCP.Core/1.0.4?_src=template)
- [RestSharp](https://www.nuget.org/packages/RestSharp/106.11.7?_src=template)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/13.0.1?_src=template)
- [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/5.0.6?_src=template)
- [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/5.0.6?_src=template)
- [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/5.0.0?_src=template)
- [Microsoft.Extensions.Configuration.Json](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json/5.0.0?_src=template)
- [EasyConsoleApplication](https://www.nuget.org/packages/EasyConsoleApplication/0.4.0?_src=template)

## Build process
InstantMessagingServer:

run `make` in /instantMessagingServer directory

InstantMessagingClient:

run `make` in /instantMessagingClient directory

## Usage
### InstantMessagingServer:

Launche the server with
```
dotnet run --project /instantMessagingServer/instantMessagingServer
```

You can configure token key, token validity duration and connection string in
`/instantMessagingServer/instantMessagingServer/appsettings.json`

### InstantMessagingClient:

You have to configure the serveur adresse and port in `/instantMessagingClient/instantMessagingClient/config.json`

Launche the client with
```
dotnet run --project /instantMessagingClient/instantMessagingClient
```

## Auteurs
-**54024 Arno Pierre Pion**
-**54456 Damiano Deplano**
