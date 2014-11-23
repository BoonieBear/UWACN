using System;
using System.IO ;

namespace MSPTracer.Helper
{
	/// <summary>
	/// A simple interface to file IO methods
	/// </summary>
	public class csFile {
		private StreamReader ts;
		public csFile(string filename) 	
		{
            ts = new StreamReader((System.IO.Stream)File.OpenRead(filename),System.Text.Encoding.Default);
			
		}
		public string readLine() {
			return ts.ReadLine ();
		}
		public void close() {
			ts.Close ();
		}
	}
}
