# PV179-BookHub



## Overview

BookHub is a web application designed to manage books, authors, publishers, and user ratings. It provides a comprehensive platform for users to interact with a wide range of books, leave ratings, and comments. The application is built using ASP.NET Core and Entity Framework Core.

## Features

- Book Management: Allows users to view, add, update, and delete books. Each book has properties like name, price, stock in storage, overall rating, genres, publisher, and authors.
Author and Publisher Management: Allows users to view, add, update, and delete authors and publishers.


- Rating Management: Users can rate books and leave comments. The rating value and comments can be updated.


- Request Logging: All requests to the application are logged with details like the request method and path. The logs are stored in a text file.

## Setup and Installation

Clone the repository to your local machine.
Navigate to the project directory.
Install the required dependencies using the command dotnet restore.
Build the project using the command dotnet build.
Run the project using the command dotnet run.

```
cd project_directory
git remote add origin https://gitlab.fi.muni.cz/xmarianc/pv179-bookhub.git
git branch -M main
git push -uf origin main
```

## Usage
The application exposes several endpoints for interacting with the books, authors, publishers, and ratings. Here are some examples:
- GET /Books: Fetches a list of all books.
- POST /Books: Adds a new book.
- PUT /UpdateBook/{id}: Updates the details of a book with the given ID.
- DELETE /Books/{id}: Deletes the book with the given ID.

Similar endpoints are available for authors, publishers, and ratings.

***