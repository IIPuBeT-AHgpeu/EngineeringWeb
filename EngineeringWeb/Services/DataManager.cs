using EngineeringWeb.Settings;

namespace EngineeringWeb.Services
{
    public class DataManager : IDataManager
    {
        private string _path;

        public DataManager()
        {
        }

        public int CountPhotos(int level)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(_path + "\\" + level);
                int count = 0;
                foreach (FileInfo file in di.GetFiles())
                {
                    count++;
                }
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool DeleteData(string fileName, int level, string format)
        {
            string fullName = _path + "\\" + level + "\\" + fileName + "." + format;

            if (_path == null) return false;
            if (!File.Exists(fullName)) return false;
            else
            {
                try
                {
                    File.Delete(fullName);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }              
            }

        }

        public bool Exist(string fileName, int level, string format)
        {
            return File.Exists(_path + "\\" + level + "\\" + fileName + "." + format);
        }

        public bool SaveData(IFormFile data, int level, string fileName, string format)
        {
            string fullName = _path + "\\" + level + "\\" + fileName + "." + format;

            if (_path == null) return false;              
            if (File.Exists(fullName)) return false;

            try
            {
                if (!Directory.Exists(_path + "\\" + level)) Directory.CreateDirectory(_path + "\\" + level);
                using (Stream s = File.Create(fullName))
                {
                    data.CopyTo(s);
                    s.Flush();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }        
        }

        public void SetPath(string path)
        {
            _path = path;
        }

        public string GetPath()
        {
            return _path;
        }

        public bool Clear(int level)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(_path + "\\" + level);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
