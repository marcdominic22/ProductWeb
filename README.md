# ProductWeb
Develop a simple web application using C# and .NET Core that performs CRUD operations on a list of products. 

## Installation

Clone First the project via Github Desktop or Git on Terminal

Then After that Open the Project via VS Code or Visual Studio

In VS Code Change the Directory in Terminal

```bash
  cd \src\Web\
```

comment the `await initialiser.SeedAsync()` in `ApplicationDbContextInitialiser`

Then run 
```bash
  dotnet clean
  dotnet restore
  dotnet ef database update
  dotnet build
```

run the project via clicking `F5` or Go to Run > Click Start Debugging

after that go to the terminal once again and click on the keyboard the `ctrl + C` then change the directory to `ClientApp`

```
cd ..
cd ClientApp
dotnet run
```

then `ctrl` click the localhost 

after that you may register if you don't have a account or login

## API Reference

#### Get all products

```http
  GET /api/products
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `api_key` | `string` | **Required**. Your API key |

#### Get product by Id

```http
  GET /api/products/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |

#### Create product

```http
  POST /api/products
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `data`      | `object` | **Required**. Object of product |
| `data.name`      | `string` | Name of product |
| `data.description`      | `string` |  Description of product |
| `data.supplierName`      | `string` |  Supplier of product |
| `data.price`      | `int` | Price of product |
| `data.tax`      | `double` | Tax of product |

#### Update product by Id

```http
  PUT /api/products/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to update |
| `name`      | `string` | Name of product |
| `description`      | `string` |  Description of product |
| `supplierName`      | `string` |  Supplier of product |
| `price`      | `int` | Price of product |
| `tax`      | `double` | Tax of product |

#### Delete product by Id

```http
  Delete /api/products/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |


## Limitations

*  You can't directly go to the uri of the page, you need to login first to access  the api
* The Authorization of the client App is not yet fully setup
* This is still buggy

  
## Tech Stack

**Client:** Bootstrap, Razor

**Server:** C#, Dotnet 8

