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
    public class TransUnionCommercialService 
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
        public async Task<string> BusinessSearch(BusinessSearchRequest Request, int UserID,int ProductID)
        {
            bool result = false;
            string itNumber = string.Empty;
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
                result = response.ErrorCode == null ? true : false;
                if (result)
                {
                    if (response.FirstResponse != null)
                    {
                        itNumber = response.FirstResponse.ITNumber;
                    }
                }
                 else
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

            return itNumber;
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
        public async Task<bool> MailboxRetrieve(MailboxRequest Request, int UserID, int ProductID)
        {
            bool result = false;

            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<MailboxRequest, MailboxRetrieve>();
                    }
                    );

                var request = config.CreateMapper().Map<MailboxRetrieve>(Request);
                request.SubscriberCode = subNo;
                request.SecurityCode = securityCode;
                var response = await client.MailboxRetrieveAsync(request);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "MailboxRetrieve", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.MailboxRetrieve", ex.Message);
            }

            return result;
        }
        public async Task<bool> GetTickedtStatus(string TicketNo, int UserID, int ProductID)
        {
            bool result = false;

            try
            {
                var request = new MailboxTicketStatus
                {
                    TicketNumber = TicketNo,
                    SecurityCode = securityCode,
                    SubscriberCode = subNo
                };
                var response = await client.MailboxTicketStatusAsync(request);


                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "GetTickedtStatus", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.GetTickedtStatus", ex.Message);
            }

            return result;
        }
        public async Task<bool> ModuleAvailabilitySearch(ModuleAvailabilityRequest Request, int UserID, int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                    cfg =>                    {

                        cfg.CreateMap<ModuleAvailabilityRequest, ModuleAvailabilitySearch>();
                });

                var module = config.CreateMapper().Map<ModuleAvailabilitySearch>(Request);
                module.SecurityCode = securityCode;
                module.SubscriberCode = subNo;
                var response = await client.ModuleAvailabilitySearchAsync(module);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "ModuleAvailabilitySearch", response.ErrorCode + ":" + response.ErrorMessage);


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


                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.ModuleAvailabilitySearch", ex.Message);
            }

            return result;
        }
        public async Task<bool> ModuleRequest(CommercialModuleRequest Request, int UserID, int ProductID)
        {
            bool result = false;

            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<CommercialModuleRequest, ModuleRequest>();
                    }
                    );

                var module = config.CreateMapper().Map<ModuleRequest>(Request);
                module.SubscriberCode = subNo;
                module.SecurityCode = securityCode;
                var response = await client.ModuleRequestAsync(module);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "ModuleInvestigateRequest", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.ModuleInvestigateRequest", ex.Message);
            }

            return result;
        }
        public async Task<bool> ModuleRequestInvestigate(ModuleInvestigateRequest Request, int UserID, int ProductID)
        {
            bool result = false;

            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<ModuleInvestigateRequest, ModuleRequestInvestigate>();
                    }
                    );

                var module = config.CreateMapper().Map<ModuleRequestInvestigate>(Request);
                module.SecurityCode = securityCode;
                module.SubscriberCode = subNo;
                var response = await client.ModuleRequestInvestigateAsync(module);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "ModuleRequestInvestigate", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.ModuleRequestInvestigate", ex.Message);
            }

            return result;
        }
        public async Task<bool> PrincipalClearanceEnquiry(PrincipalRequest Request, int UserID, int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<PrincipalRequest, PrincipalClearanceEnquiry>();
                    }
                    );
                var principal = config.CreateMapper().Map<PrincipalClearanceEnquiry>(Request);
                principal.SecurityCode = securityCode;
                principal.SubscriberCode = subNo;
                var response = await client.PrincipalClearanceEnquiryAsync(principal);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "PrincipalClearanceEnquiry", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.PrincipalClearanceEnquiry", ex.Message);
            }

            return result;
        }
        public async Task<bool> PrincipalClearanceIDVEnquiry(PrincipalRequest Request, int UserID, int ProductID)
        {
            bool result = false;

            try
            {
                var config = new MapperConfiguration(
                   cfg =>
                   {
                       cfg.CreateMap<PrincipalRequest, PrincipalClearanceIDVEnquiry>();
                   }
                   );
                var principal = config.CreateMapper().Map<PrincipalClearanceIDVEnquiry>(Request);
                principal.SecurityCode = securityCode;
                principal.SubscriberCode = subNo;
                var response = await client.PrincipalClearanceIDVEnquiryAsync(principal);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "PrincipalClearanceIDVEnquiry", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.PrincipalClearanceIDVEnquiry", ex.Message);
            }

            return result;
        }
        public async Task<bool> PrincipalLinkEnquiry(PrincipalRequest Request, int UserID, int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<PrincipalRequest, PrincipalLinkEnquiry>();
                 }
                 );
                var principal = config.CreateMapper().Map<PrincipalLinkEnquiry>(Request);
                principal.SecurityCode = securityCode;
                principal.SubscriberCode = subNo;
                var response = await client.PrincipalLinkEnquiryAsync(principal);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "PrincipalClearanceIDVEnquiry", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.PrincipalClearanceIDVEnquiry", ex.Message);
            }

            return result;
        }
        public async Task<bool> PrincipalModuleRequest(TransUnionCommercialRequest.PrincipalModuleRequest Request, int UserID, int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<TransUnionCommercialRequest.PrincipalModuleRequest, DataSolution.TransUnionCommercialService.PrincipalModuleRequest>();
                 }
                 );
                var principal = config.CreateMapper().Map<DataSolution.TransUnionCommercialService.PrincipalModuleRequest>(Request);
                principal.SecurityCode = securityCode;
                principal.SubscriberCode = subNo;
                var response = await client.PrincipalModuleRequestAsync(principal);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "PrincipalClearanceIDVEnquiry", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.PrincipalClearanceIDVEnquiry", ex.Message);
            }

            return result;
        }
        public async  Task<bool> ProcessTrasaction01(RequestTrans01 Request, int UserID, int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<RequestTrans01, DataSolution.TransUnionCommercialService.BusinessSearch>();
                 }
                 );
                var trans01 = config.CreateMapper().Map<DataSolution.TransUnionCommercialService.BusinessSearch>(Request);
                trans01.SecurityCode = securityCode;
                trans01.SubscriberCode = subNo;
                destination = environment == "Test" ? Destination.Test : Destination.Live;
                var response = await client.ProcessRequestTrans01Async(trans01,destination);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "ProcessTrasaction01", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.ProcessTrasaction01", ex.Message);
            }

            return result;
        }
        public async Task<bool> ProcessTrasactionA1(RequestTrans15 Request, int UserID, int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<RequestTrans15, ModuleAvailabilitySearch>();
                 }
                 );
                var transA1 = config.CreateMapper().Map<ModuleAvailabilitySearch>(Request);
                transA1.SecurityCode = securityCode;
                transA1.SubscriberCode = subNo;
                destination = environment == "Test" ? Destination.Test : Destination.Live;
                var response = await client.ProcessRequestTransA1Async(transA1, destination);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "ProcessTrasactionA1", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.ProcessTrasaction01", ex.Message);
            }

            return result;
        }
        public async Task<bool> ProcessTrasaction15(RequestTrans15 Request, int UserID, int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<RequestTrans15, Transaction15>();
                 }
                 );
                var trans15 = config.CreateMapper().Map<Transaction15>(Request);
                trans15.SecurityCode = securityCode;
                trans15.SubscriberCode = subNo;
    
                var response = await client.Transaction15Async(trans15);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "ProcessTrasaction15", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.ProcessTrasaction15", ex.Message);
            }

            return result;
        }
        public async Task<bool> ProcessTrasaction21(RequestTrans21 Request, int UserID, int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<RequestTrans21, Transaction21>();
                 }
                 );
                var trans01 = config.CreateMapper().Map<Transaction21>(Request);
                trans01.SecurityCode = securityCode;
                trans01.SubscriberCode = subNo;

                var response = await client.Transaction21Async(trans01);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "ProcessTrasaction21", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.ProcessTrasaction21", ex.Message);
            }

            return result;
        }
        public async Task<bool> ProcessTrasaction32(RequestTrans32 Request, int UserID, int ProductID)
        {
            bool result = false;
            try
            {
                var config = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<RequestTrans32, Transaction32>();
                 }
                 );
                var trans01 = config.CreateMapper().Map<Transaction32>(Request);
                trans01.SecurityCode = securityCode;
                trans01.SubscriberCode = subNo;

                var response = await client.Transaction32Async(trans01);

                if (!result)
                    log.LogError(UserID.ToString(), "DataSolution.Services", "ProcessTrasaction32", response.ErrorCode + ":" + response.ErrorMessage);


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

                log.LogError(UserID.ToString(), "DataSolutions.Web", "TransUnionCommercialService.ProcessTrasaction32", ex.Message);
            }

            return result;
        }


        private bool SaveTransaction(TransactionModel.TransactionData Transaction)
        {

            return new TransactionData().InsertTransaction(Transaction, Transaction.UserID.ToString());
        }
    }
}