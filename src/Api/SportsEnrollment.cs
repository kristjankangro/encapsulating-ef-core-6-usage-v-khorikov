namespace EFCoreEncapsulation.Api;

public class SportsEnrollment
{
	public long Id { get; set; }
	public Sports Sports { get; set; }
	public Student Student { get; set; }
	public Grade Grade { get; set; }
}