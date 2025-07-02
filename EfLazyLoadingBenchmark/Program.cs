using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;

namespace MyApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			BenchmarkRunner.Run<EfLazyLoading>();
		}
	}

	public class FastConfig : ManualConfig
	{
		public FastConfig()
		{
			// Fast execution for development (approx 2 min per runtime)
			AddJob(Job.Default
				.WithWarmupCount(1)
				.WithInvocationCount(3)
				.WithUnrollFactor(3));

			// AddJob(Job.Default
			// 	.WithWarmupCount(6)
			// 	.WithInvocationCount(16)
			// 	.WithUnrollFactor(16));
		}
	}

	[MemoryDiagnoser]
	[Config(typeof(FastConfig))]
	public class EfLazyLoading
	{
		[GlobalSetup]
		public void Setup()
		{
			using var context = new EntityDbContext();
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			for (var i = 0; i < 1000000; i++)
			{
				context.Entities.Add(new Entity());
			}

			context.SaveChanges();
		}

		[Benchmark]
		public long Enumerate()
		{
			using var context = new EntityDbContext();
			long id = 0;
			foreach (var entity in context.Entities)
			{
				id = entity.Id;
			}

			return id;
		}
	}

	public class Entity
	{
		private readonly Action<object, string>? _lazyLoader;

		public int Id { get; set; }

		public Entity() : this(null)
		{
		}

		protected Entity(Action<object, string>? lazyLoader)
		{
			_lazyLoader = lazyLoader;
		}
	}

	public class EntityDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase("TestDatabase");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Entity>(b =>
			{
				b.HasKey(e => e.Id);
				b.Property(e => e.Id).ValueGeneratedOnAdd();
			});
		}

		public DbSet<Entity> Entities { get; set; }
	}

}