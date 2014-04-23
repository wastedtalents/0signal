using UnityEngine;
using System;
using System.Collections;

namespace ZS.Engine.Logging {

	// Simple console logger.
	public class ConsoleLogger : ILogger {

		private readonly string _className;
		private const string LoggerFormat = "[{0}]({1}) - {2}";

		public ConsoleLogger() {
			_className = null;
		}

		public ConsoleLogger(Type typeInfo) {
			_className = typeInfo.IsGenericType ? typeInfo.Name + "<" + typeInfo.GetGenericArguments()[0].FullName + ">" : typeInfo.Name;
		}

		public void Info(string message) {
			Debug.Log(String.Format(LoggerFormat, String.IsNullOrEmpty(_className) ? "?" : _className, "Info", message));
		}

		public void Error( string message) {
			Debug.Log(String.Format(LoggerFormat, String.IsNullOrEmpty(_className) ? "?" : _className, "Error", message));
		}

		public void Warning( string message) {
			Debug.Log(String.Format(LoggerFormat, String.IsNullOrEmpty(_className) ? "?" : _className, "Warning", message));
		}

		public void Info(string className, string message) {
			Debug.Log(String.Format(LoggerFormat, className, "Info", message));
		}

		public void Error(string className, string message){
			Debug.Log(String.Format(LoggerFormat, className, "Error", message));
		}

		public void Warning(string className , string message) {
			Debug.Log(String.Format(LoggerFormat, className, "Warning", message));
		}

	}

}