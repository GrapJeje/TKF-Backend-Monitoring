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
- [ ] Filter op AssetId in de API, op een efficiënte manier zonder de gehele dataset door te lopen.
- [X] Gelijktijdig gebruik van API tijdens de gegevensprocessing en het opruimen van de events.
- [ ] Beveiligen tegen maximaal geheugengebruik bij hoge throughput van events.
