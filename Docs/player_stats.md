# Player Stats Design Document

<br>

## Flow

### Recording Game Statistics

1. User clicks "Play" on the <ins>Main Menu</ins>
2. System presents users with <ins>Player Select screen</ins>
3. User 1 selects to play as guest or to enter a name and email
4. System stores User 1 player data
5. User 2 selects to play as guest or to enter a name and email
6. System stores User 2 player data
7. User clicks 'Play' button on <ins>Player Select screen</ins>
8. System presents checkers board
9. System records game start
10. Users play game until win or draw
11. System displays <ins>Game Over screen</ins>
12. System stores game result (outcome, captures, time)

### Viewing Player Statistics

1. User clicks "Player Stats" on the <ins>Main Menu</ins>
2. System presents user with <ins>Find Player Stats screen</ins>
3. User enters an email
4. System finds all games associated with that email
5. System displays <ins>Stats Summary screen</ins>
6. User clicks back button
7. System displays <ins>Main Menu</ins>
