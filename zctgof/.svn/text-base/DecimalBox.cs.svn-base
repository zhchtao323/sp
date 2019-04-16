using System;
//using System. Collections. Generic;
//using System. ComponentModel;
//using System. Data;
//using System. Drawing;
//using System. Linq;
//using System. Text;
using System. Windows. Forms;

namespace DecimalBox
{
	public partial class DecimalBox : TextBox
	{
		private string textData = "";

		public DecimalBox ( )
		{
            //InitializeComponent ( );
		}

		protected override void OnPaint ( PaintEventArgs pe )
		{
			base. OnPaint ( pe );
		}

				//		On getting the focus change the back-color
		private void DecimalBox_Enter ( object sender, EventArgs e )
		{
			this. BackColor = System. Drawing. SystemColors. GradientInactiveCaption;
		}

	
				//		reject all but digits and the decimal point keys
		private void DecimalBox_KeyPress ( object sender, KeyPressEventArgs e )
		{
					//		If we already have a decimal point in the string,
					//		only accept control keys or digits
            //if (this.Text.Contains('.'))
            //{
            //    if ( !char. IsControl ( e. KeyChar ) &&
            //        !char. IsDigit ( e. KeyChar ) )
            //        e. Handled = true;
            //}
            //        //		If we don't already have a decimal point,
            //        //		accept control keys, digits, OR a decimal point
            //else
            //    if ( !char. IsControl ( e. KeyChar ) &&
            //        !char. IsDigit ( e. KeyChar ) &&
            //        !char. Equals ( e. KeyChar, '.' ) )
            //        e. Handled = true;
		}


				//		copy the amount to the local variable
		private void DecimalBox_TextChanged ( object sender, EventArgs e )
		{
			textData = this. Text;
		}


		private void DecimalBox_Leave ( object sender, EventArgs e )
		{
			if ( textData != "" )
			{		//		we'll need to flag unexpected characters
				bool formattedAlready = false;
				
					//		Any form using this control might
					//		programmatically alter the string data by
					//		formatting it or adding characters that
					//		would cause an unexpected error to occur
					//		when the currency format code attempted to
					//		format it. So, check the string for any
					//		chars other than digits or a decimal point...

				foreach ( char item in textData )
				{
					if ( !char. IsDigit ( item ) &&
						!char. Equals ( item, '.' ) )
						formattedAlready = true;
				}		//	setting the bool var to true if true

						//	if no unexpected characters were found,
				if ( formattedAlready == false )
				{		//	format the string as currency...
					this. Text = ( Convert. ToDecimal ( textData ) ).
										ToString ( "C" );
				}		//		otherwise skip this code
			}
			this.BackColor = System. Drawing. Color. White;
		}
	}
}
