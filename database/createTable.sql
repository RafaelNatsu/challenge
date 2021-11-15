SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;

CREATE TABLE `RegistrationUrl`(
    `Id` INT(6) AUTO_INCREMENT PRIMARY KEY,
    `PathUrl` VARCHAR(255),
    `VersionApi` VARCHAR(20),
    `HasAdapter` VARCHAR(1) DEFAULT 'n' COMMENT "options(y or n): if the adapter was implemented",
    `DateAdd` DATETIME DEFAULT CURRENT_TIMESTAMP,
    `DateUpdated` DATETIME DEFAULT NULL,
    `Cooldown` DATETIME DEFAULT NULL
)ENGINE=InnoDB;


CREATE TABLE `ListUrl` (
	`Id` int AUTO_INCREMENT PRIMARY KEY,
    `IdRegistrationUrl` int DEFAULT NULL,
    `IpAddress` varchar(45) DEFAULT NULL,
    `Type` varchar(4) DEFAULT NULL,
    `Name` varchar(50) DEFAULT NULL,
    `Fingerprint` varchar(40) DEFAULT NULL,
    `RouterPort` int DEFAULT NULL,
    `DirectoryPort` int DEFAULT NULL,
    `Flags` varchar(50) DEFAULT NULL,
    `Uptime` varchar(50) DEFAULT NULL,
    `Version` varchar(50) DEFAULT NULL,
    `ContactInfo` Text DEFAULT NULL,
    `Inserted` DATETIME DEFAULT CURRENT_TIMESTAMP,
	
    CONSTRAINT fk_Id_RegistrationUrl
    FOREIGN KEY (IdRegistrationUrl)
        REFERENCES RegistrationUrl(Id) 
        ON DELETE CASCADE,
    -- UNIQUE(IpAddress)
)ENGINE=InnoDB;

CREATE TABLE `DenyList` (
	`Id` INT AUTO_INCREMENT PRIMARY KEY,
    `IdRef` INT,
    `Inserted` DATETIME DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_Id_Ref
    FOREIGN KEY (IdRef)
        REFERENCES ListUrl(Id) 
        ON DELETE CASCADE
)ENGINE=InnoDB;