using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Reservation.Domain.Models.GeneralModels;
using Reservation.Domain.Models.RequestDtos.Table;
using Reservation.Domain.Models.ResponseModels.Table;
using System.Net.Mime;
using Reservation.Domain.Models.RequestDtos.Reservation;
using Reservation.Domain.Models.ResponseModels.Reservation;

namespace Reservation.Api.Test
{
    [TestClass]
    public class ApiTest
    {
        private readonly HttpClient _httpClient;

        public ApiTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = true,

            });
            SaveTableBeforeTesting();
        }

        private void SaveTableBeforeTesting()
        {
            var serializePostDataSaveTable = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 1,
                Number = 1
            });
            var serviceResponseSaveTable = _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTable, Encoding.UTF8, "application/json")).Result;
        }

        #region TableController Actions Test

        #region SaveTable Action Test Methods
        [TestMethod]
        public async Task TableSaveTable_SaveTableRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializePostDataSaveTable = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 1,
                Number = 1
            });

            //Act
            var clientResponse = await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTable, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseServiceSaveTable = JsonSerializer.Deserialize<GeneralDataResponse<SaveTableResponse>>(contentReadStringValue);

            //Assert
            Assert.IsTrue(responseServiceSaveTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableSaveTable_NullRequestDto_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSaveTable = JsonSerializer.Serialize(default(SaveTableRequestDto));

            //Act
            var clientResponse = await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTable, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseServiceSaveTable = JsonSerializer.Deserialize<GeneralDataResponse<SaveTableResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseServiceSaveTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableSaveTable_SaveTableRequestDtoZeroCapacity_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSaveTable = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 0,
                Number = 1
            });

            //Act
            var clientResponse = await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTable, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseServiceSaveTable = JsonSerializer.Deserialize<GeneralDataResponse<SaveTableResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseServiceSaveTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableSaveTable_SaveTableRequestDtoZeroNumber_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSaveTable = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 1,
                Number = 0
            });

            //Act
            var clientResponse = await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTable, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseServiceSaveTable = JsonSerializer.Deserialize<GeneralDataResponse<SaveTableResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseServiceSaveTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableSaveTable_SaveTableRequestDtoZeroNumberAndCapacity_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSaveTable = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 0,
                Number = 0
            });

            //Act
            var clientResponse = await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTable, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseServiceSaveTable = JsonSerializer.Deserialize<GeneralDataResponse<SaveTableResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseServiceSaveTable?.IsOk ?? false);
        }
        #endregion

        #region UpdateTable Action Test Methods
        [TestMethod]
        public async Task TableUpdateTable_UpdateTableRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializePostDataUpdateTable = JsonSerializer.Serialize(new UpdateTableRequestDto()
            {
                Capacity = 1,
                Number = 1,
                Id = 1
            });


            //Act

            var clientResponseUpdateTable = await _httpClient.PostAsync("/Table/UpdateTable", new StringContent(serializePostDataUpdateTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueUpdateTable = await clientResponseUpdateTable.Content.ReadAsStringAsync();
            var responseServiceUpdateTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueUpdateTable);

            //Assert
            Assert.IsTrue(responseServiceUpdateTable?.IsOk ?? false);
        }

        [TestMethod]
        public async Task TableUpdateTable_UpdateTableRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataUpdateTable = JsonSerializer.Serialize(new UpdateTableRequestDto()
            {
                Capacity = 1,
                Number = 1,
                Id = 0
            });


            //Act
            var clientResponseUpdateTable = await _httpClient.PostAsync("/Table/UpdateTable", new StringContent(serializePostDataUpdateTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueUpdateTable = await clientResponseUpdateTable.Content.ReadAsStringAsync();
            var responseServiceUpdateTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueUpdateTable);

            //Assert
            Assert.IsFalse(responseServiceUpdateTable?.IsOk);
        }
        [TestMethod]
        public async Task TableUpdateTable_UpdateTableRequestDtoZeroNumber_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataUpdateTable = JsonSerializer.Serialize(new UpdateTableRequestDto()
            {
                Capacity = 1,
                Number = 0,
                Id = 1
            });


            //Act
            var clientResponseUpdateTable = await _httpClient.PostAsync("/Table/UpdateTable", new StringContent(serializePostDataUpdateTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueUpdateTable = await clientResponseUpdateTable.Content.ReadAsStringAsync();
            var responseServiceUpdateTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueUpdateTable);

            //Assert
            Assert.IsFalse(responseServiceUpdateTable?.IsOk);
        }
        [TestMethod]
        public async Task TableUpdateTable_UpdateTableRequestDtoNoEntityTable_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataUpdateTable = JsonSerializer.Serialize(new UpdateTableRequestDto()
            {
                Capacity = 1,
                Number = 1,
                Id = 1
            });
            var clientResponseUpdateTable = await _httpClient.PostAsync("/Table/UpdateTable", new StringContent(serializePostDataUpdateTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueUpdateTable = await clientResponseUpdateTable.Content.ReadAsStringAsync();
            var responseServiceUpdateTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueUpdateTable);

            //Assert
            Assert.IsFalse(responseServiceUpdateTable?.IsOk);
        }
        #endregion

        #region SetActiveTable Action Test Methods
        [TestMethod]
        public async Task TableSetActiveTable_SetActiveTableRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializePostDataSetActiveTable = JsonSerializer.Serialize(new SetActiveTableRequestDto()
            {
                Id = 1
            });

            //Act
            var clientResponseSetActiveTable = await _httpClient.PostAsync("/Table/SetActiveTable", new StringContent(serializePostDataSetActiveTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueSetActiveTable = await clientResponseSetActiveTable.Content.ReadAsStringAsync();
            var responseServiceSetActiveTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueSetActiveTable);

            //Assert
            Assert.IsTrue(responseServiceSetActiveTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableSetActiveTable_NullRequestDto_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSetActiveTable = JsonSerializer.Serialize(default(SetActiveTableRequestDto));

            //Act
            var clientResponseSetActiveTable = await _httpClient.PostAsync("/Table/SetActiveTable", new StringContent(serializePostDataSetActiveTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueSetActiveTable = await clientResponseSetActiveTable.Content.ReadAsStringAsync();
            var responseServiceSetActiveTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueSetActiveTable);

            //Assert
            Assert.IsFalse(responseServiceSetActiveTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableSetActiveTable_SetActiveTableRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSetActiveTable = JsonSerializer.Serialize(new SetActiveTableRequestDto()
            {
                Id = 0
            });

            //Act
            var clientResponseSetActiveTable = await _httpClient.PostAsync("/Table/SetActiveTable", new StringContent(serializePostDataSetActiveTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueSetActiveTable = await clientResponseSetActiveTable.Content.ReadAsStringAsync();
            var responseServiceSetActiveTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueSetActiveTable);

            //Assert
            Assert.IsFalse(responseServiceSetActiveTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableSetActiveTable_SetActiveTableRequestDtoNoContainsEntityInDatabase_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSetActiveTable = JsonSerializer.Serialize(new SetActiveTableRequestDto()
            {
                Id = 2000
            });

            //Act
            var clientResponseSetActiveTable = await _httpClient.PostAsync("/Table/SetActiveTable", new StringContent(serializePostDataSetActiveTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueSetActiveTable = await clientResponseSetActiveTable.Content.ReadAsStringAsync();
            var responseServiceSetActiveTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueSetActiveTable);

            //Assert
            Assert.IsFalse(responseServiceSetActiveTable?.IsOk ?? false);
        }
        #endregion

        #region SetCancelTable Action Test Methods
        [TestMethod]
        public async Task TableSetCancelTable_SetCancelTableRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializePostDataSetCancelTable = JsonSerializer.Serialize(new SetCancelTableRequestDto()
            {
                Id = 1
            });


            //Act
            var clientResponseSetCancelTable = await _httpClient.PostAsync("/Table/SetCancelTable", new StringContent(serializePostDataSetCancelTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueSetCancelTable = await clientResponseSetCancelTable.Content.ReadAsStringAsync();
            var responseServiceSetCancelTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueSetCancelTable);

            //Assert
            Assert.IsTrue(responseServiceSetCancelTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableSetCancelTable_NullRequestDto_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSetCancelTable = JsonSerializer.Serialize(default(SetCancelTableRequestDto));

            //Act
            var clientResponseSetCancelTable = await _httpClient.PostAsync("/Table/SetCancelTable", new StringContent(serializePostDataSetCancelTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueSetCancelTable = await clientResponseSetCancelTable.Content.ReadAsStringAsync();
            var responseServiceSetCancelTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueSetCancelTable);

            //Assert
            Assert.IsFalse(responseServiceSetCancelTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableSetCancelTable_SetCancelTableRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSetCancelTable = JsonSerializer.Serialize(new SetCancelTableRequestDto()
            {
                Id = 0
            });

            //Act
            var clientResponseSetCancelTable = await _httpClient.PostAsync("/Table/SetCancelTable", new StringContent(serializePostDataSetCancelTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueSetCancelTable = await clientResponseSetCancelTable.Content.ReadAsStringAsync();
            var responseServiceSetCancelTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueSetCancelTable);

            //Assert
            Assert.IsFalse(responseServiceSetCancelTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableSetCancelTable_SetCancelTableRequestDtoNoContainsEntityInDatabase_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSetCancelTable = JsonSerializer.Serialize(new SetCancelTableRequestDto()
            {
                Id = 2000
            });

            //Act
            var clientResponseSetCancelTable = await _httpClient.PostAsync("/Table/SetCancelTable", new StringContent(serializePostDataSetCancelTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueSetCancelTable = await clientResponseSetCancelTable.Content.ReadAsStringAsync();
            var responseServiceSetCancelTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueSetCancelTable);

            //Assert
            Assert.IsFalse(responseServiceSetCancelTable?.IsOk ?? false);
        }
        #endregion

        #region DeleteTable Action Test Methods
        [TestMethod]
        public async Task TableDeleteTable_DeleteTableRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializePostDataDeleteTable = JsonSerializer.Serialize(new DeleteTableRequestDto()
            {
                Id = 1
            });

            //Act

            var clientResponseDeleteTable = await _httpClient.PostAsync("/Table/DeleteTable", new StringContent(serializePostDataDeleteTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueDeleteTable = await clientResponseDeleteTable.Content.ReadAsStringAsync();
            var responseServiceDeleteTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueDeleteTable);

            //Assert
            Assert.IsTrue(responseServiceDeleteTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableDeleteTable_NullRequestDto_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataDeleteTable = JsonSerializer.Serialize(default(DeleteTableRequestDto));

            //Act
            var clientResponseDeleteTable = await _httpClient.PostAsync("/Table/DeleteTable", new StringContent(serializePostDataDeleteTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueDeleteTable = await clientResponseDeleteTable.Content.ReadAsStringAsync();
            var responseServiceDeleteTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueDeleteTable);

            //Assert
            Assert.IsFalse(responseServiceDeleteTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableDeleteTable_DeleteTableRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataDeleteTable = JsonSerializer.Serialize(new DeleteTableRequestDto()
            {
                Id = 0
            });

            //Act
            var clientResponseDeleteTable = await _httpClient.PostAsync("/Table/DeleteTable", new StringContent(serializePostDataDeleteTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueDeleteTable = await clientResponseDeleteTable.Content.ReadAsStringAsync();
            var responseServiceDeleteTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueDeleteTable);

            //Assert
            Assert.IsFalse(responseServiceDeleteTable?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableDeleteTable_DeleteTableRequestDtoNoContainsEntityInDatabase_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataDeleteTable = JsonSerializer.Serialize(new DeleteTableRequestDto()
            {
                Id = 2000
            });

            //Act
            var clientResponseDeleteTable = await _httpClient.PostAsync("/Table/DeleteTable", new StringContent(serializePostDataDeleteTable, Encoding.UTF8, "application/json"));
            var contentReadStringValueDeleteTable = await clientResponseDeleteTable.Content.ReadAsStringAsync();
            var responseServiceDeleteTable = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValueDeleteTable);

            //Assert
            Assert.IsFalse(responseServiceDeleteTable?.IsOk ?? false);
        }
        #endregion

        #region GetTableListWithoutReservation Method Test Methods
        [TestMethod]
        public async Task TableGetTableListWithoutReservation_GetTableListWithoutReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializePostDataGetTableListWithoutReservation = JsonSerializer.Serialize(new GetTableListWithoutReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                NumberOfGuest = 1
            });
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTableListWithoutReservation";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent(serializePostDataGetTableListWithoutReservation, Encoding.UTF8, MediaTypeNames.Application.Json ),
            };
            //Act
            var clientResponseGetTableListWithoutReservation = await _httpClient.SendAsync(request);
            var contentReadStringValueGetTableListWithoutReservation = await clientResponseGetTableListWithoutReservation.Content.ReadAsStringAsync();
            var responseServiceGetTableListWithoutReservation = JsonSerializer.Deserialize<GeneralListResponse<GetTableListWithoutReservationResponse>>(contentReadStringValueGetTableListWithoutReservation);

            //Assert
            Assert.IsTrue(responseServiceGetTableListWithoutReservation?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableGetTableListWithoutReservation_NullDto_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataGetTableListWithoutReservation = JsonSerializer.Serialize(default(GetTableListWithoutReservationRequestDto));
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTableListWithoutReservation";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent(serializePostDataGetTableListWithoutReservation, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };
            //Act
            var clientResponseGetTableListWithoutReservation = await _httpClient.SendAsync(request);
            var contentReadStringValueGetTableListWithoutReservation = await clientResponseGetTableListWithoutReservation.Content.ReadAsStringAsync();
            var responseServiceGetTableListWithoutReservation = JsonSerializer.Deserialize<GeneralListResponse<GetTableListWithoutReservationResponse>>(contentReadStringValueGetTableListWithoutReservation);

            //Assert
            Assert.IsFalse(responseServiceGetTableListWithoutReservation?.IsOk ?? false);
        }
        [TestMethod]
        public async Task TableGetTableListWithoutReservation_GetTableListWithoutReservationRequestDtoNullReservationDate_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataGetTableListWithoutReservation = JsonSerializer.Serialize(new GetTableListWithoutReservationRequestDto()
            {
                ReservationDate = null,
                NumberOfGuest = 1
            });
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTableListWithoutReservation";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent(serializePostDataGetTableListWithoutReservation, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };
            //Act
            var clientResponseGetTableListWithoutReservation = await _httpClient.SendAsync(request);
            var contentReadStringValueGetTableListWithoutReservation = await clientResponseGetTableListWithoutReservation.Content.ReadAsStringAsync();
            var responseServiceGetTableListWithoutReservation = JsonSerializer.Deserialize<GeneralListResponse<GetTableListWithoutReservationResponse>>(contentReadStringValueGetTableListWithoutReservation);

            //Assert
            Assert.IsFalse(responseServiceGetTableListWithoutReservation?.IsOk ?? false);
        }

        [TestMethod]
        public async Task TableGetTableListWithoutReservation_GetTableListWithoutReservationRequestDtoZeroNumberOfGuest_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataGetTableListWithoutReservation = JsonSerializer.Serialize(new GetTableListWithoutReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                NumberOfGuest = 0
            });
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTableListWithoutReservation";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent(serializePostDataGetTableListWithoutReservation, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };
            //Act
            var clientResponseGetTableListWithoutReservation = await _httpClient.SendAsync(request);
            var contentReadStringValueGetTableListWithoutReservation = await clientResponseGetTableListWithoutReservation.Content.ReadAsStringAsync();
            var responseServiceGetTableListWithoutReservation = JsonSerializer.Deserialize<GeneralListResponse<GetTableListWithoutReservationResponse>>(contentReadStringValueGetTableListWithoutReservation);

            //Assert
            Assert.IsFalse(responseServiceGetTableListWithoutReservation?.IsOk ?? false);
        }
        #endregion

        #region GetTables Action Test Methods

        [TestMethod]
        public async Task TableGetTable_GetTableRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializePostDataGetTableListWithoutReservation = JsonSerializer.Serialize(new GetTableRequestDto()
            {
                Id = 1
            });
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTable";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent(serializePostDataGetTableListWithoutReservation, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };
            //Act
            var clientResponseGetTableListWithoutReservation = await _httpClient.SendAsync(request);
            var contentReadStringValueGetTableListWithoutReservation = await clientResponseGetTableListWithoutReservation.Content.ReadAsStringAsync();
            var responseServiceGetTableListWithoutReservation = JsonSerializer.Deserialize<GeneralListResponse<GetTableListWithoutReservationResponse>>(contentReadStringValueGetTableListWithoutReservation);

            //Assert
            Assert.IsTrue(responseServiceGetTableListWithoutReservation?.IsOk ?? false);
        }

        [TestMethod]
        public async Task TableGetTable_NullDtoRequest_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataGetTableListWithoutReservation = JsonSerializer.Serialize(default(GetTableRequestDto));
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTable";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent(serializePostDataGetTableListWithoutReservation, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };
            //Act
            var clientResponseGetTableListWithoutReservation = await _httpClient.SendAsync(request);
            var contentReadStringValueGetTableListWithoutReservation = await clientResponseGetTableListWithoutReservation.Content.ReadAsStringAsync();
            var responseServiceGetTableListWithoutReservation = JsonSerializer.Deserialize<GeneralListResponse<GetTableListWithoutReservationResponse>>(contentReadStringValueGetTableListWithoutReservation);

            //Assert
            Assert.IsFalse(responseServiceGetTableListWithoutReservation?.IsOk ?? false);
        }

        [TestMethod]
        public async Task TableGetTable_GetTableRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataGetTableListWithoutReservation = JsonSerializer.Serialize(new GetTableRequestDto()
            {
                Id = 0
            });
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTable";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent(serializePostDataGetTableListWithoutReservation, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };
            //Act
            var clientResponseGetTableListWithoutReservation = await _httpClient.SendAsync(request);
            var contentReadStringValueGetTableListWithoutReservation = await clientResponseGetTableListWithoutReservation.Content.ReadAsStringAsync();
            var responseServiceGetTableListWithoutReservation = JsonSerializer.Deserialize<GeneralListResponse<GetTableListWithoutReservationResponse>>(contentReadStringValueGetTableListWithoutReservation);

            //Assert
            Assert.IsFalse(responseServiceGetTableListWithoutReservation?.IsOk ?? false);
        }
        #endregion

        #endregion

        #region ReservationController Actions Test

        #region SaveReservation Action Test Methods

        [TestMethod]
        public async Task ReservationSaveReservation_SaveReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange

            var serializePostDataSaveReservation = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "ExampleCustomerName"
            });

            //Act
            var clientResponse = await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializePostDataSaveReservation, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseServiceSaveReservation = JsonSerializer.Deserialize<GeneralDataResponse<SaveReservationResponse>>(contentReadStringValue);

            //Assert
            Assert.IsTrue(responseServiceSaveReservation?.IsOk ?? false);
        }
        [TestMethod]
        public async Task ReservationSaveReservation_NullReqestDto_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSaveReservation = JsonSerializer.Serialize(default(SaveReservationRequestDto));

            //Act
            var clientResponse = await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializePostDataSaveReservation, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseServiceSaveReservation = JsonSerializer.Deserialize<GeneralDataResponse<SaveReservationResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseServiceSaveReservation?.IsOk ?? false);
        }
        [TestMethod]
        public async Task ReservationSaveReservation_SaveReservationRequestDtoZeroNumberOfGuests_ReturnEqualFalse()
        {
            //Arrange


            var serializePostDataSaveReservation = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                NumberOfGuests = 0,
                CustomerName = "ExampleCustomerName"
            });

            //Act

            var clientResponse = await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializePostDataSaveReservation, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();

            var responseServiceSaveReservation = JsonSerializer.Deserialize<GeneralDataResponse<SaveReservationResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseServiceSaveReservation?.IsOk ?? false);
        }
        [TestMethod]
        public async Task ReservationSaveReservation_SaveReservationRequestDtoNullReservationDate_ReturnEqualFalse()
        {
            //Arrange


            var serializePostDataSaveReservation = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                ReservationDate = null,
                NumberOfGuests = 1,
                CustomerName = "ExampleCustomerName"
            });

            //Act

            var clientResponse = await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializePostDataSaveReservation, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();

            var responseServiceSaveReservation = JsonSerializer.Deserialize<GeneralDataResponse<SaveReservationResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseServiceSaveReservation?.IsOk ?? false);
        }
        [TestMethod]
        public async Task ReservationSaveReservation_SaveReservationRequestDtoNullCustomerName_ReturnEqualFalse()
        {
            //Arrange


            var serializePostDataSaveReservation = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                NumberOfGuests = 1,
                CustomerName = null
            });

            //Act

            var clientResponse = await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializePostDataSaveReservation, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();

            var responseServiceSaveReservation = JsonSerializer.Deserialize<GeneralDataResponse<SaveReservationResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseServiceSaveReservation?.IsOk ?? false);
        }
        #endregion

        #region UpdateReservation Action Test Methods
        [TestMethod]
        public async Task ReservationUpdateReservation_UpdateReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializePostDataSaveReservation = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "ExampleCustomerName"
            });
            var serializePostDataUpdateReservation = JsonSerializer.Serialize(new UpdateReservationRequestDto()
            {
                ReservationDate = DateTime.Now.AddDays(1),
                TableNumber = 1,
                Id = 1,
                NumberOfGuests = 1,
                CustomerName = "ExampleCustomerName"
            });

            //Act
            await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializePostDataSaveReservation, Encoding.UTF8, "application/json"));
            var clientResponse = await _httpClient.PostAsync("/Reservation/UpdateReservation", new StringContent(serializePostDataUpdateReservation, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseServiceSaveReservation = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValue);

            //Assert
            Assert.IsTrue(responseServiceSaveReservation?.IsOk ?? false);
        }

        [TestMethod]
        public async Task ReservationUpdateReservation_NullRequestDto_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSaveReservation = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "ExampleCustomerName"
            });
            var serializePostDataUpdateReservation = JsonSerializer.Serialize(default(UpdateReservationRequestDto));

            //Act
            await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializePostDataSaveReservation, Encoding.UTF8, "application/json"));
            var clientResponse = await _httpClient.PostAsync("/Reservation/UpdateReservation", new StringContent(serializePostDataUpdateReservation, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseServiceSaveReservation = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseServiceSaveReservation?.IsOk ?? false);
        }
        [TestMethod]
        public async Task ReservationUpdateReservation_UpdateReservationRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange
            var serializePostDataSaveReservation = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                TableNumber = 1,
                NumberOfGuests = 1,
                CustomerName = "ExampleCustomerName"
            });
            var serializePostDataUpdateReservation = JsonSerializer.Serialize(new UpdateReservationRequestDto()
            {
                ReservationDate = DateTime.Now.AddDays(1),
                TableNumber = 1,
                Id = 0,
                NumberOfGuests = 1,
                CustomerName = "ExampleCustomerName"
            });

            //Act
            await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializePostDataSaveReservation, Encoding.UTF8, "application/json"));
            var clientResponse = await _httpClient.PostAsync("/Reservation/UpdateReservation", new StringContent(serializePostDataUpdateReservation, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseServiceSaveReservation = JsonSerializer.Deserialize<GeneralResponse>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseServiceSaveReservation?.IsOk ?? false);
        }
        #endregion
        #endregion

    }
}
