# Messagerie instantanée sécurisé

Ce repository contient le travail Messagerie instantanée sécurisé de SECG4 labo.

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


## Check-list TODO
1. Do I properly ensure confidentiality?
    • Are sensitive data transmitted and stored properly?
        Yes data is safely transfered with a bearer token.
        Messages are encrypted before sending them.
    • Are sensitive requests sent to the server transmitted securely?
        Yes
2. Do I properly ensure integrity of stored data?
    The server check for the integrity.
3. Do I properly ensure non-repudiation?

4. Do my security features rely on secrecy, beyond cryptographic keys and access codes?
    No
5. Am I vulnerable to injection?
    • URL, SQL, Javascript and dedicated parser injections
    No
6. Am I vulnerable to data remanence attacks?

7. Am I vulnerable to fraudulent request forgery?

8. Am I monitoring enough user activity so that I can detect malicious intents, or analyse an attack a posteriori?

9. Am I using components with know vulnerabilities?

10. Is my system updated?
    Yes we're using .NET CORE 5
11. Is my access control broken (cf. OWASP 10)?

12. Is my authentication broken (cf. OWASP 10)?

13. Are my general security features misconfigured (cf. OWASP 10)?


### Dependencies
InstantMessagingServer:
    -Swashbuckle.AspNetCore
    -Microsoft.VisualStudio.Web.CodeGeneration.Design
    -Microsoft.EntityFrameworkCore.Tools
    -Microsoft.EntityFrameworkCore.Sqlite
    -Microsoft.AspNetCore.Authentication.JwtBearer

InstantMessagingClient:
    -SimpleTCP.Core
    -RestSharp
    -Newtonsoft.Json
    -Microsoft.EntityFrameworkCore.Tools
    -Microsoft.EntityFrameworkCore.Sqlite
    -Microsoft.Extensions.Configuration
    -Microsoft.Extensions.Configuration.Json

### Build process
TODO

## Usage
Client: il y a une config (config.json) pour le host.

## Auteurs
-**54024 Arno Pierre Pion**
-**54456 Damiano Deplano**
