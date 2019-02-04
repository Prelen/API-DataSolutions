using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSolution.Domain.Model.Services;


namespace DataSolution.Domain.Interfaces.Service
{
    public interface ITransunionAPI
    {
        Task<bool> ProcessRequestTrans37Async(TransunionRequest.BureauEnquiry37Request Request , string UserID);
        Task<bool> IndividualTraceSearchAsync(TransunionRequest.IndividualTraceSearchRequest Request, string UserID);
        Task<bool> TraceOrder68Async(TransunionRequest.TraceOrder68Request Request, string UserID);
        Task<bool> ProcessPayrollEmployerInformationAsync(TransunionRequest.PayrollEmployerInformationRequest Request, string UserID);
        Task<bool> ProcessPayslipInformationAsync(TransunionRequest.PayslipInformationRequest Request, string UserID);
        Task<bool> ProcessRequestTrans01Async(TransunionRequest.RequestTrans01 Request, string UserID);
        Task<bool> ProcessRequestTrans04Async(TransunionRequest.RequestTrans01 Request, string UserID);
        Task<bool> ProcessRequestTrans07Async(TransunionRequest.BureauEnquiry37Request Request, string UserID);
        Task<bool> ProcessRequestTrans12Async(TransunionRequest.RequestTrans01 Request, string UserID);
        Task<bool> ProcessRequestTrans13Async(TransunionRequest.RequestTrans01 Request, string UserID);
        Task<bool> ProcessRequestTrans17Async(TransunionRequest.RequestTrans01 Request, string UserID);
        Task<bool> ProcessRequestTrans18Async(TransunionRequest.RequestTrans18 Request, string UserID);
        Task<bool> ProcessRequestTrans22Async(TransunionRequest.RequestTrans22 Request, string UserID);
        Task<bool> ProcessRequestTrans23Async(TransunionRequest.RequestTrans23 Request, string UserID);
        Task<bool> ProcessRequestTrans26Async(TransunionRequest.RequestTrans01 Request, string UserID);
        Task<bool> ProcessRequestTrans31Async(TransunionRequest.RequestTrans01 Request, string UserID);
        Task<bool> ProcessRequestTrans38Async(TransunionRequest.BureauEnquiry37Request Request, string UserID);
        Task<bool> ProcessRequestTrans41Async(TransunionRequest.RequestTrans01 Request, string UserID);
    }
}
