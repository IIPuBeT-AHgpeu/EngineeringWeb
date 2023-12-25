using EngineeringWeb.Settings;

namespace EngineeringWeb.Model
{
    public class Model : IModel
    {
        ISettingsManager _manager;

        public Model(ISettingsManager manager)
        {
            _manager = manager;
        }
        public bool CreateModel()
        {
            var settings = _manager.GetSettings();

            if (settings == null) return false;
            else
            {
                if (!File.Exists(settings.Path + "\\" + settings.ModelName))
                {
                    //обучение
                    object model = "it's educated model.";

                    try
                    {
                        File.WriteAllText(settings.Path + "\\" + settings.ModelName, (string)model);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }                  
                }
                else return false;
            }
        }

        public bool DeleteModel()
        {
            var settings = _manager.GetSettings();

            if (settings == null) return false;
            else
            {
                if (File.Exists(settings.Path + "\\" + settings.ModelName))
                {
                    try
                    {
                        File.Delete(settings.Path + "\\" + settings.ModelName);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else return false;
            }
        }

        public bool IsModelExist(string fullPath)
        {
            return File.Exists(fullPath);
        }

        public int UseModel(IFormFile file)
        {
            Random rnd = new Random();
            return rnd.Next();
        }
    }
}
