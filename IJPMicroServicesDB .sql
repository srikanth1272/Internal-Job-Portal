﻿CREATE DATABASE JobDB

USE JobDB

CREATE TABLE Job(
JobId CHAR(6) PRIMARY KEY,
JobTitle VARCHAR(30) NOT NULL,
JobDescription VARCHAR(100) NOT NULL,
Salary MONEY NOT NULL)


CREATE DATABASE SkillDB

USE SkillDB

CREATE TABLE Skill(
SkillId CHAR(6) PRIMARY KEY,
SkillName VARCHAR(25) NOT NULL,
SkillLevel CHAR(1) CHECK(SkillLevel IN ('B','I','A')) NOT NULL,
SkillCategory VARCHAR(30) NOT NULL)

CREATE DATABASE JobSkillDB

USE JobSkillDB

CREATE TABLE Job(
JobId CHAR(6) PRIMARY KEY)

CREATE TABLE Skill(
SkillId CHAR(6) PRIMARY KEY)

CREATE TABLE JobSkill(
JobId CHAR(6) REFERENCES Job(JobId) NOT NULL,
SkillId CHAR(6) REFERENCES Skill(SkillId) NOT NULL,
Experience INT NOT NULL
PRIMARY KEY(JobId,SkillId))



CREATE DATABASE EmployeeDB

USE EmployeeDB

CREATE TABLE Job(
JobId CHAR(6) PRIMARY KEY)

CREATE TABLE Employee(
EmpId CHAR(6) PRIMARY KEY,
EmpName VARCHAR(30) NOT NULL,
EmailId  VARCHAR(30) NOT NULL UNIQUE,
PhoneNo CHAR(10) NOT NULL UNIQUE,
TotalExperience INT NOT NULL,
JobId CHAR(6) REFERENCES Job(JobId) NOT NULL)

CREATE DATABASE EmployeeSkillDB

USE  EmployeeSkillDB
    
CREATE TABLE Employee(
EmpId CHAR(6) PRIMARY KEY)

CREATE TABLE Skill(
SkillId CHAR(6) PRIMARY KEY)

CREATE TABLE EmployeeSkill(
EmpId CHAR(6) REFERENCES Employee(EmpId),
SkillId CHAR(6) REFERENCES Skill(SkillId),
SkillExperience INT NOT NULL,
PRIMARY KEY(EmpId,SkillId))



CREATE DATABASE JobPostDB

USE  JobPostDB

CREATE TABLE Job(
JobId CHAR(6) PRIMARY KEY,
)


CREATE table JobPost(
PostId INT IDENTITY PRIMARY KEY,
JobId CHAR(6) REFERENCES Job(JobId) NOT NULL,
DateofPosting DATE NOT NULL,
LastDatetoApply DATE NOT NULL,
Vacancies INT NOT NULL)



CREATE DATABASE ApplyJobDB

USE  ApplyJobDB

CREATE TABLE JobPost(
PostId INT PRIMARY KEY,
LastDatetoApply DATE NOT NULL)

CREATE TABLE Employee(
EmpId CHAR(6) PRIMARY KEY)

CREATE TABLE ApplyJob(
PostId INT REFERENCES JobPost(PostId),
EmpId CHAR(6) REFERENCES Employee(EmpId),
AppliedDate DATE NOT NULL,
ApplicationStatus VARCHAR(10) CHECK(ApplicationStatus IN ('Reviewing','Accepted','Rejected')) NOT NULL ,
PRIMARY KEY(PostId,EmpId))




















