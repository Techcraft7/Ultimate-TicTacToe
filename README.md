# Ultimate TicTacToe

Ultimate TicTacToe is a simple game that is similar to TicTacToe, but it is on an 8x8 checker board.

## Gameplay

- 2 Players
- 8x8 Grid

When the game starts, Player 1 must place their piece on the edge for their first 2 turns. Player 2 only has to place a tile on the edge for 1 turn. After this, players can play in any space that is not occupied by a piece.

### You win by having 4 tiles in a row!

## Server

Ultimate TicTacToe has a server. The server has a primitive web interface that can only be accessed by GET requests.

**Headers are ignored**

#### Currently there is not authentication, but that will be coming soon!

## Client

The client is very basic, and has a single player mode, two player mode, and an online mode.

### Single player mode

You play against a bot

### Two player mode

You play against someone else

### Online mode

You play against people over the network

## Protocol

The protocol is simple, see the UTTTNetLib Project under Packets for info.

Every packet has a handler for the Client and Server sides.

#### Regular Packet
```
<Packet ID: byte> <Data: byte[]>
```
#### Room-bound Packet
```
<Packet ID: byte> <Room ID: uint (4 bytes)> <Data: byte[]>
```
Packets with (RB) next to them are room-bound packets

### On Connect

C -> S Ping (0x00) <Data: byte[4]>

S -> C Ping (0x00) <Data: byte[4]> server must respond with the same 4 bytes send by client or client will disconnect.

C -> S Get Rooms (0x02)

S -> C Get Rooms (0x02) <Length: int> <Available Rooms: uint[]>

C -> S Join Room (RB) (0x01) <Room ID: uint>

S -> C Join Room (RB) (0x01) <Status: byte>

##### Join Room Status

0 = Failed
1 = Success

### Gameplay

#### Playing a piece

C -> S Play Piece (RB) (0x??) <X: byte> <Y: byte>

S -> C Play Piece (RB) (0x??) <State: byte>

##### Play piece state

0 = Invalid/Not your turn

1 = Success

#### Getting Board Data

C -> S GetGameState (RB) (0x??) 

S -> C GetGameState (RB) (0x??) <P1: ulong> <P2: ulong> <TurnIndex: byte>

##### Notes

These two `ulong`'s represent the pieces of each player.
Each bit is a piece, the client then converts this into a `PieceState[8, 8]`

##### A `PieceState` can be:

- NONE
- P1
- P2

#### Disconnect packet

`0xFF<Reason: byte><Length: int><String: byte[Length]>`

##### Reason

0 = normal

1 = Player 1 wins

2 = Player 2 wins