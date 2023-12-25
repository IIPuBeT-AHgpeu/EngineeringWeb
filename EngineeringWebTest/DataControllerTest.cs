using Castle.Components.DictionaryAdapter.Xml;
using EngineeringWeb.Controllers;
using EngineeringWeb.Model;
using EngineeringWeb.Services;
using EngineeringWeb.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace EngineeringWebTest
{
    [TestClass]
    public class DataControllerTest
    {
        private Mock<ISimpleLogger> _logger;
        private Mock<ISettingsManager> _settingsManager;
        private Mock<IDataManager> _dataManager;
        private DataController _controller;

        public DataControllerTest()
        {
            _logger = new Mock<ISimpleLogger>();
            _settingsManager = new Mock<ISettingsManager>();
            _dataManager = new Mock<IDataManager>();

            _controller = new DataController(_settingsManager.Object, _dataManager.Object, _logger.Object);

            _logger
                .Setup(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<string>()))
                .Verifiable();

            _settingsManager
                .Setup(x => x.GetSettings())
                .Returns(new Settings() { ModelName = "name", Path = "path", ModelSettings = new ModelSettings { EpochCount = 3, LayersCount = 3 } });

            _settingsManager
                .Setup(x => x.SetSettings(It.IsAny<Settings>()))
                .Verifiable();

            _dataManager.Setup(x => x.SetPath(It.IsAny<string>())).Verifiable();
        }

        [TestMethod]
        public void ClearTest()
        {
            _dataManager.Setup(x => x.Clear(It.IsAny<int>())).Returns(true);

            var result = _controller.Clear(1);

            Assert.AreEqual(typeof(OkResult), result.GetComponentType());
        }

        [TestMethod]
        public void GetCountTest()
        {
            _dataManager.Setup(x => x.CountPhotos(It.IsAny<int>())).Returns(3);

            var result = _controller.GetCount(1);

            Assert.AreEqual(typeof(OkObjectResult), result.GetComponentType());
        }

        [TestMethod]
        public void AddPhotoTest()
        {
            _dataManager.Setup(x => x.SaveData(It.IsAny<IFormFile>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var result = _controller.AddPhoto(It.IsAny<IFormFile>(), 1, "name", "png");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeletePhotoTest()
        {
            _dataManager.Setup(x => x.DeleteData(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);

            var result = _controller.DeletePhoto(1, "name", "format");

            Assert.AreEqual(typeof(OkResult), result.GetComponentType());
        }

        [TestMethod]
        public void IsExistTest()
        {
            _dataManager.Setup(x => x.Exist(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);

            var result = _controller.IsExist(1, "name", "format");

            Assert.AreEqual(typeof(OkObjectResult), result.GetComponentType());
        }
    }
}