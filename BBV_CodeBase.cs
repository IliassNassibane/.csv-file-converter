using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace test1__Logging
{
    class Program
    {
        // @desc: Boekenbonnenverwerker.
        // @author: Iliass Nassibane.
        // @dateLatest: 24-10-2016.

        static void Main(string[] args)
        {
            // @desc: Logging
            // @author: Iliass Nassibane.
            // @date: 24-10-2016.

            // Locatie van/voor het logging bestand.
            string loggingFolderLocation = @"C:\Boekenbonnen_verwerker";                    // <<-- Dit moet aangepast worden als de standaard locatie voor het BBV programma 

            // Naam van het logging bestand.
            string loggingFileName = "LoggingFile.txt";

            DateTime date = DateTime.Now;
            string DatumTijdHuidig = date.ToString("yyyy-MM-dd, hh':'mm':'ss");

            // Locatie en naam in één pad voor fileExists method
            string loggingFileInFolder = loggingFolderLocation + "\\" + loggingFileName;


            // @desc: Config.File met mogelijke aanpassingen voor netwerkpaden.
            // @author: Iliass Nassibane.
            // @date: 26-10-2016.

            string xmlConfigLocation = @"C:\\Boekenbonnen_verwerker\Config\BBV_config.xml"; // <<-- Dit moet aangepast worden als de standaard locatie voor het BBV config bestand
            string ConfigPathFrom;
            string ConfigPathNew;
            string ConfigPathFromGoTo;
            string ConfigNieuweData;
            string ConfigNieuweBestandsNaam;

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlConfigLocation);

            // Pad naar het originele bestand:
            XmlNode pathFromNode = doc.DocumentElement.SelectSingleNode("/UserInput/Locations/LocationVan");
            ConfigPathFrom = pathFromNode.InnerText;

            // Pad naar het nieuwe bestand:
            XmlNode pathNewNode = doc.DocumentElement.SelectSingleNode("/UserInput/Locations/LocationNaar");
            ConfigPathNew = pathNewNode.InnerText;

            // Pad naar de nieuwe locatie voor het originele bestand:
            XmlNode pathFromGoToNode = doc.DocumentElement.SelectSingleNode("/UserInput/Locations/LocationArchive");
            ConfigPathFromGoTo = pathFromGoToNode.InnerText;

            // Pad naar de nieuwe datum:
            XmlNode NieuweDataNode = doc.DocumentElement.SelectSingleNode("/UserInput/Bestand/DatumVanBestandPickUp");
            ConfigNieuweData = NieuweDataNode.InnerText;

            // Pad naar de nieuwe naamgeving van bestand:
            XmlNode NieuweBestandsNaamNode = doc.DocumentElement.SelectSingleNode("/UserInput/Bestand/NieuweNaamVoorOutputBestand");
            ConfigNieuweBestandsNaam = NieuweBestandsNaamNode.InnerText;

            try
            {
                //Check of er al een logging bestand aanwezig is. Zo niet, maak dan een loggingbestand aan.
                if (!File.Exists(loggingFileInFolder))
                {
                    using (FileStream fs = System.IO.File.Create(loggingFileInFolder))
                    {
                        // Maakt het bestand aan op de gekozen locatie.                
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Druk op enter om dit bericht te sluiten...");
                Console.ReadKey();
            }
            finally
            {
                // Verbinding met het logging bestand, en volgt nu de daadwerkelijke verwerking van het .csv bestand plaats.
                using (StreamWriter loggingWriter = new StreamWriter(loggingFileInFolder, true))
                {

                    // @desc: Beginnende statement in het loggingsbestand.
                    loggingWriter.WriteLine("-------------------------------------- \r | {0} - Job gestart. | \r -------------------------------------- \r \n", DatumTijdHuidig);

                    //--------------------------------------------------------------------------------------------------------------------------------
                    // @desc: Variabelen die gebruikt worden voor de handelingen in het programma. Indien config bestand actief is worden deze variabelen omgezet.
                    // @author: Iliass Nassibane.
                    // @date: 27-10-2016.
                    string pathFrom;        // <-- (Indien nodig) wijzig in het config bestand de <LocationVan> element.
                    string pathFrom2;       // <-- (Indien nodig) wijzig in het config bestand de <LocationNaar> element.
                    string pathTo;          // <-- (Indien nodig) wijzig in het config bestand de <LocationArchive> element.
                    string bestandsDatum;   // <-- (Indien nodig) wijzig in het config bestand de <DatumVanBestandPickUp> element.
                    string bestandsNaam;    // <-- (Indien nodig) wijzig in het config bestand de <NieuweNaamVoorOutputBestand> element.
                    string standaardPathFrom = @"C:\Boekenbonnen_verwerker\01 - Van\";      // <<-- Dit moet aangepast worden als de standaard locatie waar het originele bestand aanwezig is.
                    string standaardPathFrom2 = @"C:\Boekenbonnen_verwerker\03 - Archief\"; // <<-- Dit moet aangepast worden als de standaard locatie voor het originele bestand wordt verplaatst na het verwerken.
                    string standaardPathTo = @"C:\Boekenbonnen_verwerker\02 - Naar\";       // <<-- Dit moet aangepast worden als de standaard locatie voor het nieuwe bestand wordt aangemaakt.
                    string bestandsDatumStandaard = date.ToString("dd-MM-yyyy");            
                    string bestandsNaamStandaardToevoeging = "Voorbeeld_nieuwBestand_";
                    
                    if (ConfigPathFrom != null)
                    {
                        pathFrom = ConfigPathFrom;                                               
                    }
                    else
                    {
                        pathFrom = standaardPathFrom;
                    }
                    
                    if (ConfigPathFromGoTo != null)
                    {
                        pathFrom2 = ConfigPathFromGoTo;
                    }
                    else
                    {
                        pathFrom2 = standaardPathFrom2;
                    }

                    if (ConfigPathNew != null)
                    {
                        pathTo = ConfigPathNew;
                    }
                    else
                    {
                        pathTo = standaardPathTo;
                    }

                    if (ConfigNieuweData != null)
                    {
                        bestandsDatum = ConfigNieuweData;
                    }
                    else
                    {
                        bestandsDatum = bestandsDatumStandaard;
                    }

                    if (ConfigNieuweBestandsNaam != null)
                    {
                        bestandsNaam = ConfigNieuweBestandsNaam;
                    }
                    else
                    {
                        bestandsNaam = bestandsNaamStandaardToevoeging;
                    }

                    // voor test:
                    Console.WriteLine("- pathFrom is: {0} ", pathFrom);
                    Console.ReadKey();
                    Console.WriteLine("- pathFrom2 is: {0} ", pathFrom2);
                    Console.ReadKey();
                    Console.WriteLine("- pathTo is: {0} ", pathTo);
                    Console.ReadKey();
                    Console.WriteLine("- bestandsDatum is: {0} ", bestandsDatum);
                    Console.ReadKey();
                    Console.WriteLine("- bestandsNaam is: {0} ", bestandsNaam);
                    Console.ReadKey();


                    //--------------------------------------------------------------------------------------------------------------------------------
                    // @desc: Path bepaling van het originele bestand.
                    // @author: Iliass Nassibane.
                    // @date: 24-10-2016.

                    string pathOriginalFileTo = "";
                    var allFilenames = Directory.EnumerateFiles(pathFrom).Select(p => Path.GetFileName(p));
                    var candidates = allFilenames.Where(fn => Path.GetExtension(fn) == ".csv")
                        .Select(fn => Path.GetFileNameWithoutExtension(fn));

                    // @desc: Neemt de datum van vandaag en slaat het op in een variabel gedurende de run van de applicate.
                    string zeroBased = bestandsDatum;

                    // @desc: Hier wordt de map gecontroleerd op de meest relevante bestand op basis van meest recente data.
                    string pathOriginalFile = "";
                    bool input = false;
                    foreach (string s in candidates)
                    {
                        input = s.Contains(zeroBased);
                        if (input == true)
                        {
                            pathOriginalFile = pathFrom + s/*fileName*/ + ".csv";
                            pathOriginalFileTo = pathFrom2 + s + ".csv";
                            loggingWriter.WriteLine("{0} - {1} is het meest recente bestand. Hier komt de informatie vandaan. \r", DatumTijdHuidig, pathOriginalFile);

                            break;
                        }
                        else
                        {
                            loggingWriter.WriteLine("{0} - Helaas, er zijn geen boekenbon bestanden gevonden met de datum van vandaag! \r Het kan zijn dat het bestandsnaam is aangepast of het is niet geplaatst op de juiste locatie. \r", DatumTijdHuidig);
                        }
                    }
                    //voor test:
                    Console.WriteLine("- Er kan na de herkenning van het origineel gewerkt worden, origineel is {0} ", pathOriginalFile);
                    Console.ReadKey();

                    //--------------------------------------------------------------------------------------------------------------------------------
                    // @desc: Check en aanmaak van een nieuwe bestand waar alle data naartoe gaat.
                    // @author: Iliass Nassibane.
                    // @date: 24-10-2016.
                    if (input == true)
                    {
                        // @desc: Het aanmaken van het nieuwe bestand of het checken of er al een nieuwe bestand is aangemaakt, waar alle data uit het origineel in wordt opgeslagen.
                        // @author: Iliass Nassibane.
                        // @date: 24-10-2016.

                        string fileName = bestandsNaam + bestandsDatum + ".csv";                 // <-- (Indien nodig) wijzig in het config bestand de <LocationNaar> element.
                        fileName = fileName.Replace(":", "-");                      // @desc: filenaam aanpassen wanneer in productie gaat.
                        pathTo = pathTo + fileName;

                        // @desc: File aanmaken op de gespecificeerde directory. Filestream kan gebruikt worden om de gemaakte item te schrijven.
                        if (input == true)
                        {
                            if (!(Directory.Exists(pathTo) || File.Exists(pathTo)))
                            {
                                FileStream fileCreator = new FileStream(pathTo, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read);
                                fileCreator.Close();

                                loggingWriter.WriteLine("{0} - Er is een nieuwe bestand aangemaakt waar de data naar gekopieerd wordt. \r locatie en bestand: {1}. \r", DatumTijdHuidig, pathTo);
                            }
                            else
                            {
                                // Schrijf naar log dat het bestand al bestaat of dat er geleegd moet worden.
                                loggingWriter.WriteLine("{0} - Er is al een bestand aanwezig. \n locatie en bestand: {1} \r", DatumTijdHuidig, pathTo);
                            }
                        }

                        //--------------------------------------------------------------------------------------------------------------------------------
                        // @desc: Reading Writing function voor het lezen van het origineel en het schrijven naar het nieuwe bestand.
                        // @author: Iliass Nassibane.
                        // @date: 24-10-2016.

                        // @desc: Variabelen en lijnen die als template vormen voor het schrijven.

                        //Dit is de eerste lijn die geschreven moet worden naar het nieuwe bestand.
                        string firstLine = "\"TransactieID\";\"Datum\";\"Kaart Code\";\"Tx Type\";\"Brand\";\"Bedrag\";\"Externe referentie\";\"Terminal referentie\";\"Vestigings Nr\";\"Terminal ID\";\"Locatie\";\"Omschrijving\"\n";

                        // 1 - "TransactieID"; Is uniek en moet overgenomen worden.
                        string transactieIDBB = "";

                        // 2 - "Datum"; voorbeeld "2016-08-15 00:00:00", gebruik eerdere tech.
                        string dateTimeForBB = date.ToString("dd-MM-yyyy ") + "00:00:00";

                        // 3 - "Kaart Code"; 
                        string kaartCodeBB = "";

                        // 4 - "Tx Type";
                        string txType = "";

                        // 5 - \"Brand\";
                        string brandBB = "";

                        // 6 - \"Bedrag\";
                        string bedragBB = "";

                        // 6 - "Externe referentie\";
                        string externeReferentieBB = "";

                        // 7 - "Terminal referentie\";
                        string terminalReferentieBB = "";

                        // 8 - "Vestigings Nr\"; standaard waarde.
                        string vestigingsNrBB = "250";

                        // 9 - "Terminal ID\"; standaard waarde.
                        string terminalIDBB = "";

                        // 10 - "Locatie\"; standaard waarde.
                        string locatieBB = "Eigen winkels-Bruna.nl(250)";

                        // 11 - Output string voor iedere regel:
                        string outputStringBB = "";

                        //--------------------------------------------------------------------------------------------------------------------------------
                        // @desc: Leest de lijnen in en schrijft de aparte regels op volgorde.
                        // @author: Iliass Nassibane.
                        // @date: 24-10-2016.

                        string[] linesArr;

                        using (StreamReader lineReader = new StreamReader(pathOriginalFile))
                        {
                            try
                            {
                                linesArr = File.ReadAllLines(pathOriginalFile);
                            }
                            catch (Exception ex)
                            {
                                // logging dat er een fout is ontstaan.
                                throw ex; // Logging: wordt later uitgewerkt.
                            }
                        }

                        int lineArrRange = linesArr.Length;
                        // voor logging = "Er zijn {0} aantal in regel(s) in het bestand.
                        loggingWriter.WriteLine("{0} - {1} lijnen ingelezen.", DatumTijdHuidig, lineArrRange);

                        if (linesArr != null)
                        {
                            using (StreamWriter lineWriter = new StreamWriter(pathTo))
                            {
                                lineWriter.Write(firstLine); // Eerste lijn wordt geschreven op het bestand.

                                for (int i = 1; i < lineArrRange; i++)
                                {
                                    string txTypePlaceholder;
                                    string bedragPlaceholder;

                                    transactieIDBB = linesArr[i].Split(',')[3];
                                    kaartCodeBB = linesArr[i].Split(',')[0];
                                    brandBB = linesArr[i].Split(',')[1];
                                    externeReferentieBB = linesArr[i].Split(',')[11];
                                    terminalReferentieBB = linesArr[i].Split(',')[10];
                                    terminalIDBB = linesArr[i].Split(',')[7];

                                    // @desc: omzetten van txType.
                                    txTypePlaceholder = linesArr[i].Split(',')[5];
                                    switch (txTypePlaceholder)
                                    {
                                        case "P":
                                            txType = "Purchase";
                                            break;
                                        case "C":
                                            txType = "Cancel";
                                            break;
                                    }

                                    // @desc: omzetten van bedrag.
                                    bedragPlaceholder = linesArr[i].Split(',')[6];
                                    string bedragBBstep1 = bedragPlaceholder.TrimStart('-');
                                    double bedragBBstep2 = Convert.ToDouble(bedragBBstep1);
                                    double bedragBBstep3 = bedragBBstep2 / 100;
                                    string bedragBBstep4 = bedragBBstep3.ToString();

                                    string regPattern = @"[0-9]+[,.].[0-9]*";
                                    Regex decimalYN = new Regex(regPattern, RegexOptions.None);
                                    Match bedragBBMatch = decimalYN.Match(bedragBBstep4);
                                    string additionBedragBB = ",00";

                                    if (!(bedragBBMatch.Success))
                                    {
                                        bedragBB = bedragBBstep4 += additionBedragBB;
                                    }
                                    else
                                    {
                                        bedragBB = bedragBBstep4;
                                    }
                                    

                                    outputStringBB = "\"" + transactieIDBB + "\";\"" + dateTimeForBB + "\";\""
                                        + kaartCodeBB + "\";\"" + txType + "\";\"" + brandBB + "\";\"" + bedragBB +
                                        "\";\""  + externeReferentieBB + "\";\"" + terminalReferentieBB + "\";\""
                                        + vestigingsNrBB + "\";\"" + terminalIDBB + "\";\"" + locatieBB + "\";";
                                    Console.WriteLine("{0}, is output string", outputStringBB);
                                    lineWriter.WriteLine(outputStringBB);
                                    loggingWriter.WriteLine("{0} - Line update: Lijn{1}, is verwerkt in het nieuwe bestand", DatumTijdHuidig, i);
                                }

                                // @desc: Het originele bestand verplaatsen naar het archief.
                                try
                                {
                                    // Voor test: 
                                    Console.WriteLine("- Origineel wordt verplaatst.");
                                    Console.ReadKey();

                                    File.Move(pathOriginalFile, pathOriginalFileTo);
                                    loggingWriter.WriteLine("{0} - {1} is verplaatst naar {2}.", DatumTijdHuidig, pathOriginalFile, pathOriginalFileTo);

                                    // @desc: nakijken of het bestand nog bestaat op de oude locatie.
                                    if (File.Exists(pathOriginalFile))
                                    {
                                        loggingWriter.WriteLine("{0} - Verplaatsing is volbracht, echter staat het origineel nog op het oude locatie. Gelieve een handmatige actie uitvoeren: {1}", DatumTijdHuidig, pathOriginalFile);

                                        // Voor test: 
                                        Console.WriteLine("- Origineel is verplaatst.");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.WriteLine("The original file no longer exists, which is expected.");
                                        loggingWriter.WriteLine("{0} - Het originele bestand staat niet meer op de oude locatie. Nieuwe locatie is: {1}", DatumTijdHuidig, pathOriginalFileTo);

                                        // Voor test: 
                                        Console.WriteLine("- Origineel is er niet meer.");
                                        Console.ReadKey();
                                    }
                                }
                                catch (Exception e)
                                {
                                    loggingWriter.WriteLine("{0} - {1}. Het verplaatsingsproces is gefaald. Er is een handmatige actie vereist om het bestand te verplaatsen: {2}", DatumTijdHuidig, e.ToString(), pathOriginalFile);
                                }
                            }
                        }
                    }
                    loggingWriter.Write("---------------------------------------------------- \n | {0} - Job Update: Job Beëindigd | \n ----------------------------------------------------", DatumTijdHuidig);
                    //--------------------------------------------------------------------------------------------------------------------------------
                } // << StreamWriter
            } // << Finally

            // @desc: XML overwriter: Dit moet de ingestelde waardes op het xml bestand overschrijven met nulls.
            // @author: Iliass Nassibane.
            // @date: 27-10-2016.

            // @desc: overschrijft pathFromNode
            if (pathFromNode != null)
            {
                pathFromNode.InnerText = "";
            }
            // @desc: overschrijft pathNewNode
            if (pathNewNode != null)
            {
                pathNewNode.InnerText = "";
            }
            // @desc: overschrijft pathFromGoToNode
            if (pathFromGoToNode != null)
            {
                pathFromGoToNode.InnerText = "";
            }
            // @desc: overschrijft NieuweDataNode
            if (NieuweDataNode != null)
            {
                NieuweDataNode.InnerText = "";
            }
            // @desc: overschrijft NieuweBestandsNaamNode
            if (NieuweBestandsNaamNode != null)
            {
                NieuweBestandsNaamNode.InnerText = "";
            }

            // @desc: Update de xml bestand, met de nieuwe waardes.
            doc.Save(xmlConfigLocation);

        } // << Main
    } // << Class
} // << NameSpace
