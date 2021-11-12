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

<br>

## Implementation

### API Design

All API responses will be of the following format...

```
{
  "data": null | <string>,
  "error": null | <string>
}
```

Where...

- `data` is a JSON object with a single attribute containing response data for `GET` request. Always `null` for non-`GET` requests
- `error` is a user-readable string for any `5xx` or `4xx` series status codes and `null` otherwise.

<br>

#### /games `POST`

Start a new game between 2 players

##### Request Body

```
{
  "game_id": <uuid>,
  "player_one": {
    "name": "Guest" | <string>,
    "email": null | <string>
  },
  "player_one": {
    "name": "Guest" | <string>,
    "email": null | <string>
  }
}
```

##### Response Status Codes

- `201` : Game created successfully
- `400` : Malformed request (missing required field)
- `409` : Conflict (game exists)
- `500` : Internal server error (database failure)

##### Response Body

```
{
  "data": null,
  "error": null | <string>
}
```

<br>

#### /games `PUT`

End a previously started game

##### Request Body

```
{
  "game_id": <uuid>,
  "winner": null | "player_one" | "player_two",
  "captures": {
    "player_one": <int>,
    "player_two": <int>
  }
}
```

##### Response Status Codes

- `201` : Game created successfully
- `400` : Malformed request (missing required field)
- `404` : Game not found (game doesn't exist, should be impossible to do this tho)
- `500` : Internal server error (database failure)

##### Response Body

```
{
  "data": null,
  "error": null | <string>
}
```

<br>

#### /stats?email `GET`

Returns statistics for all games played using a given email

##### Request Body

```
null
```

##### Response Status Codes

- `200` : Stats found successfully (`res.data != null`)
- `400` : Malformed request (no email provided) (`res.data == null`)
- `404` : Player not found (no games played) (`res.data == null`)
- `500` : Internal server error (database failure) (`res.data == null`)

##### Response Body

```
{
  "data": null | {
    "stats": {
      "games_played": <int>,
      "wins": <int>,
      "losses": <int>,
      "draws": <int>,
      "incompletes": <int>,
      "avg_game_length": <string>
    }
  },
  "error": null | <string>
}
```

<br>

#### ~~/games `GET`~~

Returns all games in the database (_may not implement this endpoint_)

##### Request Body

```
null
```

##### Response Status Codes

- `200` : Successfully retrieved game data (`res.data != null`)
- `500` : Internal server error (`res.data == null`)

##### Response Body

```
{
  "data": null | {
    "games": [

    ]
  },
  "error": null | <string>
}
```

<br>

### Database Implementation

#### Tables

##### `game`

|             | <u>id</u>            | player_one | player_two | outcome      | start_time  | end_time    |
| ----------- | -------------------- | ---------- | ---------- | ------------ | ----------- | ----------- |
| Type        | `uuid`               | `jsonb`    | `jsonb`    | `string`     | `timestamp` | `timestamp` |
| Constraints | `NOT NULL`, `UNIQUE` | `NOT NULL` | `NOT NULL` | `NOT NULL`   | `NOT NULL`  |             |
| Default     |                      |            |            | ‘incomplete’ | `NOW()`     | `NULL`      |

##### `capture`

|             | <u>game_id</u>       | player_one_captures | player_two_captures |
| ----------- | -------------------- | ------------------- | ------------------- |
| Type        | `uuid`               | `integer`           | `integer`           |
| Constraints | `NOT NULL`, `UNIQUE` | `NOT NULL`          | `NOT NULL`          |
| Default     |                      |                     |                     |

#### Enums

##### `game_outcome`

- `incomplete`
- `player_one_win`
- `player_two_win`
- `draw`

<br>

## Resources

- [Generating UUIDs in C#](https://stackoverflow.com/questions/8477664/how-can-i-generate-uuid-in-c-sharp)
- [Serving static files w/ Express.js](https://expressjs.com/en/starter/static-files.html)
