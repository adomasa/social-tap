# Xamarin main branchas

# Esamos užduotys
 * Xamarin  **[Adomas]**
 * Web servisai (ASP.NET)  **[Valentinas]**
 * Reikalavimų įgyvendinimas **[Visi]**
    * Lazy
    * Generics (in delegates, events and methods)
    * Delegates
    * Events and their usage: standard and custom.
    * Exceptions and dealing with them: standard and custom (and meaningfull usage of those).
    * Variation and covariation usage (at least demonstration).
    * Anonymous methods
    * Lambda expressions.
    * concurrent programming (threading or async/await (for your own written classes); common resource usage between threads).
    * Config file usage (both - app and user)
    * Dependency Injection.
 * Tikslesnis recognition'as su unit testais **[Andrius]**
 
# Xamarin emuliatoriaus konfigūracija
* Ekranas: Nexus 5X (dirbant su Android design dėl bugų naudoti Nexus 4)
* Android API: 24

# Xamarin projekto specifikacijos
* Tikslinė platforma: **Xamarin Native Android**
* Java SDK: **jdk1.8.0_152**. :warning: (su Java SDK 9+ gerai neveikia Android Designer)
* .NET Runtime: **Mono 5.4.0.201**
* Android SDK Platform Tools versija: **26.0.2**
* Android Emulator versija: **(26.1.4)**
* CMake versija: **3.6.4111459**
* Nuget šaltinis: **https://api.nuget.org/v3/index.json**


# Buvusios užduotys
* Sujungti pikselių skaitymo, analizavimo algoritmus su emgucv karkasu :white_check_mark:
* Sukurti demo WF UI reitingavimo sistemai :white_check_mark:
* Unit testai :white_check_mark:
* Pull request ir code review :white_check_mark:

# Tikslai

## Minimalūs
* Duomenis apie barą (koks baras, kiek įpilta alaus, komentaras apie barą, ar rekomenduoja barą) gauti per windows form UI :white_check_mark:
* Apdoroti vartotojo pateiktus duomenis iš UI :white_check_mark:
* Apdoroti įkeltą nuotrauką su EmguCV filtravimo funkcijomis (threshold, canny, etc.) :white_check_mark:

## Vidutiniai
* Reitingavimo sistema (komentarai, įvertinimas)
* Palyginti įpilto gėrimo kainos(100ml) santykį su visos istorijos vidurkiu(permokėta/nepermokėta)
* Pagal GPS vietą nustatyti vietos pavadinimą

## Aukšti
* Pagal nuotrauką nustatyti gėrimo lygį
* Pagal nuotrauką nustatyti gėrimo kiekį
* Įvertintos vietos matomos žemėlapyje, paspaudus ant pin'o nukelia į atsiliepimų langą

# Idėjos image recognition'ui
* Identifikuoti bokalą naudojant Haar Cascade (.xml) ir EmguCV *(labai daug darbo, nebent projekto tolesnėje dalyje) :x:
* Atrasti bokalo kontūrus naudojant EmguCV API *(reikalingas Haar Cascade, kitaip žymi bet ką)* :x:
* Skaičiuoti alaus spalvos pikselių koncentraciją bounding box'e, kuris eina apie bokalo kontūrą
_____
* Keičiant kameros aperture fotografuojant išblurinti foną geresniam kontūrų radimui
