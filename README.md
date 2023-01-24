# Zaandam API

## Running API with docker infrastructure

```bash
docker-compose up -d
```

## Running API locally

You will need config the `ConnectionStrings`.
Then, open the terminal in the project directory and:

```bash
dotnet restore
dotnet build
dotnet run --project src/Zaandam.Api --urls "http://localhost:5111"
```

## Fun time

Now just access the url `http://localhost:5111/swagger/index.html` and start using =)

---------------------

## Running the tests

```bash
dotnet test
```
