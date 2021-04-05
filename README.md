# Introdução ao EF Core

### Tools

```bash
dotnet tool install --global dotnet-ef
```

- SQL Script

```bash
dotnet ef migrations script -p EFCore/EFCore.csproj -o EFCore/PrimeiraMigracao.SQL
```

- Run Migration

```bash
dotnet ef database update -p EFCore/EFCore.csproj -v
```
