using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace EFCoreEncapsulation.Api.Contexts.Models;

public static class ModelBuilderExtensions
{
	public static ModelBuilder ApplyModelConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
	{
		var configurations = assembly.GetTypes()
			.Where(t => t.GetInterfaces()
				.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
			.Select(Activator.CreateInstance)
			.ToList();

		foreach (var configuration in configurations)
		{
			var entityType = configuration.GetType().GetInterfaces().First().GetGenericArguments().First();
			var applyConfigMethod = typeof(ModelBuilder)
				.GetMethod(
					nameof(ModelBuilder.ApplyConfiguration),
					[typeof(IEntityTypeConfiguration<>).MakeGenericType(entityType)]);
			applyConfigMethod?.Invoke(modelBuilder, [configuration]);
		}

		return modelBuilder;
	}
}