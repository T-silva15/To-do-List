# ✅ To-Do List - Desktop Task Management Application

[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![WPF](https://img.shields.io/badge/WPF-Windows-blue.svg)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![C#](https://img.shields.io/badge/C%23-12.0-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Syncfusion](https://img.shields.io/badge/Syncfusion-UI%20Components-orange.svg)](https://www.syncfusion.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> **Academic Project** | 2nd Year, 2nd Semester | **Final Grade: 18/20** 🏆

A feature-rich desktop task management application built with WPF and enhanced UI components, designed for efficient personal productivity with multi-user support and intelligent task organization.

## 📋 Table of Contents

- [🎯 Overview](#-overview)
- [✨ Features](#-features)
- [🏗️ Project Structure](#️-project-structure)
- [🚀 Getting Started](#-getting-started)
- [💻 Usage](#-usage)
- [🛠️ Technologies](#️-technologies)
- [📱 User Interface](#-user-interface)
- [🔮 Future Enhancements](#-future-enhancements)
- [📄 License](#-license)

## 🎯 Overview

The To-Do List application is a comprehensive desktop task management solution developed using modern WPF technologies. Built as an academic project, it demonstrates advanced GUI development principles, user experience design, and multi-user functionality within a single application instance.

### Key Highlights
- **WPF Desktop Application**: Native Windows application with modern UI
- **Multi-User Support**: Multiple user accounts on a single machine
- **Syncfusion Integration**: Enhanced UI components for professional appearance
- **Task Intelligence**: Priority-based organization and recurring task support
- **Local Data Management**: Efficient local storage without external dependencies

## ✨ Features

### 👥 User Management
- **Multi-User Accounts**: Support for multiple users on one machine
- **Secure Login System**: User authentication and profile management
- **Personal Workspaces**: Isolated task environments per user
- **Profile Customization**: User-specific settings and preferences

### 📝 Task Management
- **CRUD Operations**: Create, read, update, and delete tasks seamlessly
- **Task Categories**: Organize tasks by custom categories and projects
- **Status Tracking**: Monitor task progress (Pending, In Progress, Completed)
- **Rich Text Support**: Detailed task descriptions with formatting options

### ⏰ Smart Scheduling
- **Recurring Tasks**: Set customizable repetition intervals
- **Due Date Management**: Calendar integration for deadline tracking
- **Alert System**: Desktop notifications for upcoming deadlines
- **Priority Levels**: Categorize tasks by importance (High, Medium, Low, Critical)

### 🎨 User Experience
- **Modern UI**: Syncfusion-powered interface elements
- **Intuitive Navigation**: User-friendly design patterns
- **Visual Indicators**: Color-coded priority and status systems
- **Responsive Layout**: Adaptive interface for different screen sizes

### 🔔 Notification System
- **Desktop Alerts**: System tray notifications for reminders
- **Customizable Reminders**: User-defined notification preferences
- **Deadline Warnings**: Proactive alerts for approaching due dates
- **Meeting Integration**: Calendar-style meeting and appointment support

## 🏗️ Project Structure

```
To-do-List/
├── UTAD.ToDoList/
│   └── UTAD.ToDoList.WPF/           # Main WPF Application
│       ├── App.xaml                 # Application entry point
│       ├── MainWindow.xaml          # Main application window
│       ├── Models/                  # Data models and business logic
│       │   ├── Tarefa.cs           # Task entity model
│       │   ├── Perfil.cs           # User profile model
│       │   ├── Alerta.cs           # Alert/notification model
│       │   ├── Meeting.cs          # Meeting/appointment model
│       │   └── Periodicidade.cs    # Recurrence pattern model
│       ├── Views/                   # XAML view files
│       │   ├── ViewLogin.xaml      # User authentication
│       │   ├── ViewRegisto.xaml    # User registration
│       │   ├── ViewNovaTarefa.xaml # New task creation
│       │   ├── ViewEditarTarefa.xaml # Task editing
│       │   └── ViewPerfil.xaml     # User profile management
│       ├── Images/                  # UI icons and graphics
│       ├── Resources/               # Application resources
│       └── Fonts/                   # Custom typography
├── Mockups/                         # UI/UX design mockups
├── Relatorio To-Do List.pdf        # Project documentation
└── To - Do List.exe                # Compiled application
```

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- **Windows 10/11** (WPF application)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (for development)

### Installation

#### Option 1: Ready-to-Run Executable
1. **Download the executable**
   ```bash
   # Clone the repository
   git clone https://github.com/T-silva15/To-do-List.git
   cd To-do-List
   ```

2. **Run the application**
   - Double-click `To - Do List.exe`
   - Or run from command line: `./To\ -\ Do\ List.exe`

#### Option 2: Build from Source
1. **Clone and navigate**
   ```bash
   git clone https://github.com/T-silva15/To-do-List.git
   cd To-do-List/UTAD.ToDoList
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the application**
   ```bash
   dotnet build --configuration Release
   ```

4. **Run the application**
   ```bash
   dotnet run --project UTAD.ToDoList.WPF
   ```

## 💻 Usage

### 🚀 Getting Started
1. **Launch Application**: Run the executable or start from Visual Studio
2. **Create Account**: Register a new user profile or log in with existing credentials
3. **Main Dashboard**: Access your personal task workspace

### 📝 Task Management Workflow
1. **Create Tasks**: Click "New Task" to add items with descriptions, priorities, and due dates
2. **Set Priorities**: Assign importance levels using the priority system
3. **Configure Alerts**: Set up reminders and notifications for deadlines
4. **Track Progress**: Update task status as you work through your list
5. **Manage Recurrence**: Set up repeating tasks for regular activities

### 👤 User Features
- **Profile Management**: Customize your user settings and preferences
- **Multi-User Support**: Switch between different user accounts seamlessly
- **Data Persistence**: Your tasks and settings are saved locally

## 🛠️ Technologies

### Core Framework
- **.NET 8.0** - Modern cross-platform development framework
- **WPF (Windows Presentation Foundation)** - Rich desktop application framework
- **C# 12.0** - Primary programming language with latest features
- **XAML** - Declarative markup for user interface design

### UI/UX Enhancement
- **Syncfusion WPF Controls** - Professional UI component library
- **Custom Icons & Graphics** - Carefully designed visual elements
- **Modern Typography** - Custom fonts for enhanced readability
- **Material Design Principles** - Contemporary design language

### Development Tools
- **Visual Studio 2022** - Primary IDE with WPF designer
- **Git** - Version control and collaboration
- **NuGet Package Manager** - Dependency management

### Architecture Patterns
- **MVVM (Model-View-ViewModel)** - Clean separation of concerns
- **Event-Driven Architecture** - Responsive user interactions
- **Local Data Storage** - File-based persistence without external databases

## 📱 User Interface

The application features a modern, intuitive interface built with Syncfusion components:

### Visual Design Elements
- **Color-Coded Priorities**: Visual distinction for task importance
- **Icon-Based Navigation**: Intuitive symbols for common actions
- **Responsive Layouts**: Adaptive interface for different window sizes
- **Professional Styling**: Consistent design language throughout

### Key Interface Components
- **Task Dashboard**: Central hub for task overview and management
- **Quick Actions**: Easy-access buttons for common operations
- **Status Indicators**: Visual feedback for task states and progress
- **Notification Center**: Integrated alert and reminder system

## 🔮 Future Enhancements

- [ ] **Cloud Synchronization**: Sync tasks across multiple devices
- [ ] **Mobile Companion App**: Android/iOS applications for task access
- [ ] **Team Collaboration**: Shared projects and task assignment
- [ ] **Advanced Analytics**: Productivity insights and reporting
- [ ] **Integration APIs**: Connect with calendar and productivity tools
- [ ] **Dark Theme**: Alternative UI theme for low-light environments
- [ ] **Voice Commands**: Hands-free task creation and management
- [ ] **Export/Import**: Backup and restore functionality

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

<div align="center">

**To-Do List** - Streamlining personal productivity with modern desktop technology

Made with ❤️ using WPF & Syncfusion | Academic Excellence: 18/20 🏆

</div>  
