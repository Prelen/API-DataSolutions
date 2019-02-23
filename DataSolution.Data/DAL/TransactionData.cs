using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSolution.Domain;
using DataSolution.Domain.Model.Data;
using DataSolution.Utilities.Logging;
using AutoMapper;
using DataSolution.Domain.Interfaces.Repository;
using DataSolution.Data.Data;
using static DataSolution.Domain.Model.Data.TransactionModel;

namespace DataSolution.Data.DAL
{
    public class TransactionData : ITransactionRepository
    {
        bool result;
        Logger log;
        TransactionEntities transactionEntities;
        
        public bool InsertTransaction(TransactionModel.TransactionData TransactionDetail,string UserID)
        {
            transactionEntities = new TransactionEntities();
            try
            {

                var config = new MapperConfiguration(
                       cfg =>
                       {
                           cfg.CreateMap<TransactionModel.TransactionData, Data.Transaction>()
                           .ForMember(x => x.TransStartDate, y => y.MapFrom(z => z.StartDate))
                           .ForMember(x => x.TransEndDate, y => y.MapFrom(z => z.EndDate));
                       }
                       );

                var mapper = config.CreateMapper();
           
                var transaction = mapper.Map<Data.Transaction>(TransactionDetail);
                transactionEntities.Transactions.Add(transaction);
                transactionEntities.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {

                log = new Logger();
                log.LogError(UserID, "DataSolutions.Data", "InsertUser", ex.Message);
            }
            return result;
        }

        public List<TransactionModel.TransactionView> TransactionHistory(TransactionModel.TransactionReport TransactionDetail, string UserID)
        {
            transactionEntities = new TransactionEntities();
            List<TransactionModel.TransactionView> results = new List<TransactionModel.TransactionView>();

            
            try
            {
                var trans = from t in transactionEntities.Transactions
                            join p in transactionEntities.Products on t.ProductID equals p.ProductID
                            join u in transactionEntities.Users on t.UserID equals u.UserID
                            where t.UserID == TransactionDetail.UserID && t.TransEndDate == TransactionDetail.StartDate
                            && t.TransEndDate == TransactionDetail.EndDate
                            select new TransactionModel.TransactionView
                            {
                                EndDate = TransactionDetail.EndDate,
                                IsSuccessful = t.IsSuccessful,
                                MainOrganization = u.OrganizationName,
                                Message = t.Message,
                                StartDate = TransactionDetail.StartDate,
                                TransID = t.TransID,
                                Username = u.Username
                            };

                results = trans.ToList();
            }
            catch (Exception ex)
            {

                log = new Logger();
                log.LogError(UserID, "DataSolutions.Data", "TransactionHistory", ex.Message);
            }
            return  results;
        }

        public List<TransactionsDataPoints> GetTransactionsLadt10Days(int UserID)
        {
            return null;
        }
    }


}
