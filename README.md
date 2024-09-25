# BoxingGearReview

Boxing Gear Review API
A .NET API for managing and reviewing boxing equipment, including categories like gloves, punching bags, and protective gear.

Features
CRUD Operations: Manage boxing gear, brands, and categories.
Equipment Reviews: Add reviews for different equipment items.
Brand Filtering: Retrieve equipment based on specific brands.
Tested with Xunit: Initial unit tests implemented using Xunit framework.


Getting Started
Prerequisites
To run this project locally, ensure you have:

.NET SDK 6.x or Above
Entity Framework Core
Xunit

Installation
Clone the repository:

#bash#

git clone https://github.com/your-username/boxing-gear-review.git
Navigate to the project directory:

#bash#
Copy Code:
cd boxing-gear-review
Restore NuGet packages:

#bash#
Copy Code:
dotnet restore

Set up an in-memory database for testing (Entity Framework InMemory is being used).

Running the Application
Run the application using:

#bash#
Copy Code:
dotnet run

Running Tests
To execute unit tests (created with Xunit):

#bash#
Copy Code:
dotnet test

API Endpoints
Equipment
GET /api/equipment - Retrieve all equipment.
POST /api/equipment - Add new equipment (DTO required).
GET /api/equipment/{id} - Retrieve equipment by ID.
GET /api/equipment/brand/{brandId} - Retrieve equipment by brand.

Brands & Categories
GET /api/brands - Retrieve all available brands.
GET /api/categories - Retrieve all available categories.

Project Structure

Models: Defines the main entities (Equipment, Brand, Category).

DTOs: Data transfer objects for API requests.

Repository: Contains logic for interacting with the database.

Tests: Xunit tests for validating repository logic and API endpoints.

Testing
The project includes unit tests for repository methods.
Xunit framework is used to ensure functionality such as retrieving equipment by brand or ID.
In-memory database (EF Core) is leveraged for testing purposes without requiring an actual database setup.
Example of a test:

csharp
Copy Code:

[Fact]
public async Task EquipmentRepository_GetEquipment_ReturnsEquipment()
{
    // Arrange
    var dbContext = await GetDatabaseContext();
    var repository = new EquipmentRepository(dbContext);

    // Act
    var result = repository.GetEquipment(1);

    // Assert
    result.Should().NotBeNull();
    result.Should().BeOfType<Equipment>();
}
####Future Improvements####
Expanding unit tests to cover all methods.
Adding integration tests.
Implementing authentication and authorization.

Contributing
Feel free to fork the project, submit issues, or contribute improvements.
