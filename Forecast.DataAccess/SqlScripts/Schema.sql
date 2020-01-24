CREATE DATABASE forecast;

CREATE TABLE forecast.cities (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(64) NOT NULL,
    UNIQUE (name)
) ENGINE=INNODB;

CREATE TABLE forecast.weathertypes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(64) NOT NULL,
    UNIQUE (name)
) ENGINE=INNODB;

CREATE TABLE forecast.weathers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    created_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    date TIMESTAMP NOT NULL,
    temperature DOUBLE NOT NULL,
    city_id INT NOT NULL,
    type_id INT,
    FOREIGN KEY (city_id) REFERENCES cities(id),
    FOREIGN KEY (type_id) REFERENCES weathertypes(id) 
) ENGINE=INNODB;