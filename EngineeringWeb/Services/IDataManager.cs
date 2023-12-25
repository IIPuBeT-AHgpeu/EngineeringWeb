namespace EngineeringWeb.Services
{
    public interface IDataManager
    {
        public bool SaveData(IFormFile data, int level, string fileName, string format);
        public bool Exist(string fileName, int level, string format);
        public bool DeleteData(string fileName, int level, string format);
        public int CountPhotos(int level);
        public void SetPath(string path);
        public string GetPath();
        public bool Clear(int level);
    }
}
