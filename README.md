# legyen-on-is-milliomos

- megfelelõ adatokat (kategóriákat, kérdéseket, 1 helyes választ és 3 rossz választ) tartalmazó csv file létrehozása
- a program beolvassa a file-t, és létrehoz listát (teszt néven), amely struktúrába (Kérdés struct) rendezve tartalmazza a sorokat (kategória, kérdés, válaszokat)
- a 4 választ tömbbe (válasz tömb néven) rendezzük, amely struktúrába (Válasz struct ) rendezve tartalmazza a sorokat (válasz szövege, helyességére vonatkozó információk)
- a válaszokat összekeverjük, és egy kevert válaszok tömbbe rendezzük
- kiírjuk a feladatot:
	kérdéseket
	kevert válaszokat (a,b,c,d-vel jelölve)
- kérjük a felhasználót, hogy válaszoljon
- beolvassuk a válaszát
- kinyerjük a válasz indexét (letároljuik valaszindex néven)
- vizsgáljuk, hogy a kevertValaszok valaszindexedik eleme igaz-e
	ha igaz, kiírjuk, hogy helyes válasz és 3 mp múlva töröljük a konzolt, és a for ciklus megy tovább a következõ kérdésre
	ha hamis, vége a játéknak