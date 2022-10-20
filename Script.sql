create database DatascoringDB;

use DatascoringDB;

create table Users(
Id int primary key identity(1,1),
Email Varchar(200) UNIQUE not null ,
Name varchar(200) not null,
LastName varchar(200) not null,
Password varchar(500) not null);