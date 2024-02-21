# Cards Application

This is a RESTful web service named Cards that allows users to create and manage tasks in the form of cards. Users are uniquely identified by their email address, have roles (Member or Admin), and use a password to authenticate themselves before accessing cards.

## Features

- **User Authentication:** Users are authenticated using their email address and password.
- **Roles:** Users can have roles as either Member or Admin.
- **Card Creation:** Users can create a card by providing a name for it, and optionally, a description and a color.
- **Mandatory Fields:** Name field is mandatory for card creation.
- **Color Format:** Color, if provided, should conform to a "6 alphanumeric characters prefixed with a #" format.
- **Card Status:** Upon creation, the status of a card is set to To Do.
- **Search Functionality:** Users can search through cards they have access to using filters such as name, color, status, and date of creation.
- **Pagination and Sorting:** Results can be optionally limited using page & size or offset & limit options, and can be sorted by name, color, status, or date of creation.
- **Single Card Request:** Users can request a single card they have access to.
- **Update Card:** Users can update the name, description, color, and/or status of a card they have access to. Contents of the description and color fields can be cleared out.
- **Available Statuses:** Available statuses for a card are To Do, In Progress, and Done.
- **Delete Card:** Users can delete a card they have access to.

## Technologies Used

- C#
- .NET Framework
- SQL Database 

## Getting Started

To run this application locally, follow these steps:

1. Clone this repository to your local machine.
2. Open the project in your preferred IDE.
3. Set up the SQL database of your choice and configure the database connection in the application settings.
4. Build the project.
5. Run the application.

## Endpoints

- `POST /api/auth/login`: User login endpoint.
- `POST /api/cards`: Create a new card.
- `GET /api/cards`: Get all cards with optional filters, pagination, and sorting.
- `GET /api/cards/{id}`: Get a single card by ID.
- `PUT /api/cards/{id}`: Update a card by ID.
- `DELETE /api/cards/{id}`: Delete a card by ID.

## Authentication

Authentication is required for accessing card-related endpoints. Users need to authenticate using their email address and password.

## Roles

- **Member:** Members have access to cards they created.
- **Admin:** Admins have access to all cards.

## Error Handling

The application handles errors gracefully and returns appropriate error messages and status codes.

## Contributing

Contributions are welcome! If you have any suggestions or improvements, feel free to open an issue or create a pull request.

## License

This project is licensed under the MIT License - see the LICENSE.md file for details.
