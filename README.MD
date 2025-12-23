# Entrevista Fullstack (.NET 8 + React TypeScript + PostgreSQL)

Proyecto para entrevista técnica con:
- **Backend:** API REST en **.NET 8** (arquitectura en capas: API / BLL / DAL / CORE)
- **Frontend:** **React + TypeScript** (Vite)
- **Base de datos:** PostgreSQL
- **Scripts SQL:** en la carpeta `scripts/`

## Estructura

- `backend/` → solución .NET (API + capas)
- `frontend/` → React + TypeScript (Vite)
- `scripts/` → scripts SQL (schema, tabla, procedimientos y permisos)

---

## Requisitos

- .NET SDK 8
- Node.js + npm
- PostgreSQL (pgAdmin opcional)

---

## 1) Base de datos (PostgreSQL)

Los scripts están en `scripts/`. Ejecutar en este orden:

1. **Schema**
   - Crear el schema `aplicacion`

2. **Tabla**
   - Crear la tabla `aplicacion.elementos`

3. **Procedimientos**
   - `aplicacion.elementos_crear(...)`
   - `aplicacion.elementos_listar_cursor()`

4. **Permisos (GRANT)**
   - Permisos para el usuario de aplicación (por ejemplo `usuario_app`)

> Nota: el backend usa `ConnectionStrings:Postgres` en `appsettings.json`.

---

## 2) Backend (.NET 8)

### Visual Studio
1. Abrir la solución en `backend/`
2. Ejecutar el proyecto API

Swagger:
- `https://localhost:7146/swagger` (el puerto puede variar)

### CLI (opcional)
Dentro de `backend/`:
```bash
dotnet restore
dotnet run --project Entrevista

### Frontend

1. Instalar dependencias (node_modules)
npm install

2. Crear archivo .env
VITE_API_URL=https://localhost:7146

3. Ejecutar
npm run dev
