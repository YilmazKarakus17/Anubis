# Anubis
A 2D action/adventure platformer for our Multi-Platform Games Development (ECS657U) Group Project.

## Game Link
https://yilmazkarakus17.github.io/Anubis/build/index.html

## Most Recent Branch
Anubis_v1.2 https://github.com/YilmazKarakus17/Anubis/tree/Anubis_v1.2

## Instructions
### Movement Keys
##### To Move Left: **A** or **Left Arrow**
##### To Move Right: **D** or **Right Arrow**
##### To Jump: **W** or **Space Bar** or **Up Arrow**
##### To Dash: **L + Shift**


##### To Start a Conversation with NPCs: **Left Mouse Click**
##### To Skip Continue Dialog with NPCs: **Tap**

##### To Attack: **Left Mouse Click** or J
##### To Chain Attack: Repeatedly press **Left Mouse Click** or J
##### To Create Shockwaves: **Right Mouse Click** or K
##### To Create Thunder Strike: **Middle Mouse Click** or L

### Powerups
##### Invulnerability: Makes the player invulnerable for a limited amount of time
##### Gravity Reverse: Reverses the player's gravity and orientation (anti-gravity)
##### Jump Boost: Allows for greater jump magnitude.

## Game Design
The base game concept that our group has decided to design and implement from is concept number 4. The game is to be a 2D combat-based platformer where the player defeats enemies, in order to proceed higher and higher. The will of the gods shall not determine the fate of our protagonist. Defy the gods and ascend higher than the gods themselves!

## Concept & Features
### Essential features:
* The player can move horizontally, jump, dash, descend and wall jump and interact with NPCs which tell a tale or give the player useful advice. Such allows the player to navigate through obstacles and clear challenges that the player will face. The player must navigate the platforms to reach the end of the level to proceed to subsequent levels.
* Different types of platforms and obstacles: moving, slippery, one-time use, breakable walls, unlockable doors, traps (variations spikes and lava), etc.
  * Whilst the player is on jump pads, the player’s jump force is increased, allowing the player to reach platforms that would otherwise be unreachable.
  * Spikes traps will deal damage to the player when on top of them.
  * Lava will instantly kill the player.
* The player can engage in combat with enemies.
  * Three-hit chain combo attack, with the last hit dealing more damage than the first two.
  * Shockwaves that appear from the player sword swings, dealing moderate amounts of damage. Shockwaves will travel in one direction and have a time-to-live.
  * Thunder strikes that deal massive amounts of damage to any unfortunate victim they intersect with.
* Different types of enemies that the player can defeat. Skeletons, minotaurs, goblins, fireworoms and the flying eye boss.
  * Skeletons, the most common type of enemy, perform a two-hit combo attack when close to the player.
  * Minotaurs are beefier than skeletons and deal significantly more damage than skeletons. Minotaurs will attack the plauer by lunging towards the player.
  * Goblins have both melee and ranged attacks. If the player is within throwing range, the goblin will fling a bomb towards the player, exploding after a small delay. If in melee range, the goblin will swing his dagger.
  * Fireworms act as the main ranged enemy of the game, periodically spitting out fireballs when they have sght of the player.
  * The flying eye (the boss of the game), flies around the battlefield shooting laser from his eye and bites if the player gets too comfortable.
* A variety of potion are available to the player that give the player an edge in combat or assist with traversing through terrain.
  * Health potions regenerate the players health.
  * Invincibility potions temporarily make the player invulnerable to damage.
  * Gravity potions flip the players gravity and orientation (anti-gravity).
  * Jump boost potions give the player one-time jump enhancement.
* Checkpoints are located throughout each level but the frequency of checkpoints scales down the higher the difficulty of the game
* The difficulty of the game can be altered from the main menu by selecting the difficulty selection option and then changing the difficulty to the desired option. Altering the difficulty will affect the player’s health and checkpoints in the game. The higher the difficulty, the less health the player has and the less frequent checkpoints appear in each level.
* The player can save the state of the game and continue from wherever they left of from.

## References 
* Animated Pixel Adventurer by rvros: https://rvros.itch.io/animated-pixel-hero
* 2D Pixel Art Minotaur Sprites by Elthen's Pixel Art Shop: https://elthen.itch.io/2d-pixel-art-minotaur-sprites
* Free Pixel Font - Thaleah | 2D Fonts | Unity Asset Store: https://assetstore.unity.com/packages/2d/fonts/free-pixel-font-thaleah-140059
* 2D Pixel Item Asset Pack | 2D Icons | Unity Asset Store: https://assetstore.unity.com/packages/2d/gui/icons/2d-pixel-item-asset-pack-99645
* Pixel Art Platformer - Village Props | 2D Environments | Unity Asset Store: https://assetstore.unity.com/packages/2d/environments/pixel-art-platformer-village-props-166114
* Platformer Set | 2D Environments | Unity Asset Store: https://assetstore.unity.com/packages/2d/environments/platformer-set-150023
* 2D Ice World | 2D Environments | Unity Asset Store: https://assetstore.unity.com/packages/2d/environments/2d-ice-world-106818
* Platformer Fantasy SET1 | 2D Environments | Unity Asset Store: https://assetstore.unity.com/packages/2d/environments/platformer-fantasy-set1-159063
* Monsters Creatures Fantasy by LuizMelo: https://luizmelo.itch.io/monsters-creatures-fantasy
* Fire Worm by LuizMelo: https://luizmelo.itch.io/fire-worm
* Thunder Spell Effect 02 by pimen: https://pimen.itch.io/thunder-spell-effect-02
* Cavernas by Adam Saltsman: https://adamatomic.itch.io/cavernas
* Mossy Cavern by Maaot: https://maaot.itch.io/mossy-cavern
* Mountain Dusk Parallax background by ansimuz: https://ansimuz.itch.io/mountain-dusk-parallax-background
* Action RPG Music Free | Audio Music | Unity Asset Store: https://assetstore.unity.com/packages/audio/music/action-rpg-music-free-85434
* The Hero's Path (Free 16bit Adventure Game Music) | Audio Music | Unity Asset Store: https://assetstore.unity.com/packages/audio/music/the-hero-s-path-free-16bit-adventure-game-music-204232



