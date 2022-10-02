To open, use Obsidian or another program which supports Mermaid class diagram syntax: https://mermaid-js.github.io/mermaid/#/classDiagram
```mermaid
classDiagram
class GameManager{
	+GameObject Player
	+List~GameObject~ Maps	
}
class Map { }
Map --> GameManager
class Artstyle { }
class Player { }
Player --> GameManager
Player --> Artstyle
class Weapon {
	+Attack()* void
}
Weapon --> Artstyle
class Gun { }
Gun --|> Weapon
class Projectile { }
Projectile --> Gun
class Enemy { }
Enemy --> Map
Enemy --> Artstyle
Map --> Artstyle
```