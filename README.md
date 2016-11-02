------------------------------------------------------------------------------------

# .csv-file-converter

Gemaakt door: Iliass Nassibane
-----------------------------------------------------------------------------------

-----------------------------------------------------------------------------------
--- Voorbereiding ---
-----------------------------------------------------------------------------------

1. Plaats de "Boekenbonnen_verwerker" map op de C:// schijf.
2. Check het "01 - Van" map voor het "C019302-16-10-25.csv" bestand.
3. Voer het boekenbonnenverwerker.exe uit.
4. Check de "01 - Van" map, als het originele bestand is verdwenen dan is de verwerking goed gegaan.
5. Check de "02 - naar" map voor het nieuwe geconverteerde bestand. De inhoud kan ook ingezien worden ter controle. 
6. Check de "03 - archief" map voor het originele bestand.
7. Check de "Config" map voor het BBV_config.xml. Kijk ook na of de elementen leeg zijn. Anders zal de BBV job 
gebruik maken van deze waardes.
8. Er is een logging bestand aangemaakt als de job voor het eerst wordt gedraait. Draai je de 
job een aantal keer, dan zal de job het log bestand bijwerken met de recente jobruns.
9. Als de verwerking niet goed is gegaan dan kun je altijd kijken naar de logging voor de oorzaak.

De huidige build van de job maakt nog gebruik van weergave tekst in cmd. De hulpteksten helpen je inzicht te geven wat 
de code nu aan het doen is. Dit kan uitgezet worden via een nieuwe build van de code en de hulpteksten uit te zetten.
Mits dit niet meer nodig is, dan kunnen die worden uitgecommenteerd. 

!!Voor live gang!!:
De huidige build staat ingesteld op de huidige configuratie dat gebruik maakt van de C:// schijf. De locaties moeten aangepast worden 
in de code om te kunnen refereren naar de locaties voor de daadwerkelijke boekenbonnen, op de Exact server.

-----------------------------------------------------------------------------------
--- Vereisten om het programma te kunnen gebruiken ---
-----------------------------------------------------------------------------------

1. Plaats alles in een map op de C schijf, onder de benaming van "Boekenbonnen_verwerker".
2. Check de mappen of de vereiste originele bestand, C019302-16-10-26.csv of C019302-20-10-30.csv aanwezig is. Beide
bestanden horen in de "01 - Van" map te zitten van deze twee locaties:
		-	C:\Boekenbonnen_verwerker\01 - Van\
		-	C:\Boekenbonnen_verwerker\ConfigTestFolder

-----------------------------------------------------------------------------------
--- Voorbeeld van running test ---
-----------------------------------------------------------------------------------

Dit is een running test met de huidige instellingen in de codebase en config bestand:

1. Het programma roept de waardes aan van het config bestand.
2. Het programma pakt vervolgens het originele bestand aan in de van map uit de test locatie.
3. Het programma maakt een nieuwe bestand aan en schrijft daarin de data uit het origineel.
4. Het programma update het logging.txt bestand tijdens zijn run.
5. Het programma overschrijft de waardes in het config bestand, waardoor alle elementen een
null waarde krijgen.

Let op! Er wordt gebruik gemaakt van de testlocatie: C:\Boekenbonnen_verwerker\ConfigTestFolder
