using UnityEngine;
using System.Collections;
using ZS.Engine.Logging;

namespace ZS.Engine { 

	// Simple implementation of a singleton class.
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

		private static T _instance;
		private static object _lock = new object();
		private static bool _applicationIsQuitting;
		private static ILogger _logger = LoggerFactory.GetLogger(typeof(Singleton<T>));

		public static T Instance { 

			get { 
				if(_applicationIsQuitting) {
					_logger.Warning("<Instance> - Application already destroyed");
					return null;
				}
				if (_instance == null) {
					lock(_lock) {
						if(_instance == null) {
							_instance = (T) FindObjectOfType(typeof(T));

							if ( FindObjectsOfType(typeof(T)).Length > 1 )
							{
								_logger.Error("<Instance> - Something went really wrong " +
									" - there should never be more than 1 singleton!" +
									" Reopenning the scene might fix it.");
								return _instance;
							}
							if (_instance == null)	{
								GameObject singleton = new GameObject();
								_instance = singleton.AddComponent<T>();

								if(_instance is IInitializable) {
									((IInitializable)_instance).Initialize();
									_logger.Info("<Instance> - instance initialized.");
								}

								singleton.name = "(singleton) "+ typeof(T).ToString();

								DontDestroyOnLoad(singleton);

								_logger.Info("<Instance> - An instance of " + typeof(T) + 
									" is needed in the scene, so '" + singleton +
									"' was created with DontDestroyOnLoad.");
							} else {
								if(_instance is IInitializable) {
									((IInitializable)_instance).Initialize();
									_logger.Info("<Instance> - instance initialized.");
								}

								_logger.Info("<Instance> Using instance already created: " +
									_instance.gameObject.name);
							}
						}
					}
				}
				return _instance;
			}
		}

		public void OnDestroy() {
			_applicationIsQuitting = true;
		}


	}

}