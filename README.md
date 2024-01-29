# SimpleEnlessRunner2D | NinjaJump

Development Map is available [here](https://miro.com/app/board/uXjVN5nrFdE=/?share_link_id=575960366992)

## Development notes:

### [Finite-state machine](https://en.wikipedia.org/wiki/Finite-state_machine)

Application is controlled by two states: [StartState](Assets/Scripts/States/StartState.cs) and [PlayingState](Assets/Scripts/States/PlayingState.cs)

[StartState](Assets/Scripts/States/StartState.cs) defines game behavior upon launch and after the character dies.\
[PlayingState](Assets/Scripts/States/PlayingState.cs) defines game loop behavior, runs environment object, counts score.

### [Strategy](https://refactoring.guru/design-patterns/strategy)

Simple strategy is used to determine behavior of each [environment object](Assets/Scripts/Environment/EnvironmentObject.cs).\
There are three behaviors:\
- [Obstacle](Assets/Scripts/Environment/Obstacle.cs) - kills character, switches application to StartState
- [Coin](Assets/Scripts/Environment/Coin.cs) - gives additional score
- [JetPack](Assets/Scripts/Environment/JetPack.cs) - make character fly above all obstacles for a period of time
