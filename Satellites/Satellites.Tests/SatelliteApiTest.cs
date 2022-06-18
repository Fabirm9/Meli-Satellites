using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Satellites.Api.Controllers;
using Satellites.Core.Interfaces;
using Satellites.Core.Responses;
using Satellites.Core.ViewModel;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Satellites.Tests
{
    public class SatelliteApiTest
    {
        private readonly Mock<ISatelliteManager> _satelliteRepository;
        private readonly SatelliteController _controller;
        private readonly Mock<IMapper> _mapper;

        public SatelliteApiTest() 
        {
            _mapper = new Mock<IMapper>();
            _satelliteRepository = new Mock<ISatelliteManager>();
            _controller = new SatelliteController(_satelliteRepository.Object, _mapper.Object); 
            
        }


        [Fact]
        public async void TopSecret_Post_Satellites_Status200()
        {
            //arrange
            var listSatellites = BuildListSatellites();

            //act
            var response = new ResponseSpaceship
            {
                Status = 1,
                ResponseSuccess = true,
                Message= "Success",
                Data = new PositionAndMessage { X = 10, Y = 50, Message = "esto es un mensaje secreto" }
            };
            _satelliteRepository.Setup(x => x.CreateSatellites(listSatellites).Result).Returns(response);

            var result = await _controller.TopSecret(listSatellites) as ObjectResult;

            // assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);

        }

        [Fact]
        public async void TopSecret_Post_Satellites_Status404()
        {
            //arrange
            var listSatellites = BuildListSatellites();

            //act
            var response = new ResponseSpaceship
            {
                Status = 2,
                ResponseSuccess = false,
                Message = "without enough information",
                Data = null
            };
            _satelliteRepository.Setup(x => x.CreateSatellites(listSatellites).Result).Returns(response);

            var result = await _controller.TopSecret(listSatellites) as ObjectResult;

            // assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async void TopSecret_Post_Satellites_Status409()
        {
            //arrange
            var listSatellites = BuildListSatellites();

            //act
            var response = new ResponseSpaceship
            {
                Status = 3,
                ResponseSuccess = false,
                Message = "Object satellites aren't equals",
                Data = null
            };
            _satelliteRepository.Setup(x => x.CreateSatellites(listSatellites).Result).Returns(response);

            var result = await _controller.TopSecret(listSatellites) as ObjectResult;

            // assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Conflict, (HttpStatusCode)result.StatusCode);
        }



        private SatellitesViewModel BuildListSatellites() 
        {
            
            var satellites = new SatellitesViewModel();
            satellites.Satellites = new List<SatelliteViewModel>();
            satellites.Satellites.Add(new SatelliteViewModel { Name = "test", Distance = (float?)100.2, Message = new List<string> { "este", "es", "", "" } });
            satellites.Satellites.Add(new SatelliteViewModel { Name = "tes2", Distance = (float?)200, Message = new List<string> { "", "", "mensaje", "" } });
            satellites.Satellites.Add(new SatelliteViewModel { Name = "tes3", Distance = (float?)300, Message = new List<string> { "", "", "", "secreto" } });

            return satellites;
        }
    }
}
