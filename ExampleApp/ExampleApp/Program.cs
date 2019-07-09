using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Employee[] employees = GetEmployees();
			string message = LinqApproach(employees);
			Console.WriteLine($"Linq: \n{message}");

			employees = GetEmployees();
			message = ClassicApproach(employees);
			Console.WriteLine($"\nClassic: \n{message}");
		}

		private static string LinqApproach(Employee[] employees)
		{
			var managers = employees
				.GroupBy(empl => empl.Manager)
				.Select(group => new
				{
					Manager = group.Key,
					SubordinatesCount = group.Count()
				})
				.OrderByDescending(pool => pool.SubordinatesCount);

			var message = string.Join("\n", managers
				.Select(managerInfo =>
					$"{managerInfo.Manager.Name}: " +
					$"{managerInfo.SubordinatesCount}"));

			return message;
		}

		private static string ClassicApproach(Employee[] employees)
		{
			Dictionary<Employee, int> subordinatesCount =
				new Dictionary<Employee, int>();

			for (int i = 0; i < employees.Length; i++)
			{
				Employee manager = employees[i].Manager;
				if (!subordinatesCount.ContainsKey(manager))
				{
					subordinatesCount.Add(manager, 1);
				}
				else
				{
					var currentCount = subordinatesCount[manager];
					subordinatesCount[manager] = currentCount + 1;
				}
			}

			var sorted = new List<KeyValuePair<Employee, int>>();
			foreach (var pair in subordinatesCount)
			{
				sorted.Add(pair);
			}

			for (int i = 0; i < sorted.Count; i++)
			{
				for (int j = 0; j < sorted.Count - 1; j++)
				{
					if (sorted[j].Value < sorted[j + 1].Value)
					{
						var temp = sorted[j + 1];
						sorted[j + 1] = sorted[j];
						sorted[j] = temp;
					}
				}
			}

			string message = "";

			foreach (var pair in sorted)
			{
				message += pair.Key.Name + ": " + pair.Value + "\n";
			}

			return message;
		}

		private static Employee[] GetEmployees()
		{
			var ivan = new Employee("Иван");
			var elena = new Employee("Елена", ivan);
			var alexey = new Employee("Алексей", elena);
			var slava = new Employee("Вячеслав", elena);
			var dima = new Employee("Дмитрий", elena);
			var nina = new Employee("Нина", alexey);

			return new[] { ivan, elena, alexey, slava, dima, nina };
		}
	}
}