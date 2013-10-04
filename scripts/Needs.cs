/* -------------------------
 * Needs.cs
 * Author: McCall Bliss
 * Created: Oct 3, 2013
 * Modified: Oct 4, 2013
 *
 * Defines the users needs
 * and gives access to data
 * that should be altered
 *-------------------------*/

using System;

namespace AssemblyCSharp
{
	public class Needs
	{
		// Constructor
		public Needs (){
			this.name = "";
			this.height = 0;
			this.amount = 0f;
		}
		
		// Overloaded constructor
		public Needs (string name, int height, float amount) {
			this.name = name;
			this.height = height;
			this.amount = amount;
		}
		
		// --- Name ----
		//	purpose: get function for name of need
		//	returns: string

		string Name { get { return name; } }

		// --- Height ----
		//	purpose: get function for placement of health bar
		//	returns: int
		
		int Height { get { return height; } }
		
		// --- Amount --- 
		//	purpose: get function for amount of need
		//	returns: float
		
		float Amount { get { return amount; } }
		
		// --- alterAmount --- 
		//	purpose: changes amount of that need
		//	modifies: amount 
		
		void alterAmount(float newamount) {
			this.amount = newamount;
		}

	}

	private class Needs {

		// Name of need
		readonly string name;

		// Placement on y-axis of health bar
		readonly int height;

		// Amount that need currently has
		float amount;

	}
	
}
