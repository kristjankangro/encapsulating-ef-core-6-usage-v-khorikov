using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace EFCoreEncapsulation.Api.Contexts.Models;

public static class ModelBuilderExtensions
{
	public static ModelBuilder ApplyModelConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
	{
		var applyGenericMethod = typeof(ModelBuilder).GetMethods()
			.First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration) && m.GetParameters().Any(p => p.ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

		var configurations = assembly.GetTypes()
			.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
			.ToList();

		foreach (var config in configurations)
		{
			var entityType = config.GetInterfaces().First().GetGenericArguments().First();
			var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(entityType);
			var configurationInstance = Activator.CreateInstance(config);
			applyConcreteMethod.Invoke(modelBuilder, new[] { configurationInstance });
		}

		return modelBuilder;
	}
}