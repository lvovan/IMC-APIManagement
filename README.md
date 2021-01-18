# TD API Management

### Pré-requis
1. Installez
    - [Visual Studio Code](https://code.visualstudio.com/)
    - [Azure Functions extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions)
2. Créez un groupe de ressource ` *<nom_de_votre_group>*-rg`
3. Créez une nouvelle instance d'Azure API Management. Remarquez que le support pour des protocoles obsolètes sont proposés - il vaut mieux les proposer au niveau de l'API Management que de répercuter les risques associés dans chacune des APIs enfants!

### Création et intégration d'une API
Séparez vous en au moins deux sous-groupes. Chaque sous-groupe crééra sa propre Azure Function, hébergée indépendamment de l'autre. Chaque Azure Function:
- sera implémentée en Python (optionnel: une en Python, l'autre en .NET)
- sera ouverte uniquement aux requêtes en GET
- prendra en paramètre un nombre *n*
- sera sécurisée en mode *function* (et non *anonymous*)
- renverra la chaîne de caractère suivante `{ "result": *valeur(n)* }`
- pour le sous-groupe 1, implémentera le calcul du [nombre de Fibonacci](https://fr.wikipedia.org/wiki/Nombre_de_Fibonacci) de *n*
- pour le sous-groupe 2, implémentera le calcul du [nombre de Lucas](https://fr.wikipedia.org/wiki/Nombre_de_Lucas) de *n*

### Intégration à API Management et transformation de la sortie
On souhaite créer une API de calcul unifiée en s'appuyant sur de l'API Management.
1. Intégrez-y les deux Azure Functions vues précédemment. Observez que malgré le fait que les deux fonctions soient hébergées dans des ressources différentes (et ont donc des URL différentes), l'utilisation d'une couche d'API Management permet d'unifier leurs *endpoints*.
2. Certaines applications historiques ne supportant que le XML souhaitent utiliser les fonctions proposées. Utilisez la fonctionnalité *Test* pour requêter vos API en configurant l'entête `Accept` à `application/json` et `application/xml`. Observez que le résultat est toujours au format Json.
3. Configurez les policy de votre API Management afin de renvoyer du XML ou du Json en fonction de l'entête `Accept` utilisant la policy *Outbound* [json-to-xml](https://docs.microsoft.com/en-us/azure/api-management/api-management-transformation-policies#ConvertJSONtoXML) et testez.
4. Afin de pouvoir servir à la fois du XML ou du Json en fonction de l'appelant, reconfigurez la policy *Outbound* pour qu'elle renvoie le bon format de données en fonction du header http *accept*. Notez que cette policy permet à API Management d'automatiquement ajouter l'entête `content-type`, ce qui permet au client de connaître le format des données renvoyées par le service via un [MIME Type](https://developer.mozilla.org/en-US/docs/Glossary/MIME_type)
5. (Optionnel) Intégrez l'une des API en utilisant l'interface *Blank API* plutôt que l'assistant *Azure Functions*.
6. (Optionnel) Ajoutez une policy *inbound*, par exemple [Limit call rate by key](https://docs.microsoft.com/en-us/azure/api-management/api-management-access-restriction-policies#LimitCallRateByKey), qui limite le nombre d'appel par unité de temps.

### Gestion des accès
Maintenant que les deux APIs sont réunies, créons un produit et ouvrons un compte développeur.
1. Ouvrez votre instance d'API Management
2. Dans *Users*, créez un utilisateur
3. Dans *Products*, créez un produit en lui associant les deux APIs Fibonacci et Lucas en n'oubliant pas de la publier en cochant l'élément adéquat.
4. Créez maintenant un abonnement en cliquant sur *Subscriptions*, associez-y le produit et l'utilisateur préalablement créés. 
5. Publiez le portail développeur (sans oublier d'activer CORS) et naviguez maintenant vers le portail développeur (je vous conseille d'utiliser le mode Privé/Incognito)
6. Autentifiez-vous avec l'utilisateur préalablement créé et testez vos APIs
7. (optionnel) Configurez deux niveaux d'abonnement, un niveau Standard avec uniquement l'API Fibonacci et un niveau Premium avec les API Fibonacci et Lucas.

### Déblocage
Si vous êtes bloqués, utilisez les APIs suivantes:
 - https://lucas-fa.azurewebsites.net/api/LucasHttpTrigger?n=5
 - https://fibonacci-fa.azurewebsites.net/api/FiboTrigger?n=5