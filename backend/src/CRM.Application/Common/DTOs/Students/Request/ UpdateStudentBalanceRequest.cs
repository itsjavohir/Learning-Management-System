namespace CRM.Application.Common.DTOs.Students.Request;

public record UpdateStudentBalanceRequest(
    decimal Amount,
    string Reason
);