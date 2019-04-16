using System;

namespace ZCT.Data
{
	/// <summary>
	/// Marks a class, interface, or a struct as being a representation of a table record.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct,
					Inherited=false, AllowMultiple=false)]
	public class TableAttribute : Attribute
	{
		private string name;
		private string schema;
		private string sequence;
		private bool inherit = false;
		private bool readOnly = false;
		
		/// <summary>
		/// Marks a class, interface, or a struct as being a representation of a table record.
		/// </summary>
		public TableAttribute() : this("")
		{
		}
		
		/// <summary>
		/// Marks a class, interface, or a struct as being a representation of a table record.
		/// </summary>
		/// <param name="name">name of the table</param>
		public TableAttribute(string name) : this(name, "")
		{
		}
		
		/// <summary>
		/// Marks a class, interface, or a struct as being a representation of a table record.
		/// </summary>
		/// <param name="name">name of the table</param>
		/// <param name="schema">name of the schema to which this table belongs</param>
		public TableAttribute(string name, string schema) : base()
		{
			this.name = name;
			this.schema = schema;
			this.sequence = "";
		}
		
		/// <summary>
		/// Gets or sets the name of this table.
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		
		/// <summary>
		/// Gets or sets the name of the schema to which this table belongs.
		/// </summary>
		public string Schema
		{
			get { return schema; }
			set { schema = value; }
		}
		
		/// <summary>
		/// Gets or sets the name of the sequence associated with this table.
		/// </summary>
		public string Sequence
		{
			get { return sequence; }
			set { sequence = value; }
		}
		
		/// <summary>
		/// Gets or sets whether mapped fields from the base class (if any)
		/// should be loaded as part of this table model.
		/// </summary>
		public bool Inherit
		{
			get { return inherit; }
			set { inherit = value; }
		}
		
		/// <summary>
		/// Gets or sets whether this table model is readonly.
		/// Only SELECT operation is allowed on readonly tables.
		/// (Useful on types that map to database views.)
		/// </summary>
		public bool ReadOnly
		{
			get { return readOnly; }
			set { readOnly = value; }
		}
	}
}
