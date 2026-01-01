# Copilot instructions for HospitalSystem

Purpose: give AI coding agents the concise, actionable context they need to work productively in this repo.

- **Big picture:** This is a simple console-based .NET app (target: net8.0) with a synchronous, menu-driven UI. `Program.cs` runs a loop that shows `Menus/LoginMenu.cs` then `Menus/MainMenu.cs`. Business logic is split between menu classes (UI) and `Managers` (service-layer helpers).

- **Major components**
  - `Menus/` — console UI; each menu class contains a loop and uses `Utils/Validator.cs` and `Utils/Util.cs` for input and pauses. Example: `LoginMenu.ShowMenu()` prompts, validates, and calls `AuthManager.LogIn()`.
  - `Managers/` — static service helpers. `AuthManager` delegates to `DatabaseManager`; `DatabaseManager` uses `Microsoft.Data.Sqlite` and the connection string in `Common/AppDefs.cs`.
  - `Models/` — POCO types (`User` in `Models/User.cs`) used across UI and managers.
  - `Common/AppDefs.cs` — central constants: `MIN_LENGTH`, `MAX_LENGTH`, `DB_CONNECTION_STRING`, and `UserRole` enum.

- **Data & call flow (concrete example):**
  - Login: `Menus/LoginMenu.cs` -> `Managers/AuthManager.LogIn()` -> `Managers/DatabaseManager.GetUser(userName,password)` -> reads `Users` table and returns `Models.User`.
  - DB lifecycle: `DatabaseManager` opens/closes the single `SqliteConnection` with private `OpenConnection()`/`CloseConnection()` helpers; SQL uses named parameters with `$` (e.g. `$userName`) and `command.Parameters.AddWithValue()`.

- **Project-specific conventions & patterns**
  - Managers are implemented as `static` classes (singleton-like global helpers).
  - Menus are responsible for console loops and direct calls to `Utils.Validator.GetValidString()` to collect input.
  - DB string and enums are centralized in `Common/AppDefs.cs`.
  - SQL uses parameterized commands with `$`-prefixed names.

- **Notable implementation details (useful examples to reference or copy):**
  - Create user: `Managers/DatabaseManager.CreateUser(User user)` — shows parameter binding and `ExecuteNonQuery()` usage.
  - Read user: `Managers/DatabaseManager.GetUser(...)` — shows `ExecuteReader()` pattern and mapping reader columns to `User`.
  - Model constructor: `Models/User.cs` defines `User(int id, string userName, string email, AppDefs.UserRole userRole)` — use this shape when constructing users.

- **Quick build/run**
  - Build: `dotnet build` (run from repo root).
  - Run: `dotnet run --project HospitalSystem.csproj` or `dotnet run` from the project root.

- **Where to add features**
  - New menu actions: update `Menus/MainMenu.cs` switch-case and call into a new `Menus/` class or `Managers/` method.
  - DB schema changes: update `Managers/DatabaseManager.CreateDB()` and migration logic; connection string sits in `Common/AppDefs.cs`.

- **Small gotchas & discoverable bugs (call these out for context)**
  - `Utils/Validator.GetValidString()` uses a while condition with `&&` that prevents correct bounds checking (it references `MIN_LENGTH`/`MAX_LENGTH`), so watch input validation logic when editing.
  - `PatientsMenu` file contains several typos/unfinished branches (class name and switch scaffolding). Expect to open and correct when implementing patient flows.
  - Passwords are stored and compared in plaintext in `Users` table (visible in `DatabaseManager` SQL). Treat accordingly if adding auth/security features.

- **Suggested first tasks for an AI assistant**
  - Add a small unit-test project (optional) or a manual-run debug harness for a new feature.
  - Implement missing `DeleteUser` or finish `PatientsMenu` control flow; use existing `DatabaseManager` patterns.

If any section is unclear or you'd like more examples (e.g., exact SQL parameter usage, sample menu patch), tell me which area to expand.
