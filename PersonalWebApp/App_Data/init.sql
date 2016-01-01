CREATE TABLE User (
  UserId varchar(15) primary key,
  UserName varchar(60),
  Passwd varchar(20),
  Email varchar(30)
);		

CREATE TABLE AccessCode (
  AccessId varchar(12) primary key,
  Code varchar(10),
  ExpireDt text,
  CreateDt text
);		