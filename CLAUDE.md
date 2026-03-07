# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build Games/Games.csproj

# Run with hot reload
dotnet watch run --project Games/Games.csproj

# Publish
dotnet publish Games/Games.csproj
```

There are no automated tests in this project.

## Architecture

This is a **Blazor WebAssembly** app (targeting .NET 10) that hosts a collection of browser-based games as a PWA (Progressive Web App with service worker).

### Project layout

```
Games/
  Pages/           # Razor page components (one per game + Index)
  Shared/          # Layout, NavMenu, AlphaKeyboard component
  Services/        # IWordService / WordService
  WordGuess/       # Game logic: Wordle, Round, Letter (enum Location)
  Concentration/   # Game logic: Pexeso, Card
  WordSearch/      # Game logic: Search, Coordinate
  Bagels/          # Game logic: Game
  SliderPuzzle/    # Game logic: Game, Cell
  wwwroot/         # Static assets, word lists (words1-3.json, Wordles.txt), manifest
```

### Patterns

**Separation of concerns:** Each game has a plain C# logic class (no Blazor dependencies) in its own subfolder, and a corresponding Razor page in `Pages/` that owns all UI, state persistence, and event handling.

**State persistence:** Game state is serialized to `Blazored.LocalStorage` after every meaningful action. On load, pages restore from local storage if a save exists (and no new game was requested). `NavMenu` also tracks the last visited page and navigates back to it on first render.

**Shared components:** `AlphaKeyboard.razor` is a reusable keyboard component used by WordGuess (and intended for future word games). It accepts `Func<string, string>` callbacks for coloring and `EventCallback` for letter/delete actions.

**WordService:** Fetches the Wordle word list from `wwwroot/Wordles.txt` via `HttpClient`. Falls back to a hardcoded list if the fetch fails. Registered as a singleton in `Program.cs`.

### Games

| Game | Route | Logic class |
|---|---|---|
| Word Guess (Wordle clone) | `/wordguess` | `WordGuess/Wordle.cs` |
| Word Search | `/wordsearch` | `WordSearch/Search.cs` |
| Concentration (memory matching) | `/concentration` | `Concentration/Pexeso.cs` |
| Bagels (number guessing) | `/bagels` | `Bagels/Game.cs` |
| Slider Puzzle (15-puzzle) | `/sliderpuzzle` | `SliderPuzzle/Game.cs` |

### Word data

- `wwwroot/Wordles.txt` — newline-delimited 5-letter words used by Word Guess
- `wwwroot/words1-3.json` — word lists used by Word Search (loaded via HttpClient)
