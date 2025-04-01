using EFCoreEncapsulation.Api.Shared;
using EFCoreEncapsulation.Api.Students;

namespace EFCoreEncapsulation.Api.Sports;

public class SportsEnrollment
{
	public long Id { get; set; }
	// public Sport Sport { get; set; }
	public long SportId { get; set; } //for performance reasons
	public Student Student { get; set; }
	public Grade Grade { get; set; }
}