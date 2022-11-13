# Sheep Hunters

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: Jessie Tang
-   Section: 06

## Simulation Design

Collect fellow sheeps as your allies and hunt down the wolves coming after you and your flock.

### Controls

-   Player will move by keyboard controls(WASD)
    -   Sheeps will be able to shoot at wolves(don't ask why they have guns)(Mouse Left or Right click)

## _Agent 1 Name_

Follower sheep

### _State 1 Name_
Following State
**Objective:** Follower sheeps will follow the master sheep

#### Steering Behaviors

- _List all behaviors used by this state_
   - Pursue or Seek the master sheep
- Obstacles - Wolf enemies that seek them
- Seperation - _List all agents this state seperates from_
They will seperate from each other so the follower sheeps do not overlap each other. They will also seperate from the master sheep.
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   - _eg, When this agent gets within range of Agent2_
   - _eg, When this agent has reached target of State2_
   This is their base state; the first state they will begin in.
   
### _State 2 Name_
Running State
**Objective:** When the follower sheeps get near a wolf, they will begin to flee from them.

#### Steering Behaviors

- _List all behaviors used by this state_
Flee
- Obstacles - _List all obstacle types this state avoids_
They flee from the wolf enemies.
- Seperation - _List all agents this state seperates from_
They still seperate from each other so the follower sheeps do not overlap each other. They will also seperate from the master sheep.
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   When they get near the wolves, they will begin to flee.

## _Agent 2 Name_

Wolf

### _State 1 Name_
Wander
**Objective:** When the wolf does not see an enemy, they will wander around for one.

#### Steering Behaviors

- _List all behaviors used by this state_
Wandering
- Obstacles - _List all obstacle types this state avoids_
They do not avoid anything as they are looking for prey.
- Seperation - _List all agents this state seperates from_
They will seperate from each other so the wolves do not overlap each other. 
   
#### State Transistions

- _List all the ways this agent can transition to this state_
This is their base state so the state they will begin in.
   
### _State 2 Name_
Catching Prey State
**Objective:** The wolf has spotted a sheep is chasing after it.

#### Steering Behaviors

- _List all behaviors used by this state_
Pursue or Seek the follower sheep
- Obstacles - _List all obstacle types this state avoids_
They will not avoid anything.
- Seperation - _List all agents this state seperates from_
They will seperate from each other so the wolves do not overlap each other. 
   
#### State Transistions

- _List all the ways this agent can transition to this state_
When a wolf is near enough to a sheep, they will begin chasing them.

## Sources

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_

## Make it Your Own

- _List out what you added to your game to make it different for you_
- _If you will add more agents or states make sure to list here and add it to the documention above_
- _If you will add your own assets make sure to list it here and add it to the Sources section

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

