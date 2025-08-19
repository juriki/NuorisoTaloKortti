
# NuorisoTaloKortti

Suomeksi | English

Suomeksi

NuorisoTaloKortti on digitaalinen kulkulupajärjestelmä nuorisotaloille.
Jokaisella käyttäjällä on kortti, joka näkyy puhelimen näytöllä ja sisältää valokuvan, nimen ja muuta tärkeää tietoa.

### Ominaisuudet
- Digitaalisten kulkulupien luominen ja tallennus
- Käyttäjän valokuva ja henkilötiedot kortissa
- Web-hallintaliittymä korttien moderointiin
- Monialustaiset sovellukset: Android (.NET), iOS (Swift) ja Web (ASP.NET Core)
- Keskitetty tietokanta: Microsoft SQL Server / Azure

### Teknologia-arkkitehtuuri
- Palvelin: .NET + MS SQL Server / Azure
- Android: .NET (Xamarin)
- iOS: Swift (erillinen repository: https://github.com/juriki/NuorisoKortti)
- Web: ASP.NET Core + HTML/CSS + JavaScript
- API: REST API sovellusten yhdistämiseksi

### Asennus ja käynnistys (palvelin)
git clone https://github.com/juriki/NuorisoTaloKortti.git
cd NuorisoTaloKortti
Määritä tietokantayhteys tiedostossa appsettings.json
dotnet run
Palvelin: http://localhost:5000

### Asiakasohjelmat
- Android (.NET): avaa projekti Visual Studiossa
- iOS (Swift): katso erillinen repository: https://github.com/juriki/NuorisoKortti
- Web (ASP.NET Core): dotnet build

### Kuvakaappaukset
Tulossa pian: mobiilikortinäkymä ja web-moderaattorin paneeli

### Osallistuminen
Forkkaa repositorio
git checkout -b ominaisuus-nimi
Tee muutokset ja commitoi
Tee Pull Request

### Lisenssi
MIT

---

English

NuorisoTaloKortti is a digital access card system for youth centers.
Each user has a card displayed on their phone screen containing a photo, name, and other key information.

### Features
- Create and store digital access cards
- Display user photo and personal details on the card
- Web interface for card moderation
- Cross-platform clients: Android (.NET), iOS (Swift), and Web (ASP.NET Core)
- Centralized database: Microsoft SQL Server / Azure

### Technology Stack
- Backend: .NET + MS SQL Server / Azure
- Android: .NET (Xamarin)
- iOS: Swift (separate repository: https://github.com/juriki/NuorisoKortti)
- Web: ASP.NET Core + HTML/CSS + JavaScript
- API: REST API for client integration

### Installation & Run (server)
git clone https://github.com/juriki/NuorisoTaloKortti.git
cd NuorisoTaloKortti
Configure the database connection in appsettings.json
dotnet run
Server: http://localhost:5000

### Clients
- Android (.NET): open the project in Visual Studio
- iOS (Swift): see separate repository: https://github.com/juriki/NuorisoKortti
- Web (ASP.NET Core): dotnet build

### Screenshots
Coming soon: mobile card view and web moderation panel

### Contributing
Fork the repository
git checkout -b feature-name
Make your changes and commit
Open a Pull Request

### License
MIT
</pre>
