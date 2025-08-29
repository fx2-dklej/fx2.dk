using System.Reflection.Emit;
using System.Text.Json;

namespace LoggerDK
{
    public class Logger
    {
        public string DirectoryPath { get; set; }
        public string FileName { get; set; }
        public FileType Type { get; set; }
        public string FullPath { get => _path; }
        public string EmergencyPath { get => _emergencyPath; }
        private string _path;
        private string _emergencyPath;
        private string _dateStr;

        /// <summary>
        /// .txt by default
        /// </summary>
        public Logger(string directoryPath, string fileName)
        {
            DirectoryPath = directoryPath;
            FileName = fileName;
            Type = FileType.TXT;
        }

        public Logger(string directoryPath, string fileName, FileType filetype)
        {
            DirectoryPath = directoryPath;
            FileName = fileName;
            Type = filetype;
        }

        /// <summary>
        /// Initializes the path with directories and log file
        /// </summary>
        /// <returns>True or exception message if location is unavailable</returns>
        public bool InitLog()
        {
            UpdatePath();
            try
            {
                if (!File.Exists(_path))
                    File.Create(_path).Close();
            }
            catch (Exception e) { throw new Exception(e.Message); }

            return true;
        }

        /// <summary>
        /// Add frame with custom message to log file
        /// </summary>
        public void AddFrame(string title)
        {
            UpdatePath();
            string titleLine = $"============ {title} ============";
            string border = new string('=', titleLine.Length);
            try
            {
                using (StreamWriter writer = new StreamWriter(_path, true, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine(border);
                    writer.WriteLine(titleLine);
                    //writer.WriteLine(border);
                }
            }
            catch { AddEmergencyMessage(title); }
        }

        /// <summary>
        /// Add custom message to log file
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(string message)
        {
            UpdatePath();
            try
            {
                using (StreamWriter writer = new StreamWriter(_path, true, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine(string.Format("{0,-15}", TimeNow()) + message);
                }
            }
            catch { AddEmergencyMessage(message); }
        }

        /// <summary>
        /// Outputs the object to the log in JSON format. You can choose JSON styling (single line or natively formatted).
        /// </summary>
        public void AddObject<T>(T obj, bool prettyStyle = false)
        {
            UpdatePath();
            string json = string.Empty;
            try
            {
                var option = new JsonSerializerOptions { WriteIndented = prettyStyle, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
                json = JsonSerializer.Serialize(obj, option);
                using (StreamWriter writer = new StreamWriter(_path, true, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine(string.Format("{0,-15}", TimeNow()) + json);
                }
            }
            catch { AddEmergencyMessage(json); }
        }

        /// <summary>
        /// Add empty line to log file
        /// </summary>
        public void AddEmptyLine()
        {
            UpdatePath();
            try
            {
                using (StreamWriter writer = new StreamWriter(_path, true, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("");
                }
            }
            catch { }
        }

        private void AddEmergencyMessage(string message)
        {
            try
            {
                UpdatePath();
                try
                {
                    using (StreamWriter writer = new StreamWriter(_emergencyPath, true, System.Text.Encoding.UTF8))
                    {
                        writer.WriteLine(string.Format("{0,-15}", TimeNow()) + message);
                    }
                }
                catch { }
            }
            catch { throw new Exception("Failed to save message to designated location and alternate location"); }
        }

        private string TimeNow()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        private void UpdatePath()
        {
            _dateStr = DateTime.Today.ToString("yyyy-MM-dd");
            _path = Path.Combine(DirectoryPath, $"{FileName} {_dateStr}.{Type.ToString().ToLower()}");
            _emergencyPath = Path.Combine(AppContext.BaseDirectory, $"{FileName} {_dateStr}.{Type.ToString().ToLower()}");
        }
    }
}
