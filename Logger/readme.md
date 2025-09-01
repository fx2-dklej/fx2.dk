# 📝Looger 

Proste i elastyczne narzędzie do zapisywania logów w formie plików tekstowych.
Automatycznie tworzy katalogi i inicjuje pliki logów. Obsługuje tryb awaryjny (zapisuje logi w katalogu aplikacji, jeśli lokalizcja docelowa jest niedostępna z poziomu aplikacji). 

---

## 📌 Właściwości narzędzia
- Automatyczne tworzenie logów (TXT/CSV) 
- Kodowanie UTF-8 
- Oddzielenie sekcji w logu `AddFrame`  
- Datowanie wiadomości `AddMessage`  
- Obsługa wypisywania do pliku dowolnych obiektów w formacie JSON (wymagane publiczne właściwości) `AddObject`  
- <b>Tryb bezpieczeństwa</b> – jeśli ścieżka docelowa nie jest dostępna, logi są zapisywane w katalogu bazowym aplikacji `EmergencyPath`

---

## 🛠️ Użycie

### 1. Tworzenie logu
```csharp
using LoggerDK;

// domyślnie .txt
var logger = new Logger.Logger(@"C:\Logs", "AppLog");

// wybranie alternatywnego formatu .csv
var loggerCsv = new Logger.Logger(@"C:\Logs", "AppLog", FileType.CSV);

// inicjalizacja folderu i pliku logu
logger.InitLog();
```    

### 2. Przykład użycia i rezultaty

---

```csharp
logger.AddFrame("INIT LOGGER"); 
```

`output:`

```txt
// =========================
// ====== INIT LOGGER ======
```

---

```csharp
logger.AddMessage("Application started");
```

`output:`

```txt
12:34:56    Application started
```

---

```csharp
logger.AddObject(obj);
```

`output:`

```txt
12:34:56    {"integer":2137,"imie":"Jan","nazwisko":"Kowalski","adres":{"kod":"15-399","miejscowosc":"Białystok"}}
```

---

Pretty style ☕
```csharp
logger.AddObject(obj, true);
```

`output:`

```
12:54:19       {
  "integer": 2137,
  "imie": "Jan",
  "nazwisko": "Kowalski",
  "adres": {
    "kod": "15-399",
    "miejscowosc": "Białystok"
  }
}
```

---

## 📂 Struktura nazwy pliku

Pliki przechowywane są w formacie:

`{DirectoryPath}\{FileName} {YYYY-MM-DD}.{txt|csv}`

Przykład:

`C:\Logs\AppLog 2025-08-27.txt`

---


## Autorzy

[fx2-dklej](https://github.com/fx2-dklej)
