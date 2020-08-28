Dinosaur rocks - READ ME v1

______

MainMenu must be the open scene when pressing play
______

Game contains 12 levels, unlocked on successful completion of the previous.

Levels can have three different types of goals; score, locks (cells that require a match through them to unlock) or rocks (cells which require an adjacent link to smash them).

Game begins with the main menu, PLAY button takes you to level select. Clicking a level that isn't locked starts that level.

All levels have a move limit, creating a lose condition.

3 cells are required for a match. New tiles fall in from the top. 

If the board is deadlocked, the logic will destroy all tiles and respawn a fresh board.

There are different types of tiles in the game, standard, circle, cross, square, triangle and star. They are worth different points and some have multipliers for the overall selection score. These settings are scriptable objects found in Settings -> TileSettings.

______

Under 'custom menu' on the tool bar, there are custom made GUI interfaces for two things; player prefs (level) and a board creator.

Player prefs can be set here to skip levels if you wish.

The board creator brings up a GUI to create a new board of a certain size. The board prefab is then made, saved, spawned in the screen, and the view snapped to scene view so that you can edit it.

The user can then select cell/cells and, in the inspector in the CellSetup script, they can change the state of the cell to either disabled (meaning it will be a blank space), locked, rock, or standard.

Name can be changed for the board too and all changes saved.

______

Levels work as scriptable objects in Settings -> LevelRules

These settings objects contain a game board, objectives for that level and maximum moves. This allows for a great deal of customisation.

I have created 12 levels all with a variety of different looks and objectives to showcase this functionality.

______

Tile distribution settings allow for custom distributions of different types of tiles. I have made 3 to demo; standard only, standard with a rare star (1 in 25 chance of star) and all shapes, where the tiles are mainly standard but there are chances for different shapes.

TilePot is a component on the board that keeps track of a shuffled queue of these settings, to ensure even distribution throughout a game.

______

TileAnimationSettings is found in Settings -> AnimationSettings. This is a data hook to edit how a tile falls in terms of speed, max fall time and also the animation curve.

______

In terms of architecture, the game has a selection of managing classes which have a singleton instance.

Tiles have two components, one to control the visuals and one to control the movement.

______

Hope you enjoy playing!

Ps. Sorry for any bugs
  
