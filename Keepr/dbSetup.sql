CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture',
  Bio VARCHAR(1000) COMMENT 'Short User Bio',
  Gender VARCHAR(255) COMMENT 'Male or Female?',
  Country VARCHAR(255) COMMENT 'User Country'
) default charset utf8 COMMENT '';
-- DELETE FROM keeps;
CREATE TABLE IF NOT EXISTS vaults(
  id int NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) NOT NULL COMMENT 'Vault Name',
  description varchar(1000) COMMENT 'Vault description',
  isPrivate TINYINT COMMENT 'Bool value for making private',
  creatorId VARCHAR(255) NOT NULL COMMENT 'creator id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id int NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) NOT NULL COMMENT 'item post Name',
  description varchar(1000) COMMENT 'item post description',
  img VARCHAR(255) NOT NULL COMMENT 'item image',
  views INT COMMENT 'item user views',
  shares INT COMMENT 'item user shares',
  keeps INT COMMENT 'item user keeps',
  tags VARCHAR(255) COMMENT 'item search tags',
  creatorId VARCHAR(255) NOT NULL COMMENT 'creator id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultkeeps(
  id int NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'creator id',
  vaultId INT NOT NULL COMMENT 'vault id',
  keepId INT NOT NULL COMMENT 'keep id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
SELECT
  *
FROM
  vaultkeeps