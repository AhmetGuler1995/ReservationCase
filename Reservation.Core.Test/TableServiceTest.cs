using Moq;
using Reservation.Core.Repositories.Abstract;
using Reservation.Core.Services.Abstract;
using Reservation.Core.Services.Concrete;
using Reservation.Domain.Entities;
using Reservation.Domain.Models.RequestDtos.Table;

namespace Reservation.Core.Test
{
    public class TableServiceTest
    {
        private readonly ITableService _tableService;
        private readonly Mock<ITableRepository> _tableRepositoryMock;
        private readonly Mock<ICommonRepository> _commonRepository;

        public TableServiceTest()
        {
            _tableRepositoryMock = new Mock<ITableRepository>();
            _commonRepository = new Mock<ICommonRepository>();
            _tableService = new TableService(_commonRepository.Object,_tableRepositoryMock.Object);
        }

        #region SaveTable Method Test Methods

        [Fact]
        public void SaveTable_SaveTableRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestSaveTable = new SaveTableRequestDto()
            {
                Capacity = 2,
                Number = 1
            };

            //Act

            _tableRepositoryMock.Setup(x => x.Save(null)).Returns(0);
            _commonRepository.Setup(x => x.SaveChange());

            var saveTableServiceResponse = _tableService.SaveTable(requestSaveTable);
            _commonRepository.Object.SaveChange();
            //Assert
            Assert.True(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void SaveTable_NullRequestModel_ReturnEqualFalse()
        {
            //Arrange

            SaveTableRequestDto requestSaveTable = null;

            //Act

            _tableRepositoryMock.Setup(x => x.Save(null)).Returns(0);
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.SaveTable(requestSaveTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void SaveTable_SaveTableRequestDtoZeroCapacity_ReturnEqualFalse()
        {
            //Arrange

            var requestSaveTable = new SaveTableRequestDto()
            {
                Capacity = 0,
                Number = 1
            };

            //Act

            _tableRepositoryMock.Setup(x => x.Save(null)).Returns(0);
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.SaveTable(requestSaveTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void SaveTable_SaveTableRequestDtoZeroNumber_ReturnEqualFalse()
        {
            //Arrange

            var requestSaveTable = new SaveTableRequestDto()
            {
                Capacity = 1,
                Number = 0
            };

            //Act

            _tableRepositoryMock.Setup(x => x.Save(null)).Returns(0);
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.SaveTable(requestSaveTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void SaveTable_SaveTableRequestDtoZeroNumberZeroCapacity_ReturnEqualFalse()
        {
            //Arrange

            var requestSaveTable = new SaveTableRequestDto()
            {
                Capacity = 0,
                Number = 0
            };

            //Act

            _tableRepositoryMock.Setup(x => x.Save(null)).Returns(0);
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.SaveTable(requestSaveTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }

        #endregion

        #region UpdateTable Method Test Methods

        [Fact]
        public void UpdateTable_UpdateTableRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestUpdateTable = new UpdateTableRequestDto()
            {
                Capacity = 2,
                Number = 1,
                Id = 1
            };

            //Act
            var defaultMockTableEntity = new Table()
            {
                Id = 1,
                Capacity = 2,
                Number = 1,
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(defaultMockTableEntity);
            _tableRepositoryMock.Setup(x => x.Update(defaultMockTableEntity));
            _commonRepository.Setup(x => x.SaveChange());
            
            var saveTableServiceResponse = _tableService.UpdateTable(requestUpdateTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.True(saveTableServiceResponse.IsOk);

        }

        [Fact]
        public void UpdateTable_NullRequestModel_ReturnEqualFalse()
        {
            //Arrange

            UpdateTableRequestDto requestUpdateTable = null;

            //Act
            var defaultMockTableEntity = new Table()
            {
                Id = 1,
                Capacity = 2,
                Number = 1,
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(defaultMockTableEntity);
            _tableRepositoryMock.Setup(x => x.Update(defaultMockTableEntity));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.UpdateTable(requestUpdateTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }

        [Fact]
        public void UpdateTable_UpdateTableRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange

            UpdateTableRequestDto requestUpdateTable = new ()
            {
                Capacity = 2,
                Number = 1,
                Id = 0
            };

            //Act
            var defaultMockTableEntity = new Table()
            {
                Id = 1,
                Capacity = 2,
                Number = 1,
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(defaultMockTableEntity);
            _tableRepositoryMock.Setup(x => x.Update(defaultMockTableEntity));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.UpdateTable(requestUpdateTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }

        [Fact]
        public void UpdateTable_UpdateTableRequestDtoZeroNumber_ReturnEqualFalse()
        {
            //Arrange

            UpdateTableRequestDto requestUpdateTable = new ()
            {
                Capacity = 2,
                Number = 0,
                Id = 1
            };

            //Act
            var defaultMockTableEntity = new Table()
            {
                Id = 1,
                Capacity = 2,
                Number = 1,
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(defaultMockTableEntity);
            _tableRepositoryMock.Setup(x => x.Update(defaultMockTableEntity));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.UpdateTable(requestUpdateTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }

        [Fact]
        public void UpdateTable_UpdateTableRequestDtoZeroCapacity_ReturnEqualFalse()
        {
            //Arrange

            UpdateTableRequestDto requestUpdateTable = new ()
            {
                Capacity = 0,
                Number = 1,
                Id = 1
            };

            //Act
            var defaultMockTableEntity = new Table()
            {
                Id = 1,
                Capacity = 2,
                Number = 1,
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(defaultMockTableEntity);
            _tableRepositoryMock.Setup(x => x.Update(defaultMockTableEntity));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.UpdateTable(requestUpdateTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void UpdateTable_UpdateTableRequestDtoIdNotContains_ReturnEqualFalse()
        {
            //Arrange

            UpdateTableRequestDto requestUpdateTable = new ()
            {
                Capacity = 2,
                Number = 1,
                Id = 1
            };

            //Act
            var defaultMockTableEntity = new Table()
            {
                Id = 1,
                Capacity = 2,
                Number = 1,
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _tableRepositoryMock.Setup(x => x.GetById(1));
            _tableRepositoryMock.Setup(x => x.Update(defaultMockTableEntity));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.UpdateTable(requestUpdateTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void UpdateTable_UpdateTableRequestDtoIdNotContainsAndInstanceEntity_ReturnEqualFalse()
        {
            //Arrange

            UpdateTableRequestDto requestUpdateTable = new ()
            {
                Capacity = 2,
                Number = 1,
                Id = 1
            };

            //Act

            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(new Table());
            _tableRepositoryMock.Setup(x => x.Update(new Table()));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.UpdateTable(requestUpdateTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        #endregion

        #region DeleteTable Method Test Methods

        [Fact]
        public void DeleteTable_DeleteTableRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestDeleteTable = new DeleteTableRequestDto()
            {
                Id = 1
            };

            //Act
            var defaultMockTableEntity = new Table()
            {
                Id = 1,
                Capacity = 2,
                Number = 1,
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(defaultMockTableEntity);
            _tableRepositoryMock.Setup(x => x.Delete(defaultMockTableEntity));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.DeleteTable(requestDeleteTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.True(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void DeleteTable_NullRequestModel_ReturnEqualFalse()
        {
            //Arrange


            //Act
            var defaultMockTableEntity = new Table()
            {
                Id = 1,
                Capacity = 2,
                Number = 1,
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _tableRepositoryMock.Setup(x => x.GetById(1));
            _tableRepositoryMock.Setup(x => x.Delete(null));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.DeleteTable(null);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void DeleteTable_DeleteTableRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange

            DeleteTableRequestDto requestDeleteTable = new ()
            {
                Id = 0
            };

            //Act
            _tableRepositoryMock.Setup(x => x.GetById(1));
            _tableRepositoryMock.Setup(x => x.Delete(null));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.DeleteTable(requestDeleteTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        #endregion

        #region SetActive Method Test Methods
        [Fact]
        public void SetActiveTable_SetActiveTableRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestSetActiveTable = new SetActiveTableRequestDto()
            {
                Id = 1
            };

            //Act
            var defaultMockTableEntity = new Table()
            {
                Id = 1,
                Capacity = 2,
                Number = 1,
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(defaultMockTableEntity);
            _tableRepositoryMock.Setup(x => x.Update(defaultMockTableEntity));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.SetActiveTable(requestSetActiveTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.True(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void SetActiveTable_NullRequestModel_ReturnEqualFalse()
        {
            //Arrange


            //Act
            _tableRepositoryMock.Setup(x => x.GetById(1));
            _tableRepositoryMock.Setup(x => x.Update(null));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.SetActiveTable(null);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void SetActiveTable_SetActiveTableRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange

            SetActiveTableRequestDto requestSetActiveTable = new ()
            {
                Id = 0
            };

            //Act
            _tableRepositoryMock.Setup(x => x.GetById(1));
            _tableRepositoryMock.Setup(x => x.Update(null));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.SetActiveTable(requestSetActiveTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }


        #endregion

        #region SetCancel Method Test Methods
        [Fact]
        public void SetCancelTable_SetCancelTableRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestSetActiveTable = new SetCancelTableRequestDto()
            {
                Id = 1
            };

            //Act
            var defaultMockTableEntity = new Table()
            {
                Id = 1,
                Capacity = 2,
                Number = 1,
                Cancel = false,
                CreateDate = DateTime.Now
            };
            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(defaultMockTableEntity);
            _tableRepositoryMock.Setup(x => x.SetCancel(defaultMockTableEntity));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.SetCancelTable(requestSetActiveTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.True(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void SetCancelTable_NullRequestModel_ReturnEqualFalse()
        {
            //Arrange


            //Act
            _tableRepositoryMock.Setup(x => x.GetById(1));
            _tableRepositoryMock.Setup(x => x.SetCancel(null));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.SetCancelTable(null);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void SetCancelTable_SetCancelTableRequestDto_ReturnEqualFalse()
        {
            //Arrange

            SetActiveTableRequestDto requestSetActiveTable = new ()
            {
                Id = 0
            };

            //Act
            _tableRepositoryMock.Setup(x => x.GetById(1));
            _tableRepositoryMock.Setup(x => x.Update(null));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.SetActiveTable(requestSetActiveTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }



        #endregion

        #region GetTableListWithoutReservation Method Test Methods

        [Fact]
        public void GetTableListWithoutReservation_GetTableListWithoutReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestGetTableListWithoutReservation = new GetTableListWithoutReservationRequestDto()
            {
               NumberOfGuest = 2,
               ReservationDate = DateTime.Now
            };

            //Act
           
            _tableRepositoryMock.Setup(x => x.GetTableListWithoutReservation(requestGetTableListWithoutReservation)).Returns(new List<Table>()
            {
                new()
                {
                    Cancel = false,
                    Capacity = 2,
                    CreateDate = DateTime.Now,
                    Id = 1,
                    Number = 1
                }
            });
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.GetTableListWithoutReservation(requestGetTableListWithoutReservation);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.True(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void GetTableListWithoutReservation_NullRequestModel_ReturnEqualFalse()
        {
            //Arrange

            GetTableListWithoutReservationRequestDto requestGetTableListWithoutReservation =  null;

            //Act

            _tableRepositoryMock.Setup(x => x.GetTableListWithoutReservation(requestGetTableListWithoutReservation)).Returns(new List<Table>()
            {
                new()
                {
                    Cancel = false,
                    Capacity = 2,
                    CreateDate = DateTime.Now,
                    Id = 1,
                    Number = 1
                }
            });
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.GetTableListWithoutReservation(requestGetTableListWithoutReservation);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void GetTableListWithoutReservation_GetTableListWithoutReservationRequestDtoZeroNumberOfGuest_ReturnEqualFalse()
        {
            //Arrange

            var requestGetTableListWithoutReservation = new GetTableListWithoutReservationRequestDto()
            {
                NumberOfGuest = 0,
                ReservationDate = DateTime.Now
            };

            //Act

            _tableRepositoryMock.Setup(x => x.GetTableListWithoutReservation(requestGetTableListWithoutReservation)).Returns(new List<Table>()
            {
                new()
                {
                    Cancel = false,
                    Capacity = 2,
                    CreateDate = DateTime.Now,
                    Id = 1,
                    Number = 1
                }
            });
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.GetTableListWithoutReservation(requestGetTableListWithoutReservation);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void GetTableListWithoutReservation_GetTableListWithoutReservationRequestDtoNoReservationDate_ReturnEqualFalse()
        {
            //Arrange

            var requestGetTableListWithoutReservation = new GetTableListWithoutReservationRequestDto()
            {
                NumberOfGuest = 1,
            };

            //Act

            _tableRepositoryMock.Setup(x => x.GetTableListWithoutReservation(requestGetTableListWithoutReservation)).Returns(new List<Table>()
            {
                new()
                {
                    Cancel = false,
                    Capacity = 2,
                    CreateDate = DateTime.Now,
                    Id = 1,
                    Number = 1
                }
            });
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.GetTableListWithoutReservation(requestGetTableListWithoutReservation);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        [Fact]
        public void GetTableListWithoutReservation_GetTableListWithoutReservationRequestDtoNoReservationDateZeroNumberOfGuest_ReturnEqualFalse()
        {
            //Arrange

            var requestGetTableListWithoutReservation = new GetTableListWithoutReservationRequestDto()
            {
                NumberOfGuest = 0,
            };

            //Act

            _tableRepositoryMock.Setup(x => x.GetTableListWithoutReservation(requestGetTableListWithoutReservation)).Returns(new List<Table>()
            {
                new()
                {
                    Cancel = false,
                    Capacity = 2,
                    CreateDate = DateTime.Now,
                    Id = 1,
                    Number = 1
                }
            });
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.GetTableListWithoutReservation(requestGetTableListWithoutReservation);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }

        #endregion

        #region GetTable Method Test Methods


        [Fact]
        public void GetTable_GetTableRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var requestGetTable = new GetTableRequestDto()
            {
                Id = 1,
            };

            //Act

            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(new Table());
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.GetTable(requestGetTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.True(saveTableServiceResponse.IsOk);

        }

        [Fact]
        public void GetTable_NullRequestModel_ReturnEqualFalse()
        {
            //Act
            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(new Table());
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.GetTable(null);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }

        [Fact]
        public void GetTable_GetTableRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange
            var requestGetTable = new GetTableRequestDto()
            {
                Id = 0,
            };

            //Act
            _tableRepositoryMock.Setup(x => x.GetById(1)).Returns(new Table());
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.GetTable(requestGetTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }

        [Fact]
        public void GetTable_GetTableRequestDtoButNoRecord_ReturnEqualFalse()
        {
            //Arrange

            var requestGetTable = new GetTableRequestDto()
            {
                Id = 1,
            };

            //Act

            _tableRepositoryMock.Setup(x => x.GetById(1));
            _commonRepository.Setup(x => x.SaveChange());
            var saveTableServiceResponse = _tableService.GetTable(requestGetTable);
            _commonRepository.Object.SaveChange();

            //Assert
            Assert.False(saveTableServiceResponse.IsOk);

        }
        #endregion
    }
}
