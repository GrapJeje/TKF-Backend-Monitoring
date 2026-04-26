# Technische/Functionele eisen
[] Het backend process moet worden geschreven in c#.
[] Backend moet draaien als docker container, in een docker-compose omgeving.
[] Voor testdoeleinden moet een complete docker-compose omgeving opgezet worden. Inclusief NATS messagebus.
[] Voor de messagebus gebruiken we NATS.
[] Via de config van de applicatie moet er een mogelijkheid zijn om een minimum in te setllen voor de prioriteit. Alle berichten van lagere kwaliteit slaan we over.
[] Storage van de events moet in de container/software, geen externe database.
[] Events moeten 24 uur bewaard worden, daarna moet de apllicatie ze opschonen.

## Nice to haves:
[] Test docker applicatie die demo events injecteert in de NATS messagebus, om zo een volledige integratie te kunnen simuleren.
[] Filter op AssetId in de API, op een efficiënte manier zonde de gehele door te lopen.
[] Gelijktijdig gebruik van API tijdens de gegevensprocessing en het opruimen van de evetns.
[] Beveiligen tegen maximaal geheugengebruik bij hoge throughtput van events.
