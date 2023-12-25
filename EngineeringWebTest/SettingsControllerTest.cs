using EngineeringWeb.Controllers;
using EngineeringWeb.Services;
using EngineeringWeb.Settings;
using Microsoft.Extensions.Logging;
using Moq;

namespace EngineeringWebTest
{
    [TestClass]
    public class SettingsControllerTest
    {
        private Mock<ISimpleLogger> _loggerMoq;
        private Mock<ISettingsManager> _settingsManagerMock;
        private SettingsController _controller;

        public SettingsControllerTest()
        {
            _loggerMoq = new Mock<ISimpleLogger>();
            _settingsManagerMock = new Mock<ISettingsManager>();

            _loggerMoq.Setup(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<string>())).Verifiable();
            _settingsManagerMock.Setup(x => x.GetSettings()).Returns(new Settings() { ModelName = "name", Path = "path" });
            _settingsManagerMock.Setup(x => x.SetSettings(It.IsAny<Settings>())).Verifiable();

            _controller = new SettingsController(_settingsManagerMock.Object, _loggerMoq.Object);
        }

        [TestMethod]
        public void GetSettingsTest()
        {
            var settings = _controller.GetSettings();

            Assert.IsNotNull(settings);
            Assert.AreEqual("name", settings.ModelName);
        }

        [TestMethod]
        public void SetSettingsTest()
        {
            var sets = new Settings() { ModelName = "name", Path = "path" };

            _controller.SetSettings(sets);

            Assert.AreEqual("name", sets.ModelName);
        }
    }
}