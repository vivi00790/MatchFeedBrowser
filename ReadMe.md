# E-Sports Match Tracker

A mini full-stack application for tracking upcoming e-sports matches. Built with **React.js** on the frontend and **C# (.NET Core Web API)** on the backend, with a simulated external API and logging to a database (Azure SQL or local SQL Server).

---

## 📦 Features

- Responsive React UI to display upcoming matches
- .NET Core backend API that fetches match data from a mock external source
- Match API call logging to a SQL database
- Optimized with in-memory caching (5-minute window)
- Clean, modular architecture and code

---

## 🔧 Tech Stack

- **Frontend:** React.js
- **Backend:** C# (.NET 6+ Web API), MemoryCache, Entity Framework Core
- **Database:** SQL Server/SQLite (local setup supported)
- **API Simulation:** Local `matches.json` file

---

## 🚀 Project Setup

You have two options to get started:

### 1. Use the Boilerplate Project (Recommended)

This repository contains a pre-configured boilerplate to help you get started quickly.

- **Backend**:
  - Navigate to the `backend` directory.
  - Run `dotnet run` to start the .NET Core API.

- **Frontend**:
  - Navigate to the `frontend` directory.
  - Run `npm install` to install dependencies.
  - Run `npm start` to start the React development server.

### 2. Create Your Own Project

If you prefer, you can create the project from scratch. Make sure to use the following technologies:

- **Frontend**: React.js
- **Backend**: C# .NET Core Web API