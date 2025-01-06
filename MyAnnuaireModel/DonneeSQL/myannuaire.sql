-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : lun. 06 jan. 2025 à 12:45
-- Version du serveur : 9.1.0
-- Version de PHP : 8.3.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `myannuaire`
--

-- --------------------------------------------------------

--
-- Structure de la table `aspnetroleclaims`
--

DROP TABLE IF EXISTS `aspnetroleclaims`;
CREATE TABLE IF NOT EXISTS `aspnetroleclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Structure de la table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Structure de la table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Structure de la table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Structure de la table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Structure de la table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `aspnetusers`
--

INSERT INTO `aspnetusers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('3501f4cc-637a-4d19-adc8-8fa142cf9e4e', 'admin@myannuaire.fr', 'ADMIN@MYANNUAIRE.FR', 'admin@myannuaire.fr', 'ADMIN@MYANNUAIRE.FR', 0, 'AQAAAAIAAYagAAAAED4+2XU7hwhccm5z3LmzraB7V3MlUrEah5milGCz6CrD9ByWpEiNFaN22vG+quFbNA==', 'SRGBRJYFKK6TPZRPWS77VPDHXNIKF3IB', '26ab1912-0d66-4d53-8599-87e975a1b4fe', NULL, 0, 0, NULL, 1, 0);

-- --------------------------------------------------------

--
-- Structure de la table `aspnetusertokens`
--

DROP TABLE IF EXISTS `aspnetusertokens`;
CREATE TABLE IF NOT EXISTS `aspnetusertokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Structure de la table `pays`
--

DROP TABLE IF EXISTS `pays`;
CREATE TABLE IF NOT EXISTS `pays` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `pays`
--

INSERT INTO `pays` (`Id`, `Name`) VALUES
(1, 'France'),
(6, 'Espagne');

-- --------------------------------------------------------

--
-- Structure de la table `salaries`
--

DROP TABLE IF EXISTS `salaries`;
CREATE TABLE IF NOT EXISTS `salaries` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nom` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Prenom` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TelephoneFixe` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TelephonePortable` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ServiceId` int NOT NULL,
  `SiegeId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Salaries_ServiceId` (`ServiceId`),
  KEY `IX_Salaries_SiegeId` (`SiegeId`)
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `salaries`
--

INSERT INTO `salaries` (`Id`, `Nom`, `Prenom`, `Email`, `TelephoneFixe`, `TelephonePortable`, `ServiceId`, `SiegeId`) VALUES
(3, 'Bernard', 'Marie', 'marie.bernard@myannuaire.com', '01 45 67 89 01', '06 14 56 78 90', 1, 3),
(4, 'Dubois', 'Paul', 'paul.dubois@myannuaire.com', '01 49 56 90 12', '06 15 67 89 01', 5, 4),
(5, 'Lemoine', 'Sophie', 'sophie.lemoine@myannuaire.com', '01 50 12 34 56', '06 16 78 90 12', 2, 5),
(6, 'Moreau', 'Luc', 'luc.moreau@myannuaire.com', '01 51 23 45 67', '06 17 89 01 23', 3, 2),
(7, 'Gauthier', 'Elise', 'elise.gauthier@myannuaire.com', '01 52 34 56 78', '06 18 90 12 34', 4, 3),
(8, 'Roux', 'Thierry', 'thierry.roux@myannuaire.com', '01 53 45 67 89', '06 19 01 23 45', 1, 4),
(9, 'Faure', 'Hélène', 'helene.faure@myannuaire.com', '01 54 56 78 90', '06 20 12 34 56', 2, 5),
(10, 'Pires', 'Marc', 'marc.pires@myannuaire.com', '01 55 67 89 01', '06 21 23 45 67', 1, 2),
(11, 'Robert', 'Claire', 'claire.robert@myannuaire.com', '01 56 78 90 12', '06 22 34 56 78', 3, 2),
(12, 'Michel', 'Daniel', 'daniel.michel@myannuaire.com', '01 57 89 01 23', '06 23 45 67 89', 4, 3),
(13, 'Lemoine', 'David', 'david.lemoine@myannuaire.com', '01 58 90 12 34', '06 24 56 78 90', 1, 4),
(14, 'Vincent', 'Nathalie', 'nathalie.vincent@myannuaire.com', '01 59 01 23 45', '06 25 67 89 01', 5, 5),
(15, 'Blanc', 'Jacques', 'jacques.blanc@myannuaire.com', '01 60 12 34 56', '06 26 78 90 12', 2, 2),
(16, 'David', 'Isabelle', 'isabelle.david@myannuaire.com', '01 61 23 45 67', '06 27 89 01 23', 4, 2),
(17, 'Lemoine', 'Catherine', 'catherine.lemoine@myannuaire.com', '01 62 34 56 78', '06 28 90 12 34', 1, 3),
(18, 'Leclerc', 'Jean-Marc', 'jeanmarc.leclerc@myannuaire.com', '01 63 45 67 89', '06 29 01 23 45', 2, 4),
(19, 'Benoit', 'Charlotte', 'charlotte.benoit@myannuaire.com', '01 64 56 78 90', '06 30 12 34 56', 3, 5),
(20, 'Morin', 'Lucie', 'lucie.morin@myannuaire.com', '01 65 67 89 01', '06 31 23 45 67', 5, 2),
(21, 'Lemoine', 'Marcel', 'marcel.lemoine@myannuaire.com', '01 66 78 90 12', '06 32 34 56 78', 2, 2),
(22, 'Fournier', 'Pierre', 'pierre.fournier@myannuaire.com', '01 67 89 01 23', '06 33 45 67 89', 5, 4),
(23, 'Lopez', 'Pauline', 'pauline.lopez@myannuaire.com', '01 68 90 12 34', '06 34 56 78 90', 3, 4),
(24, 'Nicolas', 'Emmanuel', 'emmanuel.nicolas@myannuaire.com', '01 69 01 23 45', '06 35 67 89 01', 4, 5),
(25, 'Perrin', 'Alice', 'alice.perrin@myannuaire.com', '01 70 12 34 56', '06 36 78 90 12', 1, 2),
(26, 'Lambert', 'Hugo', 'hugo.lambert@myannuaire.com', '01 71 23 45 67', '06 37 89 01 23', 2, 2),
(27, 'Girod', 'Chloé', 'chloe.girod@myannuaire.com', '01 72 34 56 78', '06 38 90 12 34', 3, 3),
(28, 'Boucher', 'Martin', 'martin.boucher@myannuaire.com', '01 73 45 67 89', '06 39 01 23 45', 4, 4),
(29, 'Joly', 'Sébastien', 'sebastien.joly@myannuaire.com', '01 74 56 78 90', '06 40 12 34 56', 1, 5),
(30, 'Leconte', 'Emilie', 'emilie.leconte@myannuaire.com', '01 75 67 89 01', '06 41 23 45 67', 5, 2),
(31, 'Blanchet', 'Julien', 'julien.blanchet@myannuaire.com', '01 76 78 90 12', '06 42 34 56 78', 2, 3),
(32, 'Brun', 'Anne', 'anne.brun@myannuaire.com', '01 77 89 01 23', '06 43 45 67 89', 3, 5),
(33, 'Dumont', 'Michel', 'michel.dumont@myannuaire.com', '01 78 90 12 34', '06 44 56 78 90', 4, 2),
(34, 'Henri', 'Laura', 'laura.henri@myannuaire.com', '01 79 01 23 45', '06 45 67 89 01', 5, 4),
(35, 'Lemoine', 'Philippe', 'philippe.lemoine@myannuaire.com', '01 80 12 34 56', '06 46 78 90 12', 2, 5),
(37, 'Riviere', 'Louis', 'louis.riviere@myannuaire.com', '01 82 34 56 78', '06 48 90 12 34', 4, 5),
(38, 'Marchand', 'Isabelle', 'isabelle.marchand@myannuaire.com', '01 83 45 67 89', '06 49 01 23 45', 5, 3),
(39, 'Roger', 'Sophie', 'sophie.roger@myannuaire.com', '01 84 56 78 90', '06 50 12 34 56', 1, 4),
(41, 'Martinez', 'Carmen', 'carmen.martinez@myannuaire.com', '01 86 78 90 12', '06 52 34 56 78', 3, 2),
(42, 'Leblanc', 'François', 'francois.leblanc@myannuaire.com', '01 87 89 01 23', '06 53 45 67 89', 4, 3),
(43, 'Fournier', 'Pierre', 'pierre.fournier@myannuaire.com', '01 88 90 12 34', '06 54 56 78 90', 5, 5),
(44, 'Guillot', 'Jean-Paul', 'jeanpaul.guillot@myannuaire.com', '01 89 01 23 45', '06 55 67 89 01', 2, 4),
(46, 'Blanc', 'Gabrielle', 'gabrielle.blanc@myannuaire.com', '01 91 23 45 67', '06 57 89 01 23', 4, 2),
(47, 'Lemoine', 'Michel', 'michel.lemoine@myannuaire.com', '01 92 34 56 78', '06 58 90 12 34', 5, 3),
(48, 'Bonnet', 'Julie', 'julie.bonnet@myannuaire.com', '01 93 45 67 89', '06 59 01 23 45', 1, 2),
(49, 'Charpentier', 'René', 'rene.charpentier@myannuaire.com', '01 94 56 78 90', '06 60 12 34 56', 2, 5),
(50, 'Michel', 'Sylvie', 'sylvie.michel@myannuaire.com', '01 95 67 89 01', '06 61 23 45 67', 3, 4),
(51, 'Jean', 'Dupont', 'jeandupont@myannuaire.fr', '0616161125', '0616161125', 5, 1),
(54, 'Agostinho', 'Enzo', 'enzo.agostinho@myannuaire.fr', '0600000000', '0600000000', 4, 1);

-- --------------------------------------------------------

--
-- Structure de la table `services`
--

DROP TABLE IF EXISTS `services`;
CREATE TABLE IF NOT EXISTS `services` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `services`
--

INSERT INTO `services` (`Id`, `Name`) VALUES
(1, 'Comptabilité'),
(2, 'Production'),
(3, 'Accueil'),
(4, 'Informatique'),
(5, 'Commercial');

-- --------------------------------------------------------

--
-- Structure de la table `sieges`
--

DROP TABLE IF EXISTS `sieges`;
CREATE TABLE IF NOT EXISTS `sieges` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `VilleId` int NOT NULL,
  `TypeSiegeId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Sieges_TypeSiegeId` (`TypeSiegeId`),
  KEY `IX_Sieges_VilleId` (`VilleId`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `sieges`
--

INSERT INTO `sieges` (`Id`, `VilleId`, `TypeSiegeId`) VALUES
(1, 1, 1),
(2, 2, 2),
(3, 3, 2),
(4, 4, 2),
(5, 5, 2),
(13, 6, 2);

-- --------------------------------------------------------

--
-- Structure de la table `typesieges`
--

DROP TABLE IF EXISTS `typesieges`;
CREATE TABLE IF NOT EXISTS `typesieges` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `typesieges`
--

INSERT INTO `typesieges` (`Id`, `Name`) VALUES
(1, 'Administratif'),
(2, 'Production');

-- --------------------------------------------------------

--
-- Structure de la table `utilisateurs`
--

DROP TABLE IF EXISTS `utilisateurs`;
CREATE TABLE IF NOT EXISTS `utilisateurs` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `login` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Discriminator` varchar(21) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EstAdmin` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `utilisateurs`
--

INSERT INTO `utilisateurs` (`Id`, `login`, `password`, `Discriminator`, `EstAdmin`) VALUES
(1, 'admin', 'P@ssw0rd', 'Utilisateur', 1),
(15, 'admin', 'admin', 'Utilisateur', 1);

-- --------------------------------------------------------

--
-- Structure de la table `villes`
--

DROP TABLE IF EXISTS `villes`;
CREATE TABLE IF NOT EXISTS `villes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PaysId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Villes_PaysId` (`PaysId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `villes`
--

INSERT INTO `villes` (`Id`, `Name`, `PaysId`) VALUES
(1, 'Paris', 1),
(2, 'Nantes', 1),
(3, 'Toulouse', 1),
(4, 'Nice', 1),
(5, 'Lille', 1),
(6, 'San sébastian', 6);

-- --------------------------------------------------------

--
-- Structure de la table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20241230233200_InitialCreate', '8.0.11'),
('20241230233730_UpdateUtilisateur', '8.0.11'),
('20250102202945_UpdateDBB', '8.0.11');

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  ADD CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `aspnetuserlogins`
--
ALTER TABLE `aspnetuserlogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `aspnetuserroles`
--
ALTER TABLE `aspnetuserroles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `aspnetusertokens`
--
ALTER TABLE `aspnetusertokens`
  ADD CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `salaries`
--
ALTER TABLE `salaries`
  ADD CONSTRAINT `FK_Salaries_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `services` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Salaries_Sieges_SiegeId` FOREIGN KEY (`SiegeId`) REFERENCES `sieges` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `sieges`
--
ALTER TABLE `sieges`
  ADD CONSTRAINT `FK_Sieges_TypeSieges_TypeSiegeId` FOREIGN KEY (`TypeSiegeId`) REFERENCES `typesieges` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Sieges_Villes_VilleId` FOREIGN KEY (`VilleId`) REFERENCES `villes` (`Id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `villes`
--
ALTER TABLE `villes`
  ADD CONSTRAINT `FK_Villes_Pays_PaysId` FOREIGN KEY (`PaysId`) REFERENCES `pays` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
