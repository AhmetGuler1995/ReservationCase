using System.Linq.Expressions;
using Moq;
using Reservation.Core.Repositories.Abstract;
using Reservation.Core.Services.Concrete;
using Reservation.Domain.Entities;
using Reservation.Domain.Models.Infrastructure.Email;
using Reservation.Domain.Models.RequestDtos.Reservation;
using Reservation.Domain.Models.RequestDtos.Table;
using Reservation.Infrastructure.Email.Abstract;

namespace Reservation.Core.Test
{
    public class ReservationServiceTest
    {
        private readonly Mock<IReservationRepository> _reservationRepository;
        private readonly Mock<ITableRepository> _tableRepository;
        private readonly ReservationService _reservationService;
        private readonly Mock<ICommonRepository> _commonRepository;
        private readonly Mock<IEmailSender> _emailSender;
        public ReservationServiceTest()
        {
            _commonRepository = new Mock<ICommonRepository>();
            _reservationRepository = new Mock<IReservationRepository>();
            _tableRepository = new Mock<ITableRepository>();
            _emailSender = new Mock<IEmailSender>();
            var tableService = new TableService(_commonRepository.Object, _tableRepository.Object);
            _reservationService = new ReservationService(_commonRepository.Object, _reservationRepository.Object, tableService, _emailSender.Object);
        }

        #region SaveReservation Method Test Methods
        [Fact]
        public void SaveReservation_SaveReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestSaveReservation = new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1
            };
            //Act

            _reservationRepository.Setup(x => x.Save(It.IsAny<Domain.Entities.Reservation>()));
            _emailSender.Setup(x => x.Send(It.IsAny<EmailSenderDto>())).Returns(true);
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var saveReservationResponse = _reservationService.SaveReservation(requestSaveReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.True(saveReservationResponse.IsOk);
        }
        [Fact]
        public void SaveReservation_NullRequestModel_ReturnEqualFalse()
        {
            //Arrange

            SaveReservationRequestDto requestSaveReservation = null;
            //Act

            _reservationRepository.Setup(x => x.Save(It.IsAny<Domain.Entities.Reservation>()));
            _emailSender.Setup(x => x.Send(It.IsAny<EmailSenderDto>())).Returns(true);
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var saveReservationResponse = _reservationService.SaveReservation(requestSaveReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.False(saveReservationResponse.IsOk);
        }
        [Fact]
        public void SaveReservation_SaveReservationRequestDtoZeroNumberOfGuests_ReturnEqualFalse()
        {
            //Arrange

            var requestSaveReservation = new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests =0
            };
            //Act

            _reservationRepository.Setup(x => x.Save(It.IsAny<Domain.Entities.Reservation>()));
            _emailSender.Setup(x => x.Send(It.IsAny<EmailSenderDto>())).Returns(true);
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var saveReservationResponse = _reservationService.SaveReservation(requestSaveReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.False(saveReservationResponse.IsOk);
        }
        [Fact]
        public void SaveReservation_SaveReservationRequestDtoNullCustomerName_ReturnEqualFalse()
        {
            //Arrange

            var requestSaveReservation = new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName =null,
                NumberOfGuests = 10
            };
            //Act

            _reservationRepository.Setup(x => x.Save(It.IsAny<Domain.Entities.Reservation>()));
            _emailSender.Setup(x => x.Send(It.IsAny<EmailSenderDto>())).Returns(true);
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var saveReservationResponse = _reservationService.SaveReservation(requestSaveReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.False(saveReservationResponse.IsOk);
        }
        [Fact]
        public void SaveReservation_SaveReservationRequestDtoNullReservationDate_ReturnEqualFalse()
        {
            //Arrange

            var requestSaveReservation = new SaveReservationRequestDto()
            {
                ReservationDate = null,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 10
            };
            //Act

            _reservationRepository.Setup(x => x.Save(It.IsAny<Domain.Entities.Reservation>()));
            _emailSender.Setup(x => x.Send(It.IsAny<EmailSenderDto>())).Returns(true);
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var saveReservationResponse = _reservationService.SaveReservation(requestSaveReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.False(saveReservationResponse.IsOk);
        }
        [Fact]
        public void SaveReservation_SaveReservationRequestDtoNotContainTable_ReturnEqualFalse()
        {
            //Arrange

            var requestSaveReservation = new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 10
            };
            //Act

            _reservationRepository.Setup(x => x.Save(It.IsAny<Domain.Entities.Reservation>()));
            _emailSender.Setup(x => x.Send(It.IsAny<EmailSenderDto>())).Returns(true);
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>()));
            var saveReservationResponse = _reservationService.SaveReservation(requestSaveReservation);
            _reservationService.SaveChanges();
          
            //Assert
            Assert.False(saveReservationResponse.IsOk);
        }
        #endregion

        #region UpdateReservation Method Test Methods
        [Fact]
        public void UpdateReservation_UpdateReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestSaveReservation = new UpdateReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,
                Id = 1
            };
            //Act

            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _reservationRepository.Setup(x => x.GetById(1)).Returns(new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                Cancel = false,
                CreateDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,
                
            });
            _tableRepository.Setup(x => x.GetById(1)).Returns(new Table()
            {
                Id = 1,
                Cancel = false,
                Capacity = 1,
                CreateDate = DateTime.Now,
                Number = 1
            });
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var saveReservationResponse = _reservationService.UpdateReservation(requestSaveReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.True(saveReservationResponse.IsOk);
        }
        [Fact]
        public void UpdateReservation_NullRequestDto_ReturnEqualFalse()
        {
            //Arrange

            UpdateReservationRequestDto requestUpdateReservation = null;
            //Act

            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _reservationRepository.Setup(x => x.GetById(1)).Returns(new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                Cancel = false,
                CreateDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,

            });
            _tableRepository.Setup(x => x.GetById(1)).Returns(new Table()
            {
                Id = 1,
                Cancel = false,
                Capacity = 1,
                CreateDate = DateTime.Now,
                Number = 1
            });
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var updateReservationResponse = _reservationService.UpdateReservation(requestUpdateReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.False(updateReservationResponse.IsOk);
        }
        [Fact]
        public void UpdateReservation_UpdateReservationRequestDtoZeroTableNumber_ReturnEqualFalse()
        {
            //Arrange

            var requestUpdateReservation = new UpdateReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber =0,
                Id = 1
            };
            //Act

            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _reservationRepository.Setup(x => x.GetById(1)).Returns(new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                Cancel = false,
                CreateDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,

            });
            _tableRepository.Setup(x => x.GetById(1)).Returns(new Table()
            {
                Id = 1,
                Cancel = false,
                Capacity = 1,
                CreateDate = DateTime.Now,
                Number = 1
            });
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var updateReservationResponse = _reservationService.UpdateReservation(requestUpdateReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.False(updateReservationResponse.IsOk);
        }
        [Fact]
        public void UpdateReservation_UpdateReservationRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange

            var requestUpdateReservation = new UpdateReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber =1,
                Id = 0
            };
            //Act

            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _reservationRepository.Setup(x => x.GetById(1)).Returns(new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                Cancel = false,
                CreateDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,

            });
            _tableRepository.Setup(x => x.GetById(1)).Returns(new Table()
            {
                Id = 1,
                Cancel = false,
                Capacity = 1,
                CreateDate = DateTime.Now,
                Number = 1
            });
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var updateReservationResponse = _reservationService.UpdateReservation(requestUpdateReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.False(updateReservationResponse.IsOk);
        }
        [Fact]
        public void UpdateReservation_UpdateReservationRequestDtoZeroNumberOfGuests_ReturnEqualFalse()
        {
            //Arrange

            var requestUpdateReservation = new UpdateReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 0,
                TableNumber = 1,
                Id = 1
            };
            //Act

            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _reservationRepository.Setup(x => x.GetById(1)).Returns(new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                Cancel = false,
                CreateDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,

            });
            _tableRepository.Setup(x => x.GetById(1)).Returns(new Table()
            {
                Id = 1,
                Cancel = false,
                Capacity = 1,
                CreateDate = DateTime.Now,
                Number = 1
            });
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var updateReservationResponse = _reservationService.UpdateReservation(requestUpdateReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.False(updateReservationResponse.IsOk);
        }
        [Fact]
        public void UpdateReservation_UpdateReservationRequestDtoNullCustomerName_ReturnEqualFalse()
        {
            //Arrange

            var requestUpdateReservation = new UpdateReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = null,
                NumberOfGuests = 1,
                TableNumber = 1,
                Id = 1
            };
            //Act

            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _reservationRepository.Setup(x => x.GetById(1)).Returns(new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                Cancel = false,
                CreateDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,

            });
            _tableRepository.Setup(x => x.GetById(1)).Returns(new Table()
            {
                Id = 1,
                Cancel = false,
                Capacity = 1,
                CreateDate = DateTime.Now,
                Number = 1
            });
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var updateReservationResponse = _reservationService.UpdateReservation(requestUpdateReservation);
            _reservationService.SaveChanges();
            //Assert
            Assert.False(updateReservationResponse.IsOk);
        }
        [Fact]
        public void UpdateReservation_UpdateReservationRequestDtoNullReservationDate_ReturnEqualFalse()
        {
            //Arrange

            var requestUpdateReservation = new UpdateReservationRequestDto()
            {
                ReservationDate = null,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,
                Id = 1
            };
            //Act

            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _reservationRepository.Setup(x => x.GetById(1)).Returns(new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                Cancel = false,
                CreateDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,

            });
            _tableRepository.Setup(x => x.GetById(1)).Returns(new Table()
            {
                Id = 1,
                Cancel = false,
                Capacity = 1,
                CreateDate = DateTime.Now,
                Number = 1
            });
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var updateReservationResponse = _reservationService.UpdateReservation(requestUpdateReservation);
            _reservationService.SaveChanges();

            //Assert
            Assert.False(updateReservationResponse.IsOk);
        }
        [Fact]
        public void UpdateReservation_UpdateReservationRequestDtoNoContainReservationRecord_ReturnEqualFalse()
        {
            //Arrange

            var requestUpdateReservation = new UpdateReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,
                Id = 1
            };
            //Act

            _reservationRepository.Setup(x => x.Save(It.IsAny<Domain.Entities.Reservation>()));
            _reservationRepository.Setup(x => x.GetById(1));
            _tableRepository.Setup(x => x.GetById(1)).Returns(new Table()
            {
                Id = 1,
                Cancel = false,
                Capacity = 1,
                CreateDate = DateTime.Now,
                Number = 1
            });
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var updateReservationResponse = _reservationService.UpdateReservation(requestUpdateReservation);
            _reservationService.SaveChanges();

            //Assert
            Assert.False(updateReservationResponse.IsOk);
        }
        [Fact]
        public void UpdateReservation_UpdateReservationRequestDtoNoContainsTableList_ReturnEqualFalse()
        {
            //Arrange

            var requestUpdateReservation = new UpdateReservationRequestDto()
            {
                ReservationDate = null,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,
                Id = 1
            };
            //Act

            _reservationRepository.Setup(x => x.Save(It.IsAny<Domain.Entities.Reservation>()));
            _reservationRepository.Setup(x => x.GetById(1)).Returns(new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                Cancel = false,
                CreateDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,

            });
            _tableRepository.Setup(x => x.GetById(1)).Returns(new Table()
            {
                Id = 2,
                Cancel = false,
                Capacity = 1,
                CreateDate = DateTime.Now,
                Number = 1
            });
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    CreateDate = DateTime.Now,
                    Number = 1
                }
            });
            var updateReservationResponse = _reservationService.UpdateReservation(requestUpdateReservation);
            _reservationService.SaveChanges();

            //Assert
            Assert.False(updateReservationResponse.IsOk);
        }
        [Fact]
        public void UpdateReservation_UpdateReservationRequestDtoEmptyTableList_ReturnEqualFalse()
        {
            //Arrange

            var requestSaveReservation = new UpdateReservationRequestDto()
            {
                ReservationDate = null,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,
                Id = 1
            };
            //Act

            _reservationRepository.Setup(x => x.Save(It.IsAny<Domain.Entities.Reservation>()));
            _reservationRepository.Setup(x => x.GetById(1)).Returns(new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                Cancel = false,
                CreateDate = DateTime.Now,
                CustomerName = "ExampleCustomerName",
                NumberOfGuests = 1,
                TableNumber = 1,

            });
            _tableRepository.Setup(x => x.GetById(1)).Returns(new Table()
            {
                Id = 1,
                Cancel = false,
                Capacity = 1,
                CreateDate = DateTime.Now,
                Number = 1
            });
            _tableRepository.Setup(x => x.GetTableListWithoutReservation(It.IsAny<GetTableListWithoutReservationRequestDto>()));
            var saveReservationResponse = _reservationService.UpdateReservation(requestSaveReservation);
            _reservationService.SaveChanges();

            //Assert
            Assert.False(saveReservationResponse.IsOk);
        }
        #endregion

        #region DeleteReservation Method Test Methods

        [Fact]
        public void DeleteReservation_DeleteReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestDeleteReservation = new DeleteReservationRequestDto()
            {
                Id = 1
            };

            //Act
            var defaultMockTableEntity = new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now, 
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "Example",
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>())).Returns(defaultMockTableEntity);
            _reservationRepository.Setup(x => x.Delete(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var deleteReservationServiceResponse = _reservationService.DeleteReservation(requestDeleteReservation);

            //Assert
            Assert.True(deleteReservationServiceResponse.IsOk);

        }
        [Fact]
        public void DeleteReservation_NullRequestDto_ReturnEqualFalse()
        {
            //Arrange

            DeleteReservationRequestDto requestDeleteReservation = null;

            //Act
            var defaultMockTableEntity = new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "Example",
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>())).Returns(defaultMockTableEntity);
            _reservationRepository.Setup(x => x.Delete(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var deleteReservationServiceResponse = _reservationService.DeleteReservation(requestDeleteReservation);

            //Assert
            Assert.False(deleteReservationServiceResponse.IsOk);

        }
        [Fact]
        public void DeleteReservation_DeleteReservationRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange

            var requestDeleteReservation = new DeleteReservationRequestDto()
            {
                Id = 0
            };

            //Act
            var defaultMockTableEntity = new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "Example",
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>())).Returns(defaultMockTableEntity);
            _reservationRepository.Setup(x => x.Delete(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var deleteReservationServiceResponse = _reservationService.DeleteReservation(requestDeleteReservation);

            //Assert
            Assert.False(deleteReservationServiceResponse.IsOk);

        }
        [Fact]
        public void DeleteReservation_DeleteReservationRequestNoContainsReservationEntity_ReturnEqualFalse()
        {
            //Arrange

            var requestDeleteReservation = new DeleteReservationRequestDto()
            {
                Id = 1
            };

            //Act
           
            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>()));
            _reservationRepository.Setup(x => x.Delete(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var deleteReservationServiceResponse = _reservationService.DeleteReservation(requestDeleteReservation);

            //Assert
            Assert.False(deleteReservationServiceResponse.IsOk);

        }


        #endregion

        #region SetActiveReservation Method Test Methods
        [Fact]
        public void SetActiveReservation_SetActiveReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestSetActiveReservation = new SetActiveReservationRequestDto()
            {
                Id = 1
            };

            //Act
            var defaultMockTableEntity = new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "Example",
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>())).Returns(defaultMockTableEntity);
            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var setActiveReservationServiceResponse = _reservationService.SetActiveReservation(requestSetActiveReservation);

            //Assert
            Assert.True(setActiveReservationServiceResponse.IsOk);

        }
        [Fact]
        public void SetActiveReservation_NullRequestDto_ReturnEqualFalse()
        {
           
            //Act
            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>()));
            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var setActiveReservationServiceResponse = _reservationService.SetActiveReservation(null);

            //Assert
            Assert.False(setActiveReservationServiceResponse.IsOk);

        }
        [Fact]
        public void SetActiveReservation_SetActiveReservationRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange

            var requestSetActiveReservation = new SetActiveReservationRequestDto()
            {
                Id = 0
            };

            //Act
            var defaultMockTableEntity = new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "Example",
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>())).Returns(defaultMockTableEntity);
            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var setActiveReservationServiceResponse = _reservationService.SetActiveReservation(requestSetActiveReservation);

            //Assert
            Assert.False(setActiveReservationServiceResponse.IsOk);

        }
        [Fact]
        public void SetActiveReservation_SetActiveReservationRequestDtoNoContainsReservationEntity_ReturnEqualFalse()
        {
            //Arrange

            var requestSetActiveReservation = new SetActiveReservationRequestDto()
            {
                Id = 1
            };

            //Act

            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>()));
            _reservationRepository.Setup(x => x.Update(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var setActiveReservationServiceResponse = _reservationService.SetActiveReservation(requestSetActiveReservation);

            //Assert
            Assert.False(setActiveReservationServiceResponse.IsOk);

        }


        #endregion

        #region SetCancelReservation Method Test Methods
        [Fact]
        public void SetCancelReservation_SetCancelReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestSetCancelReservation = new SetCancelReservationRequestDto()
            {
                Id = 1
            };

            //Act
            var defaultMockTableEntity = new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "Example",
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>())).Returns(defaultMockTableEntity);
            _reservationRepository.Setup(x => x.SetCancel(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var setCancelReservationServiceResponse = _reservationService.SetCancelReservation(requestSetCancelReservation);

            //Assert
            Assert.True(setCancelReservationServiceResponse.IsOk);

        }
        [Fact]
        public void SetCancelReservation_NullRequestDto_ReturnEqualFalse()
        {

            //Act
            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>()));
            _reservationRepository.Setup(x => x.SetCancel(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var setCancelReservationServiceResponse = _reservationService.SetActiveReservation(null);

            //Assert
            Assert.False(setCancelReservationServiceResponse.IsOk);

        }
        [Fact]
        public void SetCancelReservation_SetCancelReservationRequestDtoDtoZeroId_ReturnEqualFalse()
        {
            //Arrange

            var requestSetCancelReservation = new SetCancelReservationRequestDto()
            {
                Id = 0
            };

            //Act
            var defaultMockTableEntity = new Domain.Entities.Reservation()
            {
                Id = 1,
                ReservationDate = DateTime.Now,
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "Example",
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>())).Returns(defaultMockTableEntity);
            _reservationRepository.Setup(x => x.SetCancel(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var setCancelReservationServiceResponse = _reservationService.SetCancelReservation(requestSetCancelReservation);

            //Assert
            Assert.False(setCancelReservationServiceResponse.IsOk);

        }
        [Fact]
        public void SetCancelReservation_SetCancelReservationRequestDtoNoContainsReservationEntity_ReturnEqualFalse()
        {
            //Arrange

            var requestSetCancelReservation = new SetCancelReservationRequestDto()
            {
                Id = 1
            };

            //Act

            _reservationRepository.Setup(x => x.Get(It.IsAny<Expression<Func<Domain.Entities.Reservation, bool>>>()));
            _reservationRepository.Setup(x => x.SetCancel(It.IsAny<Domain.Entities.Reservation>()));
            _commonRepository.Setup(x => x.SaveChange());
            var setCancelReservationServiceResponse = _reservationService.SetCancelReservation(requestSetCancelReservation);

            //Assert
            Assert.False(setCancelReservationServiceResponse.IsOk);

        }


        #endregion
    }
}
