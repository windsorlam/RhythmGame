// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections;

namespace Application
{
	public class DataManager
	{
		public ArrayList beatList = null;
		public ArrayList onsetList = null;
		public ArrayList melodyList = null;

		private DataManager ()
		{
		}

		public static DataManager instance=new DataManager();
		public static DataManager Instance{
			get{
				return instance;
			}
		}
	}
}

