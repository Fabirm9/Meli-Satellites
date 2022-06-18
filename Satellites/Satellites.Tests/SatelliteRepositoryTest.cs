using Moq;
using Satellites.Core.Interfaces;
using Satellites.Core.Responses;
using Satellites.Core.Services;
using Satellites.Core.ViewModel;
using System.Collections.Generic;
using Xunit;

namespace Satellites.Tests
{
    public class SatelliteRepositoryTest
    {

        private readonly Mock<ISatelliteRepository> _satelliteRepository;
        private readonly SatelliteManager _satelliteManager;

        public SatelliteRepositoryTest()
        {
            _satelliteRepository = new Mock<ISatelliteRepository>();
            _satelliteManager = new SatelliteManager(_satelliteRepository.Object);
        }


        [Fact]
        public async void Create_Satellites_With_Success_Message_Position() 
        {
            //arrange
            var listSatellites = BuildListSatellites();

            //act
            var service = await _satelliteManager.CreateSatellites(listSatellites);
            var dataServices = service.Data as PositionAndMessage;

            //assert
            Assert.True(service.ResponseSuccess);
            Assert.Equal(1,service.Status);
            Assert.Equal("succcess",service.Message);
            Assert.Equal(297.216461,dataServices.X,4);
            Assert.Equal(-237.898743, dataServices.Y,4);
            Assert.Equal("este es un mensaje secreto", dataServices.Message);
        }

        [Fact]
        public async void Create_Satellites_With_Success_But_Without_Determine_Message_Position()
        {
            //arrange
            var listSatellites = BuildListSatellitesMessageWithOutDetermine();

            //act
            var service = await _satelliteManager.CreateSatellites(listSatellites);
            
            //assert
            Assert.False(service.ResponseSuccess);
            Assert.Equal(2, service.Status);
            Assert.Equal("without enough information", service.Message);
        }

        [Fact]
        public async void Try_Create_Satellites_With_List_No_Equal_To_Dictonary() 
        {
            //arrange
            var listSatellites = BuildListSatellitesNoEqualsToDictonary();

            //act
            var service = await _satelliteManager.CreateSatellites(listSatellites);
            
            //assert
            Assert.False(service.ResponseSuccess);
            Assert.Equal(3, service.Status);
            Assert.Equal("Object satellites aren't equals", service.Message);
        }

        private SatellitesViewModel BuildListSatellites()
        {
            var satellites = new SatellitesViewModel();
            satellites.Satellites = new List<SatelliteViewModel>();
            satellites.Satellites.Add(new SatelliteViewModel { Name = "kenobi", Distance = (float?)100.0, Message = new List<string> { "este", "", "", "mensaje", "" } });
            satellites.Satellites.Add(new SatelliteViewModel { Name = "skywalker", Distance = (float?)115.5, Message = new List<string> { "", "es", "un", "", "secreto" } });
            satellites.Satellites.Add(new SatelliteViewModel { Name = "sato", Distance = (float?)142.7, Message = new List<string> { "", "es", "", "", "secreto" } });

            return satellites;
        }

        private SatellitesViewModel BuildListSatellitesNoEqualsToDictonary()
        {
            var satellites = new SatellitesViewModel();
            satellites.Satellites = new List<SatelliteViewModel>();
            satellites.Satellites.Add(new SatelliteViewModel { Name = "terpel", Distance = (float?)100.0, Message = new List<string> { "este", "", "", "mensaje", "" } });
            satellites.Satellites.Add(new SatelliteViewModel { Name = "skywalker", Distance = (float?)115.5, Message = new List<string> { "", "es", "un", "", "secreto" } });
            satellites.Satellites.Add(new SatelliteViewModel { Name = "sato", Distance = (float?)142.7, Message = new List<string> { "", "es", "", "", "secreto" } });

            return satellites;
        }

        private SatellitesViewModel BuildListSatellitesMessageWithOutDetermine()
        {
            var satellites = new SatellitesViewModel();
            satellites.Satellites = new List<SatelliteViewModel>();
            satellites.Satellites.Add(new SatelliteViewModel { Name = "kenobi", Distance = (float?)100.0, Message = new List<string> { "este", "", "", "lola", "" } });
            satellites.Satellites.Add(new SatelliteViewModel { Name = "skywalker", Distance = (float?)115.5, Message = new List<string> { "", "es", "un", "", "secreto" } });
            satellites.Satellites.Add(new SatelliteViewModel { Name = "sato", Distance = (float?)142.7, Message = new List<string> { "", "", "", "", "vaca" } });

            return satellites;
        }
    }
}
