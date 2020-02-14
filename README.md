# Video Game Project
This was a project for CS4332 - Intro Programming Video Games at the Univerity of Texas at Dallas in Fall 2019. It was a group project with collaboration between users DanSutter, zvences, kitersi, and Lpotersnak. The player raises and battles creatures called Seduds.  
The game was never completely finished, but we received an A for our work.  

## Gameplay
Raise your own creatures called Seduds and battle NPC's Seduds. Breed them to create the strongest Sedud possible. Go on dates with NPCs for bonus items. Save the world from [secret boss's name goes here]!  


## Controls
WASD - movement  
Mouse - look  
Mouse 1 Click - Pet Sedud/Pick fruit  
Mouse 2 Click on Sedud - Pull up Sedud menu  
	Sometimes you have to Mouse 1 click before you mouse 2 click to work properly.  
E - Whistle to Seduds  
Hold Left CTRL - Freeze mouse movement  
ESC - open pause menu  

## Known Issues
Some or all of this information may be outdated.  
Actual attacking and heath-draining battle code may or may not be broken.  
Player's Seduds carry over to battles on World 1, but not on any other World.  
Both the player's sedud and the randomly generated one do not appear at the same time in battle- Unity itself seems to have bugs with the way that we utilized the asynchonous coroutine to load scenes. This might be able to be fixed by trying LoadSceneMode.Single instead of LoadSceneMode.Additive.  