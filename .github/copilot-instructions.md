# Copilot Instructions for retro-configurator

## Project Overview
This is a .NET-based configurator application for building and ordering retro game consoles. The project helps users customize and configure retro gaming hardware.

## Technology Stack
- **Platform**: .NET
- **Language**: C#
- **Build System**: MSBuild / dotnet CLI

## Project Structure
- Root directory contains the main .NET project files
- Build artifacts are excluded via `.gitignore` (bin/, obj/, Debug/, Release/, etc.)

## Development Guidelines

### Code Style
- Follow standard .NET naming conventions
- Use PascalCase for classes, methods, and properties
- Use camelCase for local variables and parameters
- Maintain consistent indentation and formatting

### Building and Testing
- Use `dotnet build` to compile the project
- Use `dotnet test` to run tests (when available)
- Use `dotnet run` to execute the application

### Dependencies
- NuGet packages are managed through standard .NET package management
- Keep dependencies up to date and minimal

### Git Workflow
- Build artifacts (bin/, obj/, etc.) are automatically ignored
- Commit messages should be clear and descriptive
- Follow conventional commit format when possible

## Best Practices
- Write clean, maintainable code
- Add comments for complex logic
- Ensure backward compatibility when making changes
- Test changes locally before committing
- Keep the codebase simple and focused on the configurator functionality
