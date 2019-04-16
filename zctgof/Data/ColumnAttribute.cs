using System;
using System.Data;

namespace ZCT.Data
{
	/// <summary>
	/// Marks a property or a field as a column.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
					Inherited=false, AllowMultiple=false)]
	public class ColumnAttribute : Attribute
	{
		private string name;
		private DbType dbtype;
		private int size;
		private byte precision;
		private byte scale;
		private bool primaryKey;
		private bool autoIncrement;
		
		/// <summary>
		/// Marks a property or a field as a column.
		/// </summary>
		public ColumnAttribute() : this("")
		{
		}
		
		/// <summary>
		/// Marks a property or a field as a column.
		/// </summary>
		/// <param name="name">column name</param>
		public ColumnAttribute(string name)
			: this(name, DbType.Object)
		{
		}
		
		/// <summary>
		/// Marks a property or a field as a column.
		/// </summary>
		/// <param name="dbtype">database data type</param>
		public ColumnAttribute(DbType dbtype) : this("", dbtype)
		{
		}
		
		/// <summary>
		/// Marks a property or a field as a column.
		/// </summary>
		/// <param name="name">column name</param>
		/// <param name="dbtype">database data type</param>
		public ColumnAttribute(string name, DbType dbtype)
			: this(name, dbtype, 0)
		{
		}
		
		/// <summary>
		/// Marks a property or a field as a column.
		/// </summary>
		/// <param name="dbtype">database data type</param>
		/// <param name="size">database data type size</param>
		public ColumnAttribute(DbType dbtype, int size)
			: this("", dbtype, size)
		{
		}
		
		/// <summary>
		/// Marks a property or a field as a column.
		/// </summary>
		/// <param name="name">column name</param>
		/// <param name="dbtype">database data type</param>
		/// <param name="size">database data type size</param>
		public ColumnAttribute(string name, DbType dbtype, int size)
			: base()
		{
			this.name = name;
			this.dbtype = dbtype;
			this.size = size;
		}
		
		/// <summary>
		/// Gets or sets the name of this column.
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		
		/// <summary>
		/// Gets the database data type of this column.
		/// </summary>
		public DbType DBType
		{
			get { return dbtype; }
			set { dbtype = value; }
		}
		
		/// <summary>
		/// Gets or sets the size of the database data type.
		/// </summary>
		public int Size
		{
			get { return size; }
			set { size = value; }
		}
		
		/// <summary>
		/// Gets or sets the precision of the database data type.
		/// </summary>
		public byte Precision
		{
			get { return precision; }
			set { precision = value; }
		}
		
		/// <summary>
		/// Gets or sets the scale of the database data type.
		/// </summary>
		public byte Scale
		{
			get { return scale; }
			set { scale = value; }
		}
		
		/// <summary>
		/// Gets or sets whether this column is a primary key column.
		/// </summary>
		public bool PrimaryKey
		{
			get { return primaryKey; }
			set { primaryKey = value; }
		}
		
		/// <summary>
		/// Gets or sets whether this column is an auto incrementing column.
		/// </summary>
		public bool AutoIncrement
		{
			get { return autoIncrement; }
			set { autoIncrement = value; }
		}
	}
}
