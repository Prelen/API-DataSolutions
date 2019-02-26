using AutoMapper;
using DataSolution.Data.DAL;
using DataSolution.Domain.Interfaces.Service;
using DataSolution.Domain.Model.Data;
using DataSolution.Domain.Model.Services;
using DataSolution.TransUnionCommercialService;
using DataSolution.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static DataSolution.Domain.Model.Services.TransUnionCommercialRequest;

namespace DataSolution.Services
{
    public class TransUnionCommercialService : ITransunionCommercialAPI
    {
        DataSolution.TransUnionCommercialService.CommercialSoapClient client = new DataSolution.TransUnionCommercialService.CommercialSoapClient();
        readonly string subNo = ConfigurationManager.AppSettings["TransunionSub"].ToString();
        readonly string securityCode = ConfigurationManager.AppSettings["TransunionSecurityCode"].ToString();
        readonly string environment = ConfigurationManager.AppSettings["Environment"].ToString();
        Destination destination = new Destination();
        private Logger log;
        bool result;
        DateTime startDate;
        DateTime endDate;
        TransactionModel.TransactionData transData;
       

        public async Task<bool> BMSRetrieveAlert(BMSAlertsRetrieveRequest Request, int UserID,int ProductID)
        {
            startDate = DateTime.Now;
            log = new Logger();
            bool result = false;
            try
            {

                BMSAlert alert = (BMSAlert)Enum.Parse(typeof(BMSAlert), Request.alertType, true);
                var response = await client.BMSAlertsRetrieveAsync(subNo, securityCode, Request.startDate, Request.endDate, alert);
                endDate = DateTime.Now;
                result = response.ErrorMessage.Count() < 1 ? true : false;


                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "BMSRetrieveAlert", response.ErrorMessage);

                //log transaction
                endDate = DateTime.Now;
                transData = new TransactionModel.TransactionData
                {
                    EndDate = endDate,
                    IsSuccessful = result,
                    Message = response.ErrorMessage,
                    ProductID = ProductID,
                    StartDate = startDate,
                    UserID = Convert.ToInt32(UserID)
                };

                SaveTransaction(transData);
            }
            catch (Exception ex)
            {

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.BMSRetrieveAlert", ex.Message);
            }
            return result;
        }
        public async Task<bool> BusinessSearch(BusinessSearchRequest Request, int UserID,int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<BusinessSearchRequest, BusinessSearch>();
                    }
                    );
                var mapper = config.CreateMapper();
                var business = mapper.Map<BusinessSearch>(Request);
                business.SubscriberCode = subNo;
                business.SecurityCode = securityCode;
                var response = await client.BusinessSearchAsync(business);
                result = response.ErrorCode.Trim() != string.Empty ? true : false;
                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "BMSRetrieveAlert",response.ErrorCode + ":" +  response.ErrorMessage);


                //log transaction
                endDate = DateTime.Now;
                transData = new TransactionModel.TransactionData
                {
                    EndDate = endDate,
                    IsSuccessful = result,
                    Message = response.ErrorMessage,
                    ProductID = ProductID,
                    StartDate = startDate,
                    UserID = Convert.ToInt32(UserID)
                };

                SaveTransaction(transData);
            }
            catch (Exception ex)
            {

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.BusinessSearch", ex.Message);
            }

            return result;
        }
        public async Task<bool> ForensicRequest(ForensicRequest Request, int UserID, int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                    cfg => {

                        cfg.CreateMap<ForensicRequest, ForensicEnquiry>()
                         .ForMember(x => x.RSAIdNo, y => y.MapFrom(z => z.IDNumber));
                    }
                    );
                var mapper = config.CreateMapper();
                var forensic = mapper.Map<ForensicEnquiry>(Request);
                forensic.SubscriberCode = subNo;
                forensic.SecurityCode = securityCode;
                var response = await client.ForensicEnquiryRequestAsync(forensic);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "ForensicRequest", response.ErrorCode + ":" + response.ErrorMessage);


                //log transaction
                endDate = DateTime.Now;
                transData = new TransactionModel.TransactionData
                {
                    EndDate = endDate,
                    IsSuccessful = result,
                    Message = response.ErrorMessage,
                    ProductID = ProductID,
                    StartDate = startDate,
                    UserID = Convert.ToInt32(UserID)
                };

                SaveTransaction(transData);
            }
            catch (Exception ex)
            {

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.BusinessSearch", ex.Message);
            }
            return result;
        }
        public async Task<bool> GetMailboxList(MailboxRequest Request, int UserID, int ProductID)
        {
            bool result = false;

            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<MailboxRequest, MailboxList>();
                    }
                    );

                var mapper = config.CreateMapper();
                var mailbox = mapper.Map<MailboxList>(Request);
                var response = await client.MailboxListAsync(mailbox);

                result = response.ErrorCode.Trim() != string.Empty ? true : false;

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "GetMailboxList", response.ErrorCode + ":" + response.ErrorMessage);


                //log transaction
                endDate = DateTime.Now;
                transData = new TransactionModel.TransactionData
                {
                    EndDate = endDate,
                    IsSuccessful = result,
                    Message = response.ErrorMessage,
                    ProductID = ProductID,
                    StartDate = startDate,
                    UserID = Convert.ToInt32(UserID)
                };

                SaveTransaction(transData);

            }
            catch (Exception ex)
            {

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.GetMailboxList", ex.Message);
            }

            return result;
        }
        public Task<bool> MailboxRetrieve(MailboxRequest Requestt, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> GetTickedtStatus(string TicketNo, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ModuleAvailabilitySearch(ModuleAvailabilityRequest Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ModuleRequest(ModuleInvestigateRequest Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ModuleRequestInvestigate(ModuleInvestigateRequest Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> PrincipalClearanceEnquiry(PrincipalRequest Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> PrincipalClearanceIDVEnquiry(PrincipalRequest Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> PrincipalLinkEnquiry(PrincipalRequest Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> PrincipalModuleRequest(TransUnionCommercialRequest.PrincipalModuleRequest Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ProcessTrasaction01(RequestTrans01 Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ProcessTrasactionA1(RequestTrans15 Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ProcessTrasaction15(RequestTrans15 Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ProcessTrasaction21(RequestTrans21 Request, int UserID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ProcessTrasaction32(RequestTrans32 Request, int UserID)
        {
            throw new NotImplementedException();
        }


        private bool SaveTransaction(TransactionModel.TransactionData Transaction)
        {

            return new TransactionData().InsertTransaction(Transaction, Transaction.UserID.ToString());
        }
    }
}