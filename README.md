# Technische/Functionele eisen

- [x] Het backend process moet worden geschreven in C#.
- [x] Backend moet draaien als Docker container, in een docker-compose omgeving.
- [x] Voor testdoeleinden moet een complete docker-compose omgeving opgezet worden, inclusief NATS messagebus.
- [x] Voor de messagebus gebruiken we NATS.
- [x] Via de config van de applicatie moet er een mogelijkheid zijn om een minimum in te stellen voor de prioriteit. Alle berichten van lagere kwaliteit slaan we over.
- [x] Storage van de events moet in de container/software, geen externe database.
- [x] Events moeten 24 uur bewaard worden, daarna moet de applicatie ze opschonen.

## Nice to haves

- [X] Test Docker applicatie die demo events injecteert in de NATS messagebus, om zo een volledige integratie te kunnen simuleren.
- [X] Filter op AssetId in de API, op een efficiënte manier zonder de gehele dataset door te lopen.
- [ ] Gelijktijdig gebruik van API tijdens de gegevensprocessing en het opruimen van de events.

-> In java kun je dit doen doormiddel van een lock en bepaalde synchronisatie doen. Hierbij heb je op elke thread dezelfde value ipv elke thread kan een eigen value hebben. Ik zou moeten opzoeken in C# hoe dit precies in zijn werking zou gaan.
- [ ] Beveiligen tegen maximaal geheugengebruik bij hoge throughput van events.

-> Ik zou denken dat dit op te lossen kan door een memory limit erop te zetten en sneller oudere events te verwijderen.

# Design keuzes
- **?code=**, ik heb gekozen voor een code in de URL wegens het belangrijk is dat je API beschermt is tegen gebruikers die je liever er niet wilt hebben. Ik heb gekozen i nde URL wegens dit iets fijner werkt in development voor mij, in een live omgeving zou ik dit in de header zetten.
- **APiServer.cs**, ik heb gekozen voor een ApiServer als het grootste gedeelte van de klasse wegens ik origineel iets wou maken met een constante loop of task die zelf een server start, maar dit bleek niet nodig te zijn. Ik heb er voor gekozen om de server te starten in de Main methode, en de ApiServer klasse alleen verantwoordelijk te maken voor het opzetten van de server en het afhandelen van requests.
- **EventListenerTask.cs**, Als dit project jaren lang zich zou moeten voortzetten zou ik deze klasse hernoemen en ergens anders in de structuur zetten. Origineel had ik in gedachte dat ik een loop moest maken die elke keer checkte of dat er een event werd gegooit. Maar dit bleek later overbodig te zijn en zelf memory leaks te veroorzaken.
- **Worker service**, ik heb gekozen voor een worker service wegens ik de mogelijkheid wil hebben dat de EventThrowerTask niet constant aan het runnen is en onze storge vol kan gooien. Met mijn ervaringen in Docker is dit de enigste mogelijkheid hoe ik dit zou kunnen maken.
- **Opbouw van het project**, ik heb in mijn achterhoofd gehouden dat dit project jaren lang mee zou kunnen gaan, daarom dat ik ENUM's heb gemaakt en de TkfTask.cs. Ik hou zelf van een overzichtelijke structuur waar alles redelijk makkelijk te vinden is, zelfs zonder README.md.