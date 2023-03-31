# LogApi
L'objectif est de réaliser une API qui exploite les données contenu dans le fichier .tsv, et qui propose 2 endpoints :     /1/queries/count/&lt;DATE_PREFIX> /1/queries/popular/&lt;DATE_PREFIX>?size=&lt;SIZE>   

/1/queries/count/<DATE_PREFIX>
    renvoie un JSON comptant le nombre de requête distincte sur la période, tel que ce jeu de test est respecté :

 

/1/queries/count/2015' 
    { "count": 573697 }
/1/queries/count/2015-08'
    { "count": 573697 }
/1/queries/count/2015-08-03'
    { "count": 198117 }
/1/queries/count/2015-08-01 00:04'
    { "count": 617 }

 


/1/queries/popular/<DATE_PREFIX>?size=<SIZE>
    renvoie un JSON contenant les <size> requêtes les plus populaires, ainsi que leur nombre, tel que ce jeu de test est respecté :

 

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

 


Conseil :
Au lancement de l'API, il faut lire et charger en mémoire les données du fichier, et ranger les informations dans une structure de donnée qui rendra simple et rapide la récupération des informations demandés.

