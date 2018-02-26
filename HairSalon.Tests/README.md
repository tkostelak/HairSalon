##Lil' Tammy's Salon App##

##.NET and MYSQL Exercise for Epicodus, 02/25/2018
#By Tyler Kostelak

##Description
This website allows an employee at Lil Tammy's Salon to add stylists to the database, and assign clients to the stylists in the database.

##Specifications
#User Stories

*As a salon employee, I need to be able to see a list of all our stylists.*
*As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist.*
*As an employee, I need to add new stylists to our system when they are hired.*
*As an employee, I need to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added.*

#Behavior Driven Development

*First, the employee should be able to add a stylist to the database.*
*Next, the employee should be able to view all existing stylists in the database.*
*After that is working, the employee should be able to view one specific client and their details.*
*Once a specific client can be selected, the employee should be able to add a client to that specific stylist.*

#Setup/Installation Requirements
Clone this repository to your computer
Create SQL databases by executing the following commands:
CREATE DATABASE tyler_kostelak;
CREATE TABLE stylists (id serial AUTO INCREMENT PRIMARY KEY, name VARCHAR(255), client_id	int(255), number	varchar(255), tenure	int(255), specialty	varchar(255));
CREATE TABLE clients (id serial AUTO INCREMENT PRIMARY KEY, name VARCHAR(255), stylist_id	int(255));
Create test database named 'tyler_kostelak_test' by navigating to phpMyAdmin and selecting the database tyler_kostelak, then navigate to operations tab and use command copy database to. Make sure to only select structure only, and create database before copying.

Then run the following commands in your terminal:
dotnet add package MySqlConnector
dotnet restore
dotnet build
dotnet run
Navigate to "localhost:5000" in your preferred browser

#Known Bugs
There are no known bugs at this time.

#Support and contact details
If you have suggestions for how to help me make any additions, or if you have other feedback, please feel free to contact me at tkostelak@gmail.com. All feedback is welcome, keep in mind that the primary focus of this project is C#/.NET and MYSQL so I am not necessarily looking for HTML/CSS advice.

Technologies Used
C#/.NET MVC
MySql
CSS
HTML

License
The software is licensed under the MIT license.

Copyright (c) 2018 Tyler Kostelak
