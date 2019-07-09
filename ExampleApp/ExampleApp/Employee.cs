namespace ExampleApp
{
	class Employee
	{
		public string Name { get; set; }
		public Employee Manager { get; }

		public Employee(string name, Employee manager = null)
		{
			Name = name;
			Manager = manager ?? this;
		}

		public override string ToString()
		{
			return $"name - {Name}, manager - {Manager.Name}";
		}
	}
}