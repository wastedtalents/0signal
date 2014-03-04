using UnityEngine;
using System.Collections;

public interface ILogger  {

	void Info(string message);
	void Error( string message);
	void Warning( string message);
	void Info(string className, string message);
	void Error(string className, string message);
	void Warning(string className , string message);
}
