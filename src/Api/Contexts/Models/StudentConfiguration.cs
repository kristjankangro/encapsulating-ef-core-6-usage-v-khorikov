using EFCoreEncapsulation.Api.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreEncapsulation.Api.Contexts.Models;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
	public void Configure(EntityTypeBuilder<Student> builder)
	{
		builder.ToTable("Student").HasKey(k => k.Id);
		builder.Property(p => p.Id).HasColumnName("StudentID");
		builder.Property(p => p.Email);
		builder.Property(p => p.Name);
		builder.HasMany(p => p.Enrollments).WithOne(p => p.Student);
		builder.HasMany(p => p.SportsEnrollments).WithOne(p => p.Student);
	}
}