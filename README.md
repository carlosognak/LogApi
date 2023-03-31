# LogApi
L'objectif est de réaliser une ```API``` qui exploite les données contenu dans le ```fichier .tsv```, et qui propose 2 endpoints :    
```python
/1/queries/count/&lt;DATE_PREFIX> 
/1/queries/popular/&lt;DATE_PREFIX>?size=&lt;SIZE>  
```
```python
 /1/queries/count/<DATE_PREFIX>  renvoie un JSON comptant le nombre de requête distincte sur la période, tel que ce jeu de test est respecté :
 
 /1/queries/count/2015' 
    { "count": 573697 }
/1/queries/count/2015-08'
    { "count": 573697 }
/1/queries/count/2015-08-03'
    { "count": 198117 }
/1/queries/count/2015-08-01 00:04'
    { "count": 617 }
```
```python
/1/queries/popular/<DATE_PREFIX>?size=<SIZE> renvoie un JSON contenant les <size> requêtes les plus populaires, ainsi que leur nombre, tel que ce jeu de test est respecté :

/1/queries/popular/2015?size=3
    { "queries": [
        { "query": "http%3A%2F%2Fwww.getsidekick.com%2Fblog%2Fbody-language-advice", "count": 6675 },
        { "query": "http%3A%2F%2Fwebboard.yenta4.com%2Ftopic%2F568045", "count": 4652 },
        { "query": "http%3A%2F%2Fwebboard.yenta4.com%2Ftopic%2F379035%3Fsort%3D1", "count": 3100 }
   ]}
    
/1/queries/popular/2015-08-02?size=5'
    { "queries": [
        { "query": "http%3A%2F%2Fwww.getsidekick.com%2Fblog%2Fbody-language-advice", "count": 2283 },
        { "query": "http%3A%2F%2Fwebboard.yenta4.com%2Ftopic%2F568045", "count": 1943 },
        { "query": "http%3A%2F%2Fwebboard.yenta4.com%2Ftopic%2F379035%3Fsort%3D1", "count": 1358 },
        { "query": "http%3A%2F%2Fjamonkey.com%2F50-organizing-ideas-for-every-room-in-your-house%2F", "count": 890 },
        { "query": "http%3A%2F%2Fsharingis.cool%2F1000-musicians-played-foo-fighters-learn-to-fly-and-it-was-epic", "count": 701 }
    ]}

```
 

## Conseil :
Au lancement de ```l'API```, il faut lire et charger en mémoire les données du fichier, et ranger les informations dans une structure de donnée qui rendra simple et rapide la récupération des informations demandés.
## Résolution:
Pour résoudre ce problème, nous pouvons utiliser un ```HashMap en .NET Core``` pour stocker les requêtes et leur compteur. Un ```HashMap``` est une structure de données qui permet de stocker des éléments sous forme de ```clé-valeur```, et d'y accéder rapidement.
Voici comment nous pouvons utiliser un ```HashMap``` pour résoudre ce problème :
- Nous lisons chaque ligne du ```fichier.tsv``` et nous séparons la date et la requête en utilisant la tabulation comme séparateur.

- Nous vérifions si la date correspond au préfixe de date spécifié dans ```l'URL``` ```(<DATE_PREFIX>).``` Si ce n'est pas le cas, nous ignorons la requête.

- Si la date correspond, nous ajoutons la requête dans le HashMap en utilisant la requête comme clé et en incrémentant le compteur associé à cette requête.

- Après avoir lu toutes les lignes, nous parcourons le HashMap pour récupérer les ```<SIZE>``` requêtes les plus populaires et leur compteur.

- Nous retournons les résultats sous la forme d'un objet ```JSON``` qui contient les ```<SIZE>``` requêtes les plus populaires, ainsi que leur nombre.
Dans ce projet , nous utilisons un ```Dictionary<string, int>``` pour stocker les requêtes et leur compteur. La ```clé``` du dictionnaire est la ```requête```, et la ```valeur``` est le ```compteur``` associé à cette requête.
    
  

