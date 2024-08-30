# Event Management App

This project is a simple event management application developed using .NET Core MVC. The application provides basic CRUD (Create, Read, Update, Delete) functionalities for managing events.

## Table of Contents
- [Project Overview](#project-overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Requirements](#requirements)
- [Usage](#usage)
- [Seeding Data](#seeding-data)


## Project Overview
The goal of this project is to develop a basic application for managing events using .NET Core MVC. The application is designed to be simple, with a focus on essential event management functionality. 

## Features
- **User Authentication**: Utilizes .NET Core Identity for user registration and login.
- **Event Management**: Create, view, edit, and delete events.
- **Dynamic Fields**: Display and input of event fees dynamically based on the event type (paid or free).
- **Bootstrap Integration**: Responsive and clean UI using Bootstrap.
- **In-Memory Caching**: Utilizes in-memory caching for performance optimization.
- **Database Support**: Supports MySQL (recommended) or PostgreSQL for data storage.

## Technologies Used
- **.NET Core MVC**
- **Entity Framework Core**
- **MySQL**
- **Bootstrap**
- **.NET Core Identity**

## Requirements
- **.NET 6.0 SDK** or later
- **MySQL** for database
- **Visual Studio 2022** or later
- **Git** for version control


## Usage

- Home Page: Lists all the events.
- Create Event: Add a new event with details like title, location, time, and type (paid or free).
- Edit Event: Modify existing event details.
- Delete Event: Remove an event from the list.
- Event Details: View the full details of an event.

## Seeding Data

The application automatically seeds the database with an initial admin user and roles when the database is first created. 
The following default user is created:

- Username: admin@example.com
- Password: Admin@123
- The admin user is assigned the "Admin" role. This seeding process is handled automatically when you run the application for the first time after setting up the database.