namespace QApiGateway.DTO.RecommendationDTO;

public class RecommendedBranchRequest
{
    public int CompanyId { get; set; }
    public int PageNumber { get; set; } = 1;
}