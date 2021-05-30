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
Sur le client:
    Au moment de s'enregistrer, le client crée une clé privé qu'il va stocker dans sa propre base de donnée.

    Quand vous envoyer/recever un message, pour garder un historique ceux-ci sont encrypté et stocké sur une base de donnée
    locale, ces messages peuvent seulement être décrypté par votre clé privé.

Sur le serveur:
    

## Build

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

### Build process
TODO

## Usage
TODO

## Auteurs
-**54024 Arno Pierre Pion**
-**54456 Damiano Deplano**
