namespace EngineeringWeb.Model
{
    public interface IModel
    {
        public bool CreateModel();
        public bool DeleteModel();
        public int UseModel(IFormFile file);
        public bool IsModelExist(string fullPath);
    }
}
