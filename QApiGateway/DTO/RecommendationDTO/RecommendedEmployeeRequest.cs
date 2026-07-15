namespace QApiGateway.DTO.RecommendationDTO;

public class RecommendedEmployeeRequest
{
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int ServiceId { get; set; }
    public int PageNumber { get; set; } = 1;
}