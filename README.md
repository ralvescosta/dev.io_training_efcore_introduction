# Introdução ao EF Core

### Tools

- Install CLI

```bash
dotnet tool install --global dotnet-ef
```

- Create Migration

```bash
dotnet ef migrations add NOME_DA_MIGRATION -p EFCore/EFCore.csproj
```

- SQL Script

```bash
dotnet ef migrations script -p EFCore/EFCore.csproj -o EFCore/PrimeiraMigracao.SQL -i
```

- Run Migration

```bash
dotnet ef database update -p EFCore/EFCore.csproj -v
```

