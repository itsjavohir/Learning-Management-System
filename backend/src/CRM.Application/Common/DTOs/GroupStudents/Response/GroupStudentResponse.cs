namespace CRM.Application.Common.DTOs.GroupStudents.Response;

public record GroupStudentResponse(
    Guid Id,
    Guid GroupId,
    string GroupName,
    Guid StudentId,
    string StudentFullName,
    DateTime JoinedAt,
    DateTime? LeftAt,
    bool IsActive,
    string? RemoveReason,
    Guid? TransferredFromGroupStudentId,
    Guid? TransferredToGroupStudentId
);
