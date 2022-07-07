# ABC_Banking

## Description
ABC Bank wants to implement a new system to manage a list of contacts with personal data
such as name, addresses, phone numbers and photos for their internal needs.
You are required to write a small .NET application with the following characteristics:
* .NET Framework 4.7+, C#
* Use of entity framework
* Use of SQLite for storage
* Handling of the following information:
First name, Second name
Addresses
Date of birth
Phone numbers
Personal photo
* The .NET application should expose REST services for CRUD operations on the
cited entities.
* The .NET application should also expose some service for retrieving massively
the data based on the following optional criteria:
Free search on Names and Addresses
Range of age (from/to)
To complete the programming exercise you are also required to write a small Front-End
application with the following characteristics:
* Static simple HTML page and Javascript
* Mock to invoke all services and display results

## Annotations
* Cors
	Enabled Cors for all Origins (*), since i do not know which port you will be using.
* Database
	Thought about tables for Person (Firstname & Secondname), Address (Country, City, Streetname, Housenumber) and ContactInfo (all together)
* Certification Problems
	I had an issue with Chrome. If it should occur, just type: chrome://flags/      and check: Allow invalid certificates for resources loaded from localhost
* Exception Handling
	is discreetly present and definitely needs improvement.
* Other thoughts during the exercise:
	How to use proper Logging.
	How to use proper Dependency Injection.
	Am I allowed to use third party packages? (since not mentioned in the instruction, I dropped it)
	EF6 with SQLite (version for .Net framework 4.7) not able to add Database and Tables at runtime if they do not exist
	
Explanation of why Phonenumbers and Housenumbers are strings too.  => 
Phonenumbers can be written differently depending on the country (examples: austrian "+43 660/1071254" or american format "+1-202-555-0111") (no specification in the instruction)
Housenumbers can range from "34" to "Door 2, Staircase 2, Top 10" (no specification in the instruction)
For the profile picture only .png is allowed, since every image format has to be handled in html <img> tag differently (no specification in the instruction)

Server-wise:
DateOfBirth validation is included. Differenciation between various date formats (examples:  DD/MM/YYYY ; MM/DD/YYYY)  is not included.
Phonenumber validation is not included.
Image validation is not included. 

## Setup
Run backend only in Visual Studio (IIS) -> if you need to build the application, make sure to include the ./AppData/ABC.sqlite file in the root directory of the built executable.
Run frontend with Visual Studio Code (Live Server) or terminal (node index.js) -> might need to change the URL in the GetURL function of ./public/main.js

Searching for names and addresses is a static search (no fuzzy search)
Searching for an age range - pattern: min,max  (example to find age range between 20 and 30 = 20,30)
