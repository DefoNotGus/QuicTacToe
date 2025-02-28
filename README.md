# QuicTacToe - Yes there was a typo!

This is my first C# program also my first time using a .NET as a framework (Winform library) where I am designing a simple modified Tic-Tac-Toe version to play with my friends during Uni breaks!

QuicTacToe is a game where each player can only have their last **three** moves on the board at any given time. If a player places a fourth mark, their oldest mark is removed. The game also includes a **3-second turn timer**, and if a player exceeds the time limit, the opponent wins.

---
>I will detail everything as I am new to C# and this will be my cheatsheet for a while.
## **Built-in C#/.NET Functions Used:**
### **WinForms Framework (UI Elements & Events)**
- `Form` - Defines the main game window.
- `Button` - Used to represent each cell on the board.
- `TextBox` - Allows players to enter their names.
- `Label` - Displays the countdown timer.
- `MessageBox.Show()` - Displays the game result.
- `Application.Run(new GameForm())` - Starts the game loop.
- `Application.Exit()` - Closes the game after it ends.
- `Invoke(new Action(...))` - Ensures UI updates from the timer thread.

### **Timers & Event Handling**
- `System.Timers.Timer` - Manages the turn countdown.
- `ElapsedEventHandler` - Handles time-based events (`UpdateTimer`).
- `Click` Event - Triggers when a board cell or button is clicked.

### **Data Structures Used**
- `Queue<Point>[] playerMoves` - Stores the last three moves of each player.
- `int[,] gameState` - Stores the board state (0 = empty, 1 = Player 1, 2 = Player 2).
- `string[] playerNames` - Holds player names.

---

## **User-Defined Functions**

### **1. `GameForm()` (Constructor)**
- Initializes the game window and UI components.
- Creates the Tic-Tac-Toe board.
- Sets up the game state variables.
- Configures the turn timer.

### **2. `StartGame(object sender, EventArgs e)`**
- Reads player names from the text boxes.
- Enables the board.
- Starts the timer for the first player.

### **3. `InitializeBoard()`**
- Creates a **3x3 grid** of buttons.
- Assigns click event handlers.
- Adds buttons to the form.

### **4. `OnCellClick(object sender, EventArgs e)`**
- Checks if the selected cell is empty.
- Enforces the "last 3 moves" rule by dequeuing the oldest move.
- Updates the game board and `gameState` array.
- Checks if the player has won.
- Switches turns.

### **5. `SwitchPlayer()`**
- Alternates between Player 1 and Player 2.
- Resets the countdown timer.

### **6. `ResetTimer()`**
- Resets the countdown to **4 seconds**.
- Starts the timer.

### **7. `UpdateTimer(object sender, ElapsedEventArgs e)`**
- Decrements the remaining time.
- Updates the timer label with graphical countdown: `💣----🔥 → 💣--🔥 → 💣🔥 → ☠️`
- If time runs out, the other player **wins automatically**.

### **8. `CheckWinCondition()`**
- Checks **rows, columns, and diagonals** for a win condition.

### **9. `EndGame(int winner)`**
- Displays a message declaring the winner.
- Closes the game.

---

## **Variables & Data Structures**

| Variable | Type | Purpose |
|----------|------|---------|
| `board` | `Button[,]` | Holds the 3x3 button grid |
| `gameState` | `int[,]` | Stores board state (0 = empty, 1 = P1, 2 = P2) |
| `currentPlayer` | `int` | Tracks whose turn it is (1 or 2) |
| `playerMoves` | `Queue<Point>[]` | Stores the **last 3 moves** for each player |
| `playerNames` | `string[]` | Stores names of Player 1 and Player 2 |
| `player1TextBox, player2TextBox` | `TextBox` | Input fields for player names |
| `startGameButton` | `Button` | Button to start the game |
| `timerLabel` | `Label` | Displays countdown timer emojis |
| `turnTimer` | `System.Timers.Timer` | Manages the turn countdown |
| `timeLeft` | `int` | Stores remaining seconds before timeout |
| `timerGraphics` | `string[]` | Stores emoji countdown sequence |

---

## **Game Logic & Loops**

### **Game Loop**
1. Players enter their names and click "Start Game".
2. The **current player** clicks an empty cell to mark it.
3. If they exceed **3 marks**, their oldest mark is removed.
4. The game checks for a **win condition**.
5. The turn switches, and a **3-second countdown** starts.
6. If a player runs out of time, the **other player wins**.
7. The game repeats until someone wins.

### **Looping Structures Used**
- **`for` loops** - Used in `InitializeBoard()` to generate the 3x3 grid.
- **`foreach` loop** - Used in `ToggleBoard()` to enable/disable board buttons.
- **`if` conditions** - Used to enforce the 3-move rule, check win conditions, and handle the countdown timer.

---

## **Conclusion**
This was a fun and easy way to understand the similarities and differences with other languages that I have used, like Java, Python, and JavaScript, and honestly, it wasn't scary.  

This project demonstrates:  
✅ **Game logic implementation** (turns, win conditions, move limits).  
✅ **UI handling** Learned to use WinForms (Which remids me of tkinter).  
✅ **Timers & event-driven programming**.  
✅ **Basic AI/logic constraints** (move limit enforcement).  

This game can be further improved by adding:  
- **AI Opponent** for single-player mode.  
- **Better animations** for the countdown timer.  
- **Sound effects**.  



