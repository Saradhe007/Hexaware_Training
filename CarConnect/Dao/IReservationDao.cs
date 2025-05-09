
using CarConnect.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Dao
{
   
    
        public interface IReservationDao<T>
        {
            T CreateReservation(T reservation);
            T UpdateReservation(T reservation);
            bool CancelReservation(long reservationId);
            T GetReservationById(long reservationId);
            List<T> GetReservationsByCustomerId(long customerId);
        }

    }



