﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSolution.Domain.Model.Services;


namespace DataSolution.Domain.Interfaces.Service
{
    public interface ITransunionAPI
    {
        Task<bool> ProcessRequestTrans37Async(TransunionRequest.BureauEnquiry37Request Request , string UserID, int ProductID);
        Task<bool> IndividualTraceSearchAsync(TransunionRequest.IndividualTraceSearchRequest Request, string UserID ,int ProductID);
        Task<bool> TraceOrder68Async(TransunionRequest.TraceOrder68Request Request, string UserID ,int ProductID);
        Task<bool> ProcessPayrollEmployerInformationAsync(TransunionRequest.PayrollEmployerInformationRequest Request, string UserID, int ProductID);
        Task<bool> ProcessPayslipInformationAsync(TransunionRequest.PayslipInformationRequest Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans01Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans04Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        //Task<bool> ProcessRequestTrans04Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans07Async(TransunionRequest.BureauEnquiry37Request Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans12Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans13Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans17Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans18Async(TransunionRequest.RequestTrans18 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans22Async(TransunionRequest.RequestTrans22 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans23Async(TransunionRequest.RequestTrans23 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans26Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans31Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans38Async(TransunionRequest.BureauEnquiry37Request Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans41Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans42Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans43Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans47Async(TransunionRequest.BureauEnquiry37Request Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans91Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTrans92Async(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTransC20Async(string IDNumber, string UserID, int ProductID);
        Task<bool> ProcessRequestTransC29Async(TransunionRequest.RequestC29 Request ,string UserID, int ProductID);
        Task<bool> ProcessRequestTransC3Async( string UserID, int ProductID);
        Task<bool> ProcessRequestTransC30Async(TransunionRequest.RequestTransC30 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTransC4Async(TransunionRequest.RequestTransC4 Request, string UserID, int ProductID);
        Task<bool> ProcessRequestTransC6Async(string UserID, int ProductID);
        Task<bool> ProcessRequestStrikeDate(TransunionRequest.RequestTrans01 Request, string UserID, int ProductID);

    }
}
