Begin building data import utility for data extracted from: 

data.gov

*************************************************************************
* TODOs:                                     
* - figure out a better system to handle the files. Maybe a metadata table
* - Start the frontend to present the data
* - 
* - 
* - 
* - 
*************************************************************************

History:
2025.11.16 - Migrate meta worlds scripts to this project, wired up Winter Achievement in typescript
2025.11.15 - Migrate project to Visual Studio 2026! Make sure the app still works. Continue migrating db stuff into source control folder
2025.11.14 - Add reference to stored proc spCleareTable or whatever it's called, wired it up and checked for the environment variable before clearing before import
2025.11.10 - Developed view getCurrentDayRows or something, tested it and it's good
2025.11.8 - Continue moving db objects into source control Present the data in the ui, Rearchitect backend source code solution; it'll work for now , Stub out the backend, create insert stored proc, db table updates ,fix reference to old folder name in relative path, took raw file dmp out of ui, try to workaround warning messages without tring to optimize the code yet, implement debug instead of output to console.
2025.11.4 - Change readline() to relative path, code cleanup and testing, begin saving data to db
2025.10.30 - Begin import file, continue to test changes, move file import out of Program.cs, db work to create a general schema for all files and metadata
- Add implementation of OpenXml to read/modify .xlsx files, test and debug