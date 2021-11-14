SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;

CREATE TABLE `registration_url`(
    `id` INT(6) AUTO_INCREMENT PRIMARY KEY,
    `path_url` VARCHAR(255),
    `version_api` VARCHAR(20),
    `has_adapter` VARCHAR(1) DEFAULT 'n' COMMENT "options(y or n): if the adapter was implemented",
    `date_add` DATETIME DEFAULT CURRENT_TIMESTAMP,
    `date_updated` DATETIME DEFAULT NULL,
    `cooldown` DATETIME DEFAULT NULL
)ENGINE=InnoDB;

-- TODO:criar procedure para update do campo version

CREATE TABLE `list` (
	`id` int AUTO_INCREMENT PRIMARY KEY,
    `id_registration_url` int DEFAULT NULL,
    `ip_address` varchar(45) DEFAULT NULL,
    `type` varchar(4) DEFAULT NULL,
    `name` varchar(50) DEFAULT NULL,
    `fingerprint` varchar(40) DEFAULT NULL,
    `router_port` int DEFAULT NULL,
    `directory_port` int DEFAULT NULL,
    `flags` varchar(50) DEFAULT NULL,
    `uptime` varchar(50) DEFAULT NULL,
    `version` varchar(50) DEFAULT NULL,
    `contactinfo` Text DEFAULT NULL,
    `inserted` DATETIME DEFAULT CURRENT_TIMESTAMP,
	
    CONSTRAINT fk_id_registration_url
    FOREIGN KEY (id_registration_url)
        REFERENCES registration_url(id) 
        ON DELETE CASCADE
)ENGINE=InnoDB;

CREATE TABLE `denylist` (
	`id` INT AUTO_INCREMENT PRIMARY KEY,
    `id_ref` INT,
    `inserted` DATETIME DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_id_ref
    FOREIGN KEY (id_ref)
        REFERENCES list(id) 
        ON DELETE CASCADE
)ENGINE=InnoDB;