# TexasHoldem
> Play texas Holdem with your friends online!
## General info

- Modern flat interface (see pictures below)
- All features of classic Texas holdem
- Socket connection
- Inspired by PokerStars

## Features

- Login or sign up to play
- Get 1000 coins after signing up
- All passwords are hashed
- TCP socket connection
- Add money by typing phrases in add money menu
- Create or join a room and play with friends
- Set limits for amount of players in room

## Installation

> Packages required for TexasHoldem.Server
- BCrypt.Net-Next
- Microsoft.AspNetCore.Hosting
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.Extensions.Hosting

> Packages required for TexasHoldem.DAL
- Microsoft.EntityFrameworkCore
- System.ComponentModel.Annotations

> Packages required for TexasHoldem.Migrations
- Microsoft.EntityFrameworkCore.Relational
- Microsoft.EntityFrameworkCore.SqlServer

> Add reference to projects TexasHoldem.CommonAssembly, TexasHoldem.DAL and TexasHoldem.DAL.Migrations from TexasHoldem.Server
> Add reference to projects TexasHoldem.CommonAssembly from TexasHoldem.Client

> Build SQL database table using:
- Open git bash at root of TexasHoldem.Server
- dotnet tool install --global dotnet-ef  
- Open git bash in core project and use command: dotnet add package Microsoft.EntityFrameworkCore.Design   
- dotnet-ef migrations add Users -p (path to TexasHoldem.DAL.Migrations.csproj) --context TexasHoldem.DAL.UsersContext
- dotnet-ef database update -p (path to TexasHoldem.DAL.Migrations.csproj)--context TexasHoldem.DAL.UsersContext

## Screenshots 
![alt text](https://github.com/D3AD-E/TexasHoldem/blob/master/README_Pictures/SampleImage1.png?raw=true)
![alt text](https://github.com/D3AD-E/TexasHoldem/blob/master/README_Pictures/SampleImage2.png?raw=true)
![alt text](https://github.com/D3AD-E/TexasHoldem/blob/master/README_Pictures/SampleImage3.png?raw=true)
![alt text](https://github.com/D3AD-E/TexasHoldem/blob/master/README_Pictures/SampleImage4.png?raw=true)
