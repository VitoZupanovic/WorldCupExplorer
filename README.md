# World Cup Explorer

A university project containing two C# desktop applications built with
Windows Forms and WPF. Both applications use a shared data layer to
explore men's and women's World Cup data.

## Features

- Load championship data from an API or bundled JSON files
- Switch between men's and women's championships
- Select a favorite team and favorite players
- Manage favorite players using drag and drop in WinForms
- Assign custom player images
- View player rankings and match attendance statistics
- Print rankings from the WinForms application
- Display players on a football field in WPF
- View team and player details
- Configure language and display settings
- Save settings and favorites locally

## Technologies

- C#
- .NET 8
- Windows Forms
- WPF and XAML
- HttpClient
- System.Text.Json

## Project structure

- DataLayer/ — shared models, services, and bundled JSON datasets
- WinForms/ — Windows Forms application
- WPF/ — WPF application
- Images/ — shared image assets

## Requirements

- Windows
- Visual Studio 2022 with the .NET desktop development workload
- .NET 8 SDK

## How to run

1. Clone or download the repository.
2. Open WorldCup.sln in Visual Studio.
3. Set WinForms or WPF as the startup project.
4. Build the solution and press F5.
5. Choose the championship and data source in the application settings.

No database or API credentials are required.

## Data sources

API base address: https://worldcup-vua.nullbit.hr

Bundled JSON datasets are available in DataLayer/Data.
Choose JSON mode to use the application without accessing the API.

API mode requires an internet connection and depends on the external
service being available.

## Local settings

Display preferences, favorites, and player image mappings are saved
locally and excluded from Git.

## Author

Vito Zupanovic