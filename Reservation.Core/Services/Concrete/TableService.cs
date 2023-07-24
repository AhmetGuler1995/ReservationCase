using Reservation.Core.Repositories.Abstract;
using Reservation.Core.Services.Abstract;
using Reservation.Core.Services.Base.Concrete;
using Reservation.Domain.Entities;
using Reservation.Domain.Models.GeneralModels;
using Reservation.Domain.Models.RequestDtos.Table;
using Reservation.Domain.Models.ResponseModels.Table;

namespace Reservation.Core.Services.Concrete
{
    public class TableService : BaseService, ITableService
    {
        private readonly ITableRepository _tableRepository;


        public TableService(ICommonRepository commonRepository, ITableRepository tableRepository) : base(commonRepository)
        {
            _tableRepository = tableRepository;
        }

        public GeneralDataResponse<SaveTableResponse> SaveTable(SaveTableRequestDto requestSaveTable)
        {
            if (requestSaveTable == null)
            {
                return new GeneralDataResponse<SaveTableResponse>()
                {
                    Data = null,
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Verileri Kontrol Ediniz."
                };
            }

            if (requestSaveTable.Capacity == 0)
            {
                return new GeneralDataResponse<SaveTableResponse>()
                {
                    Data = null,
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Masa Kapasitesi Sıfırdan Farklı Olması Gerekmektedir."
                };
            }

            if (requestSaveTable.Number == 0)
            {
                return new GeneralDataResponse<SaveTableResponse>()
                {
                    Data = null,
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Masa Numarası Sıfırdan Farklı Olması Gerekmektedir."
                };
            }

            var entityTable = new Table()
            {
                Cancel = false,
                CreateDate = DateTime.Now,
                Capacity = requestSaveTable.Capacity,
                Number = requestSaveTable.Number
            };
            var entityTableReferanceId = _tableRepository.Save(entityTable);
            return new GeneralDataResponse<SaveTableResponse>()
            {
                Data = new SaveTableResponse()
                {
                    ReferenceId = entityTableReferanceId
                },
                IsOk = true,
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };

        }
        public GeneralResponse UpdateTable(UpdateTableRequestDto requestUpdateTable)
        {
            if (requestUpdateTable == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Oluşmuştur. Gönderdiğiniz Verileri Kontrol Ediniz."
                };
            }

            if (requestUpdateTable.Capacity == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Masa Kapasitesi Sıfırdan Farklı Olması Gerekmektedir."
                };
            }
            if (requestUpdateTable.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Masa Referans Id'si Sıfırdan Farklı Olması Gerekmektedir."
                };
            }
            if (requestUpdateTable.Number == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Masa Numarası Sıfırdan Farklı Olması Gerekmektedir."
                };
            }

            var tableEntity = _tableRepository.GetById(requestUpdateTable.Id);
            if (tableEntity == null || tableEntity.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message =
                        "Geçersiz İşlem Durumu Gözlenmiştir. Göndermiş Olduğunuz Masa Referans Id'sine Bağlı Aktif Kayıt Bulunmamıştır."
                };
            }

            tableEntity.Capacity = requestUpdateTable.Capacity;
            tableEntity.Number = requestUpdateTable.Number;
            _tableRepository.Update(tableEntity);
            return new GeneralResponse()
            {
                IsOk = true,
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };
        }
        public GeneralResponse SetActiveTable(SetActiveTableRequestDto requestSetActiveTable)
        {
            if (requestSetActiveTable == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Oluşmuştur. Gönderdiğiniz Verileri Kontrol Ediniz."
                };
            }


            if (requestSetActiveTable.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Masa Referans Id'si Sıfırdan Farklı Olması Gerekmektedir."
                };
            }
            var tableEntity = _tableRepository.GetById(requestSetActiveTable.Id);
            if (tableEntity == null || tableEntity.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message =
                        "Geçersiz İşlem Durumu Gözlenmiştir. Göndermiş Olduğunuz Masa Referans Id'sine Bağlı Aktif Kayıt Bulunmamıştır."
                };
            }

            tableEntity.Cancel = false;
            _tableRepository.Update(tableEntity);
            return new GeneralResponse()
            {
                IsOk = true,
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };
        }
        public GeneralResponse SetCancelTable(SetCancelTableRequestDto requestSetCancelTable)
        {
            if (requestSetCancelTable == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Oluşmuştur. Gönderdiğiniz Verileri Kontrol Ediniz."
                };
            }


            if (requestSetCancelTable.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Masa Referans Id'si Sıfırdan Farklı Olması Gerekmektedir."
                };
            }
            var tableEntity = _tableRepository.GetById(requestSetCancelTable.Id);
            if (tableEntity == null || tableEntity.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message =
                        "Geçersiz İşlem Durumu Gözlenmiştir. Göndermiş Olduğunuz Masa Referans Id'sine Bağlı Aktif Kayıt Bulunmamıştır."
                };
            }

            _tableRepository.SetCancel(tableEntity);
            return new GeneralResponse()
            {
                IsOk = true,
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };
        }
        public GeneralResponse DeleteTable(DeleteTableRequestDto requestDeleteTable)
        {
            if (requestDeleteTable == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Oluşmuştur. Gönderdiğiniz Verileri Kontrol Ediniz."
                };
            }


            if (requestDeleteTable.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Masa Referans Id'si Sıfırdan Farklı Olması Gerekmektedir."
                };
            }
            var tableEntity = _tableRepository.GetById(requestDeleteTable.Id);
            if (tableEntity == null || tableEntity.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message =
                        "Geçersiz İşlem Durumu Gözlenmiştir. Göndermiş Olduğunuz Masa Referans Id'sine Bağlı Aktif Kayıt Bulunmamıştır."
                };
            }
            _tableRepository.Delete(tableEntity);
            return new GeneralResponse()
            {
                IsOk = true,
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };
        }

        public GeneralListResponse<GetTableListWithoutReservationResponse> GetTableListWithoutReservation(
            GetTableListWithoutReservationRequestDto requestGetTableListWithoutReservation)
        {
            if (requestGetTableListWithoutReservation == null)
            {
                return new GeneralListResponse<GetTableListWithoutReservationResponse>()
                {
                    IsOk = false,
                    Items = null,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Lütfen Gönderdiğiniz Verileri Kontrol Ediniz."
                };
            }
            if (requestGetTableListWithoutReservation.NumberOfGuest == 0)
            {
                return new GeneralListResponse<GetTableListWithoutReservationResponse>()
                {
                    Items = null,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Lütfen Ziyaretçi Sayısını Sıfırdan Farklı Gönderiniz.",
                    IsOk = false
                };
            }
            if (!requestGetTableListWithoutReservation.ReservationDate.HasValue)
            {
                return new GeneralListResponse<GetTableListWithoutReservationResponse>()
                {
                    Items = null,
                    Message =
                        "Geçersiz İşlem Durumu Gözlenmiştir. Rezervasyon Tarihi Olmadan Rezervasyon Kaydı Açılamaz.",
                    IsOk = false

                };
            }
            var responseTableListWithoutList =
                _tableRepository.GetTableListWithoutReservation(requestGetTableListWithoutReservation);
            if (responseTableListWithoutList == null)
            {
                return new GeneralListResponse<GetTableListWithoutReservationResponse>()
                {
                    IsOk = false,
                    Message = "Üzgünüz, uygun masa bulunamadı.",
                    Items = new List<GetTableListWithoutReservationResponse>(),
                };
            }
            var serviceResponseList = responseTableListWithoutList
                .Select(x => new GetTableListWithoutReservationResponse()
                {
                    Number = x.Number,
                    Cancel = x.Cancel,
                    Capacity = x.Capacity,
                    CreateDate = x.CreateDate,
                    Id = x.Id
                }).ToList();
            return new GeneralListResponse<GetTableListWithoutReservationResponse>()
            {
                Message = "İşlem Başarı İle Tamamlanmıştır.",
                IsOk = true,
                Items = serviceResponseList
            };
        }

        public GeneralDataResponse<GetTableResponse> GetTable(GetTableRequestDto requestGetTable)
        {
            if (requestGetTable == null)
            {
                return new GeneralDataResponse<GetTableResponse>()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Verileri Kontrol Ediniz.",
                    Data = null
                };
            }

            if (requestGetTable.Id == 0)
            {
                return new GeneralDataResponse<GetTableResponse>()
                {
                    IsOk = false,
                    Message =
                        "Geçersiz İşlem Durumu Gözlenmiştir. Göndermiş Olduğunuz Id Verisi Sıfırdan Farklı Olması Gerekmektedir.",
                    Data = null
                };
            }

            var entityTable = _tableRepository.GetById(requestGetTable.Id);
            if (entityTable == null)
            {
                return new GeneralDataResponse<GetTableResponse>()
                {
                    IsOk = false,
                    Message =
                        "Geçersiz İşlem Durumu Gözlenmiştir. Göndermiş Olduğunuz Id Verisine Bağlı Aktif Kayıt Bulunmamıştır.",
                    Data = null
                };
            }

            return new GeneralDataResponse<GetTableResponse>()
            {
                Message = "İşlem Başarı İle Tamamlanmıştır.",
                IsOk = true,
                Data = new GetTableResponse()
                {
                    Number = entityTable.Number,
                    Cancel = entityTable.Cancel,
                    Capacity = entityTable.Capacity,
                    CreateDate = entityTable.CreateDate,
                    Id = entityTable.Id
                }
            };
        }
    }
}
