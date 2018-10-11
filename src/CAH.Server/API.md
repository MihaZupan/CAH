# Lobby

## Lobby: Client API

#### ***`lobby/login(username, [password])`***
* Success - available deck names, opened game rooms, player's score (only saved if a password is set)
* Failure: reason (account exists and has a password / wrong password)

#### ***`lobby/joinGameRoom(gameRoomId)`***
* Success: target game room info
* Failure: reason (closed / wrong password / room full).

#### ***`lobby/createGameRoom(gameRoomInfo)`***
* Returns the ID of the created room


## Lobby: Server updates

#### ***`lobby/gameRoomUpdated`***
* ID, new info
* If the ID is new, this is a creation update

#### ***`lobby/gameRoomClosed`***
* ID



# Game room

## Game room: Client API

#### ***`gameRoom/changeSettings(newInfo)`***
* Change the name, settings, password, decks, room host ...
* Some changes are only possible if the game is not active

#### ***`gameRoom/startGame()`***

#### ***`gameRoom/pause()`***
* The game will be paused at the start of next round

#### ***`gameRoom/resume()`***

#### ***`gameRoom/kick(id)`***

#### ***`gameRoom/leave()`***

#### ***`gameRoom/sendMessage(message)`***


## Game room: Server updates

#### ***`gameRoom/playerJoined`***
* Player info

#### ***`gameRoom/playerLeft`***
* Player info, left/disconnected

#### ***`gameRoom/settingsChanged`***
* New game room info

#### ***`gameRoom/pausePending`***
* Number of the round that will be paused

#### ***`gameRoom/messageReceived`***



# Game

## Game: Client API

#### ***`game/useEmptyCard(text)`***
* Returns a unique ID of the created card

#### ***`game/selectCards(cardIds)`***
* May be submitted multiple times in the same round - last one counts

#### ***`game/deselectCards()`***

#### ***`game/vote(combinationId)`***

#### ***`game/resetHand()`***
* Hand will be reset on the next round


## Game: Server updates

#### ***`game/readyListChanged`***
* Fired whenever a user calls select/deselect

#### ***`game/roundStarted`***
* Round number, participating players, black card, available cards in hand

#### ***`game/roundEnded`***
* Round number, winner (or winners - each of the winners gets half a point), updated scoreboard

#### ***`game/votingStage`***
* Played combinations in random order