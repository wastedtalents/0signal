using UnityEngine;
using System.Collections;

namespace ZS.Engine { 

// Used for all components that need to be initialized.
	public interface IInitializable  {
		void Initialize();
	}

}