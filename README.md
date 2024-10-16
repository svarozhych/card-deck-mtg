# card-deck-mtg
A Web Application, where you can create or view card decks based on the Magic The Gathering theme.

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