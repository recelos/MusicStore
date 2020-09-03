using MusicStore.SDK.Enumerations;
using MusicStore.SDK.Extensions;
using System;

namespace MusicStore.SDK
{
    public sealed class Product
    {
        private static readonly ConsoleChainer Chainer = new ConsoleChainer();
        public string ModelName { get; set; }
        public Instruments Instrument { get; set; }
        public Brands Brand { get; set; }
        public int Price { get; set; }
        public bool IsReserved { get; set; }
        public DateTime ReservationDate { get; set; }

        public Product(Brands brand, string modelName, Instruments instrument, int price, bool isReserved, DateTime reservationDate)
        {
            Brand = brand;
            ModelName = modelName;
            Instrument = instrument;
            Price = price;
            IsReserved = isReserved;
            ReservationDate = reservationDate;
        }

        

        public Product()
        {

        }        // Blank constructor for JSON deserializer

        private static string DisplayReservationDate(DateTime reservationDate)
        {
            if (reservationDate != default)
                return reservationDate.ToShortDateString();
            else
                return "";
        }       // Prevents from displaying reservation date if product is not reserved

        public override string ToString() => String.Format("{0, -10} \t{1, -10} \t{2, -10} \t{3, -10} \t{4, -10}\t {5, 0}",
            ModelName, Brand, Instrument, Price + " zl", IsReserved, DisplayReservationDate(ReservationDate));

        public Product ConfirmReservation()
        {
            if (IsReserved)
            {
                Chainer
                    .DisplayTextInRow("Product is already reserved!\n")
                    .PressToContinue();
            }
            else
                IsReserved = true;
            return this;
        }
        public Product SetReservationDate()
        {
            if (!IsReserved)
            {
                DateTime resevationDate = GetReservationDate();
                if (resevationDate > DateTime.Today)
                    ReservationDate = resevationDate;
                else
                {
                    Chainer
                        .DisplayTextInRow("Invalid date!\n")
                        .DisplayTextInRow("Try again\n");
                    SetReservationDate();
                }
            }
            return this;
        }
        private static DateTime GetReservationDate()
        {
            Chainer.RetrieveInput("Set reservation date (dd/mm/yyyy format) : ", out string input);
            DateTime resevationDay = Chainer.ParseInputAsDateTime(ref input);
            return resevationDay;
        }

        public Product DeleteReservation()
        {
            if (IsReserved)
            {
                IsReserved = false;
                ReservationDate = default;
            }
            else
            {
                Chainer
                    .DisplayTextInRow("This product is not reserved!\n")
                    .PressToContinue();
            }
            return this;
        } 

    }
 }
