# Pet Store Relational Database API

A .NET 8 Web API deployed on Azure that manages dogs, cats, and named lists using Azure SQL Database with proper relational structure.

**Swagger Documentation**: [https://petstore-api-bruno2024-gna0bxdjg3dwdphr.centralus-01.azurewebsites.net/index.html](https://petstore-api-bruno2024-gna0bxdjg3dwdphr.centralus-01.azurewebsites.net/index.html)

## Sample Data

The database contains:

- **2 Dogs**: Max (Golden Retriever), Bella (Labrador)
- **2 Cats**: Whiskers (Persian), Luna (Siamese)
- **1 Named List**: "Family Pets" containing Max, Bella, and Whiskers

## 🔧 API Features

- **Dogs API**: Full CRUD operations with traits (breed, age, size, energy level, etc.)
- **Cats API**: Full CRUD operations with traits (breed, age, playfulness, indoor status, etc.)
- **Named Lists API**: Create and manage lists containing subsets of pets
- **Relational Structure**: Foreign key relationships between entities
- **Interactive Documentation**: Test all endpoints via Swagger UI

## 📋 Available Endpoints

- `GET /api/dogs` - Get all dogs
- `GET /api/cats` - Get all cats
- `GET /api/namedlists` - Get all named lists
- `GET /api/namedlists/{id}/dogs` - Get dogs in a specific list
- `GET /api/namedlists/{id}/cats` - Get cats in a specific list
- Full CRUD operations for all entities

## 🛠️ Technology Stack

- **.NET 8** - Web API framework
- **Azure App Service** - Cloud hosting (Free tier)
- **Azure SQL Database** - Relational database (Free tier)
- **Entity Framework Core** - ORM with migrations
- **Swagger/OpenAPI** - API documentation

## API Endpoints

### Dogs

- `GET /api/dogs` - Get all dogs
- `GET /api/dogs/{id}` - Get a specific dog
- `POST /api/dogs` - Create a new dog
- `PUT /api/dogs/{id}` - Update an existing dog
- `DELETE /api/dogs/{id}` - Delete a dog

### Cats

- `GET /api/cats` - Get all cats
- `GET /api/cats/{id}` - Get a specific cat
- `POST /api/cats` - Create a new cat
- `PUT /api/cats/{id}` - Update an existing cat
- `DELETE /api/cats/{id}` - Delete a cat

### Named Lists

- `GET /api/namedlists` - Get all named lists
- `GET /api/namedlists/{id}` - Get a specific named list
- `GET /api/namedlists/{id}/dogs` - Get all dogs in a named list
- `GET /api/namedlists/{id}/cats` - Get all cats in a named list
- `POST /api/namedlists` - Create a new named list
- `PUT /api/namedlists/{id}` - Update an existing named list
- `POST /api/namedlists/{listId}/dogs/{dogId}` - Add a dog to a named list
- `POST /api/namedlists/{listId}/cats/{catId}` - Add a cat to a named list
- `DELETE /api/namedlists/{listId}/dogs/{dogId}` - Remove a dog from a named list
- `DELETE /api/namedlists/{listId}/cats/{catId}` - Remove a cat from a named list
- `DELETE /api/namedlists/{id}` - Delete a named list

## Sample Data

The SQL script creates sample data including:

### Dogs

- Max (Golden Retriever) - Family friendly, high energy
- Bella (Labrador) - Great with kids, very active

### Cats

- Whiskers (Persian) - Calm and fluffy
- Luna (Siamese) - Vocal and playful

### Named Lists

- "Family Friendly Dogs" - Dogs great with children
