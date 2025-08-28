# 📝Looger 

A simple and flexible logger for text and CSV files.  
It automatically creates log directories and files, appends messages, and supports a fallback mode (saving logs in the application directory if the target location is unavailable).

---

## ✨ Features
- Automatic creation of log files (TXT/CSV)  
- Date-based filenames (`YYYY-MM-DD`)  
- Section frames with `AddFrame`  
- Timestamped messages with `AddMessage`  
- <b>Emergency mode</b> – if the target path is not available, logs are saved in the application’s base directory  

---

## 🛠️ Usage

### 1. Create a logger
```csharp
using Logger;

// default is TXT
var logger = new Logger.Logger(@"C:\Logs", "AppLog");

// explicitly specify CSV format
var loggerCsv = new Logger.Logger(@"C:\Logs", "AppLog", FileType.CSV);

// initialize the log file
logger.InitLog();
```    

### 2. Add log entries

```
logger.AddFrame("INIT LOGGER"); 
// output:
// =================
// == INIT LOGGER ==
// =================
```

```
logger.AddMessage("Application started");
// output: "12:34:56    Application started"
```

---

## 📂 File structure

Logs are stored in the format

```
{DirectoryPath}\{FileName} {YYYY-MM-DD}.{txt|csv}
```

Example:

```C:\Logs\AppLog 2025-08-27.txt```

---

## 📖 Example log output

```
=============================
== INIT LOGGER APPLICATION ==
=============================
12:30:05    Application started
12:30:07    Database connected
12:30:10    User logged in
12:45:22    Application stopped
```

## Authors

[fx2-dklej](https://github.com/fx2-dklej)