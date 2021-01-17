# TD API Management

### Pré-requis
1. Installez
    - [Visual Studio Code](https://code.visualstudio.com/)
    - [Azure Functions extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions)
2. Créez un groupe de ressource ** *<nom_de_votre_group>*-rg**

### Création et intégration d'une API
Séparez vous en au moins deux sous-groupes. Chaque sous-groupe crééra sa propre Azure Function, hébergée indépendamment de l'autre.Chaque Azure Function:
    - sera implémentée en Python (optionnel: une en Python, l'autre en .NET)
    - sera ouverte uniquement aux requêtes en GET
    - prendra en paramètre un nombre *n*
    - renverra la chaîne de caractère suivante **{ "result": *valeur(n)* }**
    - implémentera
        1. Pour le sous-groupe 1, le calcul du [https://fr.wikipedia.org/wiki/Nombre_de_Fibonacci](nombre de Fibonacci) de *n*
        2. Pour le sous-groupe 2, le calcul du [https://fr.wikipedia.org/wiki/Nombre_de_Lucas](nombre de Lucas) de *n*

    

### Intégration à API Management et transformation de la sortie
On souhaite créer une API de calcul unifiée en s'appuyant sur de l'API Management.
1. Depuis le portail Azure, créez une nouvelle instance d'Azure API Management
2. Intégrez-y les deux Azure Functions vues précédemment. Observez que malgré le fait que les deux fonctions soient hébergées dans des ressources différentes (et ont donc des URL différentes), l'utilisation d'une couche d'API Management permet d'unifier leurs *endpoints*.
3. Certaines applications historiques ne supportant que le XML souhaitent utiliser les fonctions proposées. Configurez les policy de votre API Management afin de toujours renvoyer du XML (*outbound*). Utilisez l'onglet *Test* ou votre navigateur pour vérifier que votre application fonctionne.
4. Afin de pouvoir servir à la fois du XML ou du Json en fonction de l'appelant, reconfigurez la policy *Outbound* pour qu'elle renvoie le bon format de données en fonction du header http *accept*

### Gestion d'accès
