using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataSolution.Domain.Model.Data.TransactionModel;

namespace DataSolution.Domain.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        bool InsertTransaction(TransactionData Transaction, string UserID);
        List<TransactionView> TransactionHistory(TransactionReport TransactionDetail, string UserID);
    }
}
