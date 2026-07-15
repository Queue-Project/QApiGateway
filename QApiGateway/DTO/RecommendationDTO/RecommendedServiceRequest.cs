namespace QApiGateway.DTO.RecommendationDTO;

public class RecommendedServiceRequest
{
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int PageNumber { get; set; } = 1;
}