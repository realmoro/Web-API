# Building a CRUD Web API with ASP.NET Core (.NET 9)

By the end of this tutorial, you’ll have a working **ASP.NET Core Web API** that supports full **CRUD** (**Create, Read, Update, Delete**) operations.

---

## 🔧 Prerequisites

* **.NET 9 SDK (Preview)**
* **Visual Studio 2022+** or **VS Code**
* Basic knowledge of **C#** and **REST** concepts

---

## 🚀 Step 1: Create the ASP.NET Core Web API Project

In **.NET 9**, the default `webapi` template is minimal and does not include Swagger or controllers by default.

Create a clean project using the CLI:

```bash
dotnet new webapi -n ProductApi --use-controller
cd ProductApi
```

> 💡 The `--use-controller` flag enables the traditional controller-based approach, which is easier for beginners.

---

## 🧩 Step 2: Add Swagger Manually (if missing)

### 1️⃣ Add the Swagger NuGet package

```bash
dotnet add package Swashbuckle.AspNetCore
```

### 2️⃣ Enable Swagger in `Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
```

---

## 📁 Step 3: Understand the Folder Structure

* `Controllers/WeatherForecastController.cs` – sample controller (can be deleted)
* `Program.cs` – application startup logic
* `appsettings.json` – application configuration

Next, we’ll create our **model** and **controller**.

---

## 🧱 Step 4: Create the Product Model

Create a new folder named `Models`, then add:

**`Models/Product.cs`**

```csharp
namespace ProductApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

---

## 🧠 Step 5: Set Up a Product Controller

Create a controller in the `Controllers` folder:

**`Controllers/ProductController.cs`**

```csharp
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1200 },
            new Product { Id = 2, Name = "Phone", Price = 800 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> Post(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            products.Remove(product);
            return NoContent();
        }
    }
}
```

---

## 🧪 Step 6: Test the API Using Swagger

Run the application:

```bash
dotnet run
```

Open your browser and navigate to:

```
https://localhost:{port}/swagger
```

You’ll see **Swagger UI** with all available API endpoints. You can test:

* `GET` – retrieve products
* `POST` – create a new product
* `PUT` – update an existing product
* `DELETE` – remove a product

---

## ✅ What’s Next?

In future articles, we’ll cover:

* 🔐 Securing your Web API with **JWT Authentication**
* 🧪 Writing **unit tests** with xUnit and Moq
* 💡 Implementing **API versioning** and **caching**
* 🏗 Structuring APIs with **Clean Architecture**

---

## 📌 Final Thoughts

Creating a basic RESTful API in **ASP.NET Core (.NET 9)** is straightforward, even with the minimal template approach. This tutorial gives you a solid foundation for building clean, testable APIs using controllers, while still allowing you to adopt newer platform features over time.

If you enjoyed this guide, follow for more **practical ASP.NET Core content** — from beginner to expert 🚀
