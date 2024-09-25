using AutoMapper;
using BoxingGearReview.Controllers.Api;
using BoxingGearReview.Dto;
using BoxingGearReview.Interfaces;
using BoxingGearReview.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingGearReview.Tests.Controller
{
    public class EquipmentApiControllerTests
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public EquipmentApiControllerTests()
        {
            _equipmentRepository = A.Fake<IEquipmentRepository>();
            _brandRepository = A.Fake<IBrandRepository>();
            _categoryRepository = A.Fake<ICategoryRepository>();
            _reviewRepository = A.Fake<IReviewRepository>();
            _mapper = A.Fake<IMapper>();
        }


        [Fact]// -> TEST
      
            public void EquipmentApiController_GetEquipments_ReturnOk()
        {
            // Arrange
            var equipmentDtos = A.Fake<ICollection<EquipmentDto>>();
            var equipmentList = A.Fake < List<EquipmentDto>>();
            A.CallTo(() => _mapper.Map<List<EquipmentDto>>(equipmentDtos)).Returns(equipmentList);
            var controller = new EquipmentApiController(_equipmentRepository, _brandRepository, _categoryRepository, _reviewRepository, _mapper);

            //Act
            var result = controller.GetEquipments();

            //Assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

        }
        

    }
   
}
