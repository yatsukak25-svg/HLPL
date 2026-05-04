# TutorFlow

ASP.NET Core MVC-РїСЂРѕС”РєС‚ РґР»СЏ РѕСЂРіР°РЅС–Р·Р°С†С–С— Р·Р°РЅСЏС‚СЊ РјС–Р¶ СЂРµРїРµС‚РёС‚РѕСЂРѕРј С‚Р° СѓС‡РЅСЏРјРё.

## Р©Рѕ СЂРµР°Р»С–Р·РѕРІР°РЅРѕ

- СЂРѕР»С– `Admin`, `Tutor`, `Student`
- `Identity` + `SQLite` + `Entity Framework Core`
- С€Р°СЂРё `Web`, `Application`, `Domain`, `Infrastructure`
- РѕРєСЂРµРјР° Р±С–Р±Р»С–РѕС‚РµРєР° `TutorFlow.AccountingLibrary`
- CRUD РґР»СЏ `Subjects`, `Students`, `Lessons`, `Homework`, `Payments`
- async-РјРµС‚РѕРґРё Сѓ РєРѕРЅС‚СЂРѕР»РµСЂР°С… С– СЃРµСЂРІС–СЃР°С…
- `Generic Repository Pattern`
- DTO + mapping
- specifications РґР»СЏ Р·Р°РЅСЏС‚СЊ С– Р±РѕСЂРіС–РІ
- С„С–Р»СЊС‚СЂР°С†С–СЏ С‚Р° РїР°РіС–РЅР°С†С–СЏ СЃРїРёСЃРєСѓ Р·Р°РЅСЏС‚СЊ
- РІР°Р»С–РґР°С†С–СЏ СѓРєСЂР°С—РЅСЃСЊРєРѕРіРѕ РЅРѕРјРµСЂР° С‚РµР»РµС„РѕРЅСѓ
- session-С„СѓРЅРєС†С–РѕРЅР°Р» РґР»СЏ РѕР±СЂР°РЅРёС… РЅР°РІС‡Р°Р»СЊРЅРёС… РјР°С‚РµСЂС–Р°Р»С–РІ
- partial view РґР»СЏ СЃРїРёСЃРєСѓ Р·Р°РЅСЏС‚СЊ
- С–РЅС‚РµРіСЂР°С†С–СЏ Р· API СЃРІСЏС‚ `Nager.Date`
- seed-РґР°РЅС– РґР»СЏ РґРµРјРѕРЅСЃС‚СЂР°С†С–С—
- EF Core migration `InitialCreate`

## РўРµСЃС‚РѕРІС– Р°РєР°СѓРЅС‚Рё

- `admin@hlpl.local` / `Admin123!`
- `tutor@hlpl.local` / `Tutor123!`
- `student1@hlpl.local` / `Student123!`
- `student2@hlpl.local` / `Student123!`

## Р—Р°РїСѓСЃРє

```powershell
dotnet restore
dotnet build /m:1
dotnet run --project .\TutorFlow.csproj
```

РџС–Рґ С‡Р°СЃ СЃС‚Р°СЂС‚Сѓ Р·Р°СЃС‚РѕСЃСѓРЅРѕРє:

- Р·Р°СЃС‚РѕСЃСѓС” РјС–РіСЂР°С†С–С— РґРѕ `app.db`
- СЃС‚РІРѕСЂРёС‚СЊ СЂРѕР»С–
- СЃС‚РІРѕСЂРёС‚СЊ С‚РµСЃС‚РѕРІРёС… РєРѕСЂРёСЃС‚СѓРІР°С‡С–РІ
- Р·Р°РїРѕРІРЅРёС‚СЊ Р±Р°Р·Сѓ РґРµРјРѕРЅСЃС‚СЂР°С†С–Р№РЅРёРјРё РґР°РЅРёРјРё

## РЎС‚СЂСѓРєС‚СѓСЂР°

- `TutorFlow` - Web
- `TutorFlow.Application` - DTO, interfaces, services, specifications, mappings
- `TutorFlow.Domain` - entities, enums, validation
- `TutorFlow.Infrastructure` - `DbContext`, repositories, seed, DI
- `TutorFlow.AccountingLibrary` - СЂРѕР·СЂР°С…СѓРЅРєРё РѕРїР»Р°С‚

## РќРѕС‚Р°С‚РєР°

РЈ С†СЊРѕРјСѓ СЃРµСЂРµРґРѕРІРёС‰С– СЃС‚Р°Р±С–Р»СЊРЅР° Р·Р±С–СЂРєР° РїСЂРѕС…РѕРґРёР»Р° С‡РµСЂРµР· `dotnet build /m:1`, С‚РѕРјСѓ С†РµР№ РїР°СЂР°РјРµС‚СЂ Р·Р°Р»РёС€РµРЅРѕ РІ С–РЅСЃС‚СЂСѓРєС†С–С— Р·Р°РїСѓСЃРєСѓ.

