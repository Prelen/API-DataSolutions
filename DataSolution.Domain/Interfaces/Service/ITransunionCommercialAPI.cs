using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataSolution.Domain.Model.Services.TransUnionCommercialRequest;
using static DataSolution.Domain.Model.Services.TransunionRequest;
using RequestTrans01 = DataSolution.Domain.Model.Services.TransUnionCommercialRequest.RequestTrans01;

namespace DataSolution.Domain.Interfaces.Service
{
    public interface ITransunionCommercialAPI
    {
        Task<bool> BMSRetrieveAlert(BMSAlertsRetrieveRequest Request, int UserID,int ProductID);
        Task<bool> BusinessSearch(BusinessSearchRequest Requestt, int UserID, int ProductID);
        Task<bool> ForensicRequest(ForensicRequest Requestt, int UserID,int ProductID);
        Task<bool> GetMailboxList(MailboxRequest Request, int UserID, int ProductID);
        Task<bool> MailboxRetrieve(MailboxRequest Requestt, int UserID, int ProductID);
        Task<bool> GetTickedtStatus(string TicketNo, int UserID, int ProductID);
        Task<bool> ModuleAvailabilitySearch(ModuleAvailabilityRequest Request, int UserID, int ProductID);
        Task<bool> ModuleRequest(CommercialModuleRequest Request, int UserID, int ProductID);
        Task<bool> ModuleRequestInvestigate(ModuleInvestigateRequest Request, int UserID, int ProductID);
        Task<bool> PrincipalClearanceEnquiry(PrincipalRequest Request, int UserID, int ProductID);
        Task<bool> PrincipalClearanceIDVEnquiry(PrincipalRequest Request, int UserID, int ProductID);
        Task<bool> PrincipalLinkEnquiry(PrincipalRequest Request, int UserID, int ProductID);
        Task<bool> PrincipalModuleRequest(PrincipalModuleRequest Request, int UserID, int ProductID);
        Task<bool> ProcessTrasaction01(RequestTrans01 Request, int UserID, int ProductID);
        Task<bool> ProcessTrasactionA1(RequestTrans15 Request, int UserID, int ProductI);
        Task<bool> ProcessTrasaction15(RequestTrans15 Request, int UserID, int ProductID);
        Task<bool> ProcessTrasaction21(RequestTrans21 Request, int UserID, int ProductI);
        Task<bool> ProcessTrasaction32(RequestTrans32 Request, int UserID, int ProductID);

    }
}
