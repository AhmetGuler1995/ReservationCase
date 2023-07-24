using Reservation.Core.Repositories.Abstract;
using Reservation.Core.Services.Abstract;
using Reservation.Core.Services.Base.Concrete;
using Reservation.Domain.Models.GeneralModels;
using Reservation.Domain.Models.Infrastructure.Email;
using Reservation.Domain.Models.RequestDtos.Reservation;
using Reservation.Domain.Models.RequestDtos.Table;
using Reservation.Domain.Models.ResponseModels.Reservation;
using Reservation.Infrastructure.Email.Abstract;

namespace Reservation.Core.Services.Concrete
{
    public class ReservationService :BaseService, IReservationService
    {
        private readonly ITableService _tableService;
        private readonly IReservationRepository _reservationRepository;
        private readonly IEmailSender _emailSender;
        public ReservationService(ICommonRepository commonRepository, IReservationRepository reservationRepository, ITableService tableService, IEmailSender emailSender) : base(commonRepository)
        {
            _reservationRepository = reservationRepository;
            _tableService = tableService;
            _emailSender = emailSender;
        }

        public GeneralDataResponse<SaveReservationResponse> SaveReservation(SaveReservationRequestDto requestSaveReservation)
        {
            if (requestSaveReservation == null)
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Göndermiş Olduğunuz Verileri Kontrol Ediniz."
                };
            }
            if (requestSaveReservation.NumberOfGuests == 0)
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. İlgili Rezarvasyonun Kaç Kişi Olacağı Bilgisi Sıfırdan Farklı Bir Değer Alması Gerekmektedir."
                };
            }
            if (!requestSaveReservation.ReservationDate.HasValue)
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Rezarvasyon Tarihi Alanı Boş Geçilemez."
                };
            }
            if (string.IsNullOrEmpty(requestSaveReservation.CustomerName))
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Müşteri İsmi Alanı Boş Geçilemez."
                };
            }
          
            var availableTableListForReservationResponse = _tableService.GetTableListWithoutReservation(new GetTableListWithoutReservationRequestDto()
            {
                ReservationDate = requestSaveReservation.ReservationDate,
                NumberOfGuest = requestSaveReservation.NumberOfGuests
            });
            if (!availableTableListForReservationResponse.IsOk)
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    IsOk = availableTableListForReservationResponse.IsOk,
                    Data = null,
                    Message = availableTableListForReservationResponse.Message
                };
            }
            var availableTable = availableTableListForReservationResponse.Items.FirstOrDefault();
            if (availableTable == null)
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    Message = "Üzgünüz, uygun masa bulunamadı.",
                    IsOk = false,
                    Data = null
                };
            }
            requestSaveReservation.TableNumber = availableTable.Id;
            var reservationEntity = new Domain.Entities.Reservation()
            {
                CustomerName = requestSaveReservation.CustomerName,
                NumberOfGuests = requestSaveReservation.NumberOfGuests,
                ReservationDate = requestSaveReservation.ReservationDate.Value,
                TableNumber = requestSaveReservation.TableNumber
            };
            var saveReservationReferenceId = _reservationRepository.Save(reservationEntity);
            _emailSender.Send(new EmailSenderDto()
            {
                Body = "Example Mail Body",
                EmailAddress = "Example Mail Address",
                Subject = "Example Mail Subject"
            });
            return new GeneralDataResponse<SaveReservationResponse>()
            {
                IsOk = true,
                Data = new SaveReservationResponse()
                {
                    ReferenceId = saveReservationReferenceId
                },
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };

        }

        public GeneralResponse UpdateReservation(UpdateReservationRequestDto requestUpdateReservation)
        {
            if (requestUpdateReservation == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Gönderdiğiniz Verileri Kontrol Ediniz."
                };
            }

            if (string.IsNullOrEmpty(requestUpdateReservation.CustomerName))
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Ziyaretçi İsmi Boş Geçilemez."
                };
            }
            if (requestUpdateReservation.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Güncellenmek İstenilen Kaydın Id Alanı Sıfırdan Farklı Olması Gerekmektedir."
                };
            }

            if (requestUpdateReservation.NumberOfGuests == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message =
                        "Geçersiz İşlem Durumu Gözlenmiştir. Ziyaretçi Sayısı Sıfırdan Farklı Olması Gerekmektedir."
                };
            }

            if (!requestUpdateReservation.ReservationDate.HasValue)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message =
                        "Geçersiz İşlem Durumu Gözlenmiştir. Rezarvasyon Tarihi Geçerli Bir Tarih Olması Gerekmektedir."
                };
            }

            var entityReservation = _reservationRepository.GetById(requestUpdateReservation.Id);
            if (entityReservation == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. Güncellenmek İstenilen Aktif Kayıt Gözlenmemiştir."
                };
            }

            var tableCheck = _tableService.GetTable(new GetTableRequestDto()
            {
                Id = requestUpdateReservation.TableNumber
            });
            if (tableCheck == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message =
                        "İşlem Yapılacak Rezarvasyon Kaydına Güncellenmek İstenilen Masa Bilgisi Aktif Masa Değildir. Lütfen Masa Bilgisini Kontrol Ediniz."
                };
            }

            var tableListWithoutReservationResponse = _tableService.GetTableListWithoutReservation(new GetTableListWithoutReservationRequestDto()
            {
                NumberOfGuest = requestUpdateReservation.NumberOfGuests,
                ReservationDate = requestUpdateReservation.ReservationDate,
            });
            if (tableListWithoutReservationResponse == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Gözlenmiştir. İşlem Yapılacak Tarihte Tüm Masalar Rezervedir."
                };
            }

            if (!tableListWithoutReservationResponse.IsOk)
            {
                return new GeneralResponse()
                {
                    IsOk = tableListWithoutReservationResponse.IsOk,
                    Message = tableListWithoutReservationResponse.Message
                };
            }

            if (tableListWithoutReservationResponse.Items.FirstOrDefault(x =>
                    x.Id == requestUpdateReservation.TableNumber) == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message =
                        "İlgili Masa İlgili Tarihte Başka Bir Rezerve Kaydına Sahip Olduğu İçin İşlem İptal Edilmiştir."
                };
            }
            entityReservation.CustomerName = requestUpdateReservation.CustomerName;
            entityReservation.ReservationDate = requestUpdateReservation.ReservationDate.Value;
            entityReservation.NumberOfGuests = requestUpdateReservation.NumberOfGuests;
            entityReservation.TableNumber = requestUpdateReservation.TableNumber;
            _reservationRepository.Update(entityReservation);
            return new GeneralResponse()
            {
                IsOk = true,
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };
        }

        public GeneralResponse DeleteReservation(DeleteReservationRequestDto requestDeleteReservation)
        {
            if (requestDeleteReservation == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Oluşmuştur. Göndermiş Olduğunuz Verileri Kontrol Ediniz.",
                };
            }

            if (requestDeleteReservation.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message =
                        "Geçersiz İşlem Durumu Oluşmuştur. Göndermiş Olduğunuz Id Verisi Sıfırdan Farklı Olması Gerekmektedir."
                };
            }

            var entityReservation = _reservationRepository.Get(x=>x.Id == requestDeleteReservation.Id);
            if (entityReservation == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Oluşmuştur. Göndermiş Olduğunuz Id Verisine Bağlı Kayıt Bulunmamıştır."
                };
            }
            _reservationRepository.Delete(entityReservation);
            return new GeneralResponse()
            {
                IsOk = true,
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };
        }

        public GeneralResponse SetActiveReservation(SetActiveReservationRequestDto requestSetActiveReservation)
        {
            if (requestSetActiveReservation == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Oluşmuştur. Gönderdiğiniz Verileri Kontrol Ediniz.",
                };
            }

            if (requestSetActiveReservation.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Oluşmuştur. Gönderdiğiniz Id Verisi Sıfırdan Farklı Olamaz."
                };
            }

            var entityReservation = _reservationRepository.Get(x => x.Id == requestSetActiveReservation.Id);
            if (entityReservation == null)
            {
                return new GeneralResponse()
                {
                    Message =
                        "Geçersiz İşlem Durumu Oluşmuştur. İşlem Yapılmak İstenilen Kayıt Sistemde Kayıtlı Değildir.",
                    IsOk = false,
                };
            }

            entityReservation.Cancel = false;
            _reservationRepository.Update(entityReservation);   
            return new GeneralResponse()
            {
                IsOk = true,
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };
        }

        public GeneralResponse SetCancelReservation(SetCancelReservationRequestDto requestSetCancelReservation)
        {
            if (requestSetCancelReservation == null)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Oluşmuştur. Gönderdiğiniz Verileri Kontrol Ediniz.",
                };
            }

            if (requestSetCancelReservation.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsOk = false,
                    Message = "Geçersiz İşlem Durumu Oluşmuştur. Gönderdiğiniz Id Verisi Sıfırdan Farklı Olamaz."
                };
            }

            var entityReservation = _reservationRepository.Get(x => x.Id == requestSetCancelReservation.Id);
            if (entityReservation == null)
            {
                return new GeneralResponse()
                {
                    Message =
                        "Geçersiz İşlem Durumu Oluşmuştur. İşlem Yapılmak İstenilen Kayıt Sistemde Kayıtlı Değildir.",
                    IsOk = false,
                };
            }

            _reservationRepository.SetCancel(entityReservation);
            return new GeneralResponse()
            {
                IsOk = true,
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };
        }
    }
}
