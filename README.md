# ✅ To-Do List

A WPF desktop task management application with multi-user support.

![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![WPF](https://img.shields.io/badge/WPF-Windows-blue.svg)
![C#](https://img.shields.io/badge/C%23-12.0-blue.svg)
![Syncfusion](https://img.shields.io/badge/Syncfusion-UI-orange.svg)
![Grade](https://img.shields.io/badge/Grade-18%2F20-brightgreen.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)

## Features

- **Multi-User** - Multiple accounts on one machine
- **Task Management** - Create, edit, delete tasks
- **Recurring Tasks** - Customizable repetition
- **Priority Levels** - High, Medium, Low, Critical
- **Desktop Alerts** - Deadline notifications
- **Syncfusion UI** - Modern interface components

## Quick Start

### Run Executable
```bash
git clone https://github.com/T-silva15/To-do-List.git
cd To-do-List
./To\ -\ Do\ List.exe
```

### Build from Source
```bash
cd UTAD.ToDoList
dotnet restore
dotnet build --configuration Release
dotnet run --project UTAD.ToDoList.WPF
```

## Project Structure

```
UTAD.ToDoList/
└── UTAD.ToDoList.WPF/
    ├── Models/         # Data models
    │   ├── Tarefa.cs   # Task entity
    │   ├── Perfil.cs   # User profile
    │   └── Alerta.cs   # Notifications
    └── Views/          # XAML views
        ├── ViewLogin.xaml
        ├── ViewNovaTarefa.xaml
        └── ViewPerfil.xaml
```

## Tech Stack

- **.NET 8.0** - Framework
- **WPF** - Desktop UI
- **Syncfusion** - UI components
- **C# 12** - Language

## Requirements

- Windows 10/11
- .NET 8.0 Runtime

## License

MIT License
