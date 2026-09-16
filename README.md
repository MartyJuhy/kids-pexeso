# KidsPexeso

A simple single-player memory matching game built with Avalonia UI.

Created by **Martin Juhasz**.

## Run the game

Open a terminal in the project folder and run:

```bash
dotnet run
```

To run an already built version:

```bash
dotnet run --no-build
```

## How to play

1. Choose a board size on the start screen.
2. Click a card to reveal its animal.
3. Click a second card.
4. If the animals match, the pair stays visible and you earn 100 points.
5. If they do not match, both cards remain visible briefly and then turn back over.
6. No other cards can be opened while a pair is being checked.
7. The game ends when all pairs have been found.

## Board sizes

### 4 × 4

- 16 cards
- 8 pairs
- uses the first 8 animals in the list
- larger cards, suitable for younger children

### 8 × 8

- 64 cards
- 32 pairs
- uses all 32 animals
- the board is larger; scrollbars are available if the window or screen is too small

## Score and timer

- correct pair: `+100 points`
- incorrect attempt: `-10 points`
- the score never goes below zero
- the timer starts when a game begins
- the left sidebar shows the score, matched pairs, and elapsed time

## Controls

- **Start screen:** choose `4 × 4` or `8 × 8`.
- **Game screen:** click cards to reveal them.
- **Back to start:** ends the current game and returns to board selection.
- **New game:** after winning, returns to board selection.

## Animals

The game currently contains these animals, each used as one matching pair:

1. 🐴 Horse
2. 🐔 Chick
3. 🐶 Dog
4. 🐱 Cat
5. 🐷 Pig
6. 🐮 Cow
7. 🐐 Goat
8. 🐓 Rooster
9. 🕊️ Dove
10. 🐹 Hamster
11. 🐊 Crocodile
12. 🦒 Giraffe
13. 🐻 Bear
14. 🐯 Tiger
15. 🦁 Lion
16. 🐒 Monkey
17. 🦍 Gorilla
18. 🦧 Orangutan
19. 🙈 Monkey covering its eyes
20. 🙉 Monkey covering its ears
21. 🙊 Monkey covering its mouth
22. 🦆 Duck
23. 🐘 Elephant
24. 🦓 Zebra
25. 🦛 Hippopotamus
26. 🦏 Rhinoceros
27. 🐪 Camel
28. 🦘 Kangaroo
29. 🐼 Panda
30. 🐨 Koala
31. 🦊 Fox
32. 🐺 Wolf

The animals are currently represented by emoji, so the application does not need to download image files. They can be replaced with custom animal illustrations later.

## Requirements

- .NET 10
- Avalonia UI 12.1.2
- a desktop operating system supported by Avalonia UI
