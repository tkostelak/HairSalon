#_Lil' Tammy's Salon App_

###_.NET and MYSQL Exercise for Epicodus, 02/25/2018_
###_By Tyler Kostelak_

## Description
_This website allows an employee at Lil Tammy's Salon to add stylists to the database, and assign clients to the stylists in the database._

## Specifications
# User Stories

* _As a salon employee, I need to be able to see a list of all our stylists._
* _As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist._
* _As an employee, I need to add new stylists to our system when they are hired._
* _As an employee, I need to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added._

# Behavior Driven Development

* _First, the employee should be able to add a stylist to the database._
* _Next, the employee should be able to view all existing stylists in the database._
* _After that is working, the employee should be able to view one specific client and their details._
* _Once a specific client can be selected, the employee should be able to add a client to that specific stylist._

## Setup/Installation Requirements
* _Clone this repository to your computer_
* _Create SQL databases by executing the following commands:
  _CREATE DATABASE tyler_kostelak;_
 _CREATE TABLE stylists (id serial AUTO INCREMENT PRIMARY KEY,  name VARCHAR(255), client_id	int(255), number	varchar(255), tenure	int(255), specialty	varchar(255));_
_CREATE TABLE clients (id serial AUTO INCREMENT PRIMARY KEY, name VARCHAR(255), stylist_id	int(255));_
_Create test database named 'tyler_kostelak_test' by navigating to phpMyAdmin and selecting the database tyler_kostelak, then navigate to operations tab and use command copy database to. Make sure to only select structure only, and create database before copying._

* _Then run the following commands in your terminal:_
_dotnet add package MySqlConnector_
_dotnet restore_
_dotnet build_
_dotnet run_
_Navigate to "localhost:5000" in your preferred browser_

## Known Bugs
There are no known bugs at this time.

## Support and contact details
If you have suggestions for how to help me make any additions, or if you have other feedback, please feel free to contact me at tkostelak@gmail.com. All feedback is welcome, keep in mind that the primary focus of this project is C#/.NET and MYSQL so I am not necessarily looking for HTML/CSS advice.

## Technologies Used
C#/.NET MVC
MySql
CSS
HTML

## License
The software is licensed under the MIT license.

Copyright (c) 2018 Tyler Kostelak
