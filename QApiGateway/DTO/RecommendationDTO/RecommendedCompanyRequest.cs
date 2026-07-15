namespace QApiGateway.DTO.RecommendationDTO;

public class RecommendedCompanyRequest
{
    public int CategoryId { get; set; }
    public int PageNumber { get; set; } = 1;
}