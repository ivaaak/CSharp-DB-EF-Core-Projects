//ProductInputDTO
public class ProductInputDTO
{
	
	public string Name { get; set; }
	
	public decimal Price { get; set; }
	
	public int SellerId { get; set; }
	
	public int? BuyerId { get; set; } 	
}
//JSON Template:
//"Name": "PAMO Kill Natural",
//"Price": 1181.06,
//"SellerId": 52,
//"BuyerId": null