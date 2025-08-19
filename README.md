# NuorisoTaloKortti

**NuorisoTaloKortti** on digitaalisten kulkulupien järjestelmä nuorisotaloille.  
Jokaisella käyttäjällä on **kortti**, joka näkyy puhelimen näytöllä ja sisältää valokuvan, nimen ja muuta tärkeää tietoa.

## Ominaisuudet
- Digitaalisten kulkulupien luominen ja tallennus  
- Käyttäjän valokuva ja henkilötiedot kortissa  
- Web-hallintaliittymä korttien moderointiin  
- Monialustaiset sovellukset: Android (.NET), iOS (Swift) ja Web (ASP.NET Core)  
- Keskitetty tietokanta: Microsoft SQL Server / Azure  

## Teknologia-arkkitehtuuri
- **Palvelin (Backend):** .NET + MS SQL Server / Azure  
- **Android:** .NET (Xamarin)  
- **iOS:** Swift  
- **Web:** ASP.NET Core + HTML/CSS + JavaScript  
- **API:** REST API sovellusten yhdistämiseksi  

## Asennus ja käynnistys (palvelin)
1. Kloonaa repositorio:
   ```bash
   git clone https://github.com/juriki/NuorisoTaloKortti.git
   cd NuorisoTaloKortti
Määritä tietokantayhteys tiedostossa appsettings.json.
Asenna riippuvuudet ja käynnistä:
dotnet run
Palvelin on saatavilla osoitteessa http://localhost:5000 (tai konfiguraation mukaan).
Asiakasohjelmat
Android (.NET): avaa projekti Visual Studio-ohjelmassa
iOS (Swift): avaa Xcode-projektina
Web (ASP.NET Core): aja Visual Studiosta tai dotnet build-komennolla
Kuvakaappaukset
(Lisätään myöhemmin: korttinäkymä mobiilissa ja web-moderaattorin näkymä)
Osallistuminen (Contribute)
Forkkaa repositorio.
Luo haara (branch):
git checkout -b ominaisuus-nimi
Tee muutokset ja commitoi.
Tee Pull Request.
Lisenssi
MIT — voit käyttää ja muokata vapaasti.
## In English

## English README

```markdown
# NuorisoTaloKortti

**NuorisoTaloKortti** is a digital access card system for youth centers.  
Each user has a **card** that is displayed on their phone screen and contains a photo, name, and other key information.

## Features
- Create and store digital access cards  
- Display user photo and personal details on the card  
- Web interface for card moderation  
- Cross-platform clients: Android (.NET), iOS (Swift), and Web (ASP.NET Core)  
- Centralized data storage on Microsoft SQL Server / Azure  

## Technology Stack
- **Backend:** .NET + MS SQL Server / Azure  
- **Android:** .NET (Xamarin)  
- **iOS:** Swift  
- **Web:** ASP.NET Core + HTML/CSS + JavaScript  
- **API:** REST API for client integration  

## Installation & Run (server)
1. Clone the repository:
   ```bash
   git clone https://github.com/juriki/NuorisoTaloKortti.git
   cd NuorisoTaloKortti
Configure database connection in appsettings.json.
Install dependencies and start:
dotnet run
The server will be available at http://localhost:5000 (or as configured).
Clients
Android (.NET): open the project in Visual Studio
iOS (Swift): open in Xcode
Web (ASP.NET Core): run via Visual Studio or dotnet build
Screenshots
(To be added later: mobile card view and web moderation panel)
Contributing
Fork the repository.
Create a new branch:
git checkout -b feature-name
Make your changes and commit.
Open a Pull Request.
License
MIT — free to use and modify.
