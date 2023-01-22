# Zaandam API

## Running API Locally

First, you will need config the `ConnectionStrings`.
Then, open the terminal in the project directory and:

```bash
dotnet restore
dotnet build
dotnet run --project src/Zaandam.Api --urls "http://localhost:5111;https://localhost:5122"
```

## Fun time

Now just access the url `https://localhost:5122/swagger/index.html` and start using =)