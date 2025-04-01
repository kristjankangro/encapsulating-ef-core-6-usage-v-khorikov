namespace EFCoreEncapsulation.Api.Students;

public class StudentSpec
{
	public long Id { get; private init; }
	public bool WithEnrollments { get; set; }
	public bool WithSportEnrollments { get; init; }
	
	public static StudentSpec All(long id)
	{
		return new StudentSpec { Id = id, WithEnrollments = true, WithSportEnrollments =  true };
	}
}