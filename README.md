SpaceShooter (Unity tutorial)

I used this game to practice adding code to game code that was already written. I wanted 
to try my hand at changing and adding different aspects to the space shooter game that 
Unity provides as a tutorial. After following the tutorial as posted on the Unity website,
I made these changes:

•	Changed the input for the trigger button. The tutorial has you link the trigger button
	to a mouse click, but I switched it to the space bar. I find this way to be easier to 
	play, especially for players using a laptop with a track-pad.
	
•	Add lives. Initially, the player was killed immediately upon their first collision – 
	with three lives, the player gets the chance to keep playing before the game is over. 
	When the player has a collision, the ship disappears in an explosion, before 
	reappearing in the initial starting position after the current wave of hazards ends.
	
•	Removed the enemy ships from the first few levels. The player gets the chance to begin 
	the game only facing the asteroids, so they can get some practice with the mechanics 
	of the game before more difficult hazards start appearing.
	
•	As the game continues, the hazards speed up. Initially, the hazards kept coming 
	towards the player at the same speed throughout the game, but by having the hazards 
	speed up the game becomes more difficult as the player progresses.
	
•	Every time an asteroid, enemy ship, or the player is destroyed, the camera gives a 
	small shake. I added this to give the game a more visceral feeling. I also wanted the 
	chance to practice using coroutines.
