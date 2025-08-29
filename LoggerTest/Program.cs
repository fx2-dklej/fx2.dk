using LoggerDK;
using LoggerTest.Models;

TestModel o = new TestModel()
 {
     nazwisko = "Kowalski",
     imie = "Jan",
     integer = 2137,
     adres = new Adres() { kod = "15-399", miejscowosc = "Białystok" }
 };

// ============================================================================//

Logger log = new Logger("E:\\TEST", "logtest");
log.InitLog();

log.AddMessage("Rozpoczęcie logu");
log.AddEmptyLine();

log.AddFrame("Json domyślny");
log.AddObject(o);
log.AddEmptyLine();

log.AddFrame("Json formatowany");
log.AddObject(o, true);


