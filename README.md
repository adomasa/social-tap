# Kitai savaitei
* Sujungti pikselių skaitymo, analizavimo algoritmus su emgucv karkasu
* Sukurti WF UI reitingavimo sistemai

# Buvusios užduotys
* Unit testai :white_check_mark:
* Pull request ir code review :white_check_mark:

# Tikslai

## Minimalūs
* Duomenis apie barą (koks baras, kiek įpilta alaus, komentaras apie barą, ar rekomenduoja barą) gauti per windows form UI
* Įrašyti duomenis į log'ą

## Vidutiniai
* Reitingavimo sistema (komentarai, įvertinimas)
* Palyginti įpilto gėrimo kainos(100ml) santykį su visos istorijos vidurkiu(permokėta/nepermokėta)
* Pagal GPS vietą nustatyti vietos pavadinimą

## Aukšti
* Pagal nuotrauką nustatyti gėrimo lygį
* Pagal nuotrauką nustatyti gėrimo kiekį
* Įvertintos vietos matomos žemėlapyje, paspaudus ant pin'o nukelia į atsiliepimų langą

# Idėjos image recognition'ui
* Identifikuoti bokalą naudojant Haar Cascade (.xml) ir EmguCV
* Atrasti bokalo kontūrus naudojant EmguCV API
* Skaičiuoti alaus spalvos pikselių koncentraciją bounding box'e, kuris eina apie bokalo kontūrą
_____
* Keičiant kameros aperture fotografuojant išblurinti foną geresniam kontūrų radimui
