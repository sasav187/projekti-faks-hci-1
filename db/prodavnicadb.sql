-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: localhost    Database: prodavnicadb
-- ------------------------------------------------------
-- Server version	8.0.42

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `adresa`
--

DROP TABLE IF EXISTS `adresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adresa` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `KorisnikId` int DEFAULT NULL,
  `Ulica` varchar(100) DEFAULT NULL,
  `Grad` varchar(50) DEFAULT NULL,
  `PostanskiBroj` varchar(10) DEFAULT NULL,
  `Drzava` varchar(50) DEFAULT NULL,
  `Tip` enum('Licna','Isporuka') DEFAULT 'Licna',
  PRIMARY KEY (`Id`),
  KEY `KorisnikId` (`KorisnikId`),
  CONSTRAINT `adresa_ibfk_1` FOREIGN KEY (`KorisnikId`) REFERENCES `korisnik` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adresa`
--

LOCK TABLES `adresa` WRITE;
/*!40000 ALTER TABLE `adresa` DISABLE KEYS */;
INSERT INTO `adresa` VALUES (1,1,'Kralja Petra 10','Banja Luka','78000','BiH','Licna'),(2,2,'Nemanjina 5','Sarajevo','71000','BiH','Licna'),(3,1,'Ilićka 22','Bijeljina','76300','BiH','Isporuka'),(4,4,'test','test','test','test','Licna');
/*!40000 ALTER TABLE `adresa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `kategorija`
--

DROP TABLE IF EXISTS `kategorija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kategorija` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Naziv` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kategorija`
--

LOCK TABLES `kategorija` WRITE;
/*!40000 ALTER TABLE `kategorija` DISABLE KEYS */;
INSERT INTO `kategorija` VALUES (1,'Elektronika'),(2,'Knjige'),(3,'Igračke'),(4,'Alkoholna pica');
/*!40000 ALTER TABLE `kategorija` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `korisnik`
--

DROP TABLE IF EXISTS `korisnik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `korisnik` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Ime` varchar(50) DEFAULT NULL,
  `Prezime` varchar(50) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Lozinka` varchar(100) DEFAULT NULL,
  `DatumRegistracije` date DEFAULT NULL,
  `UlogaId` int DEFAULT NULL,
  `Jezik` varchar(10) DEFAULT 'sr',
  `Tema` varchar(50) DEFAULT 'LightPurpleLime',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`),
  KEY `UlogaId` (`UlogaId`),
  CONSTRAINT `korisnik_ibfk_1` FOREIGN KEY (`UlogaId`) REFERENCES `uloga` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `korisnik`
--

LOCK TABLES `korisnik` WRITE;
/*!40000 ALTER TABLE `korisnik` DISABLE KEYS */;
INSERT INTO `korisnik` VALUES (1,'Marko','Marković','marko@gmail.com','lozinka123','2024-06-01',1,'sr','LightPurpleLime'),(2,'Ana','Anić','ana@gmail.com','tajna456','2024-06-02',1,'sr','LightPurpleLime'),(3,'Admin','Adminović','admin@shop.com','admin123','2024-06-01',2,'sr','DarkGreenPink'),(4,'test','test','test','test','2025-08-14',1,'en','DarkPurpleAmber');
/*!40000 ALTER TABLE `korisnik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `kupon`
--

DROP TABLE IF EXISTS `kupon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kupon` (
  `Kod` varchar(10) NOT NULL,
  `Popust` decimal(5,2) DEFAULT NULL,
  `VaziDo` date DEFAULT NULL,
  PRIMARY KEY (`Kod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kupon`
--

LOCK TABLES `kupon` WRITE;
/*!40000 ALTER TABLE `kupon` DISABLE KEYS */;
INSERT INTO `kupon` VALUES ('JUN10',10.00,'2024-06-30'),('JUN30',10.00,'2026-06-30'),('WELCOME5',5.00,'2024-12-31'),('WELCOME6',5.00,'2025-12-31');
/*!40000 ALTER TABLE `kupon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `listazelja`
--

DROP TABLE IF EXISTS `listazelja`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `listazelja` (
  `KorisnikId` int NOT NULL,
  `ProizvodId` int NOT NULL,
  `DatumDodavanja` date DEFAULT NULL,
  PRIMARY KEY (`KorisnikId`,`ProizvodId`),
  KEY `ProizvodId` (`ProizvodId`),
  CONSTRAINT `listazelja_ibfk_1` FOREIGN KEY (`KorisnikId`) REFERENCES `korisnik` (`Id`),
  CONSTRAINT `listazelja_ibfk_2` FOREIGN KEY (`ProizvodId`) REFERENCES `proizvod` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `listazelja`
--

LOCK TABLES `listazelja` WRITE;
/*!40000 ALTER TABLE `listazelja` DISABLE KEYS */;
INSERT INTO `listazelja` VALUES (1,2,'2024-06-05'),(2,3,'2024-06-07');
/*!40000 ALTER TABLE `listazelja` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `narudzba`
--

DROP TABLE IF EXISTS `narudzba`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `narudzba` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `KorisnikId` int DEFAULT NULL,
  `AdresaId` int DEFAULT NULL,
  `Datum` datetime DEFAULT CURRENT_TIMESTAMP,
  `UkupnaCijena` decimal(10,2) DEFAULT NULL,
  `Status` enum('U obradi','Poslato','Završeno') DEFAULT 'U obradi',
  PRIMARY KEY (`Id`),
  KEY `KorisnikId` (`KorisnikId`),
  KEY `AdresaId` (`AdresaId`),
  CONSTRAINT `narudzba_ibfk_1` FOREIGN KEY (`KorisnikId`) REFERENCES `korisnik` (`Id`),
  CONSTRAINT `narudzba_ibfk_2` FOREIGN KEY (`AdresaId`) REFERENCES `adresa` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `narudzba`
--

LOCK TABLES `narudzba` WRITE;
/*!40000 ALTER TABLE `narudzba` DISABLE KEYS */;
INSERT INTO `narudzba` VALUES (1,1,3,'2024-06-10 00:00:00',1540.99,'Poslato'),(2,2,2,'2024-06-12 00:00:00',25.50,'U obradi'),(3,4,4,'2025-08-14 16:04:06',1500.00,'U obradi');
/*!40000 ALTER TABLE `narudzba` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `narudzbastatuslog`
--

DROP TABLE IF EXISTS `narudzbastatuslog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `narudzbastatuslog` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `NarudzbaId` int DEFAULT NULL,
  `StariStatus` enum('U obradi','Poslato','Završeno') DEFAULT NULL,
  `NoviStatus` enum('U obradi','Poslato','Završeno') DEFAULT NULL,
  `DatumPromjene` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `narudzbastatuslog`
--

LOCK TABLES `narudzbastatuslog` WRITE;
/*!40000 ALTER TABLE `narudzbastatuslog` DISABLE KEYS */;
INSERT INTO `narudzbastatuslog` VALUES (1,1,'Završeno','Poslato','2025-06-16 20:09:47');
/*!40000 ALTER TABLE `narudzbastatuslog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `placanje`
--

DROP TABLE IF EXISTS `placanje`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `placanje` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `NarudzbaId` int DEFAULT NULL,
  `Iznos` decimal(10,2) DEFAULT NULL,
  `Nacin` enum('Kartica','Pouzećem','PayPal') DEFAULT NULL,
  `DatumPlacanja` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  KEY `NarudzbaId` (`NarudzbaId`),
  CONSTRAINT `placanje_ibfk_1` FOREIGN KEY (`NarudzbaId`) REFERENCES `narudzba` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `placanje`
--

LOCK TABLES `placanje` WRITE;
/*!40000 ALTER TABLE `placanje` DISABLE KEYS */;
INSERT INTO `placanje` VALUES (1,1,1540.99,'Kartica','2025-06-16 19:10:23'),(2,2,25.50,'Pouzećem','2025-06-16 19:10:23'),(3,3,1500.00,'Pouzećem','2025-08-14 16:04:10');
/*!40000 ALTER TABLE `placanje` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `proizvod`
--

DROP TABLE IF EXISTS `proizvod`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proizvod` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Naziv` varchar(100) DEFAULT NULL,
  `Opis` text,
  `Cijena` decimal(10,2) DEFAULT NULL,
  `NaStanju` int DEFAULT NULL,
  `KategorijaId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `KategorijaId` (`KategorijaId`),
  CONSTRAINT `proizvod_ibfk_1` FOREIGN KEY (`KategorijaId`) REFERENCES `kategorija` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `proizvod`
--

LOCK TABLES `proizvod` WRITE;
/*!40000 ALTER TABLE `proizvod` DISABLE KEYS */;
INSERT INTO `proizvod` VALUES (1,'Laptop Lenovo','Gaming laptop 16GB RAM',1500.00,9,1),(2,'Harry Potter','Knjiga na engleskom',25.50,50,2),(3,'LEGO kocke','Set za gradnju',40.99,30,3),(4,'Pivo Jelen','Okrepljuje dusu i tijelo',2.00,200,4);
/*!40000 ALTER TABLE `proizvod` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recenzija`
--

DROP TABLE IF EXISTS `recenzija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recenzija` (
  `KorisnikId` int NOT NULL,
  `ProizvodId` int NOT NULL,
  `Ocjena` int DEFAULT NULL,
  `Komentar` varchar(255) DEFAULT NULL,
  `Datum` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`KorisnikId`,`ProizvodId`),
  KEY `ProizvodId` (`ProizvodId`),
  CONSTRAINT `recenzija_ibfk_1` FOREIGN KEY (`KorisnikId`) REFERENCES `korisnik` (`Id`),
  CONSTRAINT `recenzija_ibfk_2` FOREIGN KEY (`ProizvodId`) REFERENCES `proizvod` (`Id`),
  CONSTRAINT `recenzija_chk_1` CHECK ((`Ocjena` between 1 and 5))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recenzija`
--

LOCK TABLES `recenzija` WRITE;
/*!40000 ALTER TABLE `recenzija` DISABLE KEYS */;
INSERT INTO `recenzija` VALUES (1,1,5,'Odličan laptop!','2025-06-16 19:10:23'),(2,2,4,'Zanimljiva knjiga.','2025-06-16 19:10:23');
/*!40000 ALTER TABLE `recenzija` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stavkanarudzbe`
--

DROP TABLE IF EXISTS `stavkanarudzbe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stavkanarudzbe` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `NarudzbaId` int DEFAULT NULL,
  `ProizvodId` int DEFAULT NULL,
  `Kolicina` int DEFAULT NULL,
  `Cijena` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `NarudzbaId` (`NarudzbaId`),
  KEY `ProizvodId` (`ProizvodId`),
  CONSTRAINT `stavkanarudzbe_ibfk_1` FOREIGN KEY (`NarudzbaId`) REFERENCES `narudzba` (`Id`),
  CONSTRAINT `stavkanarudzbe_ibfk_2` FOREIGN KEY (`ProizvodId`) REFERENCES `proizvod` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stavkanarudzbe`
--

LOCK TABLES `stavkanarudzbe` WRITE;
/*!40000 ALTER TABLE `stavkanarudzbe` DISABLE KEYS */;
INSERT INTO `stavkanarudzbe` VALUES (1,1,1,1,1500.00),(2,1,3,1,40.99),(3,2,2,1,25.50),(4,3,1,1,1500.00);
/*!40000 ALTER TABLE `stavkanarudzbe` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uloga`
--

DROP TABLE IF EXISTS `uloga`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `uloga` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Naziv` varchar(20) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uloga`
--

LOCK TABLES `uloga` WRITE;
/*!40000 ALTER TABLE `uloga` DISABLE KEYS */;
INSERT INTO `uloga` VALUES (1,'Kupac'),(2,'Admin');
/*!40000 ALTER TABLE `uloga` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-08-14 19:26:48
