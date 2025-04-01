using EFCoreEncapsulation.Api.Students;

namespace EFCoreEncapsulation.Api.Sports;

public class SportsEnrollment
{
	public long Id { get; set; }
	public Sport Sport { get; set; }
	public Student Student { get; set; }
	public Grade Grade { get; set; }
}