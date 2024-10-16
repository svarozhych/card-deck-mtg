# Card Decks Management Platform
A web application that allows users to easily manage and create custom card decks for Magic: The Gathering (MTG). Users can browse, filter, and view an extensive library of available MTG cards. Main features: deck building, card searching/filtering, and organization.

## Installation
1. Clone the repo:
    ```bash
    git clone git@github.com:svarozhych/card-deck-mtg.git
    ```
2. Install dependencies:
    ```bash
    dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 7.0.4
    ```
    ```bash
    dotnet add package Microsoft.EntityFrameworkCore --version 7.0.5
    ```
    
3. If you don't have .NET installed, download it here:
   ```bash 
   https://dotnet.microsoft.com/en-us/download
   ```

## Usage
To start the project:
```bash
cd mtg
dotnet build
```
```bash
dotnet run
```
