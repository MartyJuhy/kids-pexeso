# KidsPexeso

Jednoduchá hra Pexeso pro jednoho hráče vytvořená v Avalonia UI.

## Spuštění

V terminálu otevři složku projektu a spusť:

```bash
dotnet run
```

Pro spuštění již sestavené verze:

```bash
dotnet run --no-build
```

## Jak hrát

1. Na úvodní obrazovce vyber velikost hrací plochy.
2. Kliknutím na kartu ji otoč a zapamatuj si zvířátko.
3. Klikni na druhou kartu.
4. Pokud jsou zvířátka stejná, dvojice zůstane odkrytá a získáš 100 bodů.
5. Pokud se zvířátka liší, obě karty zůstanou chvíli odkryté a potom se otočí zpět.
6. Během vyhodnocování dvojice nelze otáčet další karty.
7. Hra končí po nalezení všech dvojic.

## Velikosti hry

### 4 × 4

- 16 karet
- 8 dvojic
- používá prvních 8 zvířátek ze seznamu
- větší karty vhodné pro menší děti

### 8 × 8

- 64 karet
- 32 dvojic
- používá všech 32 zvířátek
- pokud je okno nebo obrazovka malá, lze hrací plochu posouvat

## Skóre a čas

- správná dvojice: `+100 bodů`
- chybný pokus: `-10 bodů`
- skóre nikdy neklesne pod nulu
- čas začíná běžet po spuštění hry
- levý panel zobrazuje skóre, počet nalezených dvojic a čas

## Ovládání

- **Startovní obrazovka:** vyber `4 × 4` nebo `8 × 8`.
- **Herní obrazovka:** klikáním otáčej karty.
- **Začátek:** ukončí aktuální hru a vrátí se na výběr velikosti.
- **Nová hra:** po dokončení vrátí hráče na výběr velikosti.

## Zvířátka

Ve hře jsou tato zvířátka, každé v jedné dvojici:

1. 🐴 Koník
2. 🐔 Kuře
3. 🐶 Pes
4. 🐱 Kočka
5. 🐷 Prasátko
6. 🐮 Kravička
7. 🐐 Koza
8. 🐓 Kohout
9. 🕊️ Holub
10. 🐹 Křeček
11. 🐊 Krokodýl
12. 🦒 Žirafa
13. 🐻 Medvěd
14. 🐯 Tygr
15. 🦁 Lev
16. 🐒 Opice
17. 🦍 Gorila
18. 🦧 Orangutan
19. 🙈 Opice se zakrytýma očima
20. 🙉 Opice se zakrytýma ušima
21. 🙊 Opice se zakrytými ústy
22. 🦆 Kachna
23. 🐘 Slon
24. 🦓 Zebra
25. 🦛 Hroch
26. 🦏 Nosorožec
27. 🐪 Velbloud
28. 🦘 Klokan
29. 🐼 Panda
30. 🐨 Koala
31. 🦊 Liška
32. 🐺 Vlk

Zvířátka jsou zatím použita jako emoji, takže aplikace nepotřebuje stahovat žádné obrázkové soubory. Později je možné emoji nahradit vlastními obrázky zvířat.

## Technické požadavky

- .NET 10
- Avalonia UI 12.1.2
- desktopový systém podporující Avalonia UI
