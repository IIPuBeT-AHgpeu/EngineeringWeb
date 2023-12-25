using EngineeringWeb.Controllers;
using EngineeringWeb.Model;
using EngineeringWeb.Services;
using EngineeringWeb.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Http;

namespace EngineeringWebTest
{
    [TestClass]
    public class ModelControllerTest
    {
        private Mock<ISimpleLogger> _logger;
        private Mock<ISettingsManager> _settingsManager;
        private Mock<IModel> _model;
        private ModelController _controller;

        public ModelControllerTest()
        {
            _logger = new Mock<ISimpleLogger>();
            _settingsManager = new Mock<ISettingsManager>();
            _model = new Mock<IModel>();

            _controller = new ModelController(_settingsManager.Object, _logger.Object);
            _controller.model = _model.Object;

            _logger
                .Setup(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<string>()))
                .Verifiable();
            
            _settingsManager
                .Setup(x => x.GetSettings())
                .Returns(new Settings() { ModelName = "name", Path = "path", ModelSettings = new ModelSettings { EpochCount = 3, LayersCount = 3 } });
            
            _settingsManager
                .Setup(x => x.SetSettings(It.IsAny<Settings>()))
                .Verifiable();
        }

        [TestMethod]
        public void GetModelInfoTest()
        {
            var result = _controller.GetModelInfo();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.EpochCount);
        }

        [TestMethod]
        public void CreateModelTest()
        {
            _model.Setup(x => x.CreateModel()).Returns(true);

            var result = _controller.CreateModel();

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(OkResult), result.GetComponentType());
        }

        [TestMethod]
        public void DeleteModelTest()
        {
            _model.Setup(x => x.DeleteModel()).Returns(true);

            var result = _controller.DeleteModel();

            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(OkResult), result.GetComponentType());
        }

        [TestMethod]
        public void CheckTest()
        {
            _model.Setup(x => x.UseModel(It.IsAny<IFormFile>())).Returns(1);
            _model.Setup(x => x.IsModelExist(It.IsAny<string>())).Returns(true);

            var result = _controller.Check(It.IsAny<IFormFile>());

            Assert.IsNotNull(result);
        }
    }
}