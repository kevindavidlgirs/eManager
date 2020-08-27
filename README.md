# eManager
eManager est un projet de gestion de matchs de foot créé en C#. [Screenshot](https://imgur.com/a/2xnOQBS)

### Conditions préalables
Vous avez besoin pour l'utilisation de "visual-studio community" et de différents outils.

```
Proposition :

https://visualstudio.microsoft.com/fr/

```

### Installation
1) Installez visual-studio community
2) Dans "Visual-studio installer" sélectionnez les applications suivantes : 
- Développement .NET desktop
- Développement pour la plateforme Windows universelle
- Stockage et traitement des données
- Développement multiplateforme .NET core
3) Une fois installé, cliquez sur ouvrir une solution et choisissez dans le dossier du projet "prbd_1920_g04.sln"
4) Vous pouvez exécuter le projet

## Utilisation
1) Vous devez vous connecter pour accéder à l'application (toutes les informations se trouvent sur la page de login)

2) Sur la page d'accueil vous pouvez voir les différents matchs créés. Lorsqu'un petit logo apparaît à l'extrême droite d'un match cela veut dire qu'il y a assez de joueurs pour remplir une équipe. Vous pouvez double-cliquer sur l'un des matchs pour voir les détails.

3) Vous pouvez créer un match ou des joueurs. Chaque joueur entre dans une division selon son âge, par exemple la division U15 comprend les joueurs de 15 à 16 ans. Pour faciliter les tests nous avons déjà créé plusieurs joueurs, vous pouvez passer directement aller à l'onglet "add player to a match" ou créer vos propres joueurs et matchs sachants que nous avons limité le nombre de joueurs pour une équipe à 5.

4) Une fois que vous avez ajouté les joueurs à une équipe puis ajouté les résultats et les stats des joueurs (pour le match donné) vous arrivez aux détails du match en question.

5) Vous pouvez cliquer sur une ligne de la datagrid des joueurs pour atterrir sur la view du joueur sur lequel vous avez cliqué et voir ses statistiques pour tous les matchs joués !

## Architecture
- Nous avons utilisé l'architecture MVVM pour ce projet. Attention nous avons mis le code de la "VM" dans le code behind.

## Auteurs
* **Kevin David L. Girs** - [kevindavidlgirs](https://github.com/kevindavidlgirs)
* **Hervé Mbilo**

## Info
Projet de groupe pour un bachelier en informatique de gestion donné à l'EPFC (Bruxelles-Capitale)
