namespace CRM.Application.Common.DTOs.Groups.Request;

public record UpdateGroupRequest(
    string Name,
    DateTime StartDate,
    int MaxStudents
);